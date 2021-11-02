using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OpenHardwareMonitor.Hardware;
using OpenHardwareMonitor.Hardware.ATI;
using OpenHardwareMonitor.Hardware.CPU;
using OpenHardwareMonitor.Hardware.Mainboard;
using OpenHardwareMonitor.Hardware.Nvidia;
using OpenHardwareMonitor.Software.FanControl;

namespace OpenHardwareMonitor.Utilities {
  internal class FanControl {
    private static class Settings {
      public const string ENABLED = "fanControl.enabled";
      public const string RAMPUPDOWN = "fanControl.rampupdown";
      public const string SENSORS = "fanControl.sensors";
      public const string CONTROL = "fanControl.controllers";
      public const string CURVE = "fanControl.curve";
    }

    protected SoftwareFanControl swFanControl;
    protected PersistentSettings settings;
    protected List<SensorId> sensors = new List<SensorId>();
    protected List<SensorId> controllers = new List<SensorId>();
    protected Curve curve;

    public FanControl(Computer computer, PersistentSettings settings) {
      UseRampUpDown = true;
      DelayTime = 3.25;

      this.settings = settings;
      Load(settings);

      computer.Traverse(new SensorVisitor(new SensorEventHandler(sensor => {

        if(sensor.Hardware is SoftwareFanControl)
          swFanControl = sensor.Hardware as SoftwareFanControl;

        if (sensor.Hardware is SuperIOHardware ||
           sensor.Hardware is ATIGPU ||
           sensor.Hardware is NvidiaGPU ||
           sensor.Hardware is GenericCPU) {

          bool isTemperature = (sensor.SensorType == SensorType.Temperature);
          bool isController = (sensor.SensorType == SensorType.Control);

          if (!isTemperature && !isController) return;

          SensorId id = null;
          var predicate = new Predicate<SensorId>(sid => sid.Identifier.ToString() == sensor.Identifier.ToString());

          if(isTemperature) id = sensors.Find(predicate);
          if(isController) id = controllers.Find(predicate);

          if (id != null) {
            id.Sensor = sensor;
          } else {
            id = new SensorId(sensor);
            if (isTemperature) sensors.Add(id);
            if (isController) controllers.Add(id);
          }
        }
      })));

      if (IsEnabled) SetAllControlsToDefault();
    }

    public bool IsEnabled { get; set; }
    public bool UseRampUpDown { get; set; }
    public double DelayTime { get; set; }
    public int SensorCount { get => sensors.Count; }
    public int ControlCount { get => controllers.Count; }

    public void SetCurve(Curve curve) {
      this.curve = curve;
    }

    private CurvePoint lastPoint;
    private DateTime lastChange = DateTime.MinValue;
    public void Process(Computer computer) {
      if (!IsEnabled) {
        if (swFanControl != null && swFanControl.FanLevel > 0)
          swFanControl.FanLevel = 0;
        return;
      }

      int level;
      CurvePoint point = curve.FindApplicable(sensors.Select(sid => (sid.Sensor?.Value != null ? (float)sid.Sensor.Value : -1.0f)).ToList(), out level);

      if (point != null) {

        if (DateTime.Now - lastChange <= TimeSpan.FromSeconds(DelayTime)) {
          if(lastPoint != null)
            point = lastPoint; // Keep last one for at least a few seconds
        }

        if (UseRampUpDown)
          point.Apply(controllers.Select(sid => sid.Sensor).ToList(), 6);

        if (point == lastPoint) return;
        lastPoint = point;

        if (!UseRampUpDown)
          point.Apply(controllers.Select(sid => sid.Sensor).ToList(), 1);

          if (swFanControl != null) {
          if(level >= 0 && curve.PointsCount > 0)
            swFanControl.FanLevel = (level + 1.0) * 100.0 / curve.PointsCount;
          else
            swFanControl.FanLevel = 0;
        }

      } else {

        SetAllControlsToDefault();
        lastPoint = null;

        if (swFanControl != null && swFanControl.FanLevel > 0)
          swFanControl.FanLevel = 0;

      }

      lastChange = DateTime.Now;
    }

    public void SetAllControlsToDefault() {
      foreach (var sensor in controllers) {
        sensor.Sensor?.Control?.SetDefault();
      }
    }

    public override string ToString() {
      Func<IList<SensorId>, string> toString = new Func<IList<SensorId>, string>(list => {
        string s = "";
        for (int i = 0; i < list.Count; i++) {
          if (list[i].Sensor != null)
            s += list[i].Sensor.Name;
          else
            s += list[i].Identifier;
          if(i + 1 < list.Count) s += SEPARATOR;
        }
        return s;
      });

      string str = "";
      str += toString(sensors);
      str += SEPARATOR;
      str += toString(controllers);
      str += Environment.NewLine;
      str += curve.ToString().Replace(SEPARATOR.ToString(), Environment.NewLine);
      return str;
    }

    private const char SEPARATOR = ';';
    public void Load() {
      Load(settings);
    }

    protected void Load(PersistentSettings settings) {
      IsEnabled = settings.GetValue(Settings.ENABLED, false);
      UseRampUpDown = settings.GetValue(Settings.RAMPUPDOWN, true);

      string sensorSet = settings.GetValue(Settings.SENSORS, string.Empty);
      if (!string.IsNullOrWhiteSpace(sensorSet)) {
        string[] sensorIds = sensorSet.Split(SEPARATOR);
        for (int i = 0; i < sensorIds.Length; i++) {
          var predicate = new Func<SensorId, bool>(sid => sid.Identifier.ToString() == sensorIds[i]);
          if(!sensors.Any(predicate))
            sensors.Add(new SensorId(sensorIds[i]));
        }
      }

      string controllerSet = settings.GetValue(Settings.CONTROL, string.Empty);
      if (!string.IsNullOrWhiteSpace(controllerSet)) {
        string[] controllerIds = controllerSet.Split(SEPARATOR);
        for (int i = 0; i < controllerIds.Length; i++) {
          var predicate = new Func<SensorId, bool>(sid => sid.Identifier.ToString() == controllerIds[i]);
          if (!controllers.Any(predicate))
            controllers.Add(new SensorId(controllerIds[i]));
        }
      }

      curve = Curve.Parse(settings.GetValue(Settings.CURVE, string.Empty));
    }

    public void Save() {
      Save(settings);
    }

    protected void Save(PersistentSettings settings) {
      settings.SetValue(Settings.ENABLED, IsEnabled);
      settings.SetValue(Settings.RAMPUPDOWN, UseRampUpDown);
      settings.SetValue(Settings.CONTROL, string.Join(SEPARATOR.ToString(), controllers));
      settings.SetValue(Settings.SENSORS, string.Join(SEPARATOR.ToString(), sensors));
      settings.SetValue(Settings.CURVE, curve.ToString());
    }
  }

  internal class SensorId {
    public string Identifier { get; private set; }
    public ISensor Sensor { get; set; }

    public SensorId(string identifier) {
      Identifier = identifier;
    }

    public SensorId(ISensor sensor) {
      Identifier = sensor.Identifier.ToString();
      Sensor = sensor;
    }

    public override string ToString() {
      return Identifier;
    }
  }

  internal class Curve {
    private List<CurvePoint> points = new List<CurvePoint>();

    public void Clear() { points.Clear(); }

    public void AddPoint(CurvePoint point) { points.Add(point); points.Sort(); }

    public int PointsCount { get { return points.Count; } }

    public CurvePoint FindApplicable(IList<float> values, out int index) {
      index = -1;
      for (int i = 0; i < points.Count; i++) {
        if (points[i].IsApplicable(values))
          index = i;
      }
      if (index >= 0)
        return points[index];
      else
        return null;
    }

    private const char SEPARATOR = ';';
    public override string ToString() {
      string str = points.Count.ToString(CultureInfo.InvariantCulture);
      for (int i = 0; i < points.Count; i++)
        str += SEPARATOR + points[i].ToString();
      return str;
    }

    public static Curve Parse(string value) {
      Curve curve = new Curve();
      if (string.IsNullOrWhiteSpace(value))
        return curve;

      string[] array = value.Split(SEPARATOR);
      int pointCount = int.Parse(array[0], CultureInfo.InvariantCulture);
      for (int i = 0; i < pointCount; i++) {
        curve.AddPoint(CurvePoint.Parse(array[i + 1]));
      }
      return curve;
    }

  }

  internal class CurvePoint : IComparable<CurvePoint> {
    private List<float> sensorValues = new List<float>();
    private List<float> controlValues = new List<float>();

    public CurvePoint() { }

    public CurvePoint(float[] sensorValues, float[] controlValues) {
      this.sensorValues.AddRange(sensorValues);
      this.controlValues.AddRange(controlValues);
    }

    public void AddSensorValue(float value) {
      sensorValues.Add(value);
    }

    public void AddControlValue(float value) {
      controlValues.Add(value);
    }

    public void Clear() {
      sensorValues.Clear();
      controlValues.Clear();
    }

    public int CompareTo(CurvePoint other) => sensorValues.Where(IsValidValue).Min().CompareTo(other.sensorValues.Where(IsValidValue).Min());

    public static bool IsValidValue(float value) => (value >= 0.0f);

    public bool IsApplicable(IList<float> values) {
      for (int i = 0; i < Math.Min(values.Count, sensorValues.Count); i++) {
        if(IsValidValue(sensorValues[i]) && values[i] >= sensorValues[i])
          return true;
      }
      return false;
    }

    protected const float MIN_CHANGE = 2.5f;
    public void Apply(IList<ISensor> sensors, int ticks) {
      for (int i = 0; i < Math.Min(sensors.Count, controlValues.Count); i++) {
        ISensor sensor = sensors[i];
        if (sensor == null) continue;
        IControl control = sensor.Control;
        if (control == null) continue;

        if (!IsValidValue(controlValues[i])) continue;

        if (sensor.Value != null && ticks > 1 && Math.Abs(control.SoftwareValue - (float)sensor.Value) > MIN_CHANGE)
          control.SetSoftware(ToSoftwareValue(control, (float)sensor.Value));

        float diff = ToSoftwareValue(control, controlValues[i]) - control.SoftwareValue;
        float oneTickDiff = Math.Max(Math.Abs(diff) / ticks, MIN_CHANGE);
        diff = Math.Sign(diff) * Math.Min(oneTickDiff, Math.Abs(diff));
        control.SetSoftware(control.SoftwareValue + diff);
      }
    }

    private static float ToSoftwareValue(IControl control, float value) {
      return (float)Math.Round(value / 100.0 * (control.MaxSoftwareValue - control.MinSoftwareValue) + control.MinSoftwareValue);
    }

    private const char SEPARATOR = ',';
    public override string ToString() {
      string str = sensorValues.Count.ToString(CultureInfo.InvariantCulture) + SEPARATOR + controlValues.Count.ToString(CultureInfo.InvariantCulture);
      for(int i = 0; i < sensorValues.Count; i++)
        str += SEPARATOR + sensorValues[i].ToString(CultureInfo.InvariantCulture);
      for( int i = 0;i < controlValues.Count; i++)
        str += SEPARATOR + controlValues[i].ToString(CultureInfo.InvariantCulture);
      return str;
    }

    public static CurvePoint Parse(string value) {
      if(string.IsNullOrWhiteSpace(value))
        return new CurvePoint();

      string[] array = value.Split(SEPARATOR);
      int sensorCount = int.Parse(array[0], CultureInfo.InvariantCulture);
      int controlCount = int.Parse(array[1], CultureInfo.InvariantCulture);
      float[] sensorValues = new float[sensorCount];
      float[] controlValues = new float[controlCount];
      for(int i = 0; i < sensorCount; i++)
        sensorValues[i] = float.Parse(array[i + 2], CultureInfo.InvariantCulture);
      for(int i = 0; i < controlCount; i++)
        controlValues[i] = float.Parse(array[i + 2 + sensorCount], CultureInfo.InvariantCulture);
      return new CurvePoint(sensorValues, controlValues);
    }
  }
}

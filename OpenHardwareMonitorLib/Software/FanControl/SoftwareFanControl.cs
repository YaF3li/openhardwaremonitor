using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenHardwareMonitor.Hardware;

namespace OpenHardwareMonitor.Software.FanControl {
  internal class SoftwareFanControl : Hardware.Hardware {
    public override HardwareType HardwareType => HardwareType.FanControl;

    private Sensor loadSensor;
    private double fanLevel;

    public SoftwareFanControl(ISettings settings)
      : base("Software Fan Controller", new Identifier("softwarefancontrol"), settings) {

      FanLevel = 0.0;
      loadSensor = new Sensor("Fan Level", 0, SensorType.Load, this, settings);
      ActivateSensor(loadSensor);
    }

    public double FanLevel {
      get {
        return fanLevel;
      }
      set {
        if (value > 100.0) fanLevel = 100.0;
        else if (value < 0.0) fanLevel = -1.0;
        else fanLevel = value;
      }
    }

    public override void Update() {
      if(FanLevel >= 0.0)
        loadSensor.Value = FanLevel;
       else
        loadSensor.Value = null;
    }
  }
}

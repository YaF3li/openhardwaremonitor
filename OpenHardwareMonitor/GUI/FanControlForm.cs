using System;
using System.Windows.Forms;
using OpenHardwareMonitor.Hardware;
using OpenHardwareMonitor.Utilities;

namespace OpenHardwareMonitor.GUI {
  public partial class FanControlForm : Form {
    private FanControl fanControl;

    public FanControlForm(Computer computer, PersistentSettings settings) {
      InitializeComponent();
      fanControl = new FanControl(computer, settings);
      controlEnabledChk.Checked = fanControl.IsEnabled;
      rampChk.Checked = fanControl.UseRampUpDown;
      holdTimeNud.Value = (decimal)fanControl.DelayTime;
      settingsBox.Text = fanControl.ToString();
      if(settingsBox.Lines.Length >= 2 && settingsBox.Lines[1] == "0") {
        string curve = fanControl.SensorCount + "," + fanControl.ControlCount;
        for (int i = 0; i < fanControl.SensorCount + fanControl.ControlCount; i++)
          curve += ",0";
        settingsBox.Text += Environment.NewLine + curve;
      }
    }

    private void acceptBtn_Click(object sender, EventArgs e) {
      fanControl.IsEnabled = controlEnabledChk.Checked;
      fanControl.UseRampUpDown = rampChk.Checked;
      fanControl.DelayTime = (double)holdTimeNud.Value;
      string curve = (settingsBox.Lines.Length - 2).ToString();
      for (int i = 2; i < settingsBox.Lines.Length; i++) {
        curve += ";" + settingsBox.Lines[i].Replace(" ", "");
      }
      fanControl.SetCurve(Curve.Parse(curve));
      fanControl.Save();
    }
  }
}

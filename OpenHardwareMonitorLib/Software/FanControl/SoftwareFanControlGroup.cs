using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenHardwareMonitor.Hardware;

namespace OpenHardwareMonitor.Software.FanControl {
  internal class SoftwareFanControlGroup : IGroup {
    private List<IHardware> hardware = new List<IHardware>();

    public SoftwareFanControlGroup(ISettings settings) {
      hardware.Add(new SoftwareFanControl(settings));
    }

    public IReadOnlyList<IHardware> Hardware {
      get {
        return hardware.ToArray();
      }
    }
    public void Close() { }
    public string GetReport() => string.Empty;
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenHardwareMonitor.Hardware;

namespace OpenHardwareMonitor.GUI {
  public class UpdateGadgetVisitor : IVisitor {
    private SensorGadget gadget;

    public UpdateGadgetVisitor(SensorGadget gadget) {
      if (gadget == null)
        throw new ArgumentNullException("gadget");
      this.gadget = gadget;
    }

    public void VisitComputer(IComputer computer) {
      computer.Traverse(this);
    }

    public void VisitHardware(IHardware hardware) {
      if (gadget.Contains(hardware))
        hardware.Update();
      foreach (IHardware subHardware in hardware.SubHardware)
        subHardware.Accept(this);
    }

    public void VisitSensor(ISensor sensor) { }

    public void VisitParameter(IParameter parameter) { }
  }
}


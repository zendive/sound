using System;
using System.Diagnostics;
using System.Threading;

namespace SoCP
{
  using System.Threading;
  class Ambient
  {
    public class Sensor
    {
      public double cpu = 1;
      public double ram = 1000000;
      public double ramTotal = 0;
      public double reserved = -1;
      public bool IsSameAs(ref Sensor _) {
        return (this.cpu == _.cpu
          && this.ram == _.ram
          && this.ramTotal == _.ramTotal);
      }
      public void Copy(Sensor _) {
        this.cpu = _.cpu;
        this.ram = _.ram;
        this.ramTotal = _.ramTotal;
      }
      public override string ToString() {
        return String.Format("CPU: {0}% RAM: {1}/{2}MB",
          Math.Round(this.cpu), (int)(this.ram/1000000d), (int)(this.ramTotal/1000000d));
      }
    }

    public delegate void DelegateTrigger();
    public event DelegateTrigger OnTrigger = null;
    public Sensor sensor = new Sensor();
    public bool m_bManualCPU = false;
    public bool m_bManualRAM = false;
    public static int m_iUpdateRate = 150;
    private Thread m_threadUpdate;
    private PerformanceCounter m_pc1 = null;
    private PerformanceCounter m_pc2 = null;
    private PerformanceCounter m_pc3 = null;

    public Ambient()
    {
      m_threadUpdate = new Thread(new ThreadStart(Listen));
      m_threadUpdate.Priority = ThreadPriority.AboveNormal;
      m_threadUpdate.IsBackground = true;

      try { m_pc1 = new PerformanceCounter("Processor", "% Processor Time", "_Total"); } catch {}
      try { m_pc2 = new PerformanceCounter("Memory", "Available Bytes", ""); } catch {}
      try { m_pc3 = new PerformanceCounter("Memory", "Commit Limit", ""); } catch {}
    }

    public void Start()
    {
      if (!m_bStop)
      {
        m_threadUpdate.Start();
      }
      else
      {
        m_bStop = false;
        m_threadUpdate.Interrupt();
      }
    }

    public void Stop()
    {
      m_bStop = true;
    }

    private bool m_bStop = false;
    private void Listen()
    {
      while (true)
      {
        try
        {
          if (m_bStop)
          {
            Thread.Sleep(Timeout.Infinite);
          }

          /// http://blogs.msdn.com/b/bclteam/archive/2006/06/02/618156.aspx
          if (!m_bManualCPU) {
            sensor.cpu = (m_pc1 != null) ? m_pc1.NextValue() : sensor.cpu;
            if (sensor.cpu < 1)
            {
              sensor.cpu = 1;
            }
          }
          if (!m_bManualRAM)
          {
            sensor.ram = (uint)((m_pc2 != null) ? m_pc2.NextValue() : sensor.ram);
            //if (sensor.ramTotal == 0)
              sensor.ramTotal = (uint)((m_pc3 != null) ? m_pc3.NextValue() : sensor.ramTotal);
          }
          
          OnTrigger.Invoke();
          System.Threading.Thread.Sleep(m_iUpdateRate);
        }
        catch { }
      }
    }
  }
}
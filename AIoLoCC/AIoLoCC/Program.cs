using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AIoLoCC
{
  static class Program
  {
    /// <summary>
    /// Audible indication of the load on the computer components (AIoLoCC)
    /// [�������� ��������� �������� �� �������� ����������]
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }
  }
}
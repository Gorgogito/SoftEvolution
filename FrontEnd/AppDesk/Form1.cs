using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AppDesk
{

  public partial class Form1 : DevComponents.DotNetBar.Metro.MetroAppForm
  {

    private struct LASTINPUTINFO
    {
      long cbSize;
      long dwTime;
    }
    public Form1()
    {
      InitializeComponent();
    }
  }
}

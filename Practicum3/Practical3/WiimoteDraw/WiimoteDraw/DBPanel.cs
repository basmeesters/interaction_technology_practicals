using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsApplication1
{
    public partial class DBPanel : Panel
    {
        public DBPanel()
        {
            //Making this class is a workaround to double buffering panels
            this.DoubleBuffered = true;
        }
    }
}
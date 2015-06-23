using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BWT
{
    public partial class TextWindow : Form
    {
        public string TextContent
        {
            get
            {
                return this.txb.Text;
            }
            set
            {
                this.txb.Text = value;
            }
        }
        public TextWindow(): this("")
        {

        }
        public TextWindow(string txt)
        {
            InitializeComponent();
            this.txb.Text = txt;
            this.txb.Select(0, 0);
        }
    }
}

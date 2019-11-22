using Fanex.Affiliate.DataService.Importer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class TopazForm : ImporterFormBase
    {
        public TopazForm() : base()
        {
            InitializeComponent();
            Service = new TopazService();
        }

        private void TopazForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}

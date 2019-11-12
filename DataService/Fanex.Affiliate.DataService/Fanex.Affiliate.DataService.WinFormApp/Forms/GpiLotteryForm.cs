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
    public partial class GpiLotteryForm : ImporterFormBase
    {
        public GpiLotteryForm()
        {
            InitializeComponent();
            Service = new GpiLotteryService();
        }

        private void GpiLotteryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Service.Dispose();
        }
    }
}

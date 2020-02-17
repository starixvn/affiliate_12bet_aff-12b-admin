using Fanex.Affiliate.DataService.WinFormApp.Converter;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    public partial class FileImportForm : Form
    {
        private IFileImporter FileImporter;

        public FileImportForm()
        {
            InitializeComponent();
            SetUpSupportProduct();
        }

        private void SetUpSupportProduct()
        {
            var dic = GetSupportedProduct();
            cbProductType.DataSource = new BindingSource(dic, null);
            cbProductType.DisplayMember = "Key";
            cbProductType.ValueMember = "Value";
        }

        private Dictionary<string,IFileImporter> GetSupportedProduct()
        {
            var dic = new Dictionary<string, IFileImporter>
            {
                {"----Select one-----", null },
                {"VR Lottery", new VRLotteryFileImporter() },
                {"LB Keno", new LBKenoFileImporter() },          
            };
            return dic;
        }

        private void cbProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileImporter = cbProductType.SelectedValue as IFileImporter;
        }

        private void btnChoseFile_Click(object sender, EventArgs e)
        {
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = "txt";
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var file = openFileDialog.FileName;
                txtBoxFilePath.Text = file;
            }
        }

        private async void btnConvert_Click(object sender, EventArgs e)
        {
            if (FileImporter == null)
            {
                MessageBox.Show("Need select a product type");
                return;
            }
            var path = txtBoxFilePath.Text;
            if (!File.Exists(path))
            {
                MessageBox.Show("Not exist file path");
                return;
            }

            var data = File.ReadAllText(path);
            var xmls = await FileImporter.ConvertToXml(data);
            ShowXml(xmls);           
        }

        private void ShowXml(IEnumerable<XDocument> xmls)
        {
            if(richTxtBoxConvertedXml.InvokeRequired)
            {
                richTxtBoxConvertedXml.Invoke(new MethodInvoker(() => ShowXml(xmls)));
            }
            richTxtBoxConvertedXml.Clear();
            var datas = xmls.Select(xml => xml.ToString());
            foreach (var data in datas)
            {
                richTxtBoxConvertedXml.AppendText(data);
            }
        }

        private async void btnImport_Click(object sender, EventArgs e)
        {
            if (FileImporter == null)
            {
                MessageBox.Show("Not set up IFileImporter");
                return;
            }
            var path = txtBoxFilePath.Text;
            if (!File.Exists(path))
            {
                MessageBox.Show("Not exist file path");
                return;
            }

            var data = File.ReadAllText(path);
            var xmls = await FileImporter.ConvertToXml(data);
            var importData = xmls.Select(xml => xml.ToString());
            try
            {
                await FileImporter.ImportAsync(importData);
                MessageBox.Show("Import success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error when import data {ex.Message}");
            }
        }
    }
}

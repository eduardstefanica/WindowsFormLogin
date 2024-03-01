using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsLogin.DataLayer;
using WindowsFormsLogin.Models;
using WindowsFormsLogin.Utility;

namespace WindowsFormsLogin
{
    // https://stackoverflow.com/questions/35567814/is-it-possible-to-display-serilog-log-in-the-programs-gui
    // You can use Serilog.Sinks.TextWriter

    // File Logging in Windows Form Application using SERILOG
    // https://www.thecodebuzz.com/file-logging-in-windows-form-application-using-serilog/
    // SERILOG PACKAGES TO INSTALL
    // Serilog.Sinks.File
    // Serilog.Sinks.RollingFile

    // Additionally, please add below NuGet packages,
    // Microsoft.Extensions.Hosting
    // Serilog.Extensions.Logging

    public partial class frmLogin : Form
    {
        // SERILOG
        // private StringWriter _messages;
        private readonly ILogger _logger;

        List<Utente> listUtenti = null;
        Utente oUt = null;

        ListeDati oDL = null;

        List<Regione> listRegioni = null;

        List<Provincia> listProvince = null;
        Provincia oProv = null;

        List<Comune> listComuni = null;
        Comune oCom = null;
        public frmLogin()
        {
            // NIENTE E' DISEGNATO
            InitializeComponent();
            // TUTTO E' DISEGNATO

            // SERILOG TextWriter
            //_messages = new StringWriter();
            //_logger = new LoggerConfiguration()
            //    .WriteTo.TextWriter(_messages)
            //    .CreateLogger();

            // serilog window forms write to file system example
            // SERILOG File

            _logger = new LoggerConfiguration()
                  .WriteTo.File("D:\\LOGS\\WindowsFormsLogin\\Log_WindowsFormsLogin.txt")
                  .CreateLogger();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            textBoxUsername.Text = "p.tardiolobonifazi@vivasoft.it";
            textBoxPassword.Text = "password";
            textBoxConfermaPassword.Text = "password";

            //labelRegione.Visible = false;
            //comboBoxRegione.Visible = false;
            //labelProvincia.Visible = false;
            //comboBoxProvincia.Visible = false;
            //labelComune.Visible = false;
            //comboBoxComune.Visible = false;
            ManageUI.manageVisibility(labelRegione, comboBoxRegione, false);
            ManageUI.manageVisibility(labelProvincia, comboBoxProvincia, false);
            ManageUI.manageVisibility(labelComune, comboBoxComune, false);

            oDL = new ListeDati(_logger);
            listUtenti = oDL.LoadUtente();

            // https://stackoverflow.com/questions/35567814/is-it-possible-to-display-serilog-log-in-the-programs-gui
            // You can use Serilog.Sinks.TextWriter

            foreach (var item in listUtenti)
            {
                _logger.Information(string.Format("Nome:{0}{5}Cognome:{1}{5}Username:{2}{5}IsValid:{3}{5}Sesso:{4}{5}", item.nome, item.cognome, item.email, item.isValid, item.sesso, Environment.NewLine));
            }

            listRegioni = oDL.LoadRegione();
            listProvince = oDL.LoadProvincia();
            listComuni = oDL.LoadComune();

            // textBoxResult.Text = _messages.ToString();

            // saveFileDialogJson.InitialDirectory = "D:\\___Corsi\\HTML\\RegProvCom\\JSON";
            saveFileDialogJson.InitialDirectory = @"D:\___Corsi\HTML\RegProvCom\JSON";
            saveFileDialogJson.Filter = "JSON files|*.json";
            saveFileDialogJson.RestoreDirectory = true;

            openFileDialogJson.InitialDirectory = @"D:\___Corsi\HTML\RegProvCom\JSON";
            openFileDialogJson.Filter = "JSON files|*.json";
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string pwd = textBoxPassword.Text;
            string confermaPwd = textBoxConfermaPassword.Text;

            if (pwd != confermaPwd)
            {
                _logger.Information("Password non coincidente");
                MessageBox.Show("Password non coincidente", "Login", MessageBoxButtons.OKCancel);
                return;
            }
            else
            {
                bool isPresente = false;
                //foreach (Utente item in listUtenti)
                //{
                //    if(item.email.ToLower() == username.ToLower() && item.password == pwd)
                //    {
                //        // MessageBox.Show("Utente verificato", "Login", MessageBoxButtons.OKCancel);
                //        isPresente = true;
                //        // Esco dal foreach
                //        break;
                //    }
                //}

                Utente item = listUtenti.Where(x => x.email.ToLower() == username.ToLower() && x.password == pwd).FirstOrDefault();
                if (item != null)
                {
                    isPresente = true;
                }

                if (isPresente)
                {
                    _logger.Information("Utente verificato");
                    MessageBox.Show("Utente verificato", "Login", MessageBoxButtons.OKCancel);

                    //labelRegione.Visible = true;
                    //comboBoxRegione.Visible = true;

                    //labelProvincia.Visible = false;
                    //comboBoxProvincia.Visible = false;

                    //labelComune.Visible = false;
                    //comboBoxComune.Visible = false;

                    ManageUI.manageVisibility(labelRegione, comboBoxRegione, true);
                    ManageUI.manageVisibility(labelProvincia, comboBoxProvincia, false);
                    ManageUI.manageVisibility(labelComune, comboBoxComune, false);

                    comboBoxRegione.Items.Clear();

                    ComboBoxItem cmbItem = new ComboBoxItem();
                    cmbItem.Text = "Seleziona regione";
                    cmbItem.Value = "0";
                    comboBoxRegione.Items.Add(cmbItem);
                    foreach (Regione reg in listRegioni)
                    {
                        cmbItem = new ComboBoxItem();
                        cmbItem.Text = reg.Nome;
                        cmbItem.Value = reg.ID.ToString();

                        comboBoxRegione.Items.Add(cmbItem);

                        comboBoxRegione.SelectedIndex = 0;
                    }
                }
                else
                {
                    labelRegione.Visible = false;
                    comboBoxRegione.Visible = false;
                    _logger.Information("Utente NON presente");
                    MessageBox.Show("Utente NON presente", "Login", MessageBoxButtons.OKCancel);
                }
            }
            // https://stackoverflow.com/questions/35567814/is-it-possible-to-display-serilog-log-in-the-programs-gui
            // You can use Serilog.Sinks.TextWriter
            // textBoxResult.Text = _messages.ToString();
        }

        private void comboBoxRegione_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox cmb = (System.Windows.Forms.ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)cmb.SelectedItem;

            if (Convert.ToInt32(item.Value) > 0)
            {
                int IdRegione = Convert.ToInt32(item.Value);
                //labelProvincia.Visible = true;
                //comboBoxProvincia.Visible = true;
                ManageUI.manageVisibility(labelProvincia, comboBoxProvincia, true);
                ManageUI.manageVisibility(labelComune, comboBoxComune, false);

                comboBoxProvincia.Items.Clear();

                ComboBoxItem cmbItem = new ComboBoxItem();
                cmbItem.Text = "Seleziona Provincia";
                cmbItem.Value = "0";
                comboBoxProvincia.Items.Add(cmbItem);

                //foreach (var prov in listProvince)
                //{ 
                //    if(prov.IdRegione == IdRegione)
                //    {
                //        cmbItem = new ComboBoxItem();
                //        cmbItem.Text = prov.Nome;
                //        cmbItem.Value = prov.ID;
                //        comboBoxProvincia.Items.Add(cmbItem);
                //    }
                //}

                List<Provincia> listProvRegione = new List<Provincia>();
                listProvRegione = listProvince.Where(p => p.IdRegione == IdRegione).ToList();

                foreach (var prov in listProvRegione)
                {
                    cmbItem = new ComboBoxItem();
                    cmbItem.Text = prov.Nome;
                    cmbItem.Value = prov.ID;
                    comboBoxProvincia.Items.Add(cmbItem);
                }

                comboBoxProvincia.SelectedIndex = 0;
            }
        }

        private void comboBoxProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox cmb = (System.Windows.Forms.ComboBox)sender;

            ComboBoxItem item = (ComboBoxItem)cmb.SelectedItem;

            if (Convert.ToInt32(item.Value) > 0)
            {
                int IdProvincia = Convert.ToInt32(item.Value);
                labelComune.Visible = true;
                comboBoxComune.Visible = true;

                comboBoxComune.Items.Clear();

                ComboBoxItem cmbItem = new ComboBoxItem();
                cmbItem.Text = "Seleziona Comune";
                cmbItem.Value = "0";
                comboBoxComune.Items.Add(cmbItem);

                List<Comune> listComProv = new List<Comune>();
                listComProv = listComuni.Where(p => p.IdProvincia == IdProvincia).ToList();

                foreach (var comprov in listComProv)
                {
                    cmbItem = new ComboBoxItem();
                    cmbItem.Text = comprov.Nome;
                    cmbItem.Value = comprov.ID;
                    comboBoxComune.Items.Add(cmbItem);
                }

                comboBoxComune.SelectedIndex = 0;
            }

        }

        private void comboBoxComune_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sMsg = string.Empty;
            System.Windows.Forms.ComboBox cmb = (System.Windows.Forms.ComboBox)sender;

            ComboBoxItem item = (ComboBoxItem)cmb.SelectedItem;

            if (Convert.ToInt32(item.Value) > 0)
            {
                string utenteLogin = textBoxUsername.Text;
                string regioneSelected = comboBoxRegione.Text;
                string provinciaSelected = comboBoxProvincia.Text;
                string comuneSelected = item.Text;
                sMsg = utenteLogin + " - " + comuneSelected + " - " + provinciaSelected + " - " + regioneSelected;
            }
            textBoxResult.Text = sMsg;
            _logger.Information(sMsg);
        }

        private void buttonJson_Click(object sender, EventArgs e)
        {
            oDL = new ListeDati(_logger);
            List<Regione> listRegioni = oDL.LoadRegione();

            string strJson = JsonConvert.SerializeObject(listRegioni);

            // SAVE FILE
            if (saveFileDialogJson.ShowDialog() == DialogResult.OK)
            {
                using (Stream fileStream = saveFileDialogJson.OpenFile())
                {
                    if (fileStream != null)
                    {
                        // Code to write the stream goes here.
                        // https://stackoverflow.com/questions/60029221/save-file-to-a-specific-folder-in-windows-form-c-sharp
                        using (StreamWriter sw = new StreamWriter(fileStream))
                        {
                            sw.WriteLine(strJson);
                        }
                    }
                }
            }

            textBoxResult.Text = strJson;
        }

        private void buttonObject_Click(object sender, EventArgs e)
        {
            string strJson = string.Empty;
            string fileName = "DatiRegioni.json";
            // Open FILE READ
            // https://stackoverflow.com/questions/16136383/reading-a-text-file-using-openfiledialog-in-windows-forms
            openFileDialogJson.FileName = fileName;
            if (openFileDialogJson.ShowDialog() == DialogResult.OK)
            {
                using (Stream fileStream = openFileDialogJson.OpenFile())
                {
                    if (fileStream != null)
                    {
                        // Code to read from file 
                        using (StreamReader sr = new StreamReader(fileStream))
                        {
                            strJson = sr.ReadToEnd(); //all text wil be saved in text enters are also saved
                        }
                    }
                }
            }

            // strJson = "[{\"ID\":1,\"Nome\":\"Umbria\",\"isAutonoma\":false,\"NumAbitanti\":500000},{\"ID\":2,\"Nome\":\"Lazio\",\"isAutonoma\":false,\"NumAbitanti\":6500000},{\"ID\":3,\"Nome\":\"Friuli Venezia Giulia\",\"isAutonoma\":true,\"NumAbitanti\":1150000}]";
            List<Regione> listRegioni = JsonConvert.DeserializeObject<List<Regione>>(strJson);

            foreach (Regione region in listRegioni)
            {
                textBoxResult.Text = textBoxResult.Text + region.Nome + " ";
            }
        }

        private void buttonJsonProvincia_Click(object sender, EventArgs e)
        {
            oDL = new ListeDati(_logger);
            List<Provincia> listProvince = oDL.LoadProvincia();

            string strJson = JsonConvert.SerializeObject(listProvince);

            // SAVE FILE
            if (saveFileDialogJson.ShowDialog() == DialogResult.OK)
            {
                using (Stream fileStream = saveFileDialogJson.OpenFile())
                {
                    if (fileStream != null)
                    {
                        // Code to write the stream goes here.
                        // https://stackoverflow.com/questions/60029221/save-file-to-a-specific-folder-in-windows-form-c-sharp
                        using (StreamWriter sw = new StreamWriter(fileStream))
                        {
                            sw.WriteLine(strJson);
                        }
                    }
                }
            }

            textBoxResult.Text = strJson;
        }

        private void buttonObjectProvincia_Click(object sender, EventArgs e)
        {
            string strJson = string.Empty;
            string fileName = "DatiProvince.json";
            // Open FILE READ
            // https://stackoverflow.com/questions/16136383/reading-a-text-file-using-openfiledialog-in-windows-forms
            openFileDialogJson.FileName = fileName;
            if (openFileDialogJson.ShowDialog() == DialogResult.OK)
            {
                using (Stream fileStream = openFileDialogJson.OpenFile())
                {
                    if (fileStream != null)
                    {
                        // Code to read from file 
                        using (StreamReader sr = new StreamReader(fileStream))
                        {
                            strJson = sr.ReadToEnd(); //all text wil be saved in text enters are also saved
                        }
                    }
                }
            }

            List<Provincia> listProvince = JsonConvert.DeserializeObject<List<Provincia>>(strJson);

            foreach (Provincia prov in listProvince)
            {
                textBoxResult.Text = textBoxResult.Text + prov.Nome + " ";
            }
        }

        private void buttonJsonComune_Click(object sender, EventArgs e)
        {
            oDL = new ListeDati(_logger);
            List<Comune> listComuni = oDL.LoadComune();

            string strJson = JsonConvert.SerializeObject(listComuni);

            // SAVE FILE
            if (saveFileDialogJson.ShowDialog() == DialogResult.OK)
            {
                using (Stream fileStream = saveFileDialogJson.OpenFile())
                {
                    if (fileStream != null)
                    {
                        // Code to write the stream goes here.
                        // https://stackoverflow.com/questions/60029221/save-file-to-a-specific-folder-in-windows-form-c-sharp
                        using (StreamWriter sw = new StreamWriter(fileStream))
                        {
                            sw.WriteLine(strJson);
                        }
                    }
                }
            }

        }

        private void buttonObjectComune_Click(object sender, EventArgs e)
        {
            string strJson = string.Empty;
            string fileName = "DatiComuni.json";
            // Open FILE READ
            // https://stackoverflow.com/questions/16136383/reading-a-text-file-using-openfiledialog-in-windows-forms
            openFileDialogJson.FileName = fileName;
            if (openFileDialogJson.ShowDialog() == DialogResult.OK)
            {
                using (Stream fileStream = openFileDialogJson.OpenFile())
                {
                    if (fileStream != null)
                    {
                        // Code to read from file 
                        using (StreamReader sr = new StreamReader(fileStream))
                        {
                            strJson = sr.ReadToEnd(); //all text wil be saved in text enters are also saved
                        }
                    }
                }
            }

            List<Provincia> listComuni = JsonConvert.DeserializeObject<List<Provincia>>(strJson);

            foreach (Provincia com in listComuni)
            {
                textBoxResult.Text = textBoxResult.Text + com.Nome + " ";
            }
        }
    }
}

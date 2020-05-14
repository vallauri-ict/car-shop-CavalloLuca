using System;
using System.Windows.Forms;
using carShopDllProject;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Threading;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using openXML;

namespace WindowsFormsAppProject
{
    public partial class FormMain : Form
    {
        public static openXMLUtilities _openXml = new openXMLUtilities();
        public static dbUtils _dbUtils = new dbUtils();
        SerializableBindingList<Veicolo> bindingListVeicoli;
        public FormMain()
        {
            InitializeComponent();
            bindingListVeicoli = new SerializableBindingList<Veicolo>();
            listBoxVeicoli.DataSource = bindingListVeicoli;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            CaricaDatiDiTest();
        }

        private void CaricaDatiDiTest()
        {
            Moto m = new Moto();
            bindingListVeicoli.Add(m);
            m = new Moto("Aprilia", "125", "Rosso", 600, 70, DateTime.Now, false, false, 20000, 0, "Harley Davison");
            bindingListVeicoli.Add(m);

            Auto a = new Auto();
            a = new Auto("Audi", "A1", "Blu", 1400, 75, DateTime.Now, false, false, 20000, 0, 7);
            bindingListVeicoli.Add(a);
        }

        private void nuovoToolStripButton_Click(object sender, EventArgs e)
        {
            frmAggiungiVeicolo dialogAggiungi = new frmAggiungiVeicolo(bindingListVeicoli);
            dialogAggiungi.ShowDialog();
        }

        /*private void apriToolStripButton_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(@"Veicoli.json");
            string jsonString = sr.ReadToEnd();
            var items = JsonConvert.DeserializeObject<object>(jsonString);
            SerializableBindingList<object> lst = JsonConvert.DeserializeObject<SerializableBindingList<object>>(jsonString);
            SerializableBindingList<Veicolo> lstz = Utils.deserializeObject(lst);
            MessageBox.Show(items.ToString());
        }*/

        private void salvaToolStripButton_Click(object sender, EventArgs e)
        {
            _dbUtils.eliminaTabella("AUTO");
            _dbUtils.eliminaTabella("MOTO");

            _dbUtils.creaTabella("AUTO");
            _dbUtils.creaTabella("MOTO");

            foreach (Auto car in bindingListVeicoli.OfType<Auto>())
            {
                _dbUtils.aggiungiItem("AUTO", car.Marca, car.Modello, car.Colore, Convert.ToInt32(car.Cilindrata),
                    Convert.ToDouble(car.PotenzaKw), Convert.ToDateTime(car.Immatricolazione), car.IsUsato.ToString(), 
                    car.IsKmZero.ToString(), Convert.ToInt32(car.KmPercorsi), Convert.ToDouble(car.Prezzo), 
                    Convert.ToInt32(car.NumAirbag),null);
            }

            foreach (Moto moto in bindingListVeicoli.OfType<Moto>())
            {
                _dbUtils.aggiungiItem("MOTO", moto.Marca, moto.Modello, moto.Colore, Convert.ToInt32(moto.Cilindrata),
                    Convert.ToDouble(moto.PotenzaKw), Convert.ToDateTime(moto.Immatricolazione), moto.IsUsato.ToString(),
                    moto.IsKmZero.ToString(), Convert.ToInt32(moto.KmPercorsi), Convert.ToDouble(moto.Prezzo), 
                    0, moto.MarcaSella);
            }

            MessageBox.Show("Database aggiornato.","AVVISO");
        }

        private void stampaToolStripButton_Click(object sender, EventArgs e)
        {
            string webPath = (@"www\index.html");
            Utils.createHtml(bindingListVeicoli, webPath);
            System.Diagnostics.Process.Start(webPath);
        }

        private void btnEliminaElem_Click(object sender, EventArgs e)
        {
            bindingListVeicoli.RemoveAt(listBoxVeicoli.SelectedIndex);
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            string path= (@"\\CarShop.docx");
            using (WordprocessingDocument doc = WordprocessingDocument.Create(path, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = doc.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());

                openXMLUtilities.InsertPicture(doc,"carShop.docx");
                //openXMLUtilities.AddImageToBody(doc,);
                openXMLUtilities.addHeading1Style(mainPart);
                //openXMLUtilities.createHeading();
                openXMLUtilities.createParagraphWithStyles();
                openXMLUtilities.createTable();
                openXMLUtilities.getTableProperties();
                //openXMLUtilities.createBulletNumberingPart();
                openXMLUtilities.createNumberedList();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //
        }
    }
}

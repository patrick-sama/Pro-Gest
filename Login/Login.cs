using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using MySql.Data.MySqlClient;

namespace Progim_Gest
{
    public partial class Login : Form
    {
        Dictionary<String, String> Dic;
        public Login()
        {
            InitializeComponent();
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            string query = "SELECT Nome,Diritti FROM TbUtenti WHERE Username = @userutente AND Password = @password";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = ProGestDatabase.OpenConnection();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@userutente", TxtLogin.Text);
            cmd.Parameters.AddWithValue("@password", TxtPassword.Text);
            MySqlDataReader dr = cmd.ExecuteReader();
            bool found = false;
            while (dr.Read())
            {
                found = true;
                string nome = dr.GetString(0);
                int diritti = dr.GetInt32(1);
                if (diritti == 0)
                {
                    Anteprima ante = new Anteprima(nome);
                    ante.Show();
                }
                else
                {
                    AdminForm admin = new AdminForm(nome);
                    admin.Show();
                }
            }

            if(!found)
                MessageBox.Show("Username o password sbagliati!");

            
            
        }

        

        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button1_Click(sender, e);
            }
        }







        // funzione per popolare il database delle commesse
        /*
        private void popolaTbCommesse()
        {
            ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(@"L:\PROGIM\Gestione Ore\elenco comdis.xlsx"));
            ExcelWorksheet sheet = package.Workbook.Worksheets[1];
            
            int cont = 2;
            bool chiuso = false;
            int val = 0;
            int anno;
            do
            {               
                string commessa = sheet.Cells[cont, 1].Value.ToString();
                try
                {
                    anno = int.Parse(commessa.Substring(0, 2)) + 2000;
                }
                catch(Exception ex)
                {
                    anno = 2017;
                }
                
                
                if (chiuso)
                    val = 1;        
                cont++;
                string query = "INSERT INTO TbCommesse(NomeCommessa , Anno , Chiusa) VALUES(@NomeComm , @An , @Chiu)";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@NomeComm" , commessa);
                cmd.Parameters.AddWithValue("@An", anno);
                cmd.Parameters.AddWithValue("@Chiu", val);
                cmd.Connection = ProGestDatabase.OpenConnection();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                if (commessa == "FORMAZIONE")
                    chiuso = true;
            } while (sheet.Cells[cont, 1].Value != null);
            MessageBox.Show("finito");
          
        } */


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using OfficeOpenXml;
using System.IO;
namespace Progim_Gest
{
    public partial class Commesse : Form
    {
        public Commesse()
        {
            InitializeComponent();

            AggiornaCombo();
        }

        private void AggiornaCombo()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd = new MySqlCommand();
            string query = "SELECT DISTINCT NomeCommessa FROM TbCommesse WHERE Chiusa=1 order by NomeCommessa ";
            cmd.CommandText = query;
            cmd.Connection = ProGestDatabase.OpenConnection();
            MySqlDataReader dr = cmd.ExecuteReader();
            comboCommesseChiuse.Items.Clear();
            while (dr.Read())
            {
                comboCommesseChiuse.Items.Add(dr.GetString(0));
            }
            cmd.Connection.Close();

            comboCommesseChiuse.SelectedIndex = 0;


            query = "SELECT DISTINCT NomeCommessa FROM TbCommesse WHERE Chiusa=0 order by NomeCommessa";
            cmd.CommandText = query;
            cmd.Connection = ProGestDatabase.OpenConnection();
            dr = cmd.ExecuteReader();
            comboCommesseAperte.Items.Clear();
            while (dr.Read())
            {
                comboCommesseAperte.Items.Add(dr.GetString(0));
            }
            cmd.Connection.Close();
            
            comboCommesseAperte.SelectedIndex = 0;

            cmd = new MySqlCommand();
            query = "SELECT DISTINCT NomeCommessa FROM TbCommesse order by NomeCommessa";
            cmd.CommandText = query;
            cmd.Connection = ProGestDatabase.OpenConnection();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboCommesse.Items.Add(dr.GetString(0));
            }
            cmd.Connection.Close();

            comboCommesse.SelectedIndex = 0;

            txt_AnnoCommessa.Text = "";
            txt_NomeCommessa.Text = "";
        }

        //Add Commessa
        private void btn_AddCommessa_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = ProGestDatabase.OpenConnection();

            var confirmResult = MessageBox.Show("Controlla di aver ben scritto il nome ", "Confermi ?", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                String query = "SELECT NomeCommessa FROM TbCommesse WHERE NomeCommessa=@nome";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@nome", txt_NomeCommessa.Text);
                if (cmd.ExecuteScalar() != null)
                {
                    MessageBox.Show("Errore è già presente la commessa che stai cercando di inserire");
                }
                else
                {
                    query = "INSERT INTO TbCommesse(NomeCommessa ,Anno,Chiusa) VALUES(@nome , @anno , @chiusa)";
                    cmd.CommandText = query;
                    //cmd.Parameters.AddWithValue("@nome", txt_NomeCommessa.Text);
                    cmd.Parameters.AddWithValue("@anno", Convert.ToInt16(txt_AnnoCommessa.Text));
                    cmd.Parameters.AddWithValue("@chiusa", Convert.ToInt16(0));
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("inserimento effetuato");
                    }
                    else
                        MessageBox.Show("Errore verifica di aver ben scritto il nome");
                }
            }
            else
            {
                // If 'No', do something here.
            }
            AggiornaCombo();
            cmd.Connection.Close();
        }

        private void BtnApri_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = ProGestDatabase.OpenConnection();
            string query = "SELECT Id FROM TbCommesse WHERE NomeCommessa=@nome";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@nome", comboCommesseChiuse.SelectedItem.ToString());
            int id ;

            if (cmd.ExecuteScalar() != null)
            {
                id = (int)cmd.ExecuteScalar();               
                query = "UPDATE TbCommesse SET Chiusa=0 WHERE Id=@id ";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@id", id);
                if ( cmd.ExecuteNonQuery() != -1)
                {
                    MessageBox.Show("Commessa aperta con successo");
                }else
                    MessageBox.Show("problema !!!");
            }
            else
            {
                MessageBox.Show("non puoi modificare questa commessa");
            }
            AggiornaCombo();
            cmd.Connection.Close();
        }

        private void BtnChiudi_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = ProGestDatabase.OpenConnection();
            string query = "SELECT Id FROM TbCommesse WHERE NomeCommessa=@nome AND Chiusa!=2 ";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@nome", comboCommesseAperte.SelectedItem.ToString());
            int id;
            if(cmd.ExecuteScalar() != null)
            {
                 id = (int)cmd.ExecuteScalar();
                query = "UPDATE TbCommesse SET Chiusa=1 WHERE Id=@idcommessa ";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@idcommessa", id);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Commessa chiusa con successo");
                }
                else
                    MessageBox.Show("Errore Chiama il tecnico ");
            }
            else
            {
                MessageBox.Show("non puoi modificare questa commessa");
            }
            AggiornaCombo();
            cmd.Connection.Close();
        }

        private void BtnCancella_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = ProGestDatabase.OpenConnection();
            string query = "Delete FROM TbCommesse WHERE NomeCommessa=@nome";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@nome", comboCommesse.SelectedItem.ToString());
            if (cmd.ExecuteNonQuery() != -1)
            {
                MessageBox.Show("Commessa cancellata con successo");
            }
            else
                MessageBox.Show("problema !!!");
         
            AggiornaCombo();
            cmd.Connection.Close();
        }
    }
}

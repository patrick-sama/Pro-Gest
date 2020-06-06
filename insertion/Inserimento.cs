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

namespace Progim_Gest
{
    public partial class Inserimento : Form
    {
        public Inserimento(string nome, DateTime date)
        {

            TimeSpan time = new TimeSpan(0, 0, 0);
            date = date.Date + time;
            date.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss");
            InitializeComponent();
            labelNome.Text = nome;
            dateTimePicker1.Value = date;
            MySqlConnection con = ProGestDatabase.OpenConnection();

            string query = "SELECT DISTINCT NomeCommessa FROM TbCommesse WHERE Chiusa=0 or Chiusa=2 order by NomeCommessa ";
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            List<string> commesse = new List<string>();

            foreach (DataRow r in dt.Rows)
                commesse.Add(r[0].ToString());
            DataGridViewComboBoxColumn col = ((DataGridViewComboBoxColumn)dgv.Columns[0]);
            col.DataSource = dt;
            col.ValueMember = "NomeCommessa";

            string query2 = "SELECT NomeCommessa,Ore,Attivita,TbOre.Id FROM (TbOre INNER JOIN TbCommesse on TbOre.IdCommessa=TbCommesse.ID)INNER JOIN TbUtenti " +
                           "on TbOre.IdUtente=TbUtenti.ID WHERE TbOre.Data= '" + date.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss") + "' AND TbUtenti.Nome='" + nome + "'";

            dataAdapter = new MySqlDataAdapter(query2, con);
            dt = new DataTable();
            BindingSource bs = new BindingSource();
            dataAdapter.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {

                int i = 0;
                while (row[0].ToString() != commesse[i])
                    i++;
                DataGridViewRow dgvrow = (DataGridViewRow)dgv.Rows[0].Clone();
                ((DataGridViewComboBoxCell)dgvrow.Cells[0]).DataSource = col.DataSource;
                dgvrow.Cells[0].Value = row[0].ToString();
                dgvrow.Cells[1].Value = row[1].ToString();
                dgvrow.Cells[2].Value = row[2].ToString();
                dgvrow.Cells[3].Value = row[3].ToString();
                dgv.Rows.Add(dgvrow);

            }
            settotal();
        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            MySqlCommand cmd;
            string queryId = "SELECT Id FROM TbCommesse WHERE NomeCommessa = @name";
            cmd = new MySqlCommand();
            cmd.CommandText = queryId;
            cmd.Parameters.AddWithValue("@name", dgv[0, e.RowIndex].Value);
            cmd.Connection = ProGestDatabase.OpenConnection();
            var idcom = cmd.ExecuteScalar();

            string query = "UPDATE TbOre SET IdCommessa=@IdCom,Ore=@ore,Attivita=@attivita WHERE Id=@Id";
            cmd = new MySqlCommand();
            cmd.CommandText = query;

            cmd.Parameters.AddWithValue("@Id", Convert.ToInt16(dgv[3, e.RowIndex].Value));
            cmd.Parameters.AddWithValue("@IdCom", Convert.ToInt16(idcom));
            cmd.Parameters.AddWithValue("@ore", dgv[1, e.RowIndex].Value);
            cmd.Parameters.AddWithValue("@attivita", dgv[2, e.RowIndex].Value);
            cmd.Connection = ProGestDatabase.OpenConnection();
            cmd.ExecuteNonQuery();

            settotal();
        }

        private void settotal()
        {
            double total = 0;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                if (dgv[1, i].Value != null)
                    total += double.Parse(dgv[1, i].Value.ToString());
            }
            LabelTotale.Text = total.ToString();
        }

        private void BtnAddRow_Click(object sender, EventArgs e)
        {
            int result = -1;
            try
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[3].Value == null)
                    {
                        MySqlCommand cmd;
                        string queryId = "SELECT Id FROM TbUtenti WHERE Nome = @name";
                        cmd = new MySqlCommand();
                        cmd.CommandText = queryId;
                        cmd.Parameters.AddWithValue("@name", labelNome.Text);
                        cmd.Connection = ProGestDatabase.OpenConnection();
                        var idUte = cmd.ExecuteScalar();

                        cmd = new MySqlCommand();
                        string queryIdCom = "SELECT Id FROM TbCommesse WHERE NomeCommessa = @name";
                        cmd = new MySqlCommand();
                        cmd.CommandText = queryIdCom;
                        cmd.Parameters.AddWithValue("@name", dgv[0, row.Index].Value);
                        cmd.Connection = ProGestDatabase.OpenConnection();
                        var idcom = cmd.ExecuteScalar();

                        string query = "INSERT INTO TbOre(Ore , Attivita ,Data ,IdUtente ,IdCommessa) VALUES(@ore, @attivita,@data,@idutente,@idcommessa)";
                        cmd = new MySqlCommand();
                        cmd.CommandText = query;

                        cmd.Parameters.AddWithValue("@idutente", Convert.ToInt16(idUte));
                        cmd.Parameters.AddWithValue("@idcommessa", Convert.ToInt16(idcom));
                        cmd.Parameters.AddWithValue("@data", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@ore", dgv[1, row.Index].Value);
                        cmd.Parameters.AddWithValue("@attivita", dgv[2, row.Index].Value);
                        cmd.Connection = ProGestDatabase.OpenConnection();
                        result = cmd.ExecuteNonQuery();
                    }
                }
                if(result > 0)
                    MessageBox.Show("Inserimento Eseguito ");
                //this.Close();
            }
            catch
            {
                MessageBox.Show("Controlla di aver compilato la colona delle ore ");
            }
            
        }

        private void BtnElimina_Click(object sender, EventArgs e)
        {

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = ProGestDatabase.OpenConnection();

            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                int id = int.Parse(row.Cells[3].Value.ToString());              
                string query = "DELETE FROM TbOre WHERE Id='" + id + "';";
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Data Deleted");
            //this.Close();
        }

        

        private void btnCambiaData_Click(object sender, EventArgs e)
        {
            string nome = labelNome.Text;
            DateTime date = dateTimePicker1.Value;           
            date.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss");                       
            MySqlConnection con = ProGestDatabase.OpenConnection();

            string query = "SELECT DISTINCT NomeCommessa FROM TbCommesse WHERE Chiusa=0 or Chiusa=2 order by NomeCommessa ";
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            List<string> commesse = new List<string>();

            foreach (DataRow r in dt.Rows)
                commesse.Add(r[0].ToString());
            DataGridViewComboBoxColumn col = ((DataGridViewComboBoxColumn)dgv.Columns[0]);
            col.DataSource = dt;
            col.ValueMember = "NomeCommessa";

            string query2 = "SELECT NomeCommessa,Ore,Attivita,TbOre.Id FROM (TbOre INNER JOIN TbCommesse on TbOre.IdCommessa=TbCommesse.ID)INNER JOIN TbUtenti " +
                           "on TbOre.IdUtente=TbUtenti.ID WHERE TbOre.Data= '" + date.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss") + "' AND TbUtenti.Nome='" + nome + "'";

            dataAdapter = new MySqlDataAdapter(query2, con);
            dt = new DataTable();
            BindingSource bs = new BindingSource();
            dataAdapter.Fill(dt);

            dgv.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {

                int i = 0;
                while (row[0].ToString() != commesse[i])
                    i++;
                DataGridViewRow dgvrow = (DataGridViewRow)dgv.Rows[0].Clone();
                ((DataGridViewComboBoxCell)dgvrow.Cells[0]).DataSource = col.DataSource;
                dgvrow.Cells[0].Value = row[0].ToString();
                dgvrow.Cells[1].Value = row[1].ToString();
                dgvrow.Cells[2].Value = row[2].ToString();
                dgvrow.Cells[3].Value = row[3].ToString();
                dgv.Rows.Add(dgvrow);

            }
            settotal();
        }
    }
}
    


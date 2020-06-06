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

namespace Progim_Gest
{
    public partial class Anteprima : Form
    {
        string name;
        MySqlCommand cmd;
        public Anteprima(String Username)
        {
            InitializeComponent();

            name =Username;
            labelusername.Text = name;
            MySqlConnection con = ProGestDatabase.OpenConnection();

            string query = "SELECT DISTINCT NomeCommessa FROM (TbCommesse INNER JOIN TbOre on TbCommesse.Id = TbOre.IdCommessa) " +
                               " INNER JOIN TbUtenti on TbOre.IdUtente = TbUtenti.Id" +
                               " WHERE TbUtenti.Nome ='" + name + "'";
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);


        }

        private void btnAvvio_Click(object sender, EventArgs e)
        {
            if (checkInserimento.Checked)
            {
                Inserimento ins = new Inserimento(name, dateTimeGiorno.Value);
                ins.Show();
            }

            if (checkRiempMensile.Checked)
            {
                GeneraMensile(labelusername.Text);
            }

            if (checkRiempAnnuale.Checked)
            {
                GeneraAnnuale(labelusername.Text);
            }

        }

        //Riempilogo mensile
        public void GeneraMensile(string utente)
        {
            ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(@"\\PRO-SERVER\dis\PROGIM\Gestione Ore\ORE MENSILI.xlsx"));


            cmd = new MySqlCommand();
            string queryId = "SELECT Id FROM TbUtenti WHERE Nome = @name";
            cmd = new MySqlCommand();
            cmd.CommandText = queryId;
            cmd.Parameters.AddWithValue("@name", utente);
            cmd.Connection = ProGestDatabase.OpenConnection();
            var idUte = cmd.ExecuteScalar();
            cmd.Connection.Close();

            // da qui controllare --------------------
            string query = "SELECT * FROM TbOre INNER JOIN TbCommesse on TbOre.IdCommessa=TbCommesse.Id WHERE IdUtente = @idutente AND MONTH(Data) = @mese AND YEAR(Data) = @anno AND Chiusa<2 ORDER BY IdCommessa";
            cmd = new MySqlCommand();
            cmd.Connection = ProGestDatabase.OpenConnection();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@idutente", (int)(idUte));
            cmd.Parameters.AddWithValue("@anno", dateTimeMese.Value.Year);
            cmd.Parameters.AddWithValue("@mese", dateTimeMese.Value.Month);
            List<RigaOre> righe = new List<RigaOre>();
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                int id = dr.GetInt32(0);
                double ore = dr.GetDouble(1);
                string attivita;
                try { attivita = dr.GetString(2); }
                catch { attivita = null; }
                DateTime data = dr.GetDateTime(3);
                int idut = dr.GetInt32(4);
                int idcom = dr.GetInt32(5);
                RigaOre riga = new RigaOre(id, ore, attivita, data, idut, idcom);
                righe.Add(riga);
            }

            cmd.Connection.Close();

            ExcelPackage pack = new ExcelPackage(new System.IO.FileInfo("templatemensiledisegnatore.xlsx"));
            ExcelWorksheet sheet = pack.Workbook.Worksheets[1];
            sheet.Cells[1, 5, 1, 17].Value = utente;
            sheet.Cells[2, 5, 2, 17].Value = dateTimeMese.Value.Month + "/" + dateTimeMese.Value.Year;
            string prevnomecom = "a";
            int puntatoreriga = 5;
            foreach (RigaOre riga in righe)
            {
                cmd = new MySqlCommand();
                string queryIdCom = "SELECT NomeCommessa FROM TbCommesse WHERE ID ='" + riga.IdCommessa + "'";
                cmd = new MySqlCommand();
                cmd.CommandText = queryIdCom;
                cmd.Connection = ProGestDatabase.OpenConnection();
                string nomeCommessa = (string)(cmd.ExecuteScalar());
                cmd.Connection.Close();
                if (prevnomecom != nomeCommessa)
                {
                    prevnomecom = nomeCommessa;
                    sheet.Cells[puntatoreriga, 1].Value = nomeCommessa;
                    sheet.Cells[puntatoreriga, riga.Data.Day + 1].Value = riga.Ore;
                    puntatoreriga++;
                }
                else
                {
                    if (sheet.Cells[puntatoreriga - 1, riga.Data.Day + 1].Value != null)
                    {
                        sheet.Cells[puntatoreriga - 1, riga.Data.Day + 1].Value = riga.Ore;
                    }
                    else
                    {
                        //sheet.Cells[puntatoreriga - 1, riga.Data.Day + 1].Value = (double)sheet.Cells[puntatoreriga - 1, riga.Data.Day + 1].Value + riga.Ore;
                        sheet.Cells[puntatoreriga - 1, riga.Data.Day + 1].Value = riga.Ore;
                    }
                }
            }
            puntatoreriga++;

            query = "SELECT * FROM TbOre INNER JOIN TbCommesse on TbOre.IdCommessa=TbCommesse.Id WHERE IdUtente = @idutente AND MONTH(Data) = @mese AND YEAR(Data) = @anno AND Chiusa=2 ORDER BY IdCommessa";
            cmd = new MySqlCommand();
            cmd.Connection = ProGestDatabase.OpenConnection();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@idutente", (int)(idUte));
            cmd.Parameters.AddWithValue("@anno", dateTimeMese.Value.Year);
            cmd.Parameters.AddWithValue("@mese", dateTimeMese.Value.Month);
            righe = new List<RigaOre>();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                int id = dr.GetInt32(0);
                double ore = dr.GetDouble(1);
                string attivita;
                try { attivita = dr.GetString(2); }
                catch { attivita = null; }
                DateTime data = dr.GetDateTime(3);
                int idut = dr.GetInt32(4);
                int idcom = dr.GetInt32(5);
                RigaOre riga = new RigaOre(id, ore, attivita, data, idut, idcom);
                righe.Add(riga);
            }
            cmd.Connection.Close();
            prevnomecom = "a";
            foreach (RigaOre riga in righe)
            {
                cmd = new MySqlCommand();
                string queryIdCom = "SELECT NomeCommessa FROM TbCommesse WHERE ID ='" + riga.IdCommessa + "'";
                cmd = new MySqlCommand();
                cmd.CommandText = queryIdCom;
                cmd.Connection = ProGestDatabase.OpenConnection();
                string nomeCommessa = (string)(cmd.ExecuteScalar());
                cmd.Connection.Close();
                if (prevnomecom != nomeCommessa)
                {
                    prevnomecom = nomeCommessa;
                    sheet.Cells[puntatoreriga, 1].Value = nomeCommessa;
                    sheet.Cells[puntatoreriga, riga.Data.Day + 1].Value = riga.Ore;
                    puntatoreriga++;
                }
                else
                {
                    if (sheet.Cells[puntatoreriga - 1, riga.Data.Day + 1].Value != null)
                    {
                        sheet.Cells[puntatoreriga - 1, riga.Data.Day + 1].Value = riga.Ore;
                    }
                    else
                    {
                        sheet.Cells[puntatoreriga - 1, riga.Data.Day + 1].Value = riga.Ore;
                    }
                }
            }
            query = "SELECT NomeCommessa FROM TbCommesse WHERE Chiusa=2 AND NomeCommessa <> ALL(SELECT NomeCommessa FROM TbOre INNER JOIN TbCommesse on TbOre.IdCommessa=TbCommesse.Id WHERE IdUtente = @idutente AND MONTH(Data) = @mese AND YEAR(Data) = @anno AND Chiusa=2)";
            cmd = new MySqlCommand();
            cmd.Connection = ProGestDatabase.OpenConnection();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@idutente", (int)(idUte));
            cmd.Parameters.AddWithValue("@anno", dateTimeMese.Value.Year);
            cmd.Parameters.AddWithValue("@mese", dateTimeMese.Value.Month);
            righe = new List<RigaOre>();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string nomecommessa = dr.GetString(0);
                sheet.Cells[puntatoreriga, 1].Value = nomecommessa;
                puntatoreriga++;
            }

            cmd.Connection.Close();
            double somma;
            for (int i = 5; i < puntatoreriga; i++)
            {
                somma = 0;
                for (int j = 2; j < 32; j++)
                {
                    if (sheet.Cells[i, j].Value != null)
                        somma += (double)sheet.Cells[i, j].Value;
                }
                sheet.Cells[i, 33].Value = somma;
            }
            sheet.Cells[puntatoreriga, 1].Value = "TOTALE";
            for (int i = 2; i < 33; i++)
            {
                somma = 0;
                for (int j = 5; j < puntatoreriga; j++)
                {
                    if (sheet.Cells[j, i].Value != null)
                        somma += (double)sheet.Cells[j, i].Value;
                }
                sheet.Cells[puntatoreriga, i].Value = somma;
            }
            somma = 0;
            for (int i = 2; i < 33; i++)
            {
                if (sheet.Cells[puntatoreriga, i].Value != null)
                    somma += (double)sheet.Cells[puntatoreriga, i].Value;
            }
            sheet.Cells[puntatoreriga, 33].Value = somma;

            //formattazione

            sheet.Column(1).AutoFit();

            for (int i = 5; i < puntatoreriga; i++)
            {
                for (int j = 2; j <= 32; j++)
                {
                    sheet.Cells[i, j].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                    sheet.Cells[i, j].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                }
                sheet.Cells[i, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[i, 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[i, 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                sheet.Cells[i, 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                sheet.Cells[i, 33].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[i, 33].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[i, 33].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            }

            for (int i = 2; i <= 33; i++)
            {
                sheet.Cells[puntatoreriga, i].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[puntatoreriga, i].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[puntatoreriga, i].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[puntatoreriga, i].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            }

            sheet.Cells[puntatoreriga, 1].Style.Font.Bold = true;
            sheet.Cells[puntatoreriga, 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[puntatoreriga, 1].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[puntatoreriga, 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[puntatoreriga, 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            sheet.Cells[puntatoreriga, 33].Style.Font.Bold = true;

            //save and exit
            pack.SaveAs(new System.IO.FileInfo(@"C:\Report\" + utente + "_" + dateTimeMese.Value.Month + "-" + dateTimeMese.Value.Year + ".xlsx"));
            MessageBox.Show("Report "+ utente + "_" + dateTimeMese.Value.Month + "-" + dateTimeMese.Value.Year + ".xlsx" + " Generato al percorso C:\\Report");
            pack.Dispose();
            System.Diagnostics.Process.Start(@"C:\Report\" + utente + "_" + dateTimeMese.Value.Month + "-" + dateTimeMese.Value.Year + ".xlsx");
        }

        //riempilogo annuale
        public void GeneraAnnuale(string utente)
        {
            cmd = new MySqlCommand();
            string queryId = "SELECT Id FROM TbUtenti WHERE Nome = @name";
            cmd = new MySqlCommand();
            cmd.CommandText = queryId;
            cmd.Parameters.AddWithValue("@name", utente);
            cmd.Connection = ProGestDatabase.OpenConnection();
            var idUte = cmd.ExecuteScalar();
            cmd.Connection.Close();

            // la mia query 

            /*SELECT IdCommessa,NomeCommessa ,MONTH(Data) as Mese ,sum(ore) as totaleOre FROM TbOre INNER JOIN TbCommesse on TbOre.IdCommessa=TbCommesse.Id 
            WHERE IdUtente = 2 AND YEAR(Data) = 2019 AND Chiusa<2 group by Mese ,NomeCommessa  ORDER BY NomeCommessa*/
            string query = "SELECT IdCommessa,NomeCommessa ,MONTH(Data) as Mese ,sum(ore) as totaleOre FROM TbOre INNER JOIN TbCommesse on TbOre.IdCommessa=TbCommesse.Id" +
                            " WHERE IdUtente = @idutente AND YEAR(Data) = @anno AND Chiusa<2 group by Mese ,NomeCommessa  ORDER BY NomeCommessa";
            cmd = new MySqlCommand();
            cmd.Connection = ProGestDatabase.OpenConnection();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@idutente", (int)(idUte));
            cmd.Parameters.AddWithValue("@anno", dateTimeMese.Value.Year);
            MySqlDataReader dr = cmd.ExecuteReader();

            List<RigaOre> righe = new List<RigaOre>();
            while (dr.Read())
            {
                int id = dr.GetInt32(0);              
                string NomCom = dr.GetString(1); ;
                int mese = dr.GetInt32(2);
                double totOre = dr.GetDouble(3);
                RigaOre riga = new RigaOre(NomCom, mese, totOre);
                righe.Add(riga);
            }

            cmd.Connection.Close();

            ExcelPackage pack = new ExcelPackage(new System.IO.FileInfo("templateannualedisegnatore.xlsx"));
            ExcelWorksheet sheet = pack.Workbook.Worksheets[1];
            sheet.Cells[1, 4].Value = utente;
            sheet.Cells[2, 4].Value = dateTimeMese.Value.Year;
            
            int puntatoreriga = 5;
            string prevnomecom = "a";
            foreach (RigaOre riga in righe)
            {

                string nomeCommessa = riga.NomeCommessa;
                double totalOre = riga.ToTotaleOre;
                int mese = riga.Mese;
                
                if (prevnomecom != nomeCommessa)
                {
                    prevnomecom = nomeCommessa;
                    sheet.Cells[puntatoreriga, 1].Value = nomeCommessa;
                    sheet.Cells[puntatoreriga, mese + 1].Value = totalOre;
                    puntatoreriga++;
                }
                else
                {                  
                        sheet.Cells[puntatoreriga - 1, mese + 1].Value = totalOre;                   
                }
            }
            puntatoreriga++;


            int puntatore_seconda_parte = puntatoreriga;
            // parte formazione , riunioni , ferie , mallatie.....

            query = "SELECT IdCommessa,NomeCommessa ,MONTH(Data) as Mese ,sum(ore) as totaleOre FROM TbOre INNER JOIN TbCommesse on TbOre.IdCommessa=TbCommesse.Id" +
                           " WHERE IdUtente = @idutente AND YEAR(Data) = @anno AND Chiusa=2 group by Mese ,NomeCommessa  ORDER BY NomeCommessa";
            cmd = new MySqlCommand();
            cmd.Connection = ProGestDatabase.OpenConnection();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@idutente", (int)(idUte));
            cmd.Parameters.AddWithValue("@anno", dateTimeMese.Value.Year);
             dr = cmd.ExecuteReader();

            righe = new List<RigaOre>();
            while (dr.Read())
            {
                int id = dr.GetInt32(0);
                string NomCom = dr.GetString(1); ;
                int mese = dr.GetInt32(2);
                double totOre = dr.GetDouble(3);
                RigaOre riga = new RigaOre(NomCom, mese, totOre);
                righe.Add(riga);
            }
            cmd.Connection.Close();
            prevnomecom = "a";
            foreach (RigaOre riga in righe)
            {

                string nomeCommessa = riga.NomeCommessa;
                double totalOre = riga.ToTotaleOre;
                int mese = riga.Mese;

                if (prevnomecom != nomeCommessa)
                {
                    prevnomecom = nomeCommessa;
                    sheet.Cells[puntatoreriga, 1].Value = nomeCommessa;
                    sheet.Cells[puntatoreriga, mese + 1].Value = totalOre;
                    puntatoreriga++;
                }
                else
                {
                    sheet.Cells[puntatoreriga - 1, mese + 1].Value = totalOre;
                }
            }

            List<string> commesseSpeciale = new List<string>();
            for(int i = puntatore_seconda_parte; i < puntatoreriga; i++)
            {
                commesseSpeciale.Add(sheet.Cells[i, 1].Value.ToString());
            }

            List<string> comspe = new List<string> { "RIUNIONI" , "FORMAZIONE" , "IT" , "FERIE" , "MALATTIA" };
    
            foreach(string com in comspe)
            {
                if (!commesseSpeciale.Contains(com))
                {
                    sheet.Cells[puntatoreriga, 1].Value = com;
                    puntatoreriga++;
                }
            }
            //totali

            double somma;
            for (int i = 5; i < puntatoreriga; i++)
            {
                somma = 0;
                for (int j = 2; j < 14; j++)
                {
                    if (sheet.Cells[i, j].Value != null)
                        somma += (double)sheet.Cells[i, j].Value;
                }
                sheet.Cells[i, 14].Value = somma;
            }
            sheet.Cells[puntatoreriga, 1].Value = "TOTALE";
            for (int i = 2; i < 14; i++)
            {
                somma = 0;
                for (int j = 5; j < puntatoreriga; j++)
                {
                    if (sheet.Cells[j, i].Value != null)
                        somma += (double)sheet.Cells[j, i].Value;
                }
                sheet.Cells[puntatoreriga, i].Value = somma;
            }
            somma = 0;
            for (int i = 2; i < 14; i++)
            {
                if (sheet.Cells[puntatoreriga, i].Value != null)
                    somma += (double)sheet.Cells[puntatoreriga, i].Value;
            }
            sheet.Cells[puntatoreriga, 14].Value = somma;

            //formattazione

            sheet.Column(1).AutoFit();

            for (int i = 5; i < puntatoreriga; i++)
            {
                for (int j = 2; j <= 13; j++)
                {
                    sheet.Cells[i, j].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                    sheet.Cells[i, j].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                }
                sheet.Cells[i, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                sheet.Cells[i, 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[i, 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                sheet.Cells[i, 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                sheet.Cells[i, 14].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[i, 14].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[i, 14].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            }

            for (int i = 2; i <= 14; i++)
            {
                sheet.Cells[puntatoreriga, i].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[puntatoreriga, i].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[puntatoreriga, i].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                sheet.Cells[puntatoreriga, i].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            }

            sheet.Cells[puntatoreriga, 1].Style.Font.Bold = true;
            sheet.Cells[puntatoreriga, 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[puntatoreriga, 1].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[puntatoreriga, 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            sheet.Cells[puntatoreriga, 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            sheet.Cells[puntatoreriga, 14].Style.Font.Bold = true;

            //save and exit
            pack.SaveAs(new System.IO.FileInfo(@"C:\Report\" + utente + "_" + dateTimeMese.Value.Year + ".xlsx"));
            MessageBox.Show("Report "+ utente + "_" + dateTimeMese.Value.Year + ".xlsx"+" Generato al percorso C:\\Report");
            pack.Dispose();
            System.Diagnostics.Process.Start(@"C:\Report\" + utente + "_" + dateTimeMese.Value.Year + ".xlsx");
            
        }
    }

    class RigaOre
    {
        public int Id { get; set; }
        public double Ore { get; set; }
        public string Attivita { get; set; }
        public DateTime Data { get; set; }
        public int IdUtente { get; set; }
        public int IdCommessa { get; set; }
        
        // attributi aggiuntivi per riemp annuale
        public int Mese { get; set; }
        public double ToTotaleOre { get; set; }
        public string NomeCommessa { get; set; }

        public RigaOre(int id , double ore , string attivita , DateTime data , int iutente , int idcommessa)
        {
            this.Id = id;
            this.Ore = ore;
            this.Attivita = attivita;
            this.Data = data;
            this.IdUtente = IdUtente;
            this.IdCommessa = idcommessa;
        }

        //costruttore aggiuntivo per riemp annuale
        public RigaOre(string NomeCom , int mese , double totOre )
        {
            NomeCommessa = NomeCom;
            Mese = mese;
            ToTotaleOre = totOre;
        }

    }
}

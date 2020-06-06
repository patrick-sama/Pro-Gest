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
using System.Globalization;

namespace Progim_Gest
{
    public partial class AdminForm : Form
    {
        private string nome;

        public AdminForm(string NomeAdmin)
        {
            InitializeComponent();
            labelusername.Text = NomeAdmin;

            MySqlCommand cmd = new MySqlCommand();
            string query = "SELECT DISTINCT Nome FROM TbUtenti  ";     
            cmd.CommandText = query;
            cmd.Connection = ProGestDatabase.OpenConnection();
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                checkedListDisegnatore.Items.Add(dr.GetString(0));
            }
            cmd.Connection.Close();

            cmd = new MySqlCommand();
            query = "SELECT DISTINCT NomeCommessa FROM TbCommesse order by NomeCommessa";
            cmd.CommandText = query;
            cmd.Connection = ProGestDatabase.OpenConnection();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboCommesse.Items.Add(dr.GetString(0));
                comboRiepilogoCommessa.Items.Add(dr.GetString(0));
            }
            cmd.Connection.Close();

            comboCommesse.SelectedIndex = 0;
            comboRiepilogoCommessa.SelectedIndex = 0;



        }

        private void btnAvvio_Click(object sender, EventArgs e)
        {
            if (checkInserimento.Checked)
            {
                Inserimento ins = new Inserimento(labelusername.Text, dateTimeGiorno.Value);
                ins.Show();
            }

            foreach (string utente in checkedListDisegnatore.CheckedItems)
            {
                if (checkRiempMensile.Checked)
                {
                    GeneraMensile(utente);
                }
                if (checkRiempAnnuale.Checked)
                {
                    GeneraAnnuale(utente);
                }
            }

            if (checkRiempAtt.Checked)
            {
                GeneraCommessa(comboCommesse.SelectedItem.ToString(), dateTimeRiepilogo.Value);
            }

            if (checkRiempielogoCommessa.Checked)
            {
                GeneraRiepilogoCommessa(comboRiepilogoCommessa.SelectedItem.ToString() );
            }
        }

        private void GeneraRiepilogoCommessa(string commessa)
        {
            ExcelPackage pack = new ExcelPackage(new FileInfo("templatecommessamensile.xlsx"));
            ExcelWorksheet sheet = pack.Workbook.Worksheets[1];
            MySqlCommand cmd = new MySqlCommand();

            // tutti i disegnatori che hanno lavorato in quella commessa
            string query = "SELECT DISTINCT Nome FROM TbOre inner join TbUtenti on TbOre.IdUtente = TbUtenti.Id inner join TbCommesse on TbOre.IdCommessa = TbCommesse.Id " +
                             "where NomeCommessa = '" + commessa + "'";
            cmd.CommandText = query;
            cmd.Connection = ProGestDatabase.OpenConnection();
            MySqlDataReader dr = cmd.ExecuteReader();
            int contautenti = 0;
            while (dr.Read())
            {
                sheet.Cells[5, 2 * (contautenti + 1)].Value = dr.GetString(0);
                contautenti++;
            }
            cmd.Connection.Close();

            int contarighe = 0;

            cmd = new MySqlCommand();
            query = "SELECT Ore,Data as DataInserimento ,Attivita ,TbUtenti.Nome FROM TbOre inner join TbUtenti on TbOre.IdUtente = TbUtenti.Id inner join TbCommesse on TbOre.IdCommessa = TbCommesse.Id " +
                    "where NomeCommessa = '" + commessa + "' order by DataInserimento ";
            cmd.CommandText = query;
            cmd.Connection = ProGestDatabase.OpenConnection();
            dr = cmd.ExecuteReader();


            string giornomese;
            
            while (dr.Read())
            {
                giornomese = dr.GetDateTime(1).ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                if(giornomese == sheet.Cells[5 + contarighe, 1].Value.ToString())
                {
                    contarighe--;
                    sheet.Cells[6 + contarighe, 1].Value = giornomese;
                    int countNumUtenti = 0;
                    while (countNumUtenti < contautenti)
                    {
                        if (dr.GetString(3) == sheet.Cells[5, 2 * (countNumUtenti + 1)].Value.ToString())
                        {

                            sheet.Cells[6 + contarighe, (countNumUtenti + 1) * 2].Value = dr.GetDouble(0);
                            try { sheet.Cells[6 + contarighe, (countNumUtenti + 1) * 2 + 1].Value = dr.GetString(2); }
                            catch { }
                        }
                        countNumUtenti++;
                    }
                }
                else
                {
                    sheet.Cells[6 + contarighe, 1].Value = giornomese;
                    int countNumUtenti = 0;
                    while (countNumUtenti < contautenti)
                    {
                        if (dr.GetString(3) == sheet.Cells[5, 2 * (countNumUtenti + 1)].Value.ToString())
                        {

                            sheet.Cells[6 + contarighe, (countNumUtenti + 1) * 2].Value = dr.GetDouble(0);
                            try { sheet.Cells[6 + contarighe, (countNumUtenti + 1) * 2 + 1].Value = dr.GetString(2); }
                            catch { }
                        }
                        countNumUtenti++;
                    }
                }
                
                contarighe++;
            }
            cmd.Connection.Close();


            double somma = 0;
            double totale = 0;
            for (int i = 1; i <= contautenti; i++)
            {
                somma = 0;
                for (int j = 0; j < contarighe; j++)
                {
                    if (sheet.Cells[6 + j, i * 2].Value != null)
                        somma += (double)sheet.Cells[6 + j, i * 2].Value;
                }
                totale += somma;
                sheet.Cells[6 + contarighe, i * 2].Value = somma;
            }
            sheet.Cells[3, 17].Value = totale;
            sheet.Cells[3, 1].Value = commessa;
            sheet.Cells[3, 8].Value = "Tutto";

            //formattazione

            sheet.Cells[6, 1, 6 + contarighe, contautenti * 2 + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            for (int i = 1; i <= contarighe; i++)
            {
                sheet.Cells[i + 5, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                sheet.Cells[i + 5, 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                for (int j = 0; j < 8; j++)
                {
                    sheet.Cells[i + 5, (j + 1) * 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    sheet.Cells[i + 5, (j + 1) * 2 + 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    sheet.Cells[i + 5, (j + 1) * 2 + 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    sheet.Cells[6 + contarighe, (j + 1) * 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[6, (j + 1) * 2 + 1, 6 + contarighe, (j + 1) * 2 + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                }
            }
            sheet.Cells[6 + contarighe, 1, 6 + contarighe, contautenti * 2 + 1].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            sheet.Cells[6, 1, 6 + contarighe, contautenti * 2 + 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;





            pack.SaveAs(new FileInfo(@"C:\Report\" + commessa + ".xlsx"));
            MessageBox.Show("Report" + commessa + ".xlsx  " + " Generato al percorso C:\\Report");
            pack.Dispose();
            System.Diagnostics.Process.Start(@"C:\Report\" + commessa + ".xlsx");


        }

        private void GeneraCommessa(string commessa, DateTime giorno)
        {
            ExcelPackage pack = new ExcelPackage(new FileInfo("templatecommessamensile.xlsx"));
            ExcelWorksheet sheet = pack.Workbook.Worksheets[1];
            MySqlCommand cmd = new MySqlCommand();

            // tutti i disegnatori che hanno lavorato in quella commessa
            string query = "SELECT DISTINCT Nome FROM TbOre inner join TbUtenti on TbOre.IdUtente = TbUtenti.Id inner join TbCommesse on TbOre.IdCommessa = TbCommesse.Id " +
                             "where NomeCommessa = '" + commessa + "'";
            cmd.CommandText = query;
            cmd.Connection = ProGestDatabase.OpenConnection();
            MySqlDataReader dr = cmd.ExecuteReader();
            int contautenti = 0;
            while (dr.Read())
            {
                sheet.Cells[5, 2 * (contautenti + 1)].Value = dr.GetString(0);
                contautenti++;
            }
            cmd.Connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////    quiiiii   //////////////
            DateTime giornomese = new DateTime(giorno.Year, giorno.Month, 1);
            int contarighe = 0;
            while (giornomese.Month == giorno.Month)
            {
                if (giornomese.DayOfWeek == DayOfWeek.Sunday || giornomese.DayOfWeek == DayOfWeek.Saturday)
                {
                    sheet.Cells[6 + contarighe, 1, 6 + contarighe,17].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    sheet.Cells[6 + contarighe, 1, 6 + contarighe,17].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                }
                sheet.Cells[6 + contarighe, 1].Value = giornomese.Day + "/" + giornomese.Month + "/" + giornomese.Year;
                contarighe++;
                giornomese=giornomese.AddDays(1);
            }


            for (int j = 1; j <= contautenti; j++)
            {
                cmd = new MySqlCommand();
                query = "SELECT Ore,Data,Attivita FROM TbOre inner join TbUtenti on TbOre.IdUtente = TbUtenti.Id inner join TbCommesse on TbOre.IdCommessa = TbCommesse.Id " +
                        "where MONTH(TbOre.Data)= '" + giorno.Month + "' AND NomeCommessa = '" + commessa + "' AND Nome = '" +sheet.Cells[5,j*2].Value +"'";
                cmd.CommandText = query;
                cmd.Connection = ProGestDatabase.OpenConnection();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (sheet.Cells[dr.GetDateTime(1).Day + 5, j * 2].Value == null)
                        sheet.Cells[dr.GetDateTime(1).Day + 5, j * 2].Value = dr.GetDouble(0);
                    else
                        sheet.Cells[dr.GetDateTime(1).Day + 5, j * 2].Value = (double)sheet.Cells[dr.GetDateTime(1).Day + 5, j * 2].Value+ dr.GetDouble(0);
                    try { sheet.Cells[dr.GetDateTime(1).Day + 5, j * 2 + 1].Value = dr.GetString(2); }
                    catch { }
                }
                cmd.Connection.Close();
            }


            double somma = 0;
            double totale = 0;
            for (int i = 1; i <= contautenti; i++)
            {
                somma = 0;
                for (int j = 0; j < contarighe; j++)
                {
                    if (sheet.Cells[6 + j, i * 2].Value != null)
                        somma += (double)sheet.Cells[6 + j, i * 2].Value;
                }
                totale += somma;
                sheet.Cells[6 + contarighe, i * 2].Value = somma;
            }
            sheet.Cells[3, 17].Value = totale;
            sheet.Cells[3, 1].Value = commessa;
            sheet.Cells[3, 8].Value = giorno.Month + "/" + giorno.Year;

            //formattazione

            sheet.Cells[6, 1, 6 + contarighe, contautenti * 2 + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            for (int i = 1; i <= contarighe; i++)
            {
                sheet.Cells[i + 5, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                sheet.Cells[i+5, 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                for (int j = 0; j < 8; j++)
                {
                    sheet.Cells[i+5, (j+1)*2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    sheet.Cells[i+5, (j + 1) * 2+1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    sheet.Cells[i + 5, (j + 1) * 2 + 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    sheet.Cells[6 + contarighe, (j + 1) * 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    sheet.Cells[6, (j + 1) * 2 + 1, 6 + contarighe, (j + 1) * 2 + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                }
            }
            sheet.Cells[6 + contarighe, 1, 6 + contarighe, contautenti * 2 + 1].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

            sheet.Cells[6, 1, 6 + contarighe, contautenti * 2 + 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;





            pack.SaveAs(new FileInfo(@"C:\Report\" + commessa + "_" + giorno.Month + "-" + giorno.Year + ".xlsx"));
            MessageBox.Show("Report"+ commessa + "_" + giorno.Month + " - " + giorno.Year + ".xlsx  " + " Generato al percorso C:\\Report");
            pack.Dispose();
            System.Diagnostics.Process.Start(@"C:\Report\" + commessa + "_" + giorno.Month + "-" + giorno.Year + ".xlsx");


        }

        //Riempilogo mensile
        public void GeneraMensile(string utente)
        {
            ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(@"\\PRO-SERVER\dis\PROGIM\Gestione Ore\ORE MENSILI.xlsx"));


            MySqlCommand cmd = new MySqlCommand();
            string queryId = "SELECT Id FROM TbUtenti WHERE Nome = @name";
            cmd = new MySqlCommand();
            cmd.CommandText = queryId;
            cmd.Parameters.AddWithValue("@name", utente);
            cmd.Connection = ProGestDatabase.OpenConnection();
            var idUte = cmd.ExecuteScalar();
            cmd.Connection.Close();

            // da qui controllare --------------------
            string query = "SELECT * FROM TbOre INNER JOIN TbCommesse on TbOre.IdCommessa=TbCommesse.Id " +
                "       WHERE IdUtente = @idutente AND MONTH(Data) = @mese AND YEAR(Data) = @anno AND Chiusa<2 ORDER BY IdCommessa";
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
            MySqlCommand cmd = new MySqlCommand();
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
            cmd.Parameters.AddWithValue("@anno", dateTimeAnno.Value.Year);
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
            for (int i = puntatore_seconda_parte; i < puntatoreriga; i++)
            {
                commesseSpeciale.Add(sheet.Cells[i, 1].Value.ToString());
            }

            List<string> comspe = new List<string> { "RIUNIONI", "FORMAZIONE", "IT", "FERIE", "MALATTIA" };

            foreach (string com in comspe)
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
            MessageBox.Show("Report " + utente + "_" + dateTimeMese.Value.Year + ".xlsx" + " Generato al percorso C:\\Report");
            pack.Dispose();
            System.Diagnostics.Process.Start(@"C:\Report\" + utente + "_" + dateTimeMese.Value.Year + ".xlsx");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Commesse com = new Commesse();
            com.Visible = true;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Progim_Gest
{
    class ProGestDatabase
    {
        // Dati per l'accesso in lettura al database
        private const string DB_NAME = "dbname";
        private const string USER = "user";
        private const string PWD = "password";
        private const string SERVER = "server";

        /// <summary>
        /// Restituisce un oggetto per la connessione al
        /// database My SQL di carpenteria
        /// </summary>
        /// <returns>Oggetto connessione</returns>
        public static MySqlConnection OpenConnection()
        {
            string connStr = String.Format("server={0};user id={1}; password={2}; database={3}; pooling=false;ConnectionTimeout=30;Allow User Variables=True",
                                                    SERVER, USER, PWD, DB_NAME);
            MySqlConnection conn = new MySqlConnection(connStr);
            //MySQLConnectionString connBuldier = new MySQLConnectionString("192.168.1.90", DB_NAME, USER, PWD);
            //MySQLConnection conn = new MySQLConnection(connBuldier.AsString);

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn = null;
            }

            return conn;
        }
    }
}

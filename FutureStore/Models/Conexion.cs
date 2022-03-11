using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutureStore.Models
{
    public class Conexion
    {

        private readonly string conexion = "server=localhost;user=root;password=1234-;database=futurestore;port=3306;max pool size=1000;SslMode=none;Convert Zero Datetime=True;";


        private readonly MySqlConnection con;

        public Conexion()
        {
            this.con = new MySqlConnection(conexion);
        }
        public MySqlConnection GetCon()
        {
            return this.con;
        }
        public void Conectar()
        {
            if (this.con.State == System.Data.ConnectionState.Closed)
                this.con.Open();
        }
        public void Desconectar()
        {
            if (this.con.State == System.Data.ConnectionState.Open)
                this.con.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace GESTION_DE_COMMUNE_ELKSIBA
{
    class Connexion
    {
        public SqlConnection cnx=new SqlConnection();
        public SqlCommand cmd = new SqlCommand();
        public SqlDataReader dr;
        public DataTable dt = new DataTable();

        //
        public void Connecter()
        {
            if(cnx.State==ConnectionState.Closed || cnx.State==ConnectionState.Broken)
            {
                cnx.ConnectionString = @"Data Source=DESKTOP-MI0T5TJ\SQLEXPRESS;Initial Catalog=BDGestionCommuneELKsiba;Integrated Security=True";
                cnx.Open();
            }
        }
        //fermer la connection
        public void Deconnecter()
        {
            if(cnx.State==ConnectionState.Closed)
            {
                cnx.Close();
            }
        }
    }
}

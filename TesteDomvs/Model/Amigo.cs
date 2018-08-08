using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace TesteDomvs.Negocio.Model
{
    public class Amigo
    {
        public string Nome { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Distancia { get; set; }

        readonly string conexao = 
            System.Configuration.ConfigurationManager.ConnectionStrings["ConexaoTeste"].ConnectionString;

        public void InserirAmigo(string nomeAmigo, double latitude, double longitude)
        {
            using (SqlConnection cnn = new SqlConnection(conexao))
            {
                using (SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO [dbo].[tbAmigo]
                    (
                        Nome,
                        latitude,
                        longitude
                    )
                    VALUES
                    (
                        @nomeAmigo,
                        @latitude,
                        @longitude
                    )", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.Parameters.AddWithValue("@nomeAmigo", nomeAmigo);
                    cmd.Parameters.AddWithValue("@latitude", latitude);
                    cmd.Parameters.AddWithValue("@longitude", longitude);

                    cnn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Amigo> ListarAmigos(double latitudeAtual, double longitudeAtual)
        {
            using (SqlConnection cnn = new SqlConnection(conexao))
            {
                using (SqlCommand cmd = new SqlCommand(
                    @"SELECT
                        Nome,
                        latitude,
                        longitude
                    FROM
                        dbo.tbAmigo", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    cnn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Amigo> lstAmigo = new List<Amigo>();

                        while (dr.Read())
                        {
                            Amigo objAmigo = new Amigo();

                            objAmigo.Nome = Convert.ToString(dr["Nome"] == DBNull.Value ? "" : dr["Nome"]);
                            objAmigo.Latitude = Convert.ToDouble(dr["Latitude"] == DBNull.Value ? "" : dr["Latitude"]);
                            objAmigo.Longitude = Convert.ToDouble(dr["Longitude"] == DBNull.Value ? "" : dr["Longitude"]);
                            objAmigo.Distancia = Convert.ToDouble(new Util.Localizacao().CalculateDistanceInKilometer(
                                latitudeAtual, longitudeAtual, objAmigo.Latitude, objAmigo.Longitude));

                            lstAmigo.Add(objAmigo);
                        }

                        return lstAmigo;
                    }
                }
            }
        }
    }
}
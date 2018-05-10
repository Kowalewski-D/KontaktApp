using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apkakontaktowa.Kontaktklasy
{
    class kontaktclass
    {
        public int kontaktID { get; set; }

        public string imie { get; set; }

        public string nazwisko { get; set; }

        public string telefon { get; set; }

        public string adres { get; set; }

        public string plec { get; set; }

        private string myconnstrng;

        public kontaktclass(string cs)
        {
            myconnstrng = cs;
        }

        //wyciaganie z bazy danych
        public DataTable Select()
        {

            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {

                string sql = "SELECT * FROM tbl_kontakt";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //dodawanie do bazy danych

        public bool dodaj()
        {

            bool isSuccess = false;


            using (SqlConnection conn = new SqlConnection(myconnstrng))
                try
                {

                    string sql = "INSERT INTO tbl_kontakt(imie, nazwisko,telefon, adres, plec) VALUES (@imie, @nazwisko,@telefon, @adres, @plec)";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@imie", imie);
                    cmd.Parameters.AddWithValue("@nazwisko", nazwisko);
                    cmd.Parameters.AddWithValue("@telefon", telefon);
                    cmd.Parameters.AddWithValue("@adres", adres);
                    cmd.Parameters.AddWithValue("@plec", plec);


                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        isSuccess = false;
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    conn.Close();
                }
            return isSuccess;
        }

        // aktualizacja bazy danych
        public bool aktualizuj()
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {

                string sql = "UPDATE tbl_kontakt SET imie=@imie, nazwisko=@nazwisko, telefon=@telefon, adres=@adres, plec=@plec WHERE kontaktID=@kontaktID";


                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@imie", imie);
                cmd.Parameters.AddWithValue("@nazwisko", nazwisko);
                cmd.Parameters.AddWithValue("@telefon", telefon);
                cmd.Parameters.AddWithValue("@adres", adres);
                cmd.Parameters.AddWithValue("@plec", plec);
                cmd.Parameters.AddWithValue("@kontaktID", kontaktID);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;

        }
        // usuwanie z bazy danych
        public bool usun()
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "DELETE FROM tbl_kontakt WHERE kontaktID=@kontaktID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@kontaktID", kontaktID);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();

            }
            return isSuccess;
        }
    }
}

using Apkakontaktowa.Kontaktklasy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Appkakontaktowa
{
    public partial class Appkakontaktowa : Form
    {
        public Appkakontaktowa()
        {
            InitializeComponent();
        }


        kontaktclass c = new kontaktclass(ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString);

        private void dodajbutt_Click(object sender, EventArgs e)
        {
            c.imie = imietxtbox.Text;
            c.nazwisko = nazwiskotxtbox.Text;
            c.telefon = telefontxtbox.Text;
            c.adres = adrestxtbox.Text;
            c.plec = pleccombobox.Text;

            bool success = c.dodaj();
            if (success == true)
            {
                MessageBox.Show("Poprawnie dodano kontakt");
                wyczysc();
            }
            else
            {
                MessageBox.Show("Nie dodano poprawnie kontaktu. Spóbuj ponownie ");
            }
            DataTable dt = c.Select();
            dgvListakontaktow.DataSource = dt;
        }


        private void Appkakontaktowa_Load(object sender, EventArgs e)
        {
            DataTable dt = c.Select();
            dgvListakontaktow.DataSource = dt;
        }
        //metoda czyszczenia textboxow
        public void wyczysc()
        {
           
            imietxtbox.Text = "";
            nazwiskotxtbox.Text = "";
            telefontxtbox.Text = "";
            adrestxtbox.Text = "";
            pleccombobox.Text = "";
            kontaktidtxtbox.Text = "";
        }

        private void aktualizujbutt_Click(object sender, EventArgs e)
        {
            c.kontaktID = int.Parse(kontaktidtxtbox.Text);
            c.imie = imietxtbox.Text;
            c.nazwisko = nazwiskotxtbox.Text;
            c.telefon = telefontxtbox.Text;
            c.adres = adrestxtbox.Text;
            c.plec = pleccombobox.Text;

            bool success = c.aktualizuj();
            if (success == true)
            {
                MessageBox.Show("Poprawnie zaktualizowano kontakt");

                DataTable dt = c.Select();
                dgvListakontaktow.DataSource = dt;

                wyczysc();
            }
            else
            {
                MessageBox.Show("Nie poprawnie zaktualizowano kontakt. Spóbuj ponownie ");
            }

        }

        private void dgvListakontaktow_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            //wrzucanie z database grid do poszczegolnych danych do poszczegolnych textboxow
            int rowIndex = e.RowIndex;
            kontaktidtxtbox.Text = dgvListakontaktow.Rows[rowIndex].Cells[0].Value.ToString();
            imietxtbox.Text = dgvListakontaktow.Rows[rowIndex].Cells[1].Value.ToString();
            nazwiskotxtbox.Text = dgvListakontaktow.Rows[rowIndex].Cells[2].Value.ToString();
            telefontxtbox.Text = dgvListakontaktow.Rows[rowIndex].Cells[3].Value.ToString();
            adrestxtbox.Text = dgvListakontaktow.Rows[rowIndex].Cells[4].Value.ToString();
            pleccombobox.Text = dgvListakontaktow.Rows[rowIndex].Cells[5].Value.ToString();

        }

        private void wyczyscbutt_Click(object sender, EventArgs e)
        {
            wyczysc();
        }

        private void usunbutt_Click(object sender, EventArgs e)
        {
            c.kontaktID = Convert.ToInt32(kontaktidtxtbox.Text);
            bool success = c.usun();
            if(success == true)
            {
                MessageBox.Show("Kontakt poprawnie usuniety");

                DataTable dt = c.Select();
                dgvListakontaktow.DataSource = dt;

                wyczysc();
            }
            else
            {
                MessageBox.Show("Kontakt niepoprawnie usuniety. Spróbuj ponownie");
            }
        }
        static string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        private void szukajtxtbox_TextChanged(object sender, EventArgs e)
          {
            string keyword = szukajtxtbox.Text;
            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_kontakt WHERE imie LIKE '%"+keyword+ "%' OR nazwisko LIKE '%"+keyword+"%' OR telefon LIKE '%"+keyword+"%' OR adres LIKE '%"+keyword+"%' OR plec LIKE '%"+keyword+"%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvListakontaktow.DataSource = dt;

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginRegister
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        int Marka_Id = 0;
        int Kateqoriya_Id = 0;
        int Mehsul_Id = 0;

        List<Markalar> markalars = new List<Markalar>();
        List<Kateqoriyalar> kateqoriyalars = new List<Kateqoriyalar>();
        List<Mehsullar> mehsullars = new List<Mehsullar>();

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnMarkaElaveEt_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtMarkaAdi.Text))
            {
                int count = markalars.Where(m => m.Name == txtMarkaAdi.Text).Count();
                if (count == 0)
                {
                    Marka_Id++;
                    Markalar marka = new Markalar();
                    marka.Id = Marka_Id;
                    marka.Name = txtMarkaAdi.Text;
                    markalars.Add(marka);
                    cmbMarka.Items.Add(marka.Name);
                    MessageBox.Show(marka.Name + " bazaya əlavə edildi");
                }
                else
                {
                    MessageBox.Show("Bu marka bazada var");
                }
                txtMarkaAdi.Text = "";
            }
        }

        private void btnKateqoriyaElaveEt_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtKateqoriyaAdi.Text))
            {
                int count = kateqoriyalars.Where(k => k.Name == txtKateqoriyaAdi.Text).Count();
                if (count == 0)
                {
                    Kateqoriya_Id++;
                    Kateqoriyalar kateqoriya = new Kateqoriyalar();
                    kateqoriya.Id = Kateqoriya_Id;
                    kateqoriya.Name = txtKateqoriyaAdi.Text;
                    kateqoriyalars.Add(kateqoriya);
                    cmbKateqoriya.Items.Add(kateqoriya.Name);
                    MessageBox.Show(kateqoriya.Name + " bazaya əlavə edildi");
                }
                else
                {
                    MessageBox.Show("Bu marka bazada var");
                }
                txtKateqoriyaAdi.Text = "";
                txtMarkaAdi.Text = "";
                txtMehsulAdi.Text = "";
            }
        }

        private void btnMehsulElaveEt_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtMehsulAdi.Text) && !string.IsNullOrWhiteSpace(cmbKateqoriya.Text) && !string.IsNullOrWhiteSpace(cmbMarka.Text))
            {
                int count = mehsullars.Where(m => m.Name == txtMehsulAdi.Text).Count();
                if (count == 0)
                {
                    Mehsul_Id++;
                    Mehsullar mehsul = new Mehsullar();
                    mehsul.Id = Mehsul_Id;
                    mehsul.Name = txtMehsulAdi.Text;
                    mehsul.Marka_Id = markalars.First(m => m.Name == cmbMarka.Text).Id;
                    mehsul.Kateqoriya_Id = kateqoriyalars.First(k => k.Name == cmbKateqoriya.Text).Id;
                    mehsullars.Add(mehsul);
                    MessageBox.Show(mehsul.Name + " mehsullar siyahısına əlavə edildi");
                }
                else
                {
                    MessageBox.Show("Bu məhsul adı bazada mövcuddur");
                }
            }
        }

        private void btnMehsullarSiyahi_Click(object sender, EventArgs e)
        {
            markaAdlari.Rows.Clear();
            foreach (Mehsullar mehsul in mehsullars)
            {
                markaAdlari.Rows.Add(mehsul.Id,mehsul.Name, markalars.First(mark => mark.Id == mehsul.Marka_Id).Name, kateqoriyalars.First(kat => kat.Id == mehsul.Kateqoriya_Id).Name);
            }
        }

        private void markaAdlari_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int silinecekMehsul = Convert.ToInt32(markaAdlari.Rows[e.RowIndex].Cells[0].Value);

            DialogResult dialogResult =  MessageBox.Show(mehsullars.First(m=> m.Id==silinecekMehsul).Name + " silmek isteyirsinizmi?","Mehsulu sil",MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Mehsullar mehsul = mehsullars.First(m => m.Id == silinecekMehsul);
                mehsullars.Remove(mehsul);
                markaAdlari.RowCount--;
            }
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }
    }
}


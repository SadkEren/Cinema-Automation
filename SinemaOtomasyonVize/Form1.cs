using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;


namespace SinemaOtomasyonVize
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=sinema1.accdb ");

        private void veriCek()
        {
            listView1.Items.Clear();
            baglanti.Open();

            OleDbCommand dbCommand = new OleDbCommand( "select * from Tablo1 " , baglanti);
            OleDbDataReader dataReader = dbCommand.ExecuteReader();

            while (dataReader.Read())
            {
                ListViewItem viewItem = new ListViewItem();

                viewItem.Text = dataReader["SatısNo"].ToString();

                viewItem.SubItems.Add(dataReader["FilmAdi"].ToString());
                viewItem.SubItems.Add(dataReader["FilmTarihi"].ToString());
                viewItem.SubItems.Add(dataReader["FilmSeansi"].ToString());
                viewItem.SubItems.Add(dataReader["KoltukNo"].ToString());
                viewItem.SubItems.Add(dataReader["Ad"].ToString());
                viewItem.SubItems.Add(dataReader["Soyad"].ToString());
                viewItem.SubItems.Add(dataReader["Ucret"].ToString());

                listView1.Items.Add(viewItem);

                

            }
            baglanti.Close();
        }



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            veriCek();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            

            OleDbCommand command = new OleDbCommand( " insert into Tablo1 (FilmAdi,SalonAdi,FilmTarihi,FilmSeansi,KoltukNo,Ad,Soyad,Ucret) values ('"+textBox1.Text+ "','" + textBox2.Text + "','"+dateTimePicker1.Value.ToString()+"','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "' ) ", baglanti);
            command.ExecuteNonQuery();
            baglanti.Close();

            foreach (TextBox item in this.Controls.OfType<TextBox>())
            {
                item.Text = " ";
            }

            veriCek();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            OleDbCommand komut = new OleDbCommand( " Delete From Tablo1 where KoltukNo=" +textBox8.Text ,baglanti);
            

            if (textBox8.Text == "")
            {
                MessageBox.Show("Boş Bırakılamaz");
                baglanti.Close();
                return;
            }

            komut.ExecuteNonQuery();
            baglanti.Close();

            veriCek();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection lvc = listView1.SelectedItems;

            foreach (ListViewItem Item in lvc  )
            {
                textBox8.Text = Item.SubItems[4].Text.ToString();

                textBox1.Text = Item.SubItems[1].Text.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(Item.SubItems[2].Text.ToString()); //datetimepicker oalcak
                textBox3.Text = Item.SubItems[3].Text.ToString();
                textBox4.Text = Item.SubItems[4].Text.ToString();
                textBox5.Text = Item.SubItems[5].Text.ToString();
                textBox6.Text = Item.SubItems[6].Text.ToString();
                textBox7.Text = Item.SubItems[7].Text.ToString();
               
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            OleDbCommand komut = new OleDbCommand("Update Tablo1 Set FilmAdi='"+textBox1.Text+ "', FilmTarihi='"+dateTimePicker1.Value.ToString()+"' , FilmSeansi='" + textBox3.Text + "', Ad='" + textBox5.Text + "', Soyad='" + textBox6.Text + "', Ucret='" + textBox7.Text + "'  where KoltukNo=" + textBox4.Text , baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            veriCek();

        }
    }
}

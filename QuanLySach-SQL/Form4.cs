using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLySach_SQL.Models;

namespace QuanLySach_SQL
{
    public partial class Form4 : Form
    {
        string BookID;
        string BookName;
        int? ReleaseYear;
        int? BookTypeID;
        public Form4()
        {
            InitializeComponent();
        }
        public Form4(string MaSach, string TenSach, int? NamXB, int? MaLoai) : this()
        {
            BookID = MaSach;
            BookName = TenSach;
            ReleaseYear = NamXB;
            BookTypeID = MaLoai;
        }
        QuanLySach context = new QuanLySach();
        private void FillBookTypeComboBox(List<LoaiSach> BookTypeList)
        {
            this.comboBox1.DataSource = BookTypeList;
            this.comboBox1.DisplayMember = "TenLoai";
            this.comboBox1.ValueMember = "MaLoai";
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            try
            {
                QuanLySach context = new QuanLySach();
                List<LoaiSach> BookTypeList = context.LoaiSaches.ToList();
                FillBookTypeComboBox(BookTypeList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Sach> BookList = context.Saches.ToList();
            Sach ChangeBookData = new Sach();
            ChangeBookData.MaSach = BookID;
            ChangeBookData.TenSach = textBox1.Text;
            ChangeBookData.NamXB = int.Parse(textBox2.Text);
            ChangeBookData.MaLoai = (comboBox1.SelectedItem as LoaiSach).MaLoai;
            context.Saches.AddOrUpdate(ChangeBookData);
            context.SaveChanges();
            this.Close();
        }
    }
}

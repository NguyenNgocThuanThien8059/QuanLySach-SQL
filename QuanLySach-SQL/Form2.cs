using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLySach_SQL.Models;

namespace QuanLySach_SQL
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        QuanLySach context = new QuanLySach();
        private void BindGrid(List<Sach> BookList)
        {
            dataGridView1.Rows.Clear();
            foreach (var item in BookList)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = item.MaSach;
                dataGridView1.Rows[index].Cells[1].Value = item.TenSach;
                dataGridView1.Rows[index].Cells[2].Value = item.NamXB;
                dataGridView1.Rows[index].Cells[3].Value = item.LoaiSach.TenLoai;
            }
        }
        public void LoadList()
        {
            List<Sach> LoadBookList = context.Saches.ToList();
            BindGrid(LoadBookList);
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                QuanLySach context = new QuanLySach();
                List<Sach> BookList = context.Saches.ToList();
                BindGrid(BookList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Sach> BookList = context.Saches.ToList();
            foreach (var item in BookList)
            {
                if(item.MaSach == textBox1.Text)
                {
                    context.Saches.Remove(item);
                    context.SaveChanges();
                    this.Close();
                }
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}

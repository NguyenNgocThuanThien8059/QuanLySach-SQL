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
using System.Data.Entity.Migrations;
using System.Windows.Forms;
using QuanLySach_SQL.Models;
using System.Drawing.Design;

namespace QuanLySach_SQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        QuanLySach context = new QuanLySach();
        public void LoadList()
        {
            List<Sach> LoadBookList = context.Saches.ToList();
            BindGrid(LoadBookList);
        }
        private void LoadForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = 0;
        }
        private void FillBookTypeComboBox(List<LoaiSach> BookTypeList)
        {
            this.comboBox1.DataSource = BookTypeList;
            this.comboBox1.DisplayMember = "TenLoai";
            this.comboBox1.ValueMember = "MaLoai";
        }
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
        //Thêm Sách
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
                {
                    MessageBox.Show(" Vui lòng nhập đầy đủ thông tin sách ");
                }
                if(textBox1.Text.Length < 6)
                {
                    MessageBox.Show(" Mã sách phải có ít nhất 6 ký tự ");
                }
                else
                {
                    List<Sach> BookList = context.Saches.ToList();
                    Sach NewBook = new Sach();
                    NewBook.MaSach = textBox1.Text;
                    NewBook.TenSach = textBox2.Text;
                    NewBook.NamXB = int.Parse(textBox3.Text);
                    NewBook.MaLoai = (comboBox1.SelectedItem as LoaiSach).MaLoai;
                    context.Saches.Add(NewBook);
                    context.SaveChanges();
                    MessageBox.Show(" Đã thêm mới thành công ");
                    LoadForm();
                    LoadList();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.AllowUserToAddRows = false;
                QuanLySach context = new QuanLySach();
                List<LoaiSach> BookTypeList = context.LoaiSaches.ToList();
                List<Sach> BookList = context.Saches.ToList();
                FillBookTypeComboBox(BookTypeList);
                BindGrid(BookList);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Xóa Sách
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                List<Sach> BookList = context.Saches.ToList();
                Sach DeleteBook = context.Saches.FirstOrDefault(p => p.MaSach == textBox1.Text);
                if(DeleteBook != null) 
                {
                    DeleteBook.TenSach = textBox2.Text;
                    DeleteBook.NamXB = int.Parse(textBox3.Text);
                    DeleteBook.MaLoai = (comboBox1.SelectedItem as LoaiSach).MaLoai;
                    context.Saches.Remove(DeleteBook);
                    DialogResult dialogResult = MessageBox.Show(" Bạn có muốn xóa không? ", " Xóa sách ",MessageBoxButtons.YesNo);
                    if(dialogResult == DialogResult.Yes)
                    {
                        context.SaveChanges();
                        LoadForm();
                        LoadList();
                    }
                }
            }
            catch
            {
                MessageBox.Show(" Sách cần xóa không tồn tại ");
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
        //Cập nhật Sách
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                List<Sach> BookList = context.Saches.ToList();
                Sach EditedBook = context.Saches.FirstOrDefault(p => p.MaSach == textBox1.Text);
                if (EditedBook != null)
                {
                    EditedBook.TenSach = textBox2.Text;
                    EditedBook.NamXB = int.Parse(textBox3.Text);
                    EditedBook.MaLoai = (comboBox1.SelectedItem as LoaiSach).MaLoai;
                    context.SaveChanges();
                    MessageBox.Show(" Cập nhật thành công ");
                    LoadForm();
                    LoadList();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }
        private void LenhTimKiem()
        {

        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}

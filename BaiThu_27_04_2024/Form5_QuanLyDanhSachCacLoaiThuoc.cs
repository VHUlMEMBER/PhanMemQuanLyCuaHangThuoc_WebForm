using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BaiThu_27_04_2024
{
    public partial class Form5_QuanLyDanhSachCacLoaiThuoc : Form
    {
        SqlConnection conn;
        SqlDataAdapter da;
        DataTable dt;
        public Form5_QuanLyDanhSachCacLoaiThuoc()
        {
            InitializeComponent();
            conn = new SqlConnection("");

            // Đặt vị trí khởi đầu của form là giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void Form5_QuanLyDanhSachCacLoaiThuoc_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM dmloaithuoc";
                da = new SqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DataRow newRow = dt.NewRow();
            newRow["maloaithuoc"] = txtMaLoaiThuoc.Text;
            newRow["tenloaithuoc"] = txtTenLoaiThuoc.Text;
            dt.Rows.Add(newRow);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            dt.Rows[selectedIndex]["maloaithuoc"] = txtMaLoaiThuoc.Text;
            dt.Rows[selectedIndex]["tenloaithuoc"] = txtTenLoaiThuoc.Text;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            dt.Rows[selectedIndex].Delete();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Tạo một instance của Form2 mà không cung cấp username
            Form2 form2 = new Form2("");

            // Đóng Form3
            this.Close();
        }
    }
}

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
    public partial class Form3_QuanLyDanhSachThuocTrongKho : Form
    {
        SqlConnection conn;
        SqlDataAdapter da;
        DataTable dt;
        public Form3_QuanLyDanhSachThuocTrongKho()
        {
            InitializeComponent();
            conn = new SqlConnection("");

            // Đặt vị trí khởi đầu của form là giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void Form3_QuanLyDanhSachThuocTrongKho_Load(object sender, EventArgs e)
        {
            // Load dữ liệu từ database vào DataGridView
            LoadData();

        }

        private void LoadData()
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM thuoc";
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Thêm dòng mới vào DataTable
            DataRow newRow = dt.NewRow();
            newRow["mathuoc"] = txtMaThuoc.Text;
            newRow["ten_thuoc"] = txtTenThuoc.Text;
            newRow["maloaithuoc"] = txtMaLoaiThuoc.Text;
            newRow["makhoathuoc"] = txtMaKhoThuoc.Text;
            newRow["soluong"] = int.Parse(txtSoLuong.Text);
            newRow["ngaynhap"] = dateTimePicker1.Value;

            dt.Rows.Add(newRow);

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Lấy index của dòng đang chọn trong DataGridView
            int selectedIndex = dataGridView1.CurrentCell.RowIndex;

            // Sửa thông tin trên dòng được chọn
            dt.Rows[selectedIndex]["mathuoc"] = txtMaThuoc.Text;
            dt.Rows[selectedIndex]["ten_thuoc"] = txtTenThuoc.Text;
            dt.Rows[selectedIndex]["maloaithuoc"] = txtMaLoaiThuoc.Text;
            dt.Rows[selectedIndex]["makhoathuoc"] = txtMaKhoThuoc.Text;
            dt.Rows[selectedIndex]["soluong"] = int.Parse(txtSoLuong.Text);
            dt.Rows[selectedIndex]["ngaynhap"] = dateTimePicker1.Value;

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Xóa dòng được chọn khỏi DataTable
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

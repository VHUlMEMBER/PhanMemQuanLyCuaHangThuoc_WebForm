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
    public partial class Form6_QuanLyDanhSachNhaCungCapThuoc : Form
    {
        SqlConnection conn;
        SqlDataAdapter da;
        DataTable dt;
        public Form6_QuanLyDanhSachNhaCungCapThuoc()
        {
            InitializeComponent();
            conn = new SqlConnection("");

            // Đặt vị trí khởi đầu của form là giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void Form6_QuanLyDanhSachNhaCungCapThuoc_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM nhacungcap";
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
            DataRow newRow = dt.NewRow();
            newRow["manhacungcap"] = GenerateUniqueID(); // Tạo ID mới
            newRow["tennhacungcap"] = txtTenNhaCungCap.Text;
            newRow["diachi"] = txtDiaChiNhaCungCap.Text;
            newRow["sodienthoai"] = txtSoDienThoaiNhaCungCap.Text;
            dt.Rows.Add(newRow);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            dt.Rows[selectedIndex]["tennhacungcap"] = txtTenNhaCungCap.Text;
            dt.Rows[selectedIndex]["diachi"] = txtDiaChiNhaCungCap.Text;
            dt.Rows[selectedIndex]["sodienthoai"] = txtSoDienThoaiNhaCungCap.Text;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            dt.Rows[selectedIndex].Delete();
        }
        // Phương thức tạo ID duy nhất
        private string GenerateUniqueID()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 10);
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

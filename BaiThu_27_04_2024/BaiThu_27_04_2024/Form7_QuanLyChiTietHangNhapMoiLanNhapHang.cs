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
    public partial class Form7_QuanLyChiTietHangNhapMoiLanNhapHang : Form
    {
        SqlConnection conn;
        SqlDataAdapter da;
        DataTable dt;
        public Form7_QuanLyChiTietHangNhapMoiLanNhapHang()
        {
            InitializeComponent();
            conn = new SqlConnection("Data Source=Bao;Initial Catalog=QuanLyHieuThuoc;Integrated Security=True;Encrypt=False");

            // Đặt vị trí khởi đầu của form là giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form7_QuanLyChiTietHangNhapMoiLanNhapHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM nhaphang";
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
            newRow["manhaphang"] = txtMaNhapHang.Text;
            newRow["ngaynhap"] = dateTimePicker1.Value;
            newRow["tongtien"] = decimal.Parse(txtTongTien.Text);
            newRow["manhacungcap"] = txtMaNhaCungCap.Text;
            dt.Rows.Add(newRow);
        }
        
        private void btnSua_Click(object sender, EventArgs e)
        {
            int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            dt.Rows[selectedIndex]["manhaphang"] = txtMaNhapHang.Text;
            dt.Rows[selectedIndex]["ngaynhap"] = dateTimePicker1.Value;
            dt.Rows[selectedIndex]["tongtien"] = decimal.Parse(txtTongTien.Text);
            dt.Rows[selectedIndex]["manhacungcap"] = txtMaNhaCungCap.Text;
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

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
    public partial class Form3_QuanLyDanhSachCacKhoThuoc : Form
    {
        SqlConnection conn;
        
        public Form3_QuanLyDanhSachCacKhoThuoc()
        {
            InitializeComponent();
            conn = new SqlConnection("Data Source=Bao;Initial Catalog=QuanLyHieuThuoc;Integrated Security=True;Encrypt=False");

            // Đặt vị trí khởi đầu của form là giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem sự kiện có phải do người dùng kích hoạt hay không
            if (e.RowIndex >= 0)
            {
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    column.FillWeight = 50;
                }
            }
        }

        //

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO dmkhoathuoc (makhoathuoc, tenkhoathuoc) VALUES (@makho, @tenkho)", conn))
                {
                    cmd.Parameters.AddWithValue("@makho", txtMaKho.Text);
                    cmd.Parameters.AddWithValue("@tenkho", txtTenKho.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm kho thành công!");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm kho: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

            private void btnSua_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            string makho = dataGridView1.Rows[rowIndex].Cells["makhoathuoc"].Value.ToString();
            string tenkho = txtTenKho.Text;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE dmkhoathuoc SET tenkhoathuoc = @tenkho WHERE makhoathuoc = @makho", conn);
                cmd.Parameters.AddWithValue("@tenkho", tenkho);
                cmd.Parameters.AddWithValue("@makho", makho);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã cập nhật kho thành công!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật kho: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }


        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            string makho = dataGridView1.Rows[rowIndex].Cells["makhoathuoc"].Value.ToString();

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM dmkhoathuoc WHERE makhoathuoc = @makho", conn);
                cmd.Parameters.AddWithValue("@makho", makho);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã xóa kho thành công!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa kho: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void LoadData()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM dmkhoathuoc", conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                        lblTongBanGhi.Text = "Tổng số bản ghi: " + dt.Rows.Count.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void Form3_QuanLyDanhSachCacKhoThuoc_Load(object sender, EventArgs e)
        {
            LoadData();
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

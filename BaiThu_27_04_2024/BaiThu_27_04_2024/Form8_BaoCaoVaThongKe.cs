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
using System.Drawing.Printing;

namespace BaiThu_27_04_2024
{
    public partial class Form8_BaoCaoVaThongKe : Form
    {
        SqlConnection conn;
        SqlDataAdapter da;
        DataTable dt;
        public Form8_BaoCaoVaThongKe()
        {
            InitializeComponent();
            conn = new SqlConnection("Data Source=Bao;Initial Catalog=QuanLyHieuThuoc;Integrated Security=True;Encrypt=False");
            // Thiết lập SelectionMode
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Đặt vị trí khởi đầu của form là giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void Form8_BaoCaoVaThongKe_Load(object sender, EventArgs e)
        {
            LoadData();

            // Load danh sách các giá trị manhaphang từ bảng nhaphang vào ComboBox
            LoadMaNhapHangComboBox();

            // Load danh sách mã thuốc vào ComboBox
            LoadMaThuocComboBox();

        }

        // Hàm tải danh sách mã nhập hàng từ bảng nhaphang và hiển thị vào ComboBox
        private void LoadMaNhapHangComboBox()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                string query = "SELECT manhaphang FROM nhaphang";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBoxMaNhapHang.Items.Add(reader["manhaphang"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        // Hàm tải danh sách mã thuốc từ bảng thuoc và hiển thị vào ComboBox
        private void LoadMaThuocComboBox()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                string query = "SELECT mathuoc FROM thuoc";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox_MaThuoc.Items.Add(reader["mathuoc"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
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
                string query = "SELECT * FROM chitietnhaphang";
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem ComboBox đã chọn giá trị hay chưa
            if (comboBoxMaNhapHang.SelectedItem != null && comboBox_MaThuoc.SelectedItem != null)
            {
                string maNhapHang = comboBoxMaNhapHang.SelectedItem.ToString();
                string maThuoc = comboBox_MaThuoc.SelectedItem.ToString();
                string soLuong = txtSoLuong.Text;
                string donGia = txtDonGia.Text;

                // Kiểm tra xem mã thuốc đã tồn tại trong bảng thuoc hay không
                if (IsMaThuocExists(maThuoc))
                {
                    // Thêm mới chi tiết nhập hàng vào bảng chitietnhaphang
                    AddChiTietNhapHang(maNhapHang, maThuoc, soLuong, donGia);
                }
                else
                {
                    MessageBox.Show("Mã thuốc không tồn tại trong danh sách. Vui lòng kiểm tra lại hoặc thêm mới mã thuốc.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn mã nhập hàng và mã thuốc.");
            }
        }

        // Hàm kiểm tra xem mã thuốc có tồn tại trong bảng thuoc hay không
        private bool IsMaThuocExists(string maThuoc)
        {
            try
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM thuoc WHERE mathuoc = @MaThuoc";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaThuoc", maThuoc);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        // Hàm thêm mới chi tiết nhập hàng vào bảng chitietnhaphang
        private void AddChiTietNhapHang(string maNhapHang, string maThuoc, string soLuong, string donGia)
        {
            try
            {
                conn.Open();
                string query = "INSERT INTO chitietnhaphang (manhaphang, mathuoc, soluong, dongia) VALUES (@MaNhapHang, @MaThuoc, @SoLuong, @DonGia)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNhapHang", maNhapHang);
                cmd.Parameters.AddWithValue("@MaThuoc", maThuoc);
                cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                cmd.Parameters.AddWithValue("@DonGia", donGia);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Thêm mới chi tiết nhập hàng thành công.");
                    LoadData(); // Reload dữ liệu sau khi thêm mới
                }
                else
                {
                    MessageBox.Show("Thêm mới chi tiết nhập hàng thất bại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm mới chi tiết nhập hàng: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

            private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                dt.Rows[selectedIndex]["manhaphang"] = comboBoxMaNhapHang.SelectedItem.ToString(); // Sử dụng giá trị đã chọn từ ComboBox
                dt.Rows[selectedIndex]["mathuoc"] = comboBox_MaThuoc.SelectedItem.ToString();
                dt.Rows[selectedIndex]["soluong"] = txtSoLuong.Text;
                dt.Rows[selectedIndex]["dongia"] = txtDonGia.Text;
                UpdateDatabase();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa.");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                dt.Rows[selectedIndex].Delete();
                UpdateDatabase();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa.");
            }
        }
        private void UpdateDatabase()
        {
            try
            {
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                da.Update(dt);
                MessageBox.Show("Cập nhật cơ sở dữ liệu thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // Tô đậm hàng khi nhấp chuột
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);


        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
            PrintDialog printDialog = new PrintDialog();

            // Hiển thị hộp thoại in và kiểm tra xem người dùng đã chọn in chưa
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Tạo một đối tượng Graphics để vẽ
            Graphics graphics = e.Graphics;
            Font font = new Font("Arial", 12);

            // Vị trí bắt đầu của trang in
            float startX = e.MarginBounds.Left;
            float startY = e.MarginBounds.Top;

            // Vị trí dòng tiếp theo để vẽ
            float currentY = startY;

            // Vẽ tiêu đề cột
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                graphics.DrawString(dataGridView1.Columns[i].HeaderText, font, Brushes.Black, startX, currentY);
                startX += dataGridView1.Columns[i].Width;
            }
            currentY += font.GetHeight();

            // Vẽ nội dung từng dòng
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                startX = e.MarginBounds.Left;
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    graphics.DrawString(dataGridView1.Rows[i].Cells[j].Value.ToString(), font, Brushes.Black, startX, currentY);
                    startX += dataGridView1.Columns[j].Width;
                }
                currentY += font.GetHeight();
            }
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



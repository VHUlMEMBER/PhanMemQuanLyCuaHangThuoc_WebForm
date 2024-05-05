using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace BaiThu_27_04_2024
{
    public partial class Form2 : Form
    {
        private SqlConnection conn;
        private SqlDataAdapter da;
        private DataTable dt;
        private Form3_QuanLyDanhSachCacKhoThuoc form3;
        private Form3_QuanLyDanhSachThuocTrongKho form4;
        private Form5_QuanLyDanhSachCacLoaiThuoc form5;
        private Form6_QuanLyDanhSachNhaCungCapThuoc form6;
        private Form7_QuanLyChiTietHangNhapMoiLanNhapHang form7;
        private Form8_BaoCaoVaThongKe form8;

        private Timer pictureBoxTimer;
        private List<PictureBox> pictureBoxesToShow;
        private int pictureBoxIndex = 0;

        private Timer buttonTimer;
        private List<Button> buttonsToShow;
        private int buttonIndex = 0;

        private string loggedInUsername; // Biến lưu trữ tên đăng nhập đã đăng nhập
        public Form2(string username)
        {
            InitializeComponent();
            conn = new SqlConnection("Data Source=Bao;Initial Catalog=QuanLyHieuThuoc;Integrated Security=True;Encrypt=False");

            // Khởi tạo danh sách chứa các PictureBox muốn hiển thị
            pictureBoxesToShow = new List<PictureBox> { pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10, pictureBox11 };

            // Ẩn tất cả các PictureBox ban đầu
            HideAllPictureBoxes();

            // Khởi tạo và cài đặt timer cho PictureBox
            pictureBoxTimer = new Timer();
            pictureBoxTimer.Interval = 1000; // 0.1 giây
            pictureBoxTimer.Tick += PictureBoxTimer_Tick;
            pictureBoxTimer.Start();

            // Khởi tạo danh sách chứa các button muốn hiển thị
            buttonsToShow = new List<Button> { btnQuanLyDanhSachCacKhoThuoc, btnQuanLyDanhSachThuocTrongKho, btnQuanLyDanhSachCacLoaiThuoc, btnQuanLyDanhSachNhaCungCapThuoc, btnQuanLyChiTietHangNhapTrongMoiLanNhapHang, btnBaoCaoVaThongKe };

            // Ẩn tất cả các button ban đầu
            HideAllButtons();

            // Khởi tạo và cài đặt timer cho button
            buttonTimer = new Timer();
            buttonTimer.Interval = 1000; // 0.1 giây
            buttonTimer.Tick += ButtonTimer_Tick;

            loggedInUsername = username; // Lưu tên đăng nhập đã đăng nhập

            // Hiển thị tên đăng nhập
            label3.Text = "Xin chào, " + loggedInUsername + "!";

            // Đặt vị trí khởi đầu của form là giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;

            comboBox1.Visible = false;

            // Load dữ liệu thông báo vào ComboBox
            LoadNotificationData();

            // Đặt vị trí khởi đầu của form là giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;

            // Đặt kiểu DropDown của ComboBox thành DropDownList
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            // Đăng ký sự kiện ComboBox được chọn
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }

        private void LoadNotificationData()
        {
            // Khai báo DataTable dt để lưu dữ liệu từ cơ sở dữ liệu
            dt = new DataTable();

            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    string query = "SELECT NoiDung FROM ThongBao";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Load dữ liệu vào DataTable dt
                    dt.Load(reader);

                    while (reader.Read())
                    {
                        string noiDung = reader["NoiDung"].ToString();
                        comboBox1.Items.Add(noiDung);
                    }
                }
                else
                {
                    MessageBox.Show("Không thể mở kết nối đến cơ sở dữ liệu.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }



        private void PictureBoxTimer_Tick(object sender, EventArgs e)
        {
            if (pictureBoxIndex < pictureBoxesToShow.Count)
            {
                // Hiển thị PictureBox tiếp theo trong danh sách
                pictureBoxesToShow[pictureBoxIndex].Visible = true;
                pictureBoxIndex++;
            }
            else
            {
                // Đã hiển thị hết tất cả các PictureBox, dừng timer của PictureBox và bắt đầu timer của button
                pictureBoxTimer.Stop();
                buttonTimer.Start();
            }
        }

        private void ButtonTimer_Tick(object sender, EventArgs e)
        {
            if (buttonIndex < buttonsToShow.Count)
            {
                // Hiển thị button tiếp theo trong danh sách
                buttonsToShow[buttonIndex].Visible = true;
                buttonIndex++;
            }
            else
            {
                // Đã hiển thị hết tất cả các button, dừng timer của button
                buttonTimer.Stop();
            }
        }

        private void HideAllPictureBoxes()
        {
            foreach (PictureBox pictureBox in pictureBoxesToShow)
            {
                pictureBox.Visible = false;
            }
        }

        private void HideAllButtons()
        {
            foreach (Button button in buttonsToShow)
            {
                button.Visible = false;
            }
        }

        private void btnQuanLyDanhSachCacKhoThuoc_Click(object sender, EventArgs e)
        {
            this.Hide();
            form3 = new Form3_QuanLyDanhSachCacKhoThuoc();
            form3.FormClosed += Form3_FormClosed;
            form3.ShowDialog();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void btnQuanLyDanhSachThuocTrongKho_Click(object sender, EventArgs e)
        {
            this.Hide();
            form4 = new Form3_QuanLyDanhSachThuocTrongKho();
            form4.FormClosed += Form4_FormClosed;
            form4.ShowDialog();
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void btnQuanLyDanhSachCacLoaiThuoc_Click(object sender, EventArgs e)
        {
            this.Hide();
            form5 = new Form5_QuanLyDanhSachCacLoaiThuoc();
            form5.FormClosed += Form5_FormClosed;
            form5.ShowDialog();
        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void btnQuanLyDanhSachNhaCungCapThuoc_Click(object sender, EventArgs e)
        {
            this.Hide();
            form6 = new Form6_QuanLyDanhSachNhaCungCapThuoc();
            form6.FormClosed += Form6_FormClosed;
            form6.ShowDialog();
        }

        private void Form6_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void btnQuanLyChiTietHangNhapTrongMoiLanNhapHang_Click(object sender, EventArgs e)
        {
            this.Hide();
            form7 = new Form7_QuanLyChiTietHangNhapMoiLanNhapHang();
            form7.FormClosed += Form7_FormClosed;
            form7.ShowDialog();
        }

        private void Form7_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void btnBaoCaoVaThongKe_Click(object sender, EventArgs e)
        {
            this.Hide();
            form8 = new Form8_BaoCaoVaThongKe();
            form8.FormClosed += Form8_FormClosed;
            form8.ShowDialog();
        }

        private void Form8_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Kiểm tra trạng thái hiện tại của comboBox1
            if (comboBox1.Visible)
            {
                // Nếu comboBox1 đang hiển thị, ẩn nó đi
                comboBox1.Visible = false;
            }
            else
            {
                // Nếu comboBox1 không hiển thị, hiển thị nó lên
                comboBox1.Visible = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            // Đặt lại trạng thái đăng nhập
            isLoggedIn = false;

            // Hiển thị Form1
            Form1 form1 = new Form1();
            form1.Show();

            // Đóng Form2
            this.Close();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Kiểm tra trạng thái đăng nhập
            if (isLoggedIn)
            {
                // Nếu đã đăng nhập, không hiển thị Form1 mà chỉ ẩn Form2
                this.Hide();
            }
            else
            {
                // Nếu chưa đăng nhập, hiển thị lại Form1
                Form1 form1 = new Form1();
                //form1.Show();
            }
        
        }

        // Khai báo biến isLoggedIn ở phạm vi của lớp Form2
        private bool isLoggedIn = false;

        // Khi người dùng đăng nhập thành công, đặt isLoggedIn thành true
        private void SuccessfulLogin()
        {
            isLoggedIn = true;
            // Thực hiện các hành động khác sau khi đăng nhập thành công
        }

        // Khi người dùng đăng xuất hoặc thoát khỏi ứng dụng, đặt isLoggedIn thành false
        private void Logout()
        {
            isLoggedIn = false;
            // Thực hiện các hành động khác sau khi đăng xuất
        }
        private void LoadDataFromDatabase()
        {
            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    string query = "SELECT * FROM YourTableName";
                    da = new SqlDataAdapter(query, conn);
                    dt = new DataTable();
                    da.Fill(dt);
                    // Bây giờ bạn có thể sử dụng DataTable dt chứa dữ liệu từ cơ sở dữ liệu
                }
                else
                {
                    MessageBox.Show("Không thể mở kết nối đến cơ sở dữ liệu.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

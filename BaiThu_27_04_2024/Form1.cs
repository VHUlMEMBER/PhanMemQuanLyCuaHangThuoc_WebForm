using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BaiThu_27_04_2024
{
    public partial class Form1 : Form
    {
        SqlConnection conn;

        public Form1()
        {
            InitializeComponent();
            conn = new SqlConnection("");

            // Thêm các tùy chọn vào ComboBox
            comboBox1.Items.Add("admin");
            comboBox1.Items.Add("user");

            // Chọn mặc định là "admin"
            comboBox1.SelectedIndex = 0;

            // Thiết lập DropDownStyle để ComboBox không thể chỉnh sửa
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            // Đặt vị trí khởi đầu của form là giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void txtTaiKhoan_TextChanged(object sender, EventArgs e)
        {
            // Có thể thêm xử lý ở đây nếu cần
        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {
            // Có thể thêm xử lý ở đây nếu cần
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtTaiKhoan.Text;
            string password = txtMatKhau.Text;
            string role = comboBox1.SelectedItem.ToString();

            try
            {
                conn.Open();

                string query = "";
                if (role == "admin")
                {
                    query = "SELECT COUNT(*) FROM Admins WHERE Username = @username AND Password = @password";
                }
                else if (role == "user")
                {
                    query = "SELECT COUNT(*) FROM Users WHERE Username = @username AND Password = @password";
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                int result = (int)cmd.ExecuteScalar();

                if (result > 0)
                {
                    if (role == "admin")
                    {
                        // Mở Form9_GiaoDienAdmin nếu là admin
                        Form9_GiaoDienAdmin form9 = new Form9_GiaoDienAdmin(username, DateTime.Now);
                        form9.Show();
                    }
                    else if (role == "user")
                    {
                        // Mở Form2 nếu là user
                        Form2 form2 = new Form2(username);
                        form2.Show();
                    }
                    // Đóng form đăng nhập sau khi chuyển hướng
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng. Vui lòng thử lại.");
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



        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            // Kiểm tra người dùng đã chọn vai trò "user" trong ComboBox chưa
            if (comboBox1.SelectedItem.ToString() == "user")
            {
                // Tạo một điều khiển TextBox để nhận đầu vào từ người dùng
                TextBox textBox = new TextBox();
                textBox.Size = new Size(200, 20);

                // Tạo một Label để hướng dẫn người dùng
                Label labelInstruction = new Label();
                labelInstruction.Text = "Nhập tên đăng nhập";
                labelInstruction.AutoSize = true;

                // Tạo một Form để chứa TextBox và Label
                Form inputForm = new Form();
                inputForm.Text = "Yêu cầu quên mật khẩu";
                inputForm.Controls.Add(labelInstruction);
                labelInstruction.Location = new Point(10, 10);
                inputForm.Controls.Add(textBox);
                textBox.Location = new Point(10, 40);

                // Thêm các nút OK và Hủy vào Form
                Button okButton = new Button();
                okButton.Text = "OK";
                okButton.DialogResult = DialogResult.OK;
                inputForm.Controls.Add(okButton);
                okButton.Location = new Point(10, 80); // Đặt vị trí của nút OK
                Button cancelButton = new Button();
                cancelButton.Text = "Hủy";
                cancelButton.DialogResult = DialogResult.Cancel;
                inputForm.Controls.Add(cancelButton);
                cancelButton.Location = new Point(100, 80); // Đặt vị trí của nút Hủy

                // Đặt thuộc tính DialogResult của Form dựa trên nút nào được nhấp
                inputForm.AcceptButton = okButton;
                inputForm.CancelButton = cancelButton;

                // Hiển thị Form như một hộp thoại
                DialogResult dialogResult = inputForm.ShowDialog();

                // Kiểm tra xem nút OK có được nhấp và người dùng có nhập tên đăng nhập hay không
                if (dialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(textBox.Text))
                {
                    string username = textBox.Text;

                    try
                    {
                        conn.Open();

                        // Kiểm tra xem tên đăng nhập có tồn tại trong bảng Users hoặc Admin không
                        string query = "SELECT COUNT(*) FROM users WHERE username = @username UNION ALL SELECT COUNT(*) FROM Admins WHERE Username = @username";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@username", username);
                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            // Thực hiện lệnh INSERT vào bảng YeuCauQuenMatKhau
                            string insertQuery = "INSERT INTO YeuCauQuenMatKhau (TenDangNhap, ThoiGianYeuCau, VanDe) VALUES (@username, GETDATE(), @vanDe)";
                            SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                            insertCmd.Parameters.AddWithValue("@username", username);
                            insertCmd.Parameters.AddWithValue("@vanDe", "Nội dung vấn đề"); // Thay "Nội dung vấn đề" bằng giá trị thích hợp
                            insertCmd.ExecuteNonQuery();

                            // Thông báo thành công
                            MessageBox.Show("Yêu cầu đã được gửi đến admin. Vui lòng chờ xử lý.");
                        }
                        else
                        {
                            // Thông báo tài khoản không tồn tại
                            MessageBox.Show("Tài khoản không tồn tại.");
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
                else
                {
                    // Hiển thị thông báo nếu người dùng không nhập tên đăng nhập
                    MessageBox.Show("Bạn cần nhập tên đăng nhập trước khi gửi yêu cầu quên mật khẩu.");
                }
            }
            else
            {
                // Hiển thị thông báo nếu người dùng chưa chọn vai trò "user" trong ComboBox
                MessageBox.Show("Vui lòng chọn vai trò 'user' để gửi yêu cầu quên mật khẩu.");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Kiểm tra khi trạng thái của CheckBox thay đổi
            if (checkBox1.Checked)
            {
                // Nếu CheckBox được đánh dấu, hiển thị mật khẩu
                txtMatKhau.UseSystemPasswordChar = false;
            }
            else
            {
                // Nếu CheckBox không được đánh dấu, ẩn mật khẩu
                txtMatKhau.UseSystemPasswordChar = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Khi form được tải, mật khẩu sẽ được ẩn
            txtMatKhau.UseSystemPasswordChar = true;


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận trước khi thoát
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát khỏi chương trình?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Kiểm tra xem người dùng đã chọn Yes hay không
            if (result == DialogResult.Yes)
            {
                // Thoát khỏi ứng dụng
                Application.Exit();
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem ComboBox đã chọn vai trò "user" hay không
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "user")
            {
                // Tạo các điều khiển TextBox để nhận đầu vào từ người dùng
                TextBox textBoxInput = new TextBox();
                textBoxInput.Size = new Size(200, 20);

                // Tạo một Label để hướng dẫn người dùng cách nhập
                Label labelInstruction = new Label();
                labelInstruction.Text = "Nhập tài khoản và mật khẩu, vấn đề của bạn:";
                labelInstruction.AutoSize = true;

                // Tạo một Form để chứa TextBox và Label
                Form inputForm = new Form();
                inputForm.Text = "Đăng ký tài khoản";
                inputForm.Controls.Add(labelInstruction);
                labelInstruction.Location = new Point(10, 10);
                inputForm.Controls.Add(textBoxInput);
                textBoxInput.Location = new Point(10, 40);

                // Thêm các nút OK và Hủy vào Form
                Button okButton = new Button();
                okButton.Text = "OK";
                okButton.DialogResult = DialogResult.OK;
                inputForm.Controls.Add(okButton);
                okButton.Location = new Point(10, 80); // Đặt vị trí của nút OK
                Button cancelButton = new Button();
                cancelButton.Text = "Hủy";
                cancelButton.DialogResult = DialogResult.Cancel;
                inputForm.Controls.Add(cancelButton);
                cancelButton.Location = new Point(100, 80); // Đặt vị trí của nút Hủy

                // Đặt kích thước của form
                inputForm.Size = new Size(300, 150); // Đặt kích thước form chiều rộng to hơn

                // Đặt thuộc tính DialogResult của Form dựa trên nút nào được nhấp
                inputForm.AcceptButton = okButton;
                inputForm.CancelButton = cancelButton;

                // Hiển thị Form như một hộp thoại
                DialogResult dialogResult = inputForm.ShowDialog();

                // Kiểm tra xem nút OK có được nhấp và người dùng có nhập dữ liệu hay không
                if (dialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(textBoxInput.Text))
                {
                    // Lấy dữ liệu từ TextBox
                    string userInput = textBoxInput.Text;

                    try
                    {
                        conn.Open();

                        // Thực hiện lệnh INSERT vào bảng YeuCauQuenMatKhau
                        string insertQuery = "INSERT INTO YeuCauQuenMatKhau (TenDangNhap, ThoiGianYeuCau, VanDe) VALUES (@username, GETDATE(), @vanDe)";
                        SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                        insertCmd.Parameters.AddWithValue("@username", userInput);
                        insertCmd.Parameters.AddWithValue("@vanDe", "Nội dung vấn đề"); // Thay "Nội dung vấn đề" bằng giá trị thích hợp
                        insertCmd.ExecuteNonQuery();

                        // Thông báo thành công
                        MessageBox.Show("Yêu cầu đã được gửi đến admin. Vui lòng chờ xử lý.\n\nDữ liệu đã nhập: " + userInput);
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
            else
            {
                MessageBox.Show("Vui lòng chọn vai trò 'user' trong ComboBox để đăng ký.");

            }
        }
    }
}


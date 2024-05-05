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
using System.IO;

namespace BaiThu_27_04_2024
{

    public partial class Form9_GiaoDienAdmin : Form
    {

        SqlConnection conn;
        SqlDataAdapter da;
        DataTable dt;

        // Xóa RadioButton và thay thế bằng CheckBox
        CheckBox checkbox1 = new CheckBox();

        public Form9_GiaoDienAdmin(string username, DateTime loginTime)
        {
            InitializeComponent();
            conn = new SqlConnection("Data Source=Bao;Initial Catalog=QuanLyHieuThuoc;Integrated Security=True;Encrypt=False");

            // Khởi tạo nhãn và cài đặt thuộc tính
            lblNotification = new Label();
            lblNotification.AutoSize = true;
            lblNotification.Location = new Point(20, 150);
            this.Controls.Add(lblNotification); // Thêm nhãn vào form

            // Ẩn các điều khiển mặc định
            richTextBox1.Visible = false;
            listBox1.Visible = false;
            listBox2.Visible = false;
            checkBox1.Visible = false;
            listBox3.Visible = false;
            dataGridView1.Visible = false;
            btnGui.Visible = false;
            btnXoa.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            textBox1_TenDangNhap.Visible = false;
            textBox2_MatKhau.Visible = false;
            btnThem_Datagridview.Visible = false;
            btnSua_Datagridview.Visible = false;
            btnXoa_Datagridview.Visible = false;

            // Thêm các mục vào comboBox1
            comboBox1.Items.Add("Gửi thông báo cho người dùng");
            comboBox1.Items.Add("Quản lý bảo mật cho người dùng");
            comboBox1.Items.Add("Xóa thông báo cho người dùng");
            comboBox1.Items.Add("Xem lại toàn bộ thông báo đã từng thông báo cho người dùng");

            // Đặt vị trí khởi đầu của form là giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;

            // Đặt kiểu DropDown của ComboBox thành DropDownList
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            // Đăng ký sự kiện ComboBox được chọn
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

            LoadData();

            


        }

        private void LoadData()
        {

            // Trong phương thức LoadData
            string query = "SELECT Username, LoginTime FROM UserLoginInfo"; // Đảm bảo rằng cột "Password" của bảng UserLoginInfo được truy vấn đúng
            da = new SqlDataAdapter(query, conn);
            dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Username"].HeaderText = "Người dùng";
            dataGridView1.Columns["LoginTime"].HeaderText = "Thời gian đăng nhập";

            dataGridView1.Columns.Remove("Username");
            dataGridView1.Columns.Remove("LoginTime");
            ;
        }


        private void AddAccountToListBox(string username, DateTime loginTime)
        {
            listBox1.Items.Add($"{username} - {loginTime}");
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                if (comboBox1.SelectedItem.ToString() == "Quản lý bảo mật cho người dùng")
                {
                    richTextBox1.Visible = false;
                    listBox1.Visible = true;
                    btnGui.Visible = false;
                    dataGridView1.Visible = true;
                    btnXoa.Visible = false;
                    lblNotification.Visible = false;
                    label1.Visible = true;
                    label2.Visible = true;
                    textBox1_TenDangNhap.Visible = true;
                    textBox2_MatKhau.Visible = true;
                    btnThem_Datagridview.Visible = true;
                    btnSua_Datagridview.Visible = true;
                    btnXoa_Datagridview.Visible = true;

                }
                else if (comboBox1.SelectedItem.ToString() == "Gửi thông báo cho người dùng")
                {
                    richTextBox1.Visible = true;
                    listBox1.Visible = false;
                    btnGui.Visible = true;
                    dataGridView1.Visible = false;
                    listBox2.Visible = false;
                    checkbox1.Visible = false;
                    btnXoa.Visible = false;
                    label1.Visible = false;
                    label2.Visible = false;
                    textBox1_TenDangNhap.Visible = false;
                    textBox2_MatKhau.Visible = false;
                    btnThem_Datagridview.Visible = false;
                    btnSua_Datagridview.Visible = false;
                    btnXoa_Datagridview.Visible = false;
                }
                else if (comboBox1.SelectedItem.ToString() == "Xóa thông báo cho người dùng")
                {
                    // Hiện listBox2 và checkBox1 khi chọn "Xóa thông báo cho người dùng"
                    listBox2.Visible = true;
                    checkBox1.Visible = true;
                    listBox1.Visible = false;
                    btnGui.Visible = false;
                    dataGridView1.Visible = false;
                    richTextBox1.Visible = false;
                    btnXoa.Visible = true;
                    label1.Visible = false;
                    label2.Visible = false;
                    textBox1_TenDangNhap.Visible = false;
                    textBox2_MatKhau.Visible = false;
                    btnThem_Datagridview.Visible = false;
                    btnSua_Datagridview.Visible = false;
                    btnXoa_Datagridview.Visible = false;
                }
                else if (comboBox1.SelectedItem.ToString() == "Xem lại toàn bộ thông báo đã từng thông báo cho người dùng")
                {
                    // Hiển thị listBox1 khi chọn mục này và ẩn các điều khiển khác
                    listBox1.Visible = true;
                    richTextBox1.Visible = false;
                    btnGui.Visible = false;
                    listBox2.Visible = false;
                    checkBox1.Visible = false;
                    listBox3.Visible = false;
                    dataGridView1.Visible = false;
                    btnXoa.Visible = false; 
                    lblNotification.Visible = false;
                    label1.Visible = false;
                    label2.Visible = false;
                    label1.Visible = false;
                    label2.Visible = false;
                    textBox1_TenDangNhap.Visible = false;
                    textBox2_MatKhau.Visible = false;
                    btnThem_Datagridview.Visible = false;
                    btnSua_Datagridview.Visible = false;
                    btnXoa_Datagridview.Visible = false;
                    listBox3.Visible=true;
                }
                else
                {
                    // Ẩn listBox1 khi chọn mục khác và hiện các điều khiển khác
                    listBox1.Visible = false;
                    richTextBox1.Visible = false;
                    btnGui.Visible = false;
                    listBox2.Visible = false;
                    checkBox1.Visible = false;
                    listBox3.Visible = false;
                    dataGridView1.Visible = false;
                    lblNotification.Visible = false;
                    label1.Visible = false;
                    label2.Visible = false;
                    textBox1_TenDangNhap.Visible = false;
                    textBox2_MatKhau.Visible = false;
                    btnThem_Datagridview.Visible = false;
                    btnSua_Datagridview.Visible = false;
                    btnXoa_Datagridview.Visible = false;

                }
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            // Hiển thị Form1 và đóng Form admin
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }



        private void btnGui_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string noiDung = richTextBox1.Text;

                // Thêm thông báo vào bảng ThongBao
                string query = "INSERT INTO ThongBao (NoiDung) VALUES (@noiDung)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@noiDung", noiDung);
                cmd.ExecuteNonQuery();

                // Hiển thị thông báo gửi thành công trên nhãn
                lblNotification.Text = "Thông báo đã được gửi thành công và lưu vào cơ sở dữ liệu.";

                // Thêm thông báo vào listBox2 kèm theo thời gian
                string formattedMessage = $"[{DateTime.Now}] {noiDung}"; // Thêm thời gian vào thông báo
                listBox2.Items.Add(formattedMessage);

                // Thêm thông báo vào listBox3
                listBox3.Items.Add(formattedMessage);
            }
            catch (Exception ex)
            {
                // Xử lý nếu có lỗi xảy ra
                lblNotification.Text = "Đã xảy ra lỗi: " + ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadDataGridViewData();
        }

        private void LoadDataGridViewData()
        {
            try
            {
                if (conn.State == ConnectionState.Closed) // Kiểm tra trạng thái kết nối trước khi mở
                {
                    conn.Open();
                }

                string queryForDataGridView = "SELECT Username AS 'Người dùng', Password AS 'Mật khẩu' FROM Users"; // Đổi tên cột Username và Password
                using (SqlCommand cmd = new SqlCommand(queryForDataGridView, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;

                    // Đổi tên cột trên DataGridView
                    dataGridView1.Columns["Người dùng"].HeaderText = "Người dùng";
                    dataGridView1.Columns["Mật khẩu"].HeaderText = "Mật khẩu";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) // Kiểm tra trạng thái kết nối trước khi đóng
                {
                    conn.Close();
                }
            }
        }

        private void Form9_GiaoDienAdmin_Load(object sender, EventArgs e)
        {
            LoadDataGridViewData();
            LoadListBoxData();
            LoadMessagesFromFile(); // Tải danh sách thông báo từ tập tin khi chương trình khởi động
            
        }

        private void LoadMessagesFromFile()
        {
            if (File.Exists("messages.txt"))
            {
                // Đọc danh sách thông báo từ tập tin và hiển thị chúng trên listBox3
                using (StreamReader reader = new StreamReader("messages.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        listBox3.Items.Add(line);
                    }
                }
            }
        }
            

            private void LoadListBoxData()
        {
            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    string query = "SELECT TenDangNhap, ThoiGianYeuCau, VanDe FROM YeuCauQuenMatKhau";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string username = reader["TenDangNhap"].ToString();
                        string requestTime = reader["ThoiGianYeuCau"].ToString();
                        string issue = reader["VanDe"].ToString();
                        string formattedMessage = $"Người dùng: {username}, Thời gian yêu cầu: {requestTime}, Vấn đề: Hỗ trợ mật khẩu";
                        listBox1.Items.Add(formattedMessage);
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
    {
        // Lấy thông báo được chọn trong listBox2
        string selectedMessage = listBox2.SelectedItem.ToString();

        try
        {
            conn.Open();
            string query = "DELETE FROM ThongBao WHERE NoiDung = @noiDung";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@noiDung", selectedMessage);
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                // Xóa thành công từ cơ sở dữ liệu, tiến hành xóa khỏi listBox2
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                lblNotification.Text = "Thông báo đã được xóa thành công.";
            }
            else
            {
                lblNotification.Text = "Không tìm thấy thông báo trong cơ sở dữ liệu.";
            }
        }
        catch (Exception ex)
        {
            lblNotification.Text = "Đã xảy ra lỗi: " + ex.Message;
        }
        finally
        {
            conn.Close();
        }
    }
    else
    {
        lblNotification.Text = "Vui lòng chọn thông báo cần xóa.";
    }
        }


        private void LoadAllMessagesToListBox()
        {
            listBox2.Items.Clear(); // Xóa các mục hiện tại trong listBox2 trước khi tải thông báo mới

            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    string query = "SELECT NoiDung FROM ThongBao";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string noiDung = reader["NoiDung"].ToString();
                        listBox2.Items.Add(noiDung);
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                // Nếu CheckBox được chọn, tải cơ sở dữ liệu mới vào listBox2
                LoadAllMessagesToListBox();
            }
            else
            {
                // Nếu CheckBox không được chọn, xóa cơ sở dữ liệu khỏi listBox2
                listBox2.Items.Clear();
            }
        }


        private void Form9_GiaoDienAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveMessagesToFile(); // Lưu danh sách thông báo vào tập tin trước khi đóng chương trình
        }

        private void SaveMessagesToFile()
        {
            // Lưu danh sách thông báo từ listBox3 vào tập tin
            using (StreamWriter writer = new StreamWriter("messages.txt"))
            {
                foreach (var item in listBox3.Items)
                {
                    writer.WriteLine(item.ToString());
                }
            }
        }

        private void btnThem_Datagridview_Click(object sender, EventArgs e)
        {
            string tenDangNhap = textBox1_TenDangNhap.Text;
            string matKhau = textBox2_MatKhau.Text;
            string loaiUser = "staff"; // hoặc "admin" tùy thuộc vào loại người dùng bạn muốn thêm

            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    // Thực hiện truy vấn thêm người dùng vào cơ sở dữ liệu
                    string query = "INSERT INTO Users (Username, Password, loaiuser) VALUES (@tenDangNhap, @matKhau, @loaiUser)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                    cmd.Parameters.AddWithValue("@matKhau", matKhau);
                    cmd.Parameters.AddWithValue("@loaiUser", loaiUser);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã thêm người dùng thành công.");
                        LoadDataGridViewData(); // Cập nhật lại DataGridView sau khi thêm người dùng thành công
                    }
                    else
                    {
                        MessageBox.Show("Thêm người dùng không thành công.");
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
    

        private void btnSua_Datagridview_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn hàng nào trong DataGridView chưa
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy thông tin người dùng được chọn từ DataGridView
                string tenDangNhap = dataGridView1.CurrentRow.Cells["Người dùng"].Value.ToString();
                string matKhau = dataGridView1.CurrentRow.Cells["Mật khẩu"].Value.ToString();

                // Hiển thị thông tin người dùng trong các TextBox để sửa
                textBox1_TenDangNhap.Text = tenDangNhap;
                textBox2_MatKhau.Text = matKhau;

                // Ẩn nút "Thêm" và hiển thị nút "Lưu" để thực hiện cập nhật
                btnThem_Datagridview.Visible = false;
                btnLuu_Datagridview.Visible = true;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn người dùng cần sửa từ bảng.");
            }
        }

        private void btnXoa_Datagridview_Click(object sender, EventArgs e)
        {
            // Lấy thông tin người dùng được chọn từ DataGridView
            string tenDangNhap = dataGridView1.CurrentRow.Cells["Người dùng"].Value.ToString();

            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    // Thực hiện truy vấn xóa người dùng từ cơ sở dữ liệu
                    string query = "DELETE FROM Users WHERE Username = @tenDangNhap";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã xóa người dùng thành công.");
                        LoadDataGridViewData(); // Cập nhật lại DataGridView sau khi xóa người dùng thành công
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy người dùng để xóa.");
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

        private void btnLuu_Datagridview_Click(object sender, EventArgs e)
        {
            // Lấy thông tin người dùng đã chỉnh sửa từ các control TextBox
            string tenDangNhapMoi = textBox1_TenDangNhap.Text;
            string matKhauMoi = textBox2_MatKhau.Text;

            // Kiểm tra xem người dùng đã chọn hàng nào trong DataGridView chưa
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy thông tin người dùng được chọn từ DataGridView
                string tenDangNhapCu = dataGridView1.CurrentRow.Cells["Người dùng"].Value.ToString();

                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        // Thực hiện truy vấn cập nhật thông tin người dùng vào cơ sở dữ liệu
                        string query = "UPDATE Users SET Username = @tenDangNhapMoi, Password = @matKhauMoi WHERE Username = @tenDangNhapCu";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@tenDangNhapMoi", tenDangNhapMoi);
                        cmd.Parameters.AddWithValue("@matKhauMoi", matKhauMoi);
                        cmd.Parameters.AddWithValue("@tenDangNhapCu", tenDangNhapCu);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã cập nhật thông tin người dùng thành công.");
                            LoadDataGridViewData(); // Cập nhật lại DataGridView sau khi cập nhật thông tin người dùng thành công
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật thông tin người dùng không thành công.");
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
            else
            {
                MessageBox.Show("Vui lòng chọn người dùng cần sửa từ bảng.");
            }

            // Sau khi lưu, hiển thị lại nút "Thêm" và ẩn nút "Lưu"
            btnThem_Datagridview.Visible = true;
            btnLuu_Datagridview.Visible = false;
        }
    }
}

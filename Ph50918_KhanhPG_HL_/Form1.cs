using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
namespace Ph50918_KhanhPG_HL_
{
    public partial class Form1 : Form
    {
        string connectionString;
        public Form1()
        {
            connectionString = @"Data Source=DESKTOP-G1OA6S5\HOANGLAN;Initial Catalog=NET1021_Final33;Integrated Security=True;TrustServerCertificate=True";
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = " Vui lòng chọn đúng File để mở";
            openFileDialog.Filter = "Tệp tin văn bản (*.txt)|*.txt| tất cả các tệp tin(*.*)|*.*";
            openFileDialog.ShowDialog();
            string path = openFileDialog.FileName;
            textBox1.Text = File.ReadAllText(path);
            MessageBox.Show("Đọc thành công");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            string textdata = textBox1.Text;
            saveFileDialog.Filter = "tệp tin văn bản (*.txt)|*.txt";
            saveFileDialog.ShowDialog();
            string filepath = saveFileDialog.FileName;
            DialogResult result = MessageBox.Show("BẠN CÓ MUỐN LƯU TRỮ VÀO FILE HAY KO", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                File.WriteAllText(filepath, textdata);
                MessageBox.Show("Lưu thành công");
            }
            else MessageBox.Show("Tiếp tục nhập");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int tuoi;
            if (int.TryParse(textBox2.Text, out tuoi))
            {
                string thongBao;

                if (tuoi >= 0 && tuoi <= 10)
                {
                    thongBao = "Thiếu nhi";
                }
                else if (tuoi >= 11 && tuoi <= 18)
                {
                    thongBao = "Thanh thiếu niên";
                }
                else if (tuoi >= 19 && tuoi <= 60)
                {
                    thongBao = "Tuổi trưởng thành";
                }
                else if (tuoi > 60)
                {
                    thongBao = "Lão niên";
                }
                else
                {
                    thongBao = "Tuổi không hợp lệ";
                }

                MessageBox.Show($"Tuổi: {tuoi}\n{thongBao}");
            }
            else
            {
                MessageBox.Show("Vui lòng nhập một số hợp lệ.");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Thái bình");
            comboBox1.Items.Add("Hà Nội");
            comboBox1.Items.Add("Quảng ninh");
            comboBox1.Items.Add("HƯng yên");
            comboBox1.Items.Add("Hà Nam");
        }

        private void button5_load_Click(object sender, EventArgs e)
        {
            using SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            string query = "SELECT * FROM Mathang";
            SqlCommand command = new SqlCommand(query, conn); ;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox4.Text = "";
            radioButton3.Checked = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {            
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox3.Text = row.Cells["ten"].Value.ToString();
                textBox4.Text = row.Cells["gia"].Value.ToString();
                string mucthue = row.Cells["mucthue"].Value.ToString();
                if (mucthue == "10%")
                {
                    radioButton1.Checked = true;
                }
                else if (mucthue == "20%")
                {
                    radioButton2.Checked = true;
                }
                else
                {
                    radioButton3.Checked = true;
                }
            }
        }
    }
}

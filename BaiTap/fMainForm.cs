using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BaiTap
{
    public partial class fMainForm : Form
    {

        List<student> danhSach = new List<student>();


        public fMainForm()
        {
            InitializeComponent();
        }

        private void fMainForm_Load(object sender, EventArgs e)
        {
            

            SetupDataGridView();
            List<student> ds = LoadJSON("SinhVien.json", danhSach);
            ShowData(ds);

        }



        private void SetupDataGridView()
        {
            /*dataGridView1.Columns.Add("mssv", "MSSV");
            dataGridView1.Columns.Add("hovatenlot", "Họ và tên lót");
            dataGridView1.Columns.Add("ten", "Tên");
            dataGridView1.Columns.Add("ngaysinh", "Ngày sinh");
            dataGridView1.Columns.Add("lop", "Lớp");
            dataGridView1.Columns.Add("socmnd", "Số CMND");
            dataGridView1.Columns.Add("sodt", "Số điện thoại");
            dataGridView1.Columns.Add("diachi", "Địa chỉ liên lạc");

            dataGridView1.Columns["mssv"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["hovatenlot"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["ten"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["ngaysinh"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["lop"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["socmnd"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["sodt"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["diachi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            */

            //mặc định datagridview tự động sinh cột dựa vào datasource
            dataGridView1.AutoGenerateColumns = false;

            //fill đầy chiều rộng của datagridview
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Mssv",
                HeaderText = "MSSV",
                DataPropertyName = "Mssv",
                Width = 80
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "HoVaTenLot",
                HeaderText = "Họ và tên lót",
                DataPropertyName = "HoVaTenLot",
                Width = 150
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Ten",
                HeaderText = "Tên",
                DataPropertyName = "Ten",
                Width = 80
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "NgaySinh",
                HeaderText = "Ngày sinh",
                DataPropertyName = "NgaySinh",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Lop",
                HeaderText = "Lớp",
                DataPropertyName = "Lop",
                Width = 80
            });



            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "SoCMND",
                HeaderText = "Số CMND",
                DataPropertyName = "SoCMND",
                Width = 140
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "SoDT",
                HeaderText = "Số điện thoại",
                DataPropertyName = "SoDT",
                Width = 110
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "DiaChi",
                HeaderText = "Địa chỉ",
                DataPropertyName = "DiaChi",
                Width = 200
            });



        }

        /*private void LoadData(string fileName, List<student> danhSach)
        {
            danhSach.Clear();
            StreamReader sr = new StreamReader(fileName);

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    student sv = new student(line);
                    danhSach.Add(sv);
                }
            }

            sr.Close();

            // Lưu vào danh sách toàn cục
            danhSach = danhSach;

            // Hiển thị
            ShowData(danhSach);
        }*/


        //đọc sinh viên từ file json
        private List<student> LoadJSON(string fileName, List<student> dssv)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    MessageBox.Show("Không tìm thấy file SinhVien.json", "Cảnh báo");
                    return dssv;
                }

                string json;
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8))
                {
                    json = sr.ReadToEnd();
                }

                var array = (JObject)JsonConvert.DeserializeObject(json);
                var students = array["sinhvien"].Children();

                foreach (var sv in students)
                {
                    string mssv = sv["mssv"]?.Value<string>() ?? "";
                    string hovatenlot = sv["hovatenlot"]?.Value<string>() ?? "";
                    string ten = sv["ten"]?.Value<string>() ?? "";

                    DateTime ngaysinh = DateTime.MinValue;
                    if (sv["ngaysinh"] != null)
                    {
                        DateTime.TryParse(sv["ngaysinh"].ToString(), out ngaysinh);
                    }

                    string lop = sv["lop"]?.Value<string>() ?? "";
                    string socmnd = sv["socmnd"]?.Value<string>() ?? "";
                    string sodt = sv["sodt"]?.Value<string>() ?? "";
                    string diachi = sv["diachi"]?.Value<string>() ?? "";
                    string gioitinh = sv["gioitinh"]?.Value<string>() ?? "nam";

                    List<string> monhoc = sv["monhoc"]?.ToObject<List<string>>() ?? new List<string>();

                    student sinhvien = new student(mssv, hovatenlot, ten, lop, gioitinh, ngaysinh,
                                                  socmnd, sodt, diachi, monhoc);
                    dssv.Add(sinhvien);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc file JSON: " + ex.Message, "Lỗi");
            }

            return dssv;
        }


        private void ShowData(List<student> dssv)
        {
            dataGridView1.DataSource = dssv;
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //nếu click vào header thì bỏ qua
            if (e.RowIndex < 0) return;

            //lấy ra hàng hiện tại
            //DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            //lấy ra hàng đó -> lấy ra đối tượng gốc -> ép kiểu về student
            student sv = (student)dataGridView1.Rows[e.RowIndex].DataBoundItem;


            //gán cho textbox
            mtxtbMSSV.Text = sv.Mssv;
            txtHoVaTenLot.Text = sv.HoVaTenLot;
            txtTen.Text = sv.Ten;
            mtxtbCCCD.Text = sv.SoCMND;
            txtDiaChi.Text = sv.DiaChi;
            mtxtbSoDT.Text = sv.SoDT;

            //lớp
            cboLop.Text = sv.Lop;

            //giới tính

            if (sv.GioiTinh == "nam") rabtnNam.Checked = true;
            else
            {
                rabtnNu.Checked = true;
            }

            //ngày sinh
            dtpNgaySinh.Value = sv.NgaySinh;

            //môn học
            //xóa các checkbox
            foreach (Control c in tableLayoutPanel1.Controls)
            {
                if (c is CheckBox cb)
                {
                    cb.Checked = false;
                }
            }

            foreach (Control c in tableLayoutPanel1.Controls)
            {
                if(c is CheckBox cb && sv.MonHocDK.Contains(cb.Text))
                {
                    cb.Checked = true;
                }
            }


            /*foreach(var mon in sv.MonHocDK)
            {
                if(mon=="Mạng máy tính")
                    chkb1.Checked = true;
                if(mon=="Hệ điều hành")
                    chkb2.Checked = true;
                if (mon == "Lập trình CSDL")
                    chkb3.Checked = true;
                if (mon == "Lập trình mạng")
                    chkb4.Checked = true;
                if (mon == "Đồ án cơ sở")
                    chkb5.Checked = true;
                if (mon == "Phương pháp nghiên cứu khoa học")
                    chkb6.Checked = true;
                if (mon == "Lập trình trên thiết bị di động")
                    chkb7.Checked = true;
                if (mon == "An toàn và bảo mật hệ thống")
                    chkb8.Checked = true;
            }*/

        }

        //thoát chương trình
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            ReLoad();
            fTimKiem formTimKiem = new fTimKiem();
            formTimKiem.ShowDialog();
            
            dataGridView1.DataSource = formTimKiem.TimKiem(danhSach);
        }

        public void ReLoad()
        {
            danhSach.Clear(); // Xóa danh sách cũ trước
            LoadJSON("SinhVien.json", danhSach);
            dataGridView1.DataSource = null; // Reset datasource
            dataGridView1.DataSource = danhSach;
            dataGridView1.Refresh();
        }



        private void ThemMoi()
        {
            if (!IsInputValid())
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra MSSV có trùng không
            if (danhSach.Any(sv => sv.Mssv == mtxtbMSSV.Text.Trim()))
            {
                MessageBox.Show("MSSV đã tồn tại, vui lòng nhập MSSV khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var svMoi = new student()
            {
                Mssv = mtxtbMSSV.Text.Trim(),
                HoVaTenLot = txtHoVaTenLot.Text.Trim(),
                Ten = txtTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value,
                Lop = cboLop.Text.Trim(),
                GioiTinh = rabtnNam.Checked ? "Nam" : "Nữ",
                SoCMND = mtxtbCCCD.Text.Trim(),
                SoDT = mtxtbSoDT.Text.Trim(),
                DiaChi = txtDiaChi.Text.Trim(),
                MonHocDK = LayMonHocDaChon()
            };

            danhSach.Add(svMoi);

            LuuDanhSachVaoFile();

            ShowData(danhSach);
            MessageBox.Show("Thêm sinh viên thành công!", "Thông báo");
        }

        private void CapNhat()
        {
            if (!IsInputValid())
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var svCapNhat = danhSach.FirstOrDefault(sv => sv.Mssv == mtxtbMSSV.Text.Trim());
            if (svCapNhat == null)
            {
                MessageBox.Show("Không tìm thấy sinh viên cần cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            svCapNhat.HoVaTenLot = txtHoVaTenLot.Text.Trim();
            svCapNhat.Ten = txtTen.Text.Trim();
            svCapNhat.NgaySinh = dtpNgaySinh.Value;
            svCapNhat.Lop = cboLop.Text.Trim();
            svCapNhat.GioiTinh = rabtnNam.Checked ? "Nam" : "Nữ";
            svCapNhat.SoCMND = mtxtbCCCD.Text.Trim();
            svCapNhat.SoDT = mtxtbSoDT.Text.Trim();
            svCapNhat.DiaChi = txtDiaChi.Text.Trim();


            var monHocMoi = LayMonHocDaChon();
            svCapNhat.MonHocDK = monHocMoi;


            LuuDanhSachVaoFile();

            ShowData(danhSach);
            MessageBox.Show("Cập nhật sinh viên thành công!", "Thông báo");
        }

        private void LuuDanhSachVaoFile()
        {
            try
            {
                // Tạo danh sách với tên property lowercase để khớp với format đọc
                var sinhvienList = danhSach.Select(sv => new
                {
                    mssv = sv.Mssv,
                    hovatenlot = sv.HoVaTenLot,
                    ten = sv.Ten,
                    gioitinh = sv.GioiTinh.ToLower(), // "Nam" -> "nam", "Nữ" -> "nữ"
                    ngaysinh = sv.NgaySinh.ToString("M/d/yyyy"), // format: 9/9/2005
                    lop = sv.Lop,
                    socmnd = sv.SoCMND,
                    sodt = sv.SoDT,
                    diachi = sv.DiaChi,
                    monhoc = sv.MonHocDK
                }).ToList();

                // Tạo object với property "sinhvien"
                var data = new { sinhvien = sinhvienList };

                string json = JsonConvert.SerializeObject(data, Formatting.Indented);

                File.WriteAllText("SinhVien.json", json, Encoding.UTF8);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Không thể ghi file. Đảm bảo file không đang mở.\n" + ex.Message,
                                "Lỗi ghi file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private List<string> LayMonHocDaChon()
        {
            List<string> monHoc = new List<string>();

            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                if (ctrl is CheckBox chk && chk.Checked)
                {
                    monHoc.Add(chk.Text);
                }
            }

            return monHoc;
        }


        // xem người dùng có nhập đầy đủ chua
        private bool IsInputValid()
        {

            if (string.IsNullOrEmpty(mtxtbMSSV.Text) ||
                string.IsNullOrEmpty(txtHoVaTenLot.Text) ||
                string.IsNullOrEmpty(txtTen.Text) ||
                string.IsNullOrEmpty(mtxtbCCCD.Text) ||
                string.IsNullOrEmpty(mtxtbSoDT.Text) ||
                string.IsNullOrEmpty(txtDiaChi.Text) ||
                string.IsNullOrEmpty(cboLop.Text) ||
                (!rabtnNam.Checked && !rabtnNu.Checked) ||
                !CoMonHocDuocChon())
            {
                return false;
            }


            return true;
        }



        private bool CoMonHocDuocChon()
        {
            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                if (ctrl is CheckBox chk && chk.Checked)
                {
                    return true;
                }
            }

            return false;
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            ThemMoi();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            CapNhat();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            mtxtbMSSV.Text = "";
            txtHoVaTenLot.Text = "";
            txtTen.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
            cboLop.SelectedIndex = -1;
            rabtnNam.Checked = false;
            rabtnNu.Checked = false;
            mtxtbCCCD.Text = "";
            mtxtbSoDT.Text = "";
            txtDiaChi.Text = "";

            // Bỏ chọn tất cả checkbox trong môn học
            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                if (ctrl is CheckBox chk)
                {
                    chk.Checked = false;
                }
            }
            ReLoad();
           
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            XoaSinhVien();
        }


        private void XoaSinhVien()
        {
            string mssvCanXoa = mtxtbMSSV.Text.Trim();

            if (string.IsNullOrEmpty(mssvCanXoa))
            {
                MessageBox.Show("Vui lòng nhập hoặc chọn MSSV của sinh viên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var svXoa = danhSach.FirstOrDefault(sv => sv.Mssv == mssvCanXoa);
            if (svXoa == null)
            {
                MessageBox.Show("Không tìm thấy sinh viên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa sinh viên MSSV: {mssvCanXoa}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                danhSach.Remove(svXoa);
                LuuDanhSachVaoFile();
                ShowData(danhSach);
                MessageBox.Show("Xóa sinh viên thành công!", "Thông báo");

                // Reset form sau khi xóa
                btnReset_Click(null, null);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

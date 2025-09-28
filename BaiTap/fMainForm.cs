using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            LoadData("ThongTinSinhVien.txt", danhSach);

            
        }

        private void SetupDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;

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
                Width = 90,
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
                Name = "GioiTinh",
                HeaderText = "Giới tính",
                DataPropertyName = "GioiTinh",
                Width = 70
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "SoCCCD",
                HeaderText = "Số CMND",
                DataPropertyName = "SoCCCD",
                Width = 100
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
                HeaderText = "Địa chỉ liên lạc",
                DataPropertyName = "DiaChi",
                Width = 200
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "MonHoc",
                HeaderText = "Môn học đã đăng ký",
                DataPropertyName = "MonHoc",
                Width = 200
            });

        }

        private void LoadData(string fileName, List<student> danhSach)
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
        }


        private void ShowData(List<student> danhSach)
        {
            dataGridView1.DataSource = danhSach.Select(sv => new
            {
                sv.Mssv,
                sv.HoVaTenLot,
                sv.Ten,
                NgaySinh = sv.NgaySinh.ToString("dd/MM/yyyy"),
                sv.Lop,
                sv.GioiTinh,
                sv.SoCCCD,
                sv.SoDT,
                DiaChi = sv.DiaChi,
                MonHoc = string.Join(",", sv.MonHocDK)
            }).ToList();
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // tránh header
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Lấy dữ liệu từ cột (theo tên hoặc vị trí)
                mtxtbMSSV.Text = row.Cells["Mssv"].Value?.ToString();
                txtHoVaTenLot.Text = row.Cells["HoVaTenLot"].Value?.ToString();
                txtTen.Text = row.Cells["Ten"].Value?.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString();
                mtxtbCCCD.Text = row.Cells["SoCCCD"].Value?.ToString();
                mtxtbSoDT.Text = row.Cells["SoDT"].Value?.ToString();
                cboLop.Text = row.Cells["Lop"].Value?.ToString();

                DateTime ngaySinh;
                if (DateTime.TryParseExact(row.Cells["NgaySinh"].Value?.ToString(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out ngaySinh))
                {
                    dtpNgaySinh.Value = ngaySinh;
                }

                // Giới tính
                string gioiTinh = row.Cells["GioiTinh"].Value?.ToString();
                rabtnNam.Checked = gioiTinh == "Nam";
                rabtnNu.Checked = gioiTinh == "Nữ";


                // lấy chuỗi môn học từ dòng được chọn
                string monHocStr = row.Cells["MonHoc"].Value?.ToString();
                string[] monHocArr = monHocStr.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

                // bỏ chọn hết các checkbox trong TableLayoutPanel
                foreach (Control ctrl in tableLayoutPanel1.Controls)
                {
                    if (ctrl is CheckBox chk)
                    {
                        chk.Checked = monHocArr.Contains(chk.Text);
                    }
                }


            }
        }

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
            string Mssv = mtxtbMSSV.Text.Trim().ToLower();
            string Ten = txtTen.Text.Trim().ToLower();
            string Lop = cboLop.Text.Trim().ToLower();

            var ketQua = danhSach.Where(sv =>
                (string.IsNullOrEmpty(Mssv) || sv.Mssv.ToLower().Contains(Mssv)) &&
                (string.IsNullOrEmpty(Ten) || sv.Ten.ToLower().Contains(Ten)) &&
                (string.IsNullOrEmpty(Lop) || sv.Lop.ToLower().Contains(Lop))
            ).ToList();

            ShowData(ketQua);
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
                SoCCCD = mtxtbCCCD.Text.Trim(),
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
            svCapNhat.SoCCCD = mtxtbCCCD.Text.Trim();
            svCapNhat.SoDT = mtxtbSoDT.Text.Trim();
            svCapNhat.DiaChi = txtDiaChi.Text.Trim();
            

            var monHocMoi = LayMonHocDaChon();

            
            svCapNhat.MonHocDK = svCapNhat.MonHocDK.Union(monHocMoi).ToArray();


            LuuDanhSachVaoFile();

            ShowData(danhSach);
            MessageBox.Show("Cập nhật sinh viên thành công!", "Thông báo");
        }

        private void LuuDanhSachVaoFile()
        {
            using (StreamWriter sw = new StreamWriter("ThongTinSinhVien.txt"))
            {
                foreach (var sv in danhSach)
                {
                    // Bạn phải đảm bảo constructor student(string line) tách dữ liệu chính xác theo format file
                    string monHocStr = string.Join(", ", sv.MonHocDK ?? new string[0]);
                    string line = $"{sv.Mssv},{sv.HoVaTenLot},{sv.Ten},{sv.NgaySinh:dd/MM/yyyy},{sv.Lop},{sv.GioiTinh},{sv.SoCCCD},{sv.SoDT},{sv.DiaChi},{monHocStr}";
                    sw.WriteLine(line);
                }
            }
        }


        private string[] LayMonHocDaChon()
        {
            List<string> monHoc = new List<string>();

            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                if (ctrl is CheckBox chk && chk.Checked)
                {
                    monHoc.Add(chk.Text); 
                }
            }

            return monHoc.ToArray();
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

    }
}

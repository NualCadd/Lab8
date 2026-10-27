using System;
using System.Linq;
using System.Windows.Forms;

namespace QLHocSinh
{
    public partial class frmQLHocSinh : Form
    {
        private bool loading;
        private string selectedId;
        public frmQLHocSinh()
        {
            InitializeComponent();
            Load += (s, e) => Execute(LoadClasses);
            cboLop.SelectedIndexChanged += (s, e) => { if (!loading) Execute(FillStudents); };
            btnTim.Click += (s, e) => Execute(FillStudents);
            btnThem.Click += (s, e) => Execute(() => SaveStudent(true));
            btnSua.Click += (s, e) => Execute(() => SaveStudent(false));
            btnXoa.Click += (s, e) => Execute(DeleteStudent);
            btnMoi.Click += (s, e) => { ClearEditor(); txtTim.Clear(); Execute(FillStudents); };
            btnTaiLai.Click += (s, e) => Execute(LoadClasses);
            dgvHocSinh.SelectionChanged += (s, e) => SelectStudent();
            txtTim.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { Execute(FillStudents); e.SuppressKeyPress = true; } };
        }

        private void Execute(Action action)
        {
            try { action(); }
            catch (Exception ex)
            {
                lblTrangThai.Text = "Thao tác chưa hoàn tất.";
                MessageBox.Show(this, "Không thể thực hiện thao tác. Kiểm tra kết nối SQL Server và chạy Database/QLHS.sql.\n\n" + ex.Message,
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadClasses()
        {
            loading = true;
            try
            {
                using (var db = new QLHSDataContext())
                {
                    cboLop.DisplayMember = "TenLop";
                    cboLop.ValueMember = "MaLop";
                    cboLop.DataSource = db.Lops.OrderBy(x => x.MaLop).ToList();
                }
            }
            finally { loading = false; }
            FillStudents();
        }

        private void FillStudents()
        {
            if (cboLop.SelectedValue == null) { dgvHocSinh.DataSource = null; return; }
            string classId = cboLop.SelectedValue.ToString();
            using (var db = new QLHSDataContext())
            {
                var rows = (from hs in db.HocSinhs
                            join lop in db.Lops on hs.MaLop equals lop.MaLop
                            where hs.MaLop == classId && hs.TenHS.Contains(txtTim.Text.Trim())
                            orderby hs.MaHS
                            select new { hs.MaHS, hs.TenHS, hs.NgaySinh, hs.DiaChi, lop.TenLop, hs.DTB }).ToList();
                dgvHocSinh.DataSource = rows;
                lblTrangThai.Text = "Số học sinh: " + rows.Count + (rows.Count > 0 ? "   |   Điểm trung bình: " + rows.Average(x => x.DTB).ToString("0.00") : "");
            }
            ClearEditor();
            dgvHocSinh.ClearSelection();
        }

        private void SelectStudent()
        {
            if (dgvHocSinh.SelectedRows.Count == 0) return;
            var row = dgvHocSinh.SelectedRows[0];
            selectedId = Convert.ToString(row.Cells["MaHS"].Value);
            txtMaHS.Text = selectedId;
            txtMaHS.ReadOnly = true;
            txtTenHS.Text = Convert.ToString(row.Cells["TenHS"].Value);
            txtDiaChi.Text = Convert.ToString(row.Cells["DiaChi"].Value);
            dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
            nudDiem.Value = (decimal)Convert.ToDouble(row.Cells["DTB"].Value);
        }

        private void ClearEditor()
        {
            selectedId = null;
            txtMaHS.ReadOnly = false;
            txtMaHS.Clear(); txtTenHS.Clear(); txtDiaChi.Clear();
            dtpNgaySinh.Value = DateTime.Today.AddYears(-16);
            nudDiem.Value = 0;
        }

        private void SaveStudent(bool add)
        {
            if (cboLop.SelectedValue == null) throw new InvalidOperationException("Chưa có lớp học.");
            if (txtMaHS.Text.Trim().Length == 0 || txtTenHS.Text.Trim().Length == 0)
            { MessageBox.Show(this, "Vui lòng nhập mã và tên học sinh."); return; }
            if (dtpNgaySinh.Value.Date > DateTime.Today)
            { MessageBox.Show(this, "Ngày sinh không được lớn hơn ngày hiện tại."); return; }
            using (var db = new QLHSDataContext())
            {
                HocSinh hs;
                if (add)
                {
                    if (db.HocSinhs.Any(x => x.MaHS == txtMaHS.Text.Trim()))
                    { MessageBox.Show(this, "Mã học sinh đã tồn tại. Chọn Làm mới để nhập học sinh khác."); return; }
                    hs = new HocSinh { MaHS = txtMaHS.Text.Trim() };
                    db.HocSinhs.InsertOnSubmit(hs);
                }
                else
                {
                    if (selectedId == null) { MessageBox.Show(this, "Chọn học sinh trong bảng để sửa."); return; }
                    hs = db.HocSinhs.SingleOrDefault(x => x.MaHS == selectedId);
                    if (hs == null) throw new InvalidOperationException("Học sinh không còn tồn tại. Hãy tải lại.");
                }
                hs.TenHS = txtTenHS.Text.Trim(); hs.DiaChi = txtDiaChi.Text.Trim();
                hs.NgaySinh = dtpNgaySinh.Value.Date; hs.DTB = (float)nudDiem.Value;
                hs.MaLop = cboLop.SelectedValue.ToString();
                db.SubmitChanges();
            }
            txtTim.Clear();
            FillStudents();
        }

        private void DeleteStudent()
        {
            if (selectedId == null) { MessageBox.Show(this, "Chọn học sinh trong bảng để xóa."); return; }
            if (MessageBox.Show(this, "Xóa học sinh " + selectedId + "?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            using (var db = new QLHSDataContext())
            {
                var hs = db.HocSinhs.SingleOrDefault(x => x.MaHS == selectedId);
                if (hs != null) { db.HocSinhs.DeleteOnSubmit(hs); db.SubmitChanges(); }
            }
            FillStudents();
        }
    }
}


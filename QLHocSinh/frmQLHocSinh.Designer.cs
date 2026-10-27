using System.Drawing;
using System.Windows.Forms;

namespace QLHocSinh
{
    partial class frmQLHocSinh
    {
        private ComboBox cboLop;
        private TextBox txtMaHS, txtTenHS, txtDiaChi, txtTim;
        private DateTimePicker dtpNgaySinh;
        private NumericUpDown nudDiem;
        private DataGridView dgvHocSinh;
        private Button btnTim, btnThem, btnSua, btnXoa, btnMoi, btnTaiLai;
        private Label lblTrangThai;

        private void InitializeComponent()
        {
            SuspendLayout();
            Text = "LAB 08 • Quản lý học sinh — LINQ to SQL";
            ClientSize = new Size(1000, 680);
            MinimumSize = new Size(1016, 719);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.FromArgb(245, 247, 251);
            AutoScaleMode = AutoScaleMode.Font;
            var title = new Label { Text = "QUẢN LÝ HỌC SINH", Location = new Point(24, 18), AutoSize = true, Font = new Font("Segoe UI", 20F, FontStyle.Bold), ForeColor = Color.FromArgb(25, 55, 105) };
            Controls.Add(title);
            Controls.Add(new Label { Text = "Lớp học", Location = new Point(26, 83), AutoSize = true });
            cboLop = new ComboBox { Name = "cboLop", Location = new Point(118, 79), Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
            Controls.Add(cboLop);
            Controls.Add(new Label { Text = "Tìm theo tên", Location = new Point(368, 83), AutoSize = true });
            txtTim = new TextBox { Location = new Point(475, 79), Width = 250 };
            Controls.Add(txtTim);
            btnTim = MakeButton("Tìm kiếm", 740, 76, 108);
            btnTaiLai = MakeButton("Tải lại", 860, 76, 108);
            var editor = new GroupBox { Text = "Thông tin học sinh", Location = new Point(24, 128), Size = new Size(944, 186), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            Controls.Add(editor);
            txtMaHS = new TextBox { MaxLength = 20 };
            txtTenHS = new TextBox { MaxLength = 100 };
            txtDiaChi = new TextBox { MaxLength = 250 };
            dtpNgaySinh = new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy", Value = System.DateTime.Today.AddYears(-16) };
            nudDiem = new NumericUpDown { Minimum = 0, Maximum = 10, DecimalPlaces = 2, Increment = 0.1M };
            AddField(editor, "Mã học sinh", txtMaHS, 20, 35, 300);
            AddField(editor, "Họ và tên", txtTenHS, 474, 35, 300);
            AddField(editor, "Ngày sinh", dtpNgaySinh, 20, 82, 300);
            AddField(editor, "Điểm TB", nudDiem, 474, 82, 300);
            AddField(editor, "Địa chỉ", txtDiaChi, 20, 129, 754);
            btnThem = MakeButton("Thêm", 24, 330, 110);
            btnSua = MakeButton("Sửa", 146, 330, 110);
            btnXoa = MakeButton("Xóa", 268, 330, 110);
            btnMoi = MakeButton("Làm mới", 390, 330, 110);
            dgvHocSinh = new DataGridView { Name = "dgvHocSinh", Location = new Point(24, 382), Size = new Size(944, 236), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false, MultiSelect = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoGenerateColumns = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, RowHeadersVisible = false, BackgroundColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            string[] fields = { "MaHS", "TenHS", "NgaySinh", "DiaChi", "TenLop", "DTB" };
            string[] headers = { "Mã HS", "Họ và tên", "Ngày sinh", "Địa chỉ", "Lớp", "Điểm TB" };
            for (int i = 0; i < fields.Length; i++) dgvHocSinh.Columns.Add(new DataGridViewTextBoxColumn { Name = fields[i], DataPropertyName = fields[i], HeaderText = headers[i], FillWeight = i == 1 || i == 3 ? 180 : 90 });
            dgvHocSinh.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvHocSinh.Columns["DTB"].DefaultCellStyle.Format = "0.00";
            dgvHocSinh.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 245, 252);
            Controls.Add(dgvHocSinh);
            lblTrangThai = new Label { Text = "Chọn lớp để xem học sinh.", Location = new Point(24, 635), AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Bottom };
            Controls.Add(lblTrangThai);
            ResumeLayout(false);
            PerformLayout();
        }

        private Button MakeButton(string text, int x, int y, int width)
        {
            var button = new Button { Text = text, Location = new Point(x, y), Size = new Size(width, 36), BackColor = Color.White, FlatStyle = FlatStyle.Flat };
            Controls.Add(button);
            return button;
        }
        private void AddField(Control parent, string text, Control input, int x, int y, int width)
        {
            parent.Controls.Add(new Label { Text = text, Location = new Point(x, y + 4), AutoSize = true });
            input.Location = new Point(x + 120, y);
            input.Width = width;
            parent.Controls.Add(input);
        }
    }
}

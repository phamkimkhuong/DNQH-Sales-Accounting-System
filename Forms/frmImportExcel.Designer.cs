namespace DNQH_KeToanBanHang.Forms
{
    partial class frmImportExcel
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.grpChonTep = new System.Windows.Forms.GroupBox();
            this.lblLoaiDanhMuc = new System.Windows.Forms.Label();
            this.cboLoaiDanhMuc = new System.Windows.Forms.ComboBox();
            this.btnChonTep = new System.Windows.Forms.Button();
            this.btnTaiMau = new System.Windows.Forms.Button();
            this.lblDuongDanTep = new System.Windows.Forms.Label();
            this.pnlThongKe = new System.Windows.Forms.Panel();
            this.lblTongSo = new System.Windows.Forms.Label();
            this.lblHopLe = new System.Windows.Forms.Label();
            this.lblCoLoi = new System.Windows.Forms.Label();
            this.lblLoc = new System.Windows.Forms.Label();
            this.cboLoc = new System.Windows.Forms.ComboBox();
            this.dgvPreview = new System.Windows.Forms.DataGridView();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.chkBoQuaLoi = new System.Windows.Forms.CheckBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.grpChonTep.SuspendLayout();
            this.pnlThongKe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreview)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(390, 31);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📥 NHẬP DỮ LIỆU TỪ EXCEL / CSV";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtitle.Location = new System.Drawing.Point(2, 34);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(460, 20);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Xem trước, kiểm tra tính hợp lệ và nhập dữ liệu hàng loạt vào hệ thống";
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(16, 14);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(952, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // grpChonTep
            // 
            this.grpChonTep.Controls.Add(this.lblLoaiDanhMuc);
            this.grpChonTep.Controls.Add(this.cboLoaiDanhMuc);
            this.grpChonTep.Controls.Add(this.btnChonTep);
            this.grpChonTep.Controls.Add(this.btnTaiMau);
            this.grpChonTep.Controls.Add(this.lblDuongDanTep);
            this.grpChonTep.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpChonTep.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpChonTep.Location = new System.Drawing.Point(16, 74);
            this.grpChonTep.Name = "grpChonTep";
            this.grpChonTep.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.grpChonTep.Size = new System.Drawing.Size(952, 100);
            this.grpChonTep.TabIndex = 1;
            this.grpChonTep.TabStop = false;
            this.grpChonTep.Text = "1. Nguồn Dữ Liệu";
            // 
            // lblLoaiDanhMuc
            // 
            this.lblLoaiDanhMuc.AutoSize = true;
            this.lblLoaiDanhMuc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLoaiDanhMuc.Location = new System.Drawing.Point(14, 28);
            this.lblLoaiDanhMuc.Name = "lblLoaiDanhMuc";
            this.lblLoaiDanhMuc.Size = new System.Drawing.Size(107, 20);
            this.lblLoaiDanhMuc.TabIndex = 0;
            this.lblLoaiDanhMuc.Text = "Loại danh mục:";
            // 
            // cboLoaiDanhMuc
            // 
            this.cboLoaiDanhMuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiDanhMuc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboLoaiDanhMuc.FormattingEnabled = true;
            this.cboLoaiDanhMuc.Location = new System.Drawing.Point(127, 25);
            this.cboLoaiDanhMuc.Name = "cboLoaiDanhMuc";
            this.cboLoaiDanhMuc.Size = new System.Drawing.Size(180, 28);
            this.cboLoaiDanhMuc.TabIndex = 1;
            this.cboLoaiDanhMuc.SelectedIndexChanged += new System.EventHandler(this.cboLoaiDanhMuc_SelectedIndexChanged);
            // 
            // btnChonTep
            // 
            this.btnChonTep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.btnChonTep.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChonTep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChonTep.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnChonTep.ForeColor = System.Drawing.Color.White;
            this.btnChonTep.Location = new System.Drawing.Point(322, 23);
            this.btnChonTep.Name = "btnChonTep";
            this.btnChonTep.Size = new System.Drawing.Size(130, 32);
            this.btnChonTep.TabIndex = 2;
            this.btnChonTep.Text = "📂 Chọn tệp...";
            this.btnChonTep.UseVisualStyleBackColor = false;
            this.btnChonTep.Click += new System.EventHandler(this.btnChonTep_Click);
            // 
            // btnTaiMau
            // 
            this.btnTaiMau.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.btnTaiMau.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTaiMau.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaiMau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTaiMau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.btnTaiMau.Location = new System.Drawing.Point(462, 23);
            this.btnTaiMau.Name = "btnTaiMau";
            this.btnTaiMau.Size = new System.Drawing.Size(140, 32);
            this.btnTaiMau.TabIndex = 3;
            this.btnTaiMau.Text = "📥 Tải tệp mẫu...";
            this.btnTaiMau.UseVisualStyleBackColor = false;
            this.btnTaiMau.Click += new System.EventHandler(this.btnTaiMau_Click);
            // 
            // lblDuongDanTep
            // 
            this.lblDuongDanTep.AutoEllipsis = true;
            this.lblDuongDanTep.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblDuongDanTep.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblDuongDanTep.Location = new System.Drawing.Point(14, 65);
            this.lblDuongDanTep.Name = "lblDuongDanTep";
            this.lblDuongDanTep.Size = new System.Drawing.Size(920, 22);
            this.lblDuongDanTep.TabIndex = 4;
            this.lblDuongDanTep.Text = "Chưa có tệp nào được chọn. Hãy nhấn \"Chọn tệp...\" để nạp tệp Excel (.xlsx, .xls) " +
    "hoặc CSV.";
            // 
            // pnlThongKe
            // 
            this.pnlThongKe.Controls.Add(this.lblTongSo);
            this.pnlThongKe.Controls.Add(this.lblHopLe);
            this.pnlThongKe.Controls.Add(this.lblCoLoi);
            this.pnlThongKe.Controls.Add(this.lblLoc);
            this.pnlThongKe.Controls.Add(this.cboLoc);
            this.pnlThongKe.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlThongKe.Location = new System.Drawing.Point(16, 174);
            this.pnlThongKe.Name = "pnlThongKe";
            this.pnlThongKe.Padding = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.pnlThongKe.Size = new System.Drawing.Size(952, 42);
            this.pnlThongKe.TabIndex = 2;
            // 
            // lblTongSo
            // 
            this.lblTongSo.AutoSize = true;
            this.lblTongSo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTongSo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblTongSo.Location = new System.Drawing.Point(2, 10);
            this.lblTongSo.Name = "lblTongSo";
            this.lblTongSo.Size = new System.Drawing.Size(125, 21);
            this.lblTongSo.TabIndex = 0;
            this.lblTongSo.Text = "📊 Tổng cộng: 0";
            // 
            // lblHopLe
            // 
            this.lblHopLe.AutoSize = true;
            this.lblHopLe.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblHopLe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.lblHopLe.Location = new System.Drawing.Point(160, 10);
            this.lblHopLe.Name = "lblHopLe";
            this.lblHopLe.Size = new System.Drawing.Size(117, 21);
            this.lblHopLe.TabIndex = 1;
            this.lblHopLe.Text = "✔ Hợp lệ: 0 (0%)";
            // 
            // lblCoLoi
            // 
            this.lblCoLoi.AutoSize = true;
            this.lblCoLoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblCoLoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.lblCoLoi.Location = new System.Drawing.Point(320, 10);
            this.lblCoLoi.Name = "lblCoLoi";
            this.lblCoLoi.Size = new System.Drawing.Size(107, 21);
            this.lblCoLoi.TabIndex = 2;
            this.lblCoLoi.Text = "✖ Có lỗi: 0 (0%)";
            // 
            // lblLoc
            // 
            this.lblLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLoc.AutoSize = true;
            this.lblLoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLoc.Location = new System.Drawing.Point(645, 11);
            this.lblLoc.Name = "lblLoc";
            this.lblLoc.Size = new System.Drawing.Size(95, 20);
            this.lblLoc.TabIndex = 3;
            this.lblLoc.Text = "Bộ lọc hiển thị:";
            // 
            // cboLoc
            // 
            this.cboLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboLoc.FormattingEnabled = true;
            this.cboLoc.Items.AddRange(new object[] {
            "Tất cả các dòng",
            "Chỉ các dòng hợp lệ",
            "Chỉ các dòng có lỗi"});
            this.cboLoc.Location = new System.Drawing.Point(746, 7);
            this.cboLoc.Name = "cboLoc";
            this.cboLoc.Size = new System.Drawing.Size(200, 28);
            this.cboLoc.TabIndex = 4;
            this.cboLoc.SelectedIndexChanged += new System.EventHandler(this.cboLoc_SelectedIndexChanged);
            // 
            // dgvPreview
            // 
            this.dgvPreview.AllowUserToAddRows = false;
            this.dgvPreview.AllowUserToDeleteRows = false;
            this.dgvPreview.BackgroundColor = System.Drawing.Color.White;
            this.dgvPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPreview.Location = new System.Drawing.Point(16, 216);
            this.dgvPreview.Name = "dgvPreview";
            this.dgvPreview.ReadOnly = true;
            this.dgvPreview.RowHeadersWidth = 35;
            this.dgvPreview.RowTemplate.Height = 26;
            this.dgvPreview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPreview.Size = new System.Drawing.Size(952, 340);
            this.dgvPreview.TabIndex = 3;
            this.dgvPreview.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPreview_CellFormatting);
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.chkBoQuaLoi);
            this.pnlFooter.Controls.Add(this.lblStatus);
            this.pnlFooter.Controls.Add(this.btnXacNhan);
            this.pnlFooter.Controls.Add(this.btnDong);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(16, 556);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.pnlFooter.Size = new System.Drawing.Size(952, 54);
            this.pnlFooter.TabIndex = 4;
            // 
            // chkBoQuaLoi
            // 
            this.chkBoQuaLoi.AutoSize = true;
            this.chkBoQuaLoi.Checked = true;
            this.chkBoQuaLoi.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoQuaLoi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkBoQuaLoi.Location = new System.Drawing.Point(6, 16);
            this.chkBoQuaLoi.Name = "chkBoQuaLoi";
            this.chkBoQuaLoi.Size = new System.Drawing.Size(325, 24);
            this.chkBoQuaLoi.TabIndex = 0;
            this.chkBoQuaLoi.Text = "Chỉ nhập các dòng hợp lệ (bỏ qua dòng có lỗi)";
            this.chkBoQuaLoi.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoEllipsis = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblStatus.Location = new System.Drawing.Point(345, 17);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(340, 22);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Sẵn sàng.";
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXacNhan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.btnXacNhan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXacNhan.Enabled = false;
            this.btnXacNhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXacNhan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.Location = new System.Drawing.Point(695, 10);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(160, 36);
            this.btnXacNhan.TabIndex = 2;
            this.btnXacNhan.Text = "🚀 Xác Nhận Nhập";
            this.btnXacNhan.UseVisualStyleBackColor = false;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.btnDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDong.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.btnDong.Location = new System.Drawing.Point(865, 10);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(85, 36);
            this.btnDong.TabIndex = 3;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // frmImportExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.CancelButton = this.btnDong;
            this.ClientSize = new System.Drawing.Size(984, 624);
            this.Controls.Add(this.dgvPreview);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlThongKe);
            this.Controls.Add(this.grpChonTep);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(880, 560);
            this.Name = "frmImportExcel";
            this.Padding = new System.Windows.Forms.Padding(16, 14, 16, 14);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nhập Dữ Liệu Hàng Loạt từ Excel / CSV";
            this.Load += new System.EventHandler(this.frmImportExcel_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.grpChonTep.ResumeLayout(false);
            this.grpChonTep.PerformLayout();
            this.pnlThongKe.ResumeLayout(false);
            this.pnlThongKe.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreview)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.GroupBox grpChonTep;
        private System.Windows.Forms.Label lblLoaiDanhMuc;
        private System.Windows.Forms.ComboBox cboLoaiDanhMuc;
        private System.Windows.Forms.Button btnChonTep;
        private System.Windows.Forms.Button btnTaiMau;
        private System.Windows.Forms.Label lblDuongDanTep;
        private System.Windows.Forms.Panel pnlThongKe;
        private System.Windows.Forms.Label lblTongSo;
        private System.Windows.Forms.Label lblHopLe;
        private System.Windows.Forms.Label lblCoLoi;
        private System.Windows.Forms.Label lblLoc;
        private System.Windows.Forms.ComboBox cboLoc;
        private System.Windows.Forms.DataGridView dgvPreview;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.CheckBox chkBoQuaLoi;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Button btnDong;
    }
}

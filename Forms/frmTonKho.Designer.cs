namespace DNQH_KeToanBanHang.Forms
{
    partial class frmTonKho
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.cboKho = new System.Windows.Forms.ComboBox();
            this.lblKho = new System.Windows.Forms.Label();
            this.dgvTonKho = new System.Windows.Forms.DataGridView();
            this.colMaKho = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenKho = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonViTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGiaBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongTon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayCapNhat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblThongKe = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.pnlHeader.Controls.Add(this.lblSubTitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1000, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(213)))), ((int)(((byte)(255)))));
            this.lblSubTitle.Location = new System.Drawing.Point(20, 34);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(437, 15);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Kiểm tra tình trạng hàng hóa, tồn kho khả dụng phục vụ bán hàng và quản lý kho";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(18, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(271, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TRA CỨU & THEO DÕI TỒN KHO";
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlFilter.Controls.Add(this.btnDong);
            this.pnlFilter.Controls.Add(this.btnLamMoi);
            this.pnlFilter.Controls.Add(this.btnTimKiem);
            this.pnlFilter.Controls.Add(this.txtTimKiem);
            this.pnlFilter.Controls.Add(this.lblTimKiem);
            this.pnlFilter.Controls.Add(this.cboKho);
            this.pnlFilter.Controls.Add(this.lblKho);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 60);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1000, 55);
            this.pnlFilter.TabIndex = 1;
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(895, 12);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(90, 30);
            this.btnDong.TabIndex = 6;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(710, 12);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(85, 30);
            this.btnLamMoi.TabIndex = 5;
            this.btnLamMoi.Text = "Làm Mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(615, 12);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(85, 30);
            this.btnTimKiem.TabIndex = 4;
            this.btnTimKiem.Text = "Tìm Kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTimKiem.Location = new System.Drawing.Point(380, 15);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(220, 24);
            this.txtTimKiem.TabIndex = 3;
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTimKiem.Location = new System.Drawing.Point(315, 18);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(60, 17);
            this.lblTimKiem.TabIndex = 2;
            this.lblTimKiem.Text = "Từ khóa:";
            // 
            // cboKho
            // 
            this.cboKho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKho.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboKho.FormattingEnabled = true;
            this.cboKho.Location = new System.Drawing.Point(85, 15);
            this.cboKho.Name = "cboKho";
            this.cboKho.Size = new System.Drawing.Size(210, 24);
            this.cboKho.TabIndex = 1;
            this.cboKho.SelectedIndexChanged += new System.EventHandler(this.cboKho_SelectedIndexChanged);
            // 
            // lblKho
            // 
            this.lblKho.AutoSize = true;
            this.lblKho.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblKho.Location = new System.Drawing.Point(15, 18);
            this.lblKho.Name = "lblKho";
            this.lblKho.Size = new System.Drawing.Size(65, 17);
            this.lblKho.TabIndex = 0;
            this.lblKho.Text = "Kho hàng:";
            // 
            // dgvTonKho
            // 
            this.dgvTonKho.AllowUserToAddRows = false;
            this.dgvTonKho.AllowUserToDeleteRows = false;
            this.dgvTonKho.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTonKho.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTonKho.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTonKho.ColumnHeadersHeight = 32;
            this.dgvTonKho.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaKho,
            this.colTenKho,
            this.colMaSP,
            this.colTenSP,
            this.colDonViTinh,
            this.colDonGiaBan,
            this.colSoLuongTon,
            this.colNgayCapNhat});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(235)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTonKho.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTonKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTonKho.EnableHeadersVisualStyles = false;
            this.dgvTonKho.Location = new System.Drawing.Point(0, 115);
            this.dgvTonKho.MultiSelect = false;
            this.dgvTonKho.Name = "dgvTonKho";
            this.dgvTonKho.ReadOnly = true;
            this.dgvTonKho.RowHeadersVisible = false;
            this.dgvTonKho.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTonKho.Size = new System.Drawing.Size(1000, 480);
            this.dgvTonKho.TabIndex = 2;
            // 
            // colMaKho
            // 
            this.colMaKho.DataPropertyName = "MaKho";
            this.colMaKho.FillWeight = 50F;
            this.colMaKho.HeaderText = "Mã Kho";
            this.colMaKho.Name = "colMaKho";
            this.colMaKho.ReadOnly = true;
            // 
            // colTenKho
            // 
            this.colTenKho.DataPropertyName = "TenKho";
            this.colTenKho.FillWeight = 85F;
            this.colTenKho.HeaderText = "Tên Kho";
            this.colTenKho.Name = "colTenKho";
            this.colTenKho.ReadOnly = true;
            // 
            // colMaSP
            // 
            this.colMaSP.DataPropertyName = "MaSP";
            this.colMaSP.FillWeight = 60F;
            this.colMaSP.HeaderText = "Mã SP";
            this.colMaSP.Name = "colMaSP";
            this.colMaSP.ReadOnly = true;
            // 
            // colTenSP
            // 
            this.colTenSP.DataPropertyName = "TenSP";
            this.colTenSP.FillWeight = 130F;
            this.colTenSP.HeaderText = "Tên Sản Phẩm";
            this.colTenSP.Name = "colTenSP";
            this.colTenSP.ReadOnly = true;
            // 
            // colDonViTinh
            // 
            this.colDonViTinh.DataPropertyName = "DonViTinh";
            this.colDonViTinh.FillWeight = 45F;
            this.colDonViTinh.HeaderText = "ĐVT";
            this.colDonViTinh.Name = "colDonViTinh";
            this.colDonViTinh.ReadOnly = true;
            // 
            // colDonGiaBan
            // 
            this.colDonGiaBan.DataPropertyName = "DonGiaBan";
            this.colDonGiaBan.FillWeight = 70F;
            this.colDonGiaBan.HeaderText = "Đơn Giá Bán";
            this.colDonGiaBan.Name = "colDonGiaBan";
            this.colDonGiaBan.ReadOnly = true;
            // 
            // colSoLuongTon
            // 
            this.colSoLuongTon.DataPropertyName = "SoLuongTon";
            this.colSoLuongTon.FillWeight = 60F;
            this.colSoLuongTon.HeaderText = "Số Lượng Tồn";
            this.colSoLuongTon.Name = "colSoLuongTon";
            this.colSoLuongTon.ReadOnly = true;
            // 
            // colNgayCapNhat
            // 
            this.colNgayCapNhat.DataPropertyName = "NgayCapNhat";
            this.colNgayCapNhat.FillWeight = 80F;
            this.colNgayCapNhat.HeaderText = "Ngày Cập Nhật";
            this.colNgayCapNhat.Name = "colNgayCapNhat";
            this.colNgayCapNhat.ReadOnly = true;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.pnlFooter.Controls.Add(this.lblThongKe);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 595);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1000, 35);
            this.pnlFooter.TabIndex = 3;
            // 
            // lblThongKe
            // 
            this.lblThongKe.AutoSize = true;
            this.lblThongKe.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblThongKe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.lblThongKe.Location = new System.Drawing.Point(15, 9);
            this.lblThongKe.Name = "lblThongKe";
            this.lblThongKe.Size = new System.Drawing.Size(174, 17);
            this.lblThongKe.TabIndex = 0;
            this.lblThongKe.Text = "Tổng số mặt hàng tồn kho: 0";
            // 
            // frmTonKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 630);
            this.Controls.Add(this.dgvTonKho);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmTonKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tra Cứu Tồn Kho";
            this.Load += new System.EventHandler(this.frmTonKho_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.ComboBox cboKho;
        private System.Windows.Forms.Label lblKho;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.DataGridView dgvTonKho;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblThongKe;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaKho;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenKho;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonViTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGiaBan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongTon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayCapNhat;
    }
}

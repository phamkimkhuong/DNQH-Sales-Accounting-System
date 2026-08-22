namespace DNQH_KeToanBanHang.Forms
{
    partial class frmHotkeysHelp
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.picHeaderIcon = new System.Windows.Forms.PictureBox();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.picTipIcon = new System.Windows.Forms.PictureBox();
            this.lblTip = new System.Windows.Forms.Label();
            this.btnDong = new System.Windows.Forms.Button();
            this.tcCategories = new System.Windows.Forms.TabControl();
            this.tpChung = new System.Windows.Forms.TabPage();
            this.dgvChung = new System.Windows.Forms.DataGridView();
            this.colKeyChung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActionChung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colScopeChung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpDieuHuong = new System.Windows.Forms.TabPage();
            this.dgvDieuHuong = new System.Windows.Forms.DataGridView();
            this.colKeyDH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActionDH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colScopeDH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpBanPhim = new System.Windows.Forms.TabPage();
            this.dgvBanPhim = new System.Windows.Forms.DataGridView();
            this.colKeyBP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActionBP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescBP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeaderIcon)).BeginInit();
            this.pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTipIcon)).BeginInit();
            this.tcCategories.SuspendLayout();
            this.tpChung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChung)).BeginInit();
            this.tpDieuHuong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDieuHuong)).BeginInit();
            this.tpBanPhim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBanPhim)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.pnlHeader.Controls.Add(this.picHeaderIcon);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.pnlHeader.Size = new System.Drawing.Size(764, 68);
            this.pnlHeader.TabIndex = 0;
            // 
            // picHeaderIcon
            // 
            this.picHeaderIcon.BackColor = System.Drawing.Color.Transparent;
            this.picHeaderIcon.Location = new System.Drawing.Point(16, 12);
            this.picHeaderIcon.Name = "picHeaderIcon";
            this.picHeaderIcon.Size = new System.Drawing.Size(28, 28);
            this.picHeaderIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picHeaderIcon.TabIndex = 2;
            this.picHeaderIcon.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(48, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(372, 23);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "HỆ THỐNG PHÍM TẮT TOÀN CỤC KẾ TOÁN";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.lblSubtitle.Location = new System.Drawing.Point(50, 38);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(462, 15);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Chuẩn hóa thao tác gõ phím tốc độ cao, hỗ trợ phím Enter chuyển ô và phím chức năng F1-F5";
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlFooter.Controls.Add(this.picTipIcon);
            this.pnlFooter.Controls.Add(this.lblTip);
            this.pnlFooter.Controls.Add(this.btnDong);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 513);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            this.pnlFooter.Size = new System.Drawing.Size(764, 52);
            this.pnlFooter.TabIndex = 1;
            // 
            // picTipIcon
            // 
            this.picTipIcon.BackColor = System.Drawing.Color.Transparent;
            this.picTipIcon.Location = new System.Drawing.Point(16, 16);
            this.picTipIcon.Name = "picTipIcon";
            this.picTipIcon.Size = new System.Drawing.Size(20, 20);
            this.picTipIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picTipIcon.TabIndex = 2;
            this.picTipIcon.TabStop = false;
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTip.Location = new System.Drawing.Point(40, 18);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(465, 15);
            this.lblTip.TabIndex = 1;
            this.lblTip.Text = "Mẹo sử dụng: Bạn có thể nhấn phím F1 bất kỳ lúc nào để mở lại bảng tra cứu này.";
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.btnDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDong.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(636, 9);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(116, 34);
            this.btnDong.TabIndex = 0;
            this.btnDong.Text = "Đóng (Esc)";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // tcCategories
            // 
            this.tcCategories.Controls.Add(this.tpChung);
            this.tcCategories.Controls.Add(this.tpDieuHuong);
            this.tcCategories.Controls.Add(this.tpBanPhim);
            this.tcCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcCategories.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcCategories.Location = new System.Drawing.Point(0, 68);
            this.tcCategories.Name = "tcCategories";
            this.tcCategories.Padding = new System.Drawing.Point(14, 8);
            this.tcCategories.SelectedIndex = 0;
            this.tcCategories.Size = new System.Drawing.Size(764, 445);
            this.tcCategories.TabIndex = 2;
            // 
            // tpChung
            // 
            this.tpChung.Controls.Add(this.dgvChung);
            this.tpChung.Location = new System.Drawing.Point(4, 34);
            this.tpChung.Name = "tpChung";
            this.tpChung.Padding = new System.Windows.Forms.Padding(8);
            this.tpChung.Size = new System.Drawing.Size(756, 407);
            this.tpChung.TabIndex = 0;
            this.tpChung.Text = "Thao tác Dữ liệu & Chứng từ";
            this.tpChung.UseVisualStyleBackColor = true;
            // 
            // dgvChung
            // 
            this.dgvChung.AllowUserToAddRows = false;
            this.dgvChung.AllowUserToDeleteRows = false;
            this.dgvChung.AllowUserToResizeRows = false;
            this.dgvChung.BackgroundColor = System.Drawing.Color.White;
            this.dgvChung.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvChung.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChung.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colKeyChung,
            this.colActionChung,
            this.colScopeChung});
            this.dgvChung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChung.Location = new System.Drawing.Point(8, 8);
            this.dgvChung.Name = "dgvChung";
            this.dgvChung.ReadOnly = true;
            this.dgvChung.RowHeadersVisible = false;
            this.dgvChung.RowTemplate.Height = 32;
            this.dgvChung.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChung.Size = new System.Drawing.Size(740, 391);
            this.dgvChung.TabIndex = 0;
            // 
            // colKeyChung
            // 
            this.colKeyChung.HeaderText = "Phím tắt";
            this.colKeyChung.Name = "colKeyChung";
            this.colKeyChung.ReadOnly = true;
            this.colKeyChung.Width = 140;
            // 
            // colActionChung
            // 
            this.colActionChung.HeaderText = "Hành động thực thi";
            this.colActionChung.Name = "colActionChung";
            this.colActionChung.ReadOnly = true;
            this.colActionChung.Width = 320;
            // 
            // colScopeChung
            // 
            this.colScopeChung.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colScopeChung.HeaderText = "Phạm vi áp dụng";
            this.colScopeChung.Name = "colScopeChung";
            this.colScopeChung.ReadOnly = true;
            // 
            // tpDieuHuong
            // 
            this.tpDieuHuong.Controls.Add(this.dgvDieuHuong);
            this.tpDieuHuong.Location = new System.Drawing.Point(4, 34);
            this.tpDieuHuong.Name = "tpDieuHuong";
            this.tpDieuHuong.Padding = new System.Windows.Forms.Padding(8);
            this.tpDieuHuong.Size = new System.Drawing.Size(756, 407);
            this.tpDieuHuong.TabIndex = 1;
            this.tpDieuHuong.Text = "Mở Nhanh từ Màn Hình Chính";
            this.tpDieuHuong.UseVisualStyleBackColor = true;
            // 
            // dgvDieuHuong
            // 
            this.dgvDieuHuong.AllowUserToAddRows = false;
            this.dgvDieuHuong.AllowUserToDeleteRows = false;
            this.dgvDieuHuong.AllowUserToResizeRows = false;
            this.dgvDieuHuong.BackgroundColor = System.Drawing.Color.White;
            this.dgvDieuHuong.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDieuHuong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDieuHuong.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colKeyDH,
            this.colActionDH,
            this.colScopeDH});
            this.dgvDieuHuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDieuHuong.Location = new System.Drawing.Point(8, 8);
            this.dgvDieuHuong.Name = "dgvDieuHuong";
            this.dgvDieuHuong.ReadOnly = true;
            this.dgvDieuHuong.RowHeadersVisible = false;
            this.dgvDieuHuong.RowTemplate.Height = 32;
            this.dgvDieuHuong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDieuHuong.Size = new System.Drawing.Size(740, 391);
            this.dgvDieuHuong.TabIndex = 0;
            // 
            // colKeyDH
            // 
            this.colKeyDH.HeaderText = "Tổ hợp phím";
            this.colKeyDH.Name = "colKeyDH";
            this.colKeyDH.ReadOnly = true;
            this.colKeyDH.Width = 140;
            // 
            // colActionDH
            // 
            this.colActionDH.HeaderText = "Phân hệ mở nhanh";
            this.colActionDH.Name = "colActionDH";
            this.colActionDH.ReadOnly = true;
            this.colActionDH.Width = 320;
            // 
            // colScopeDH
            // 
            this.colScopeDH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colScopeDH.HeaderText = "Mô tả phân hệ";
            this.colScopeDH.Name = "colScopeDH";
            this.colScopeDH.ReadOnly = true;
            // 
            // tpBanPhim
            // 
            this.tpBanPhim.Controls.Add(this.dgvBanPhim);
            this.tpBanPhim.Location = new System.Drawing.Point(4, 34);
            this.tpBanPhim.Name = "tpBanPhim";
            this.tpBanPhim.Padding = new System.Windows.Forms.Padding(8);
            this.tpBanPhim.Size = new System.Drawing.Size(756, 407);
            this.tpBanPhim.TabIndex = 2;
            this.tpBanPhim.Text = "Kỹ Thuật Gõ Phím Kế Toán";
            this.tpBanPhim.UseVisualStyleBackColor = true;
            // 
            // dgvBanPhim
            // 
            this.dgvBanPhim.AllowUserToAddRows = false;
            this.dgvBanPhim.AllowUserToDeleteRows = false;
            this.dgvBanPhim.AllowUserToResizeRows = false;
            this.dgvBanPhim.BackgroundColor = System.Drawing.Color.White;
            this.dgvBanPhim.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBanPhim.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBanPhim.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colKeyBP,
            this.colActionBP,
            this.colDescBP});
            this.dgvBanPhim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBanPhim.Location = new System.Drawing.Point(8, 8);
            this.dgvBanPhim.Name = "dgvBanPhim";
            this.dgvBanPhim.ReadOnly = true;
            this.dgvBanPhim.RowHeadersVisible = false;
            this.dgvBanPhim.RowTemplate.Height = 32;
            this.dgvBanPhim.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBanPhim.Size = new System.Drawing.Size(740, 391);
            this.dgvBanPhim.TabIndex = 0;
            // 
            // colKeyBP
            // 
            this.colKeyBP.HeaderText = "Phím bấm";
            this.colKeyBP.Name = "colKeyBP";
            this.colKeyBP.ReadOnly = true;
            this.colKeyBP.Width = 140;
            // 
            // colActionBP
            // 
            this.colActionBP.HeaderText = "Quy chuẩn kế toán";
            this.colActionBP.Name = "colActionBP";
            this.colActionBP.ReadOnly = true;
            this.colActionBP.Width = 260;
            // 
            // colDescBP
            // 
            this.colDescBP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDescBP.HeaderText = "Chi tiết hành vi";
            this.colDescBP.Name = "colDescBP";
            this.colDescBP.ReadOnly = true;
            // 
            // frmHotkeysHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnDong;
            this.ClientSize = new System.Drawing.Size(764, 565);
            this.Controls.Add(this.tcCategories);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHotkeysHelp";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bảng Tra Cứu Phím Tắt Toàn Cục - DNQH";
            this.Load += new System.EventHandler(this.frmHotkeysHelp_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmHotkeysHelp_KeyDown);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.tcCategories.ResumeLayout(false);
            this.tpChung.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChung)).EndInit();
            this.tpDieuHuong.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDieuHuong)).EndInit();
            this.tpBanPhim.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBanPhim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHeaderIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTipIcon)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.PictureBox picHeaderIcon;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.PictureBox picTipIcon;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.TabControl tcCategories;
        private System.Windows.Forms.TabPage tpChung;
        private System.Windows.Forms.DataGridView dgvChung;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKeyChung;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActionChung;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScopeChung;
        private System.Windows.Forms.TabPage tpDieuHuong;
        private System.Windows.Forms.DataGridView dgvDieuHuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKeyDH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActionDH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScopeDH;
        private System.Windows.Forms.TabPage tpBanPhim;
        private System.Windows.Forms.DataGridView dgvBanPhim;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKeyBP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActionBP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescBP;
    }
}

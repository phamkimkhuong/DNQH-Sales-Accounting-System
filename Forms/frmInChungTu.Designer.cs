namespace DNQH_KeToanBanHang.Forms
{
    partial class frmInChungTu
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.FlowLayoutPanel flpActions;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Button btnXemTruoc;
        private System.Windows.Forms.Button btnLuuFile;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Label lblDocTitle;
        private System.Windows.Forms.WebBrowser webBrowser;

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
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.lblDocTitle = new System.Windows.Forms.Label();
            this.flpActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnIn = new System.Windows.Forms.Button();
            this.btnXemTruoc = new System.Windows.Forms.Button();
            this.btnLuuFile = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.pnlToolbar.SuspendLayout();
            this.flpActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Controls.Add(this.flpActions);
            this.pnlToolbar.Controls.Add(this.lblDocTitle);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 0);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlToolbar.Size = new System.Drawing.Size(950, 52);
            this.pnlToolbar.TabIndex = 0;
            // 
            // lblDocTitle
            // 
            this.lblDocTitle.AutoEllipsis = true;
            this.lblDocTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDocTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblDocTitle.Location = new System.Drawing.Point(12, 8);
            this.lblDocTitle.Name = "lblDocTitle";
            this.lblDocTitle.Size = new System.Drawing.Size(390, 36);
            this.lblDocTitle.TabIndex = 0;
            this.lblDocTitle.Text = "Xem trước mẫu in chứng từ";
            this.lblDocTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flpActions
            // 
            this.flpActions.AutoSize = true;
            this.flpActions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpActions.Controls.Add(this.btnIn);
            this.flpActions.Controls.Add(this.btnXemTruoc);
            this.flpActions.Controls.Add(this.btnLuuFile);
            this.flpActions.Controls.Add(this.btnDong);
            this.flpActions.Dock = System.Windows.Forms.DockStyle.Right;
            this.flpActions.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flpActions.Location = new System.Drawing.Point(408, 8);
            this.flpActions.Name = "flpActions";
            this.flpActions.Size = new System.Drawing.Size(530, 36);
            this.flpActions.TabIndex = 1;
            this.flpActions.WrapContents = false;
            // 
            // btnIn
            // 
            this.btnIn.Location = new System.Drawing.Point(0, 0);
            this.btnIn.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(125, 34);
            this.btnIn.TabIndex = 0;
            this.btnIn.Text = "🖨️ In ra máy in";
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // btnXemTruoc
            // 
            this.btnXemTruoc.Location = new System.Drawing.Point(131, 0);
            this.btnXemTruoc.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnXemTruoc.Name = "btnXemTruoc";
            this.btnXemTruoc.Size = new System.Drawing.Size(145, 34);
            this.btnXemTruoc.TabIndex = 1;
            this.btnXemTruoc.Text = "🔍 Xem trước bản in";
            this.btnXemTruoc.UseVisualStyleBackColor = true;
            this.btnXemTruoc.Click += new System.EventHandler(this.btnXemTruoc_Click);
            // 
            // btnLuuFile
            // 
            this.btnLuuFile.Location = new System.Drawing.Point(282, 0);
            this.btnLuuFile.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnLuuFile.Name = "btnLuuFile";
            this.btnLuuFile.Size = new System.Drawing.Size(165, 34);
            this.btnLuuFile.TabIndex = 2;
            this.btnLuuFile.Text = "💾 Xuất file HTML / PDF";
            this.btnLuuFile.UseVisualStyleBackColor = true;
            this.btnLuuFile.Click += new System.EventHandler(this.btnLuuFile_Click);
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(453, 0);
            this.btnDong.Margin = new System.Windows.Forms.Padding(0);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(75, 34);
            this.btnDong.TabIndex = 3;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 52);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(950, 668);
            this.webBrowser.TabIndex = 1;
            // 
            // frmInChungTu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 720);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.pnlToolbar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(820, 540);
            this.Name = "frmInChungTu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xem trước & In mẫu chứng từ";
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.flpActions.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}

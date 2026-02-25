namespace ScannerTest
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            // 
            // txtIpAddress
            // 
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.dgvScannerData = new System.Windows.Forms.DataGridView();
            this.lblIp = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();

            // 신규 컴포넌트들
            this.lblThreadStatus = new System.Windows.Forms.Label();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.tmrStatusCheck = new System.Windows.Forms.Timer(this.components);

            ((System.ComponentModel.ISupportInitialize)(this.dgvScannerData)).BeginInit();
            this.SuspendLayout();
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Location = new System.Drawing.Point(82, 12);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(125, 21);
            this.txtIpAddress.TabIndex = 0;
            this.txtIpAddress.Text = "192.168.100.1";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(265, 12);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(60, 21);
            this.txtPort.TabIndex = 1;
            this.txtPort.Text = "9004";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(340, 10);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(100, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "연결";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // dgvScannerData
            // 
            this.dgvScannerData.AllowUserToAddRows = false;
            this.dgvScannerData.AllowUserToDeleteRows = false;
            this.dgvScannerData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvScannerData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScannerData.Location = new System.Drawing.Point(12, 45);
            this.dgvScannerData.Name = "dgvScannerData";
            this.dgvScannerData.ReadOnly = true;
            this.dgvScannerData.RowTemplate.Height = 23;
            // 폼 하단에 라벨이 들어갈 공간을 확보하기 위해 높이 조절
            this.dgvScannerData.Size = new System.Drawing.Size(776, 370);
            this.dgvScannerData.TabIndex = 3;
            // 
            // lblIp
            // 
            this.lblIp.AutoSize = true;
            this.lblIp.Location = new System.Drawing.Point(12, 17);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(56, 12);
            this.lblIp.TabIndex = 4;
            this.lblIp.Text = "IP 주소 :";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(221, 17);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(37, 12);
            this.lblPort.TabIndex = 5;
            this.lblPort.Text = "포트 :";
            //
            // lblThreadStatus
            //
            this.lblThreadStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblThreadStatus.AutoSize = true;
            this.lblThreadStatus.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.lblThreadStatus.Location = new System.Drawing.Point(12, 427);
            this.lblThreadStatus.Name = "lblThreadStatus";
            this.lblThreadStatus.Size = new System.Drawing.Size(147, 14);
            this.lblThreadStatus.TabIndex = 6;
            this.lblThreadStatus.Text = "스레드 상태: 중지됨";
            this.lblThreadStatus.ForeColor = System.Drawing.Color.Gray;
            //
            // lblConnectionStatus
            //
            this.lblConnectionStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.lblConnectionStatus.Location = new System.Drawing.Point(220, 427);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(162, 14);
            this.lblConnectionStatus.TabIndex = 7;
            this.lblConnectionStatus.Text = "스캐너 연결: 끊어짐";
            this.lblConnectionStatus.ForeColor = System.Drawing.Color.Red;
            //
            // tmrStatusCheck
            //
            this.tmrStatusCheck.Interval = 1000;
            this.tmrStatusCheck.Tick += new System.EventHandler(this.tmrStatusCheck_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.lblIp);
            this.Controls.Add(this.dgvScannerData);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIpAddress);
            this.Controls.Add(this.lblThreadStatus);
            this.Controls.Add(this.lblConnectionStatus);
            this.Name = "Form1";
            this.Text = "키엔스 스캐너 연동 테스트";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvScannerData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.DataGridView dgvScannerData;
        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblThreadStatus;
        private System.Windows.Forms.Label lblConnectionStatus;
        private System.Windows.Forms.Timer tmrStatusCheck;
    }
}


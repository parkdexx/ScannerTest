using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScannerTest
{
    public partial class Form1 : Form
    {
        private ScannerManager _scannerMgr;
        private int _scanCount = 0;

        public Form1()
        {
            InitializeComponent();
            InitializeDataGridView();
            this.FormClosing += Form1_FormClosing;
        }

        private void InitializeDataGridView()
        {
            dgvScannerData.Columns.Add("No", "No");
            dgvScannerData.Columns.Add("Time", "수신 시간");
            dgvScannerData.Columns.Add("Data", "바코드 데이터");

            dgvScannerData.Columns["No"].Width = 50;
            dgvScannerData.Columns["Time"].Width = 150;
            dgvScannerData.Columns["Data"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "연결")
            {
                // 포트 번호 검증
                if (!int.TryParse(txtPort.Text, out int port))
                {
                    MessageBox.Show("포트 번호가 올바르지 않습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 매니저 초기화 및 이벤트 연결
                _scannerMgr = new ScannerManager(txtIpAddress.Text, port);
                _scannerMgr._scannerEvent += AddDataToGrid;

                // 스레드 시작 및 타이머 가동
                _scannerMgr.StartScannerThread();
                tmrStatusCheck.Start();

                btnConnect.Text = "연결 해제";
            }
            else
            {
                // 스레드 중지 및 이벤트 해제
                if (_scannerMgr != null)
                {
                    _scannerMgr.StopScannerThread();
                    _scannerMgr._scannerEvent -= AddDataToGrid;
                }

                // 타이머 중지 및 라벨 초기화
                tmrStatusCheck.Stop();
                lblThreadStatus.Text = "스레드 상태: 중지됨";
                lblThreadStatus.ForeColor = Color.Gray;
                lblConnectionStatus.Text = "스캐너 연결: 끊어짐";
                lblConnectionStatus.ForeColor = Color.Red;

                btnConnect.Text = "연결";
            }
        }

        private void tmrStatusCheck_Tick(object sender, EventArgs e)
        {
            if (_scannerMgr == null) return;

            // 스레드 상태 라벨 갱신
            if (_scannerMgr.GetScannerThreadStatus())
            {
                lblThreadStatus.Text = "스레드 상태: 동작중";
                lblThreadStatus.ForeColor = Color.Blue;
            }
            else
            {
                lblThreadStatus.Text = "스레드 상태: 중지됨";
                lblThreadStatus.ForeColor = Color.Gray;
            }

            // 스캐너 소켓 연결 상태 라벨 갱신
            if (_scannerMgr.GetScannerConnectionStatus())
            {
                lblConnectionStatus.Text = "스캐너 연결: 연결됨";
                lblConnectionStatus.ForeColor = Color.Green;
            }
            else
            {
                lblConnectionStatus.Text = "스캐너 연결: 끊어짐";
                lblConnectionStatus.ForeColor = Color.Red;
            }
        }

        private void AddDataToGrid(string barcodeData)
        {
            if (dgvScannerData.InvokeRequired)
            {
                dgvScannerData.Invoke(new Action<string>(AddDataToGrid), barcodeData);
                return;
            }

            _scanCount++;
            string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            dgvScannerData.Rows.Add(_scanCount, currentTime, barcodeData);

            // 최신 데이터가 보이도록 스크롤 이동
            dgvScannerData.FirstDisplayedScrollingRowIndex = dgvScannerData.RowCount - 1;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_scannerMgr != null)
            {
                _scannerMgr.StopScannerThread();
            }
        }
    }
}

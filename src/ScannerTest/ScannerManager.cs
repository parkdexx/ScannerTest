using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScannerTest
{
    class ScannerManager
    {
        // 스캐너 클라이언트 지속 연결용 스레드
        private Thread _scannerThread = null;
        private bool _isScannerThreadRun = false;

        // 스캐너 클라이언트
        private string _scannerIpAddress = "";
        private int _scannerPort = 0;
        private TcpClient _scannerClient = null;
        private NetworkStream _scannerStream = null;
        private bool _isScannerClientConnect = false;

        // 데이터 이벤트
        public event Action<string> _scannerEvent;



        public ScannerManager(string ipAddress = "", int port = 0)
        {
            _scannerIpAddress = ipAddress;
            _scannerPort = port;
        }



        public void StartScannerThread()
        {
            if (_isScannerThreadRun == true || _scannerThread != null)
            {
                StopScannerThread();
            }

            // [개선 3] IP와 포트 검증을 스레드 시작 전에 수행하여 무의미한 헛돌기 방지
            if (string.IsNullOrEmpty(_scannerIpAddress) || _scannerPort <= 0)
            {
                Console.WriteLine("IP 주소 또는 포트가 올바르지 않아 스캐너 스레드를 시작할 수 없습니다.");
                return;
            }

            _isScannerThreadRun = true;
            _scannerThread = new Thread(new ThreadStart(ThreadWork));
            _scannerThread.IsBackground = true;
            _scannerThread.Start();
        }



        private async void ThreadWork()
        {
            while (_isScannerThreadRun)
            {
                if (IsSocketConnected() == false)
                {
                    await ConnectScannerClient();
                }
                Thread.Sleep(1000);
            }
        }



        private bool IsSocketConnected()
        {
            if (_scannerClient == null || _scannerClient.Client == null)
            {
                return false;
            }

            bool isConnected = false;
            try
            {
                // 연결 플래그 체크 및 Socket의 Poll 메서드를 이용해 물리적 연결 상태 확인
                isConnected = !((_scannerClient.Client.Poll(1000, SelectMode.SelectRead) && (_scannerClient.Available == 0)) || !_scannerClient.Client.Connected);
            }
            catch (Exception ex)
            {
                isConnected = false;
                Console.WriteLine(ex.Message);
            }

            return isConnected;
        }



        private async Task ConnectScannerClient()
        {
            try
            {
                _scannerClient = new TcpClient();
                await _scannerClient.ConnectAsync(_scannerIpAddress, _scannerPort);
                _scannerStream = _scannerClient.GetStream();
                _isScannerClientConnect = true;

                // 수신 대기 시작 (비동기)
                _ = ReceiveDataAsync();
            }
            catch (Exception ex)
            {
                DisconnectScannerClient(); // 에러 발생 시 깔끔하게 초기화
                Console.WriteLine($"연결 실패: {ex.Message}");
            }
        } // ConnectToScanner



        private async Task ReceiveDataAsync()
        {
            byte[] buffer = new byte[4096];
            StringBuilder sb = new StringBuilder();

            try
            {
                while (_isScannerClientConnect && _scannerStream != null)
                {
                    int bytesRead = await _scannerStream.ReadAsync(buffer, 0, buffer.Length);

                    if (bytesRead > 0)
                    {
                        // 읽은 바이트를 문자열로 변환
                        string receivedText = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                        // [개선 2] 받은 데이터를 하나씩 확인하면서 분리 (패킷 분할이나 누적 방지)
                        // 미래 개선 : STX, ETX 가 붙어있으면 제일 베스트겠다.
                        foreach (char c in receivedText)
                        {
                            // CR 또는 LF 를 데이터의 끝으로 간주
                            if (c == '\r' || c == '\n')
                            {
                                if (sb.Length > 0)
                                {
                                    // 최종 데이터 이벤트 발생
                                    _scannerEvent?.Invoke(sb.ToString());
                                    sb.Clear();
                                }
                            }
                            else
                            {
                                sb.Append(c);
                            }
                        }
                    }
                    else // 연결이 끊겼음을 의미 -> finally 로 이동
                    {
                        break;
                    }
                }
            }
            catch (Exception)
            {
                // 수신 중 연결 끊김 (예외 발생)
            }
            finally
            {
                DisconnectScannerClient();
            }
        }



        public void StopScannerThread()
        {
            _isScannerThreadRun = false; // ThreadWork 루프 탈출 신호
            DisconnectScannerClient();   // 소켓 닫아서 ReceiveDataAsync 대기 상태 해제

            try
            {
                // [개선 1] 강제 Abort 대신, 스레드가 스스로 종료될 때까지 최대 1초만 기다려줌 (안전한 스레드 종료)
                if (_scannerThread != null && _scannerThread.IsAlive)
                {
                    _scannerThread.Join(1000);
                }
            }
            catch (Exception)
            {
                // 스레드 종료 중 오류
            }
            finally
            {
                _scannerThread = null;
            }
        }



        private void DisconnectScannerClient()
        {
            _isScannerClientConnect = false;

            try
            {
                if (_scannerStream != null)
                {
                    _scannerStream.Close();
                    _scannerStream.Dispose();
                }
                if (_scannerClient != null)
                {
                    _scannerClient.Close();
                    // Dispose()는 이전 .NET Framework 버전에서 접근이 제한될 수 있으므로 Close()만 호출합니다.
                }
            }
            catch (Exception)
            {
                // 연결 해제 중 오류
            }
            finally
            {
                _scannerStream = null;
                _scannerClient = null;
            }
        }



        public bool GetScannerThreadStatus()
        {
            return _isScannerThreadRun;
        }



        public bool GetScannerConnectionStatus()
        {
            return _isScannerClientConnect;
        }



    } // ScannerManager
}

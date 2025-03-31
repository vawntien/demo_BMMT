using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RC5_MaHoa_TXT
{
    public partial class Form1: Form
    {
        #region class RC5
        const int W = 32;
        const int R = 12;
        const uint P = 0xB7E15163;
        const uint Q = 0x9E3779B9;

        static uint RotL(uint x, int y) => (x << y) | (x >> (W - y));
        static uint RotR(uint x, int y) => (x >> y) | (x << (W - y));

        static void MoRongKhoaK(byte[] key, uint[] S)
        {
            int b = key.Length, c = (b + 3) / 4;
            uint[] L = new uint[c];

            for (int i = b - 1; i >= 0; i--)
                L[i / 4] = (L[i / 4] << 8) + key[i];

            S[0] = P;
            for (int i = 1; i < 2 * (R + 1); i++)
                S[i] = S[i - 1] + Q;

            uint A = 0, B = 0;
            int iIdx = 0, jIdx = 0;
            int v = 3 * Math.Max(2 * (R + 1), c);
            for (int s = 0; s < v; s++)
            {
                A = S[iIdx] = RotL(S[iIdx] + A + B, 3);
                B = L[jIdx] = RotL(L[jIdx] + A + B, (int)(A + B));
                iIdx = (iIdx + 1) % (2 * (R + 1));
                jIdx = (jIdx + 1) % c;
            }
        }

        static void MaHoaKhoi(uint[] S, ref uint A, ref uint B)
        {
            A += S[0]; B += S[1];
            for (int i = 1; i <= R; i++)
            {
                A = RotL(A ^ B, (int)B) + S[2 * i];
                B = RotL(B ^ A, (int)A) + S[2 * i + 1];
            }
        }

        static void GiaiMaTungKhoi(uint[] S, ref uint A, ref uint B)
        {
            for (int i = R; i > 0; i--)
            {
                B = RotR(B - S[2 * i + 1], (int)A) ^ A;
                A = RotR(A - S[2 * i], (int)B) ^ B;
            }
            B -= S[1]; A -= S[0];
        }

        static void MaHoaFileTXT(string inputPath, string outputPath, uint[] S)
        {
            byte[] data = Encoding.UTF8.GetBytes(File.ReadAllText(inputPath)); // Chuyển chuỗi thành byte[]

            int padding = (8 - (data.Length % 8)) % 8; // Thêm padding cho đủ block 8 byte
            byte[] paddedData = data.Concat(new byte[padding]).ToArray();

            using (BinaryWriter writer = new BinaryWriter(File.Open(outputPath, FileMode.Create)))
            {
                for (int i = 0; i < paddedData.Length; i += 8)
                {
                    uint A = BitConverter.ToUInt32(paddedData, i);
                    uint B = BitConverter.ToUInt32(paddedData, i + 4);
                    MaHoaKhoi(S, ref A, ref B);
                    writer.Write(A);
                    writer.Write(B);
                }
            }
            Console.WriteLine("File đã được mã hóa thành công!");
        }

        static void GiaiMaTXT(string inputPath, string outputPath, uint[] S)
        {
            byte[] data = File.ReadAllBytes(inputPath);
            byte[] decryptedData = new byte[data.Length];

            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(ms))
            {
                for (int i = 0; i < data.Length; i += 8)
                {
                    uint A = BitConverter.ToUInt32(data, i);
                    uint B = (i + 4 < data.Length) ? BitConverter.ToUInt32(data, i + 4) : 0; // Ensure index is within range
                    GiaiMaTungKhoi(S, ref A, ref B);
                    writer.Write(A);
                    writer.Write(B);
                }

                byte[] finalDecryptedData = ms.ToArray();
                string result = Encoding.UTF8.GetString(finalDecryptedData).TrimEnd('\0'); // Loại bỏ padding
                File.WriteAllText(outputPath, result); // Ghi chuỗi ra file
                //File.WriteAllBytes(outputPath, finalDecryptedData);

            }

            Console.WriteLine("File đã được giải mã!");
        }
        #endregion
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void btnFIleBanGoc_Click(object sender, EventArgs e)
        {
            string url = @"D:\Bao_mat\RC5_MaHoafileTXT\RC5_MaHoa_TXT\BanGoc.txt";

            
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });

        }

        private void btnFileGiaiMa_MH_Click(object sender, EventArgs e)
        {
            string url = @"D:\Bao_mat\RC5_MaHoafileTXT\RC5_MaHoa_TXT\BanMa_MH.txt";


            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void btnFileBanMa_GM_Click(object sender, EventArgs e)
        {
            string url = @"D:\Bao_mat\RC5_MaHoafileTXT\RC5_MaHoa_TXT\BanMa_MH.txt";


            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void btnFileBanRo_Click(object sender, EventArgs e)
        {
            string url = @"D:\Bao_mat\RC5_MaHoafileTXT\RC5_MaHoa_TXT\BanRo.txt";


            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void btnMaHoa_Click(object sender, EventArgs e)
        {
            //Console.OutputEncoding = Encoding.UTF8;
            //Console.InputEncoding = Encoding.UTF8;
            string DauVao = @"D:\Bao_mat\RC5_MaHoafileTXT\RC5_MaHoa_TXT\BanGoc.txt";
            string FileBanMa = @"D:\Bao_mat\RC5_MaHoafileTXT\RC5_MaHoa_TXT\BanMa_MH.txt";
            //string FileBanRo = @"D:\Bao_mat\RC%_test_2\test_with_txt\BanRo.txt";

            string khoaK = txtKhoaK_MH.Text.Trim();

            if (string.IsNullOrEmpty(khoaK))
            {
                MessageBox.Show("Vui lòng nhập khóa K!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            byte[] key = Encoding.UTF8.GetBytes(khoaK);

            //byte[] key = Encoding.UTF8.GetBytes("MySecretKey12345");
            uint[] S = new uint[2 * (R + 1)];
            MoRongKhoaK(key, S);

            MaHoaFileTXT(DauVao, FileBanMa, S);
            //GiaiMaTXT(FileBanMa, FileBanRo, S);

            //Console.WriteLine("Kiểm tra file giải mã tại: " + FileBanMa);
            DialogResult rs = MessageBox.Show("File đã được mã hóa!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnGiaiMa_Click(object sender, EventArgs e)
        {
            //    Console.OutputEncoding = Encoding.UTF8;
            //    Console.InputEncoding = Encoding.UTF8;
            //string DauVao = @"D:\Bao_mat\RC%_test_2\test_with_txt\testMH.txt";
            //File.Copy(@"D:\Bao_mat\RC5_MaHoafileTXT\RC5_MaHoa_TXT\BanMa_MH.txt", @"D:\Bao_mat\RC5_MaHoafileTXT\RC5_MaHoa_TXT\BanMa_GM.txt", true);


            string FileBanMa = @"D:\Bao_mat\RC5_MaHoafileTXT\RC5_MaHoa_TXT\BanMa_MH.txt";
            string FileBanRo = @"D:\Bao_mat\RC5_MaHoafileTXT\RC5_MaHoa_TXT\BanRo.txt";
            string khoaK_GM = txtKhoaK_GM.Text;

            string khoaK = txtKhoaK_GM.Text.Trim();

            if (string.IsNullOrEmpty(khoaK))
            {
                MessageBox.Show("Vui lòng nhập khóa K!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            byte[] key = Encoding.UTF8.GetBytes(khoaK);

            //byte[] key = Encoding.UTF8.GetBytes("MySecretKey12345");
            uint[] S = new uint[2 * (R + 1)];
            MoRongKhoaK(key, S);

            //MaHoaFileTXT(DauVao, FileBanMa, S);
            GiaiMaTXT(FileBanMa, FileBanRo, S);

            //Console.WriteLine("Kiểm tra file giải mã tại: " + FileBanMa);
            DialogResult rs = MessageBox.Show("File đã được giải mã!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            
        }

        private void btnLink_Click(object sender, EventArgs e)
        {
            string url = @"D:\Bao_mat\RC5_testby_console\RC5_test_2.sln";

            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show(
                "Bạn có muốn thoát không?",
                "Thông báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}

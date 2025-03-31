using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC5_MaHoa_TXT
{
    public class RC5
    {
        //const int W = 32;
        //const int R = 12;
        //const uint P = 0xB7E15163;
        //const uint Q = 0x9E3779B9;

        //static uint RotL(uint x, int y) => (x << y) | (x >> (W - y));
        //static uint RotR(uint x, int y) => (x >> y) | (x << (W - y));

        //static void MoRongKhoaK(byte[] key, uint[] S)
        //{
        //    int b = key.Length, c = (b + 3) / 4;
        //    uint[] L = new uint[c];

        //    for (int i = b - 1; i >= 0; i--)
        //        L[i / 4] = (L[i / 4] << 8) + key[i];

        //    S[0] = P;
        //    for (int i = 1; i < 2 * (R + 1); i++)
        //        S[i] = S[i - 1] + Q;

        //    uint A = 0, B = 0;
        //    int iIdx = 0, jIdx = 0;
        //    int v = 3 * Math.Max(2 * (R + 1), c);
        //    for (int s = 0; s < v; s++)
        //    {
        //        A = S[iIdx] = RotL(S[iIdx] + A + B, 3);
        //        B = L[jIdx] = RotL(L[jIdx] + A + B, (int)(A + B));
        //        iIdx = (iIdx + 1) % (2 * (R + 1));
        //        jIdx = (jIdx + 1) % c;
        //    }
        //}

        //static void MaHoaKhoi(uint[] S, ref uint A, ref uint B)
        //{
        //    A += S[0]; B += S[1];
        //    for (int i = 1; i <= R; i++)
        //    {
        //        A = RotL(A ^ B, (int)B) + S[2 * i];
        //        B = RotL(B ^ A, (int)A) + S[2 * i + 1];
        //    }
        //}

        //static void GiaiMaTungKhoi(uint[] S, ref uint A, ref uint B)
        //{
        //    for (int i = R; i > 0; i--)
        //    {
        //        B = RotR(B - S[2 * i + 1], (int)A) ^ A;
        //        A = RotR(A - S[2 * i], (int)B) ^ B;
        //    }
        //    B -= S[1]; A -= S[0];
        //}

        //static void MaHoaFileTXT(string inputPath, string outputPath, uint[] S)
        //{
        //    byte[] data = Encoding.UTF8.GetBytes(File.ReadAllText(inputPath)); // Chuyển chuỗi thành byte[]

        //    int padding = (8 - (data.Length % 8)) % 8; // Thêm padding cho đủ block 8 byte
        //    byte[] paddedData = data.Concat(new byte[padding]).ToArray();

        //    using (BinaryWriter writer = new BinaryWriter(File.Open(outputPath, FileMode.Create)))
        //    {
        //        for (int i = 0; i < paddedData.Length; i += 8)
        //        {
        //            uint A = BitConverter.ToUInt32(paddedData, i);
        //            uint B = BitConverter.ToUInt32(paddedData, i + 4);
        //            MaHoaKhoi(S, ref A, ref B);
        //            writer.Write(A);
        //            writer.Write(B);
        //        }
        //    }
        //    Console.WriteLine("File đã được mã hóa thành công!");
        //}

        //static void GiaiMaTXT(string inputPath, string outputPath, uint[] S)
        //{
        //    byte[] data = File.ReadAllBytes(inputPath);
        //    byte[] decryptedData = new byte[data.Length];

        //    using (MemoryStream ms = new MemoryStream())
        //    using (BinaryWriter writer = new BinaryWriter(ms))
        //    {
        //        for (int i = 0; i < data.Length; i += 8)
        //        {
        //            uint A = BitConverter.ToUInt32(data, i);
        //            uint B = BitConverter.ToUInt32(data, i + 4);
        //            GiaiMaTungKhoi(S, ref A, ref B);
        //            writer.Write(A);
        //            writer.Write(B);
        //        }

        //        byte[] finalDecryptedData = ms.ToArray();
        //        string result = Encoding.UTF8.GetString(finalDecryptedData).TrimEnd('\0'); // Loại bỏ padding
        //        File.WriteAllText(outputPath, result); // Ghi chuỗi ra file
        //    }

        //    Console.WriteLine("File đã được giải mã!");
        //}


        //static void Main()
        //{
        //    Console.OutputEncoding = Encoding.UTF8;
        //    Console.InputEncoding = Encoding.UTF8;
        //    string DauVao = @"D:\Bao_mat\RC%_test_2\test_with_txt\testMH.txt";
        //    string FileBanMa = @"D:\Bao_mat\RC%_test_2\test_with_txt\BanMa.txt";
        //    string FileBanRo = @"D:\Bao_mat\RC%_test_2\test_with_txt\BanRo.txt";

        //    byte[] key = Encoding.UTF8.GetBytes("MySecretKey12345");
        //    uint[] S = new uint[2 * (R + 1)];
        //    MoRongKhoaK(key, S);

        //    MaHoaFileTXT(DauVao, FileBanMa, S);
        //    GiaiMaTXT(FileBanMa, FileBanRo, S);

        //    Console.WriteLine("Kiểm tra file giải mã tại: " + FileBanMa);
        //    Console.ReadKey();
        //}
    }
}

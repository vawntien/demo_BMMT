using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RC5Algorithm
{
    class RC5
    {
        private const int w = 32; // Kích thước từ (word size)
        private const int r = 12; // Số vòng lặp
        private const int b = 16; // Độ dài khóa
        private const int wordCount = 2 * r + 2;
        private uint[] S; // Bảng S dùng cho giải thuật

        // Bước 1: Khởi tạo hằng số P và Q
        private static readonly uint P32 = 0xB7E15163;
        private static readonly uint Q32 = 0x9E3779B9;

        public RC5(byte[] key)
        {
            Mo_Rong_Khoa(key);
        }

        // Bước 2: Mở rộng khóa
        private void Mo_Rong_Khoa(byte[] key)
        {
            Console.WriteLine("Khởi tạo mảng S có kich thước 2r + 2 = 2(12) + 2 = 26 từ.");
            S = new uint[wordCount];
            S[0] = P32;
            for (int i = 1; i < wordCount; i++)
            {
                S[i] = S[i - 1] + Q32;
                Console.WriteLine("S[{0}] = {1}", i, S[i].ToString("X"));
            }

            Console.WriteLine("Tao khoa con, chuyen khoa K thanh mang tu L");

            uint[] L = new uint[b / 4];
            for (int i = 0; i < key.Length; i++)
            {
                L[i / 4] = (L[i / 4] << 8) + key[i];
            }

            Console.WriteLine("L[0] = "+L[0].ToString("X"));
            Console.WriteLine("L[1] = "+L[1].ToString("X"));
            Console.WriteLine("L[2] = "+L[2].ToString("X"));
            Console.WriteLine("L[3] = "+L[3].ToString("X"));

            uint A = 0, B = 0;
            int iIdx = 0, jIdx = 0;
            int Lap = 3 * Math.Max(wordCount, L.Length);

            for (int i = 0; i < Lap; i++)
            {
                A = S[iIdx] = DichTrai(S[iIdx] + A + B, 3);
                B = L[jIdx] = DichTrai(L[jIdx] + A + B, (int)(A + B));
                iIdx = (iIdx + 1) % wordCount;
                jIdx = (jIdx + 1) % L.Length;
            }
        }

        // Bước 3: Mã hóa
        public uint[] MaHoa(uint[] P)
        {
            Console.WriteLine("Mã hóa dữ liệu ");
            uint A = P[0] + S[0];
            uint B = P[1] + S[1];

            for (int i = 1; i <= r; i++)
            {
                A = DichTrai(A ^ B, (int)B) + S[2 * i];
                B = DichTrai(B ^ A, (int)A) + S[2 * i + 1];
            }

            return new uint[] { A, B };
        }

        // Bước 4: Giải mã
        public uint[] GiaiMa(uint[] cipherText)
        {
            uint A = cipherText[0];
            uint B = cipherText[1];

            for (int i = r; i > 0; i--)
            {
                B = DichPhai(B - S[2 * i + 1], (int)A) ^ A;
                A = DichPhai(A - S[2 * i], (int)B) ^ B;
            }

            A -= S[0];
            B -= S[1];

            return new uint[] { A, B };
        }

        private uint DichTrai(uint value, int shift) => (value << shift) | (value >> (w - shift));
        private uint DichPhai(uint value, int shift) => (value >> shift) | (value << (w - shift));

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            //byte[] key = new byte[] { 0x91, 0x3D, 0x76, 0xAA, 0x12, 0x3F, 0xBC, 0xD0, 0xEF, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD };
            //RC5 rc5 = new RC5(key);

            //byte[] K = Encoding.ASCII.GetBytes("SecretKey1234567");
            byte[] K = Encoding.ASCII.GetBytes("ThayThinhdeptrai");

            //byte[] K = Encoding.ASCII.GetBytes("RC5TestKey12345");
            RC5 rc5 = new RC5(K);

            uint[] Ban_ro = new uint[] { 0xDEADBEEF, 0xCAFEBABE };
            uint[] MaHoaed = rc5.MaHoa(Ban_ro);
            uint[] GiaiMaed = rc5.GiaiMa(MaHoaed);

            //uint[] Ban_ro = new uint[] { 0x12345678, 0x9ABCDEF0 };
            //uint[] MaHoaed = rc5.MaHoa(Ban_ro);
            //uint[] GiaiMaed = rc5.GiaiMa(MaHoaed);

            Console.WriteLine("Ban ro:  " + string.Join(" ", Ban_ro.Select(x => x.ToString("X8"))));
            Console.WriteLine("MaHoaed:  " + string.Join(" ", MaHoaed.Select(x => x.ToString("X8"))));
            Console.WriteLine("GiaiMaed:  " + string.Join(" ", GiaiMaed.Select(x => x.ToString("X8"))));
            Console.ReadKey();
        }
    }
}

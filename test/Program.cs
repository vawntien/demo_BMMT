using System;
using System.Linq;
using System.Text;

namespace RC5Algorithm
{
    class RC5
    {
        private const int w = 32;
        private const int r = 12;
        private const int b = 16;
        private const int wordCount = 2 * r + 2;
        private uint[] S;

        private static readonly uint P32 = 0xB7E15163;
        private static readonly uint Q32 = 0x9E3779B9;

        public RC5(byte[] key)
        {
            Mo_Rong_Khoa(key);
        }

        private void Mo_Rong_Khoa(byte[] key)
        {
            S = new uint[wordCount];
            S[0] = P32;
            for (int i = 1; i < wordCount; i++)
                S[i] = S[i - 1] + Q32;

            uint[] L = new uint[b / 4];
            for (int i = 0; i < key.Length; i++)
                L[i / 4] = (L[i / 4] << 8) + key[i];

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

        public uint[] MaHoa(uint[] P)
        {
            uint A = P[0] + S[0];
            uint B = P[1] + S[1];

            for (int i = 1; i <= r; i++)
            {
                A = DichTrai(A ^ B, (int)B) + S[2 * i];
                B = DichTrai(B ^ A, (int)A) + S[2 * i + 1];
            }

            return new uint[] { A, B };
        }

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

        // Chuyển chuỗi thành uint[]
        public static uint[] ChuyenChuoiThanhUint(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            int length = (bytes.Length + 3) / 4;
            uint[] result = new uint[length];

            for (int i = 0; i < bytes.Length; i++)
            {
                result[i / 4] = (result[i / 4] << 8) + bytes[i];
            }
            return result;
        }

        // Chuyển uint[] thành chuỗi
        public static string ChuyenUintThanhChuoi(uint[] data)
        {
            byte[] bytes = new byte[data.Length * 4];
            for (int i = 0; i < data.Length; i++)
            {
                bytes[i * 4 + 0] = (byte)(data[i] >> 24);
                bytes[i * 4 + 1] = (byte)(data[i] >> 16);
                bytes[i * 4 + 2] = (byte)(data[i] >> 8);
                bytes[i * 4 + 3] = (byte)(data[i]);
            }
            return Encoding.UTF8.GetString(bytes).TrimEnd('\0');
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            byte[] K = Encoding.ASCII.GetBytes("abc");
            RC5 rc5 = new RC5(K);

            string banRo = "tiendeptraikhoaito";
            Console.WriteLine("Bản rõ: " + banRo);

            uint[] banRoUint = ChuyenChuoiThanhUint(banRo);
            uint[] maHoaed = rc5.MaHoa(banRoUint);
            uint[] giaiMaed = rc5.GiaiMa(maHoaed);

            string banGiaiMa = ChuyenUintThanhChuoi(giaiMaed);

            Console.WriteLine("Mã hóa: " + string.Join(" ", maHoaed.Select(x => x.ToString("X8"))));
            Console.WriteLine("Giải mã: " + banGiaiMa);

            Console.ReadKey();
        }
    }
}

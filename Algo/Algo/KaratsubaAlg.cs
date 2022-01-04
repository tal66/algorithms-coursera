using System;
using System.Text;


namespace Algo
{
    public class KaratsubaAlg
    {        
        public static ReadOnlySpan<char> Karatsuba(ReadOnlySpan<char> x, ReadOnlySpan<char> y)
        {
            var result = KaratsubaRec(x, y);
            return result.TrimStart('0');
        }

        static ReadOnlySpan<char> KaratsubaRec(ReadOnlySpan<char> x, ReadOnlySpan<char> y)
        {
            int L = ClosestPowOfTwoLength(x, y);

            if ((y.Length != L) || x.Length != L)
            {
                x = PadZerosLeft(x, L);
                y = PadZerosLeft(y, L);
            }

            if (L == 1)
            {
                return (int.Parse(x.ToString()) * int.Parse(y.ToString())).ToString();
            }
            else
            {
                var a = x.Slice(0, L / 2);
                var b = x.Slice(L / 2, L / 2);
                var c = y.Slice(0, L / 2);
                var d = y.Slice(L / 2, L / 2);

                var p = Add(a, b);
                var q = Add(c, d);

                var ac = KaratsubaRec(a, c);
                var bd = KaratsubaRec(b, d);
                var pq = KaratsubaRec(p, q);

                var adbc = Subtract(Subtract(pq, ac), bd);

                var temp = Add(AddZerosRight(ac, L), AddZerosRight(adbc, L / 2));

                return Add(temp, bd);
            }
        }

        static int ClosestPowOfTwoLength(ReadOnlySpan<char> x, ReadOnlySpan<char> y)
        {
            int L = Math.Max(x.Length, y.Length);
            if ((L & (L - 1)) != 0)
            {
                L = L switch
                {
                    >= 64 => 128,
                    >= 32 => 64,
                    >= 16 => 32,
                    >= 8 => 16,
                    >= 4 => 8,
                    >= 2 => 4,
                       _ => throw new InvalidOperationException()
                };
            }
            
            return L;
        }

        static string PadZerosLeft(ReadOnlySpan<char> str, int l)
        {
            string result;
            if (str.Length < l)
            {
                result = str.ToString().PadLeft(l, '0');
            } 
            else
            {
                result = str.ToString();
            }

            return result;
        }

        static ReadOnlySpan<char> AddZerosRight(ReadOnlySpan<char> str, int l)
        {
            if (l <= 0)
            {
                return str;
            }

            StringBuilder sb = new(str.ToString());
            return sb.Append('0', l).ToString();
        }

        static ReadOnlySpan<char> Add(ReadOnlySpan<char> x, ReadOnlySpan<char> y)
        {
            StringBuilder sb = new();
            int c = 0;
            for (int i = x.Length - 1, j = y.Length - 1;
                    i >= 0 || j >= 0;
                    i--, j--
                )
            {
                int xi = i >= 0 ? x[i] - '0' : 0;
                int yj = j >= 0 ? y[j] - '0' : 0;
                int res = xi + yj + c;

                c = (xi + yj + c) / 10;

                sb.Append(res % 10);
            }

            if (c != 0)
            {
                sb.Append(c);
            }
           
            Reverse(sb);
            
            return sb.ToString();
        }
      
        // assuming: x > y and x, y >= 0;
        static ReadOnlySpan<char> Subtract(ReadOnlySpan<char> x, ReadOnlySpan<char> y)
            {
                if (y.Length + x.Length < 15)
                {
                    return (long.Parse(x) - long.Parse(y)).ToString();
                }

                StringBuilder sb = new();
                int c = 0;
                for (int i = x.Length - 1, j = y.Length - 1;
                        i >= 0 || j >= 0;
                        i--, j--
                    )
                {
                    int xi = i >= 0 ? x[i] - '0' : 0;
                    int yj = j >= 0 ? y[j] - '0' : 0;
                    int res = (xi - yj + c + 10) % 10;

                    c = (xi - yj + c) < 0 ? -1 : 0;

                    sb.Append(res % 10);

                }

                while (sb.Length > 1 && sb[^1].Equals('0'))
                {
                    sb.Remove(sb.Length - 1, 1);
                }

                Reverse(sb);
                return sb.ToString();
            }
        
        static void Reverse(StringBuilder sb)
        {
            int sbLength = sb.Length;
            for (int i = 0; i < (sbLength + 1) / 2; i++)
            {
                (sb[i], sb[sbLength - 1 - i]) = (sb[sbLength - 1 - i], sb[i]);
            }
        }

    }
}

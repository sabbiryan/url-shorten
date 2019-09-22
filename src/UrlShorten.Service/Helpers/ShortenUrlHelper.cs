using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UrlShorten.Service.Helpers
{
    public class ShortenUrlHelper
    {

        public static readonly string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public static readonly int Base = Alphabet.Length;

        public static string Encode(int i)
        {
            if (i == 0) return Alphabet[0].ToString();

            var s = string.Empty;

            while (i > 0)
            {
                s += Alphabet[i % Base];
                i = i / Base;
            }

            return string.Join(string.Empty, s.Reverse());
        }

        public static int Decode(string s)
        {
            var i = 0;

            foreach (var c in s)
            {
                i = (i * Base) + Alphabet.IndexOf(c);
            }

            return i;
        }

        public static void Main(string[] args)
        {
            // Simple test of encode/decode operations
            for (var i = 0; i < 10000; i++)
            {
                if (Decode(Encode(i)) != i)
                {
                    System.Console.WriteLine("{0} is not {1}", Encode(i), i);
                    break;
                }
            }
        }

    }
}

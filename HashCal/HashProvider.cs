using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using HashCal.HashAlgorithms;

namespace HashCal
{
    internal static class HashProvider
    {
        private static Dictionary<string, Func<HashAlgorithm>> _SupportedAlgorithms;
        public static Dictionary<string, Func<HashAlgorithm>> SupportedAlgorithms => _SupportedAlgorithms;

        static HashProvider()
        {
            _SupportedAlgorithms = new Dictionary<string, Func<HashAlgorithm>>()
            {
                { "MD4", MD4.Create },
                { "MD5", MD5.Create },
                { "SHA1", SHA1.Create },
                { "SHA256", SHA256.Create },
                { "SHA384", SHA384.Create },
                { "SHA512", SHA512.Create },
                { "RIPEMD160", RIPEMD160.Create }
            };
        }
    }
}

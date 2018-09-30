using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HashCal.Core
{
    public class CustomHashAlgorithmEntry :
        HashAlgorithmEntry
    {
        private Func<HashAlgorithm> AlgorithmFactory;

        public CustomHashAlgorithmEntry(string name, Func<HashAlgorithm> algorithmFactory)
        {
            AlgorithmName = name ?? throw new ArgumentNullException();
            AlgorithmFactory = algorithmFactory ?? throw new ArgumentNullException();
        }

        public override string AlgorithmName { get; }

        public override byte[] ComputeHash(byte[] buffer)
        {
            using (var algorithm = AlgorithmFactory())
            {
                HashValue = algorithm.ComputeHash(buffer);
                return HashValue;
            }
        }

        public override byte[] ComputeHash(Stream buffer)
        {
            using (var algorithm = AlgorithmFactory())
            {
                HashValue = algorithm.ComputeHash(buffer);
                return HashValue;
            }
        }
    }
}

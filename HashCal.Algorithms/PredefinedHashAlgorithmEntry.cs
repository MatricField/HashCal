using System.IO;
using System.Security.Cryptography;

namespace HashCal.Algorithms
{
    public class PredefinedHashAlgorithmEntry:
        HashAlgorithmEntry
    {
        private HashAlgorithmName HashAlgorithmName;

        public override string AlgorithmName => HashAlgorithmName.Name;

        public PredefinedHashAlgorithmEntry(HashAlgorithmName name)
        {
            HashAlgorithmName = name;
        }

        public override byte[] ComputeHash(byte[] buffer)
        {
            using (var algorithm = HashAlgorithm.Create(HashAlgorithmName.Name))
            {
                HashValue = algorithm.ComputeHash(buffer);
                return HashValue;
            }
        }

        public override byte[] ComputeHash(Stream buffer)
        {
            using (var algorithm = HashAlgorithm.Create(HashAlgorithmName.Name))
            {
                HashValue = algorithm.ComputeHash(buffer);
                return HashValue;
            }
        }
    }
}

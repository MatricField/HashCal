using HashCal.Algorithms;
using HashCal.Algorithms.AdditionalHashAlgorithms;
using System.Security.Cryptography;

namespace HashCal
{
    public sealed class DefaultViewModel:
        ViewModel
    {
        public DefaultViewModel()
        {
            AddCustom("MD4", () => new MD4());
            AddPredefined(HashAlgorithmName.MD5);
            AddPredefined(HashAlgorithmName.SHA1);
            AddPredefined(HashAlgorithmName.SHA256);
            AddPredefined(HashAlgorithmName.SHA384);
            AddPredefined(HashAlgorithmName.SHA512);
        }
    }
}

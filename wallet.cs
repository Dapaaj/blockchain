using System;
using System.Linq;
using System.Security.Cryptography;

namespace BlockchainAssignment.Wallet
{
    class Wallet
    {
        public string publicID;

        public Wallet(out string privateKey)
        {
            privateKey = string.Empty;

            byte[] pubKey;
            byte[] privKey;

            CngKeyCreationParameters keyCreationParameters = new CngKeyCreationParameters();
            keyCreationParameters.ExportPolicy = CngExportPolicies.AllowPlaintextExport;
            keyCreationParameters.KeyUsage = CngKeyUsages.Signing;

            CngKey key = CngKey.Create(CngAlgorithm.ECDsaP256, null, keyCreationParameters);

            byte[] keyBlob = key.Export(CngKeyBlobFormat.EccPrivateBlob);

            pubKey = keyBlob.Skip(8).Take(keyBlob.Length - 40).ToArray();
            privKey = keyBlob.Skip(72).Take(keyBlob.Length - 72).ToArray();

            publicID = Convert.ToBase64String(pubKey);
            privateKey = Convert.ToBase64String(privKey);
        }

        public static bool ValidatePrivateKey(string privateKey, string publicID)
        {
            string testHash = Convert.ToBase64String(new byte[32]);

            if (string.IsNullOrWhiteSpace(privateKey) || string.IsNullOrWhiteSpace(publicID))
                return false;

            string sig = CreateSignature(publicID, privateKey, testHash);

            if (sig == "null")
                return false;

            return ValidateSignature(publicID, testHash, sig);
        }

        public static bool ValidateSignature(string publicID, string datahash, string datasig)
        {
            if (publicID.Equals("Mine Rewards"))
                publicID = "QfF3+9GgTxyGLvb+ScOAI6nJxBh8IyZbeD0r6BJBMyabZmyuP82yrSLKMq/F05OG0VZ4gg63uHFZUKzCu3wZuA==";

            if (string.IsNullOrWhiteSpace(publicID) || string.IsNullOrWhiteSpace(datasig) || datasig.Equals("null"))
                return false;

            CngKey key = createKey(publicID);

            if (key == null)
                return false;

            using (ECDsaCng dsa = new ECDsaCng(key))
            {
                byte[] hashBytes = Convert.FromBase64String(datahash);
                byte[] sigBytes = Convert.FromBase64String(datasig);
                return dsa.VerifyHash(hashBytes, sigBytes);
            }
        }

        public static string CreateSignature(string publicID, string privateKey, string datahash)
        {
using System;
using System.Linq;
using System.Security.Cryptography;

namespace BlockchainAssignment.Wallet
{
    class Wallet
    {
        public string publicID;

        public Wallet(out string privateKey)
        {
            privateKey = string.Empty;

            byte[] pubKey;
            byte[] privKey;

            CngKeyCreationParameters keyCreationParameters = new CngKeyCreationParameters();
            keyCreationParameters.ExportPolicy = CngExportPolicies.AllowPlaintextExport;
            keyCreationParameters.KeyUsage = CngKeyUsages.Signing;

            CngKey key = CngKey.Create(CngAlgorithm.ECDsaP256, null, keyCreationParameters);

            byte[] keyBlob = key.Export(CngKeyBlobFormat.EccPrivateBlob);

            pubKey = keyBlob.Skip(8).Take(keyBlob.Length - 40).ToArray();
            privKey = keyBlob.Skip(72).Take(keyBlob.Length - 72).ToArray();

            publicID = Convert.ToBase64String(pubKey);
            privateKey = Convert.ToBase64String(privKey);
        }

        public static bool ValidatePrivateKey(string privateKey, string publicID)
        {
            string testHash = Convert.ToBase64String(new byte[32]);

            if (string.IsNullOrWhiteSpace(privateKey) || string.IsNullOrWhiteSpace(publicID))
                return false;

            string sig = CreateSignature(publicID, privateKey, testHash);

            if (sig == "null")
                return false;

            return ValidateSignature(publicID, testHash, sig);
        }

        public static bool ValidateSignature(string publicID, string datahash, string datasig)
        {
            if (publicID.Equals("Mine Rewards"))
                publicID = "QfF3+9GgTxyGLvb+ScOAI6nJxBh8IyZbeD0r6BJBMyabZmyuP82yrSLKMq/F05OG0VZ4gg63uHFZUKzCu3wZuA==";

            if (string.IsNullOrWhiteSpace(publicID) || string.IsNullOrWhiteSpace(datasig) || datasig.Equals("null"))
                return false;

            CngKey key = createKey(publicID);

            if (key == null)
                return false;

            using (ECDsaCng dsa = new ECDsaCng(key))
            {
                byte[] hashBytes = Convert.FromBase64String(datahash);
                byte[] sigBytes = Convert.FromBase64String(datasig);
                return dsa.VerifyHash(hashBytes, sigBytes);
            }
        }

        public static string CreateSignature(string publicID, string privateKey, string datahash)
        {
            if (string.IsNullOrWhiteSpace(privateKey))
            {
                if (publicID == "Mine Rewards")
                    return "null";

                return "null";
            }

            CngKey key = createKey(publicID, privateKey);

            if (key == null)
                return "null";

            using (ECDsaCng dsa = new ECDsaCng(key))
            {
                byte[] hashBytes = Convert.FromBase64String(datahash);
                byte[] byteSig = dsa.SignHash(hashBytes);
                return Convert.ToBase64String(byteSig);
            }
        }

        private static CngKey createKey(string publicID, string privateKey = "")
        {
            try
            {
                byte[] keyByte = new byte[] { 69, 67, 83, 49, 32, 0, 0, 0 };
                byte[] publicBytes = Convert.FromBase64String(publicID);
                byte[] keyByteCombine1 = new byte[72];

                keyByte.CopyTo(keyByteCombine1, 0);
                publicBytes.CopyTo(keyByteCombine1, keyByte.Length);

                if (!privateKey.Equals(string.Empty))
                {
                    keyByteCombine1[3] = 50;
                    byte[] privateBytes = Convert.FromBase64String(privateKey);
                    byte[] keyByteCombine2 = new byte[104];

                    keyByteCombine1.CopyTo(keyByteCombine2, 0);
                    privateBytes.CopyTo(keyByteCombine2, keyByteCombine1.Length);

                    return CngKey.Import(keyByteCombine2, CngKeyBlobFormat.EccPrivateBlob);
                }

                return CngKey.Import(keyByteCombine1, CngKeyBlobFormat.EccPublicBlob);
            }
            catch
            {
                return null;
            }
        }
    }
}

using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace BlockchainAssignment
{
    internal class Transaction
    {
        public DateTime timestamp;
        public string senderAddress;
        public string recipientAddress;
        public string hash;
        string signature;

        public double amount;
        public double fee;

        public Transaction(string from, string to, double amount, double fee, string privateKey)
        {
            timestamp = DateTime.Now;
            senderAddress = from;
            recipientAddress = to;
            this.amount = amount;
            this.fee = fee;
            hash = CreateHash();
            signature = Wallet.Wallet.CreateSignature(from, privateKey, hash);
        }

        public string CreateHash()
        {
            string input =
                timestamp.ToString("O", CultureInfo.InvariantCulture) + "|" +
                senderAddress + "|" +
                recipientAddress + "|" +
                amount.ToString(CultureInfo.InvariantCulture) + "|" +
                fee.ToString(CultureInfo.InvariantCulture);

            using (SHA256 hasher = SHA256.Create())
            {
                byte[] hashBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sb = new StringBuilder();

                foreach (byte b in hashBytes)
                    sb.Append(b.ToString("x2"));

                return sb.ToString();
            }
        }

        public override string ToString()
        {
            return "Transaction Hash: " + hash + "\n"
                + "Digital Signature: " + signature + "\n"
                + "Timestamp: " + timestamp + "\n"
                + "Transferred: " + amount.ToString(CultureInfo.InvariantCulture) + " Assignment\n"
                + "Fee: " + fee.ToString(CultureInfo.InvariantCulture) + "\n"
                + "Sender Address: " + senderAddress + "\n"
                + "Receiver Address: " + recipientAddress + "\n";
        }
    }
}

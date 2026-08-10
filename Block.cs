using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlockchainAssignment
{
    internal class Block
    {
        public int index;
        public DateTime timestamp;
        public string hash;
        public string prevHash;

        public List<Transaction> transactionList = new List<Transaction>();
        public string merkleRoot;

        public long nonce = 0;

        public double reward = 1.0;
        public double fees = 0.0;
        public string minerAddress = string.Empty;

        // Task 2 fields
        public double difficultyValue = 2.0;
        public long miningTimeMs = 0;

        public Block()
        {
            timestamp = DateTime.Now;
            index = 0;
            prevHash = string.Empty;
            transactionList = new List<Transaction>();
            reward = 0;
            merkleRoot = MerkleRoot(transactionList);
        }

        public Block(Block lastBlock, List<Transaction> transactions, double difficultyValue, string address = "")
        {
            timestamp = DateTime.Now;
            index = lastBlock.index + 1;
            prevHash = lastBlock.hash;
            minerAddress = address;
            this.difficultyValue = difficultyValue;

            List<Transaction> copiedTransactions = new List<Transaction>(transactions);
            copiedTransactions.Add(CreateRewardTransaction(copiedTransactions));
            transactionList = copiedTransactions;

            merkleRoot = MerkleRoot(transactionList);
        }

        public Transaction CreateRewardTransaction(List<Transaction> transactions)
        {
            fees = transactions.Aggregate(0.0, (acc, t) => acc + t.fee);
            return new Transaction("Mine Rewards", minerAddress, reward + fees, 0, "");
        }

        public string CreateHash(long nonceValue)
        {
            string input =
                index.ToString(CultureInfo.InvariantCulture) + "|" +
                timestamp.ToString("O", CultureInfo.InvariantCulture) + "|" +
                prevHash + "|" +
                nonceValue.ToString(CultureInfo.InvariantCulture) + "|" +
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlockchainAssignment
{
    internal class Block
    {
        public int index;
        public DateTime timestamp;
        public string hash;
        public string prevHash;

        public List<Transaction> transactionList = new List<Transaction>();
        public string merkleRoot;

        public long nonce = 0;

        public double reward = 1.0;
        public double fees = 0.0;
        public string minerAddress = string.Empty;

        // Task 2 fields
        public double difficultyValue = 2.0;
        public long miningTimeMs = 0;

        public Block()
        {
            timestamp = DateTime.Now;
            index = 0;
            prevHash = string.Empty;
            transactionList = new List<Transaction>();
            reward = 0;
            merkleRoot = MerkleRoot(transactionList);
        }

        public Block(Block lastBlock, List<Transaction> transactions, double difficultyValue, string address = "")
        {
            timestamp = DateTime.Now;
            index = lastBlock.index + 1;
            prevHash = lastBlock.hash;
            minerAddress = address;
            this.difficultyValue = difficultyValue;

            List<Transaction> copiedTransactions = new List<Transaction>(transactions);
            copiedTransactions.Add(CreateRewardTransaction(copiedTransactions));
            transactionList = copiedTransactions;

            merkleRoot = MerkleRoot(transactionList);
        }

        public Transaction CreateRewardTransaction(List<Transaction> transactions)
        {
            fees = transactions.Aggregate(0.0, (acc, t) => acc + t.fee);
            return new Transaction("Mine Rewards", minerAddress, reward + fees, 0, "");
        }

        public string CreateHash(long nonceValue)
        {
            string input =
                index.ToString(CultureInfo.InvariantCulture) + "|" +
                timestamp.ToString("O", CultureInfo.InvariantCulture) + "|" +
                prevHash + "|" +
                nonceValue.ToString(CultureInfo.InvariantCulture) + "|" +
                reward.ToString(CultureInfo.InvariantCulture) + "|" +
                merkleRoot;

            using (SHA256 hasher = SHA256.Create())
            {
                byte[] hashBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sb = new StringBuilder();

                foreach (byte b in hashBytes)
                    sb.Append(b.ToString("x2"));

                return sb.ToString();
            }
        }

        private int GetRequiredLeadingZeros()
        {
            int zeros = (int)Math.Round(difficultyValue);

            if (zeros < 1)
                zeros = 1;

            if (zeros > 8)
                zeros = 8;

            return zeros;
        }

        public bool IsValidHash(string hashValue)
        {
            int zeros = GetRequiredLeadingZeros();
            string prefix = new string('0', zeros);
            return hashValue.StartsWith(prefix);
        }

        public string MineSingleThread()
        {
            Stopwatch sw = Stopwatch.StartNew();
            long localNonce = 0;

            while (true)
            {
                string attempt = CreateHash(localNonce);

                if (IsValidHash(attempt))
                {
                    nonce = localNonce;
                    sw.Stop();
                    miningTimeMs = sw.ElapsedMilliseconds;
                    return attempt;
                }

                localNonce++;
            }
        }

        public string MineParallel(int threadCount)
        {
            Stopwatch sw = Stopwatch.StartNew();

            long winningNonce = -1;
            string winningHash = null;
            object lockObj = new object();

            using (CancellationTokenSource cts = new CancellationTokenSource())
            {
                CancellationToken token = cts.Token;
                Task[] tasks = new Task[threadCount];

                for (int threadId = 0; threadId < threadCount; threadId++)
                reward.ToString(CultureInfo.InvariantCulture) + "|" +
                merkleRoot;

            using (SHA256 hasher = SHA256.Create())
            {
                byte[] hashBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sb = new StringBuilder();

                foreach (byte b in hashBytes)
                    sb.Append(b.ToString("x2"));

                return sb.ToString();
            }
        }

        private int GetRequiredLeadingZeros()
        {
            int zeros = (int)Math.Round(difficultyValue);

            if (zeros < 1)
                zeros = 1;

            if (zeros > 8)
                zeros = 8;

            return zeros;
        }

        public bool IsValidHash(string hashValue)
        {
            int zeros = GetRequiredLeadingZeros();
            string prefix = new string('0', zeros);
            return hashValue.StartsWith(prefix);
        }

        public string MineSingleThread()
        {
            Stopwatch sw = Stopwatch.StartNew();
            long localNonce = 0;

            while (true)
            {
                string attempt = CreateHash(localNonce);

                if (IsValidHash(attempt))
                {
                    nonce = localNonce;
                    sw.Stop();
                    miningTimeMs = sw.ElapsedMilliseconds;
                    return attempt;
                }

                localNonce++;
            }
        }

        public string MineParallel(int threadCount)
        {
            Stopwatch sw = Stopwatch.StartNew();

            long winningNonce = -1;
            string winningHash = null;
            object lockObj = new object();

            using (CancellationTokenSource cts = new CancellationTokenSource())
            {
                CancellationToken token = cts.Token;
                Task[] tasks = new Task[threadCount];

                for (int threadId = 0; threadId < threadCount; threadId++)
                {
                    int localThreadId = threadId;

                    tasks[threadId] = Task.Run(() =>
                    {
                        long localNonce = localThreadId;

                        while (!token.IsCancellationRequested)
                        {
                            string attempt = CreateHash(localNonce);

                            if (IsValidHash(attempt))
                            {
                                lock (lockObj)
                                {
                                    if (winningHash == null)
                                    {
                                        winningHash = attempt;
                                        winningNonce = localNonce;
                                        cts.Cancel();
                                    }
                                }

                                return;
                            }

                            localNonce += threadCount;
                        }
                    }, token);
                }

                try
                {
                    Task.WaitAll(tasks);
                }
                catch (AggregateException)
                {
                }
            }

            sw.Stop();
            nonce = winningNonce;
            miningTimeMs = sw.ElapsedMilliseconds;
            return winningHash;
        }

        public static string MerkleRoot(List<Transaction> transactionList)
        {
            List<string> hashes = transactionList.Select(t => t.hash).ToList();

            if (hashes.Count == 0)
                return string.Empty;

            if (hashes.Count == 1)
                return HashCode.HashTools.CombineHash(hashes[0], hashes[0]);

            while (hashes.Count > 1)
            {
                List<string> merkleLeaves = new List<string>();

                for (int i = 0; i < hashes.Count; i += 2)
                {
                    if (i == hashes.Count - 1)
                        merkleLeaves.Add(HashCode.HashTools.CombineHash(hashes[i], hashes[i]));
                    else
                        merkleLeaves.Add(HashCode.HashTools.CombineHash(hashes[i], hashes[i + 1]));
                }

                hashes = merkleLeaves;
            }
                {
                    int localThreadId = threadId;

                    tasks[threadId] = Task.Run(() =>
                    {
                        long localNonce = localThreadId;

                        while (!token.IsCancellationRequested)
                        {
                            string attempt = CreateHash(localNonce);

                            if (IsValidHash(attempt))
                            {
                                lock (lockObj)
                                {
                                    if (winningHash == null)
                                    {
                                        winningHash = attempt;
                                        winningNonce = localNonce;
                                        cts.Cancel();
                                    }
                                }

                                return;
                            }

                            localNonce += threadCount;
                        }
                    }, token);
                }

                try
                {
                    Task.WaitAll(tasks);
                }
                catch (AggregateException)
                {
                }
            }

            sw.Stop();
            nonce = winningNonce;
            miningTimeMs = sw.ElapsedMilliseconds;
            return winningHash;
        }

        public static string MerkleRoot(List<Transaction> transactionList)
        {
            List<string> hashes = transactionList.Select(t => t.hash).ToList();

            if (hashes.Count == 0)
                return string.Empty;

            if (hashes.Count == 1)
                return HashCode.HashTools.CombineHash(hashes[0], hashes[0]);

            while (hashes.Count > 1)
            {
                List<string> merkleLeaves = new List<string>();

                for (int i = 0; i < hashes.Count; i += 2)
                {
                    if (i == hashes.Count - 1)
                        merkleLeaves.Add(HashCode.HashTools.CombineHash(hashes[i], hashes[i]));
                    else
                        merkleLeaves.Add(HashCode.HashTools.CombineHash(hashes[i], hashes[i + 1]));
                }

                hashes = merkleLeaves;
            }

            return hashes[0];
        }

        public override string ToString()
        {
            return "Index: " + index
                + "\nTimestamp: " + timestamp
                + "\nPrevious Hash: " + prevHash
                + "\nHash: " + hash
                + "\nMerkle Root: " + merkleRoot
                + "\nNonce: " + nonce
                + "\nMining Time (ms): " + miningTimeMs
                + "\nDifficulty Value: " + difficultyValue.ToString("F2", CultureInfo.InvariantCulture)
                + "\nRequired Leading Zeros: " + GetRequiredLeadingZeros()
                + "\nReward: " + reward.ToString(CultureInfo.InvariantCulture)
                + "\nFees: " + fees.ToString(CultureInfo.InvariantCulture)
                + "\nMiner's Address: " + minerAddress
                + "\nTransactions:\n" + string.Join("\n", transactionList);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace BlockchainAssignment
{
    internal class Blockchain
    {
        public List<Block> Blocks = new List<Block>();
        public List<Transaction> transactionPool = new List<Transaction>();

        private int transactionsPerBlock = 5;

        public long targetBlockTimeMs = 2000;
        public double currentDifficultyValue = 2.0;

        public Blockchain()
        {
            Block genesis = new Block();
            genesis.difficultyValue = currentDifficultyValue;
            genesis.hash = genesis.MineSingleThread();
            Blocks.Add(genesis);
        }

        public string GetBlockAsString(int index)
        {
            if (index < 0 || index >= Blocks.Count)
                return "Invalid block index.";

            return Blocks[index].ToString();
        }

        public Block GetLastBlock()
        {
            return Blocks[Blocks.Count - 1];
        }

        public List<Transaction> GetPendingTransactions(string mode, string minerAddress)
        {
            List<Transaction> orderedPool = new List<Transaction>(transactionPool);

            switch (mode)
            {
                case "Greedy":
                    orderedPool = orderedPool
                        .OrderByDescending(t => t.fee)
                        .ThenBy(t => t.timestamp)
                        .ToList();
                    break;

                case "Altruistic":
                    orderedPool = orderedPool
                        .OrderBy(t => t.timestamp)
                        .ToList();
                    break;

                case "Random":
                    Random rng = new Random();
                    orderedPool = orderedPool
                        .OrderBy(t => rng.Next())
                        .ToList();
                    break;

                case "Address Preference":
                    orderedPool = orderedPool
                        .OrderByDescending(t =>
                            t.senderAddress == minerAddress || t.recipientAddress == minerAddress)
                        .ThenByDescending(t => t.fee)
                        .ThenBy(t => t.timestamp)
                        .ToList();
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlockchainAssignment
{
    internal class Blockchain
    {
        public List<Block> Blocks = new List<Block>();
        public List<Transaction> transactionPool = new List<Transaction>();

        private int transactionsPerBlock = 5;

        public long targetBlockTimeMs = 2000;
        public double currentDifficultyValue = 2.0;

        public Blockchain()
        {
            Block genesis = new Block();
            genesis.difficultyValue = currentDifficultyValue;
            genesis.hash = genesis.MineSingleThread();
            Blocks.Add(genesis);
        }

        public string GetBlockAsString(int index)
        {
            if (index < 0 || index >= Blocks.Count)
                return "Invalid block index.";

            return Blocks[index].ToString();
        }

        public Block GetLastBlock()
        {
            return Blocks[Blocks.Count - 1];
        }

        public List<Transaction> GetPendingTransactions(string mode, string minerAddress)
        {
            List<Transaction> orderedPool = new List<Transaction>(transactionPool);

            switch (mode)
            {
                case "Greedy":
                    orderedPool = orderedPool
                        .OrderByDescending(t => t.fee)
                        .ThenBy(t => t.timestamp)
                        .ToList();
                    break;

                case "Altruistic":
                    orderedPool = orderedPool
                        .OrderBy(t => t.timestamp)
                        .ToList();
                    break;

                case "Random":
                    Random rng = new Random();
                    orderedPool = orderedPool
                        .OrderBy(t => rng.Next())
                        .ToList();
                    break;

                case "Address Preference":
                    orderedPool = orderedPool
                        .OrderByDescending(t =>
                            t.senderAddress == minerAddress || t.recipientAddress == minerAddress)
                        .ThenByDescending(t => t.fee)
                        .ThenBy(t => t.timestamp)
                        .ToList();
                    break;

                default:
                    orderedPool = orderedPool.OrderBy(t => t.timestamp).ToList();
                    break;
            }

            int n = Math.Min(transactionsPerBlock, orderedPool.Count);
            List<Transaction> selected = orderedPool.Take(n).ToList();

            foreach (Transaction t in selected)
            {
                transactionPool.Remove(t);
            }

            return selected;
        }

        public double GetBalance(string address)
        {
            double balance = 0.0;

            foreach (Block b in Blocks)
            {
                foreach (Transaction t in b.transactionList)
                {
                    if (t.recipientAddress.Equals(address))
                        balance += t.amount;

                    if (t.senderAddress.Equals(address))
                        balance -= (t.amount + t.fee);
                }
            }

            return balance;
        }

        public bool validateMerkleRoot(Block b)
        {
            string reMerkle = Block.MerkleRoot(b.transactionList);
            return reMerkle.Equals(b.merkleRoot);
        }

        public void AdjustDifficulty(long actualMiningTimeMs)
        {
            if (actualMiningTimeMs <= 0)
                actualMiningTimeMs = 1;

            double ratio = (double)targetBlockTimeMs / actualMiningTimeMs;
            double adjustmentFactor = 1.0 + ((ratio - 1.0) * 0.25);

            currentDifficultyValue *= adjustmentFactor;

            if (currentDifficultyValue < 1.0)
                currentDifficultyValue = 1.0;

            if (currentDifficultyValue > 6.0)
                currentDifficultyValue = 6.0;
        }

        public override string ToString()
        {
            return string.Join("\n", Blocks);
        }
    }
}

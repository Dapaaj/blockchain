using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace BlockchainAssignment
{
    public partial class BlockchainApp : Form
    {
        private Blockchain blockchain;

        public BlockchainApp()
        {
            InitializeComponent();
            blockchain = new Blockchain();
            richTextBox1.Text = "New Blockchain Initialised!";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;  // Select "Greedy" by default
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index;
            if (!int.TryParse(textBox1.Text, out index))
            {
                richTextBox1.Text = "Enter a valid block index.";
                return;
            }

            if (index < 0 || index >= blockchain.Blocks.Count)
            {
                richTextBox1.Text = "Block index out of range.";
                return;
            }

            richTextBox1.Text = blockchain.GetBlockAsString(index);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string privkey;
            Wallet.Wallet mynewWallet = new Wallet.Wallet(out privkey);
            textBox2.Text = mynewWallet.publicID;
            textBox3.Text = privkey;
            richTextBox1.Text = "New wallet generated.";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Wallet.Wallet.ValidatePrivateKey(textBox3.Text, textBox2.Text))
            {
                richTextBox1.Text = "Keys are valid";
            }
            else
            {
                richTextBox1.Text = "Keys are invalid";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace BlockchainAssignment
{
    public partial class BlockchainApp : Form
    {
        private Blockchain blockchain;

        public BlockchainApp()
        {
            InitializeComponent();
            blockchain = new Blockchain();
            richTextBox1.Text = "New Blockchain Initialised!";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;  // Select "Greedy" by default
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index;
            if (!int.TryParse(textBox1.Text, out index))
            {
                richTextBox1.Text = "Enter a valid block index.";
                return;
            }

            if (index < 0 || index >= blockchain.Blocks.Count)
            {
                richTextBox1.Text = "Block index out of range.";
                return;
            }

            richTextBox1.Text = blockchain.GetBlockAsString(index);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string privkey;
            Wallet.Wallet mynewWallet = new Wallet.Wallet(out privkey);
            textBox2.Text = mynewWallet.publicID;
            textBox3.Text = privkey;
            richTextBox1.Text = "New wallet generated.";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Wallet.Wallet.ValidatePrivateKey(textBox3.Text, textBox2.Text))
            {
                richTextBox1.Text = "Keys are valid";
            }
            else
            {
                richTextBox1.Text = "Keys are invalid";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(recieverkey.Text))
            {
                richTextBox1.Text = "Sender address, private key, and receiver address are required.";
                return;
            }

            double parsedAmount;
            double parsedFee;

            if (!double.TryParse(amount.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out parsedAmount) &&
                !double.TryParse(amount.Text, out parsedAmount))
            {
                richTextBox1.Text = "Enter a valid amount.";
                return;
            }

            if (!double.TryParse(fee.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out parsedFee) &&
                !double.TryParse(fee.Text, out parsedFee))
            {
                richTextBox1.Text = "Enter a valid fee.";
                return;
            }

            if (parsedAmount <= 0)
            {
                richTextBox1.Text = "Amount must be greater than 0.";
                return;
            }

            if (parsedFee < 0)
            {
                richTextBox1.Text = "Fee cannot be negative.";
                return;
            }

            try
            {
                Transaction newTransaction = new Transaction(
                    textBox2.Text,
                    recieverkey.Text,
                    parsedAmount,
                    parsedFee,
                    textBox3.Text
                );

                blockchain.transactionPool.Add(newTransaction);
                richTextBox1.Text = "Transaction added to pool.\n\n" + newTransaction.ToString();
            }
            catch (Exception ex)
            {
                richTextBox1.Text = "Transaction failed: " + ex.Message;
            }
        }

        private async void GenerateNewBlock_Click(object sender, EventArgs e)
        {
            if (blockchain.transactionPool.Count == 0)
            {
                richTextBox1.Text = "No pending transactions to mine.";
                return;
            }

            GenerateNewBlock.Enabled = false;
            button4.Enabled = false;
            richTextBox1.Text = "Mining block... please wait.";

            try
            {
            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(recieverkey.Text))
            {
                richTextBox1.Text = "Sender address, private key, and receiver address are required.";
                return;
            }

            double parsedAmount;
            double parsedFee;

            if (!double.TryParse(amount.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out parsedAmount) &&
                !double.TryParse(amount.Text, out parsedAmount))
            {
                richTextBox1.Text = "Enter a valid amount.";
                return;
            }

            if (!double.TryParse(fee.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out parsedFee) &&
                !double.TryParse(fee.Text, out parsedFee))
            {
                richTextBox1.Text = "Enter a valid fee.";
                return;
            }

            if (parsedAmount <= 0)
            {
                richTextBox1.Text = "Amount must be greater than 0.";
                return;
            }

            if (parsedFee < 0)
            {
                richTextBox1.Text = "Fee cannot be negative.";
                return;
            }

            try
            {
                Transaction newTransaction = new Transaction(
                    textBox2.Text,
                    recieverkey.Text,
                    parsedAmount,
                    parsedFee,
                    textBox3.Text
                );

                blockchain.transactionPool.Add(newTransaction);
                richTextBox1.Text = "Transaction added to pool.\n\n" + newTransaction.ToString();
            }
            catch (Exception ex)
            {
                richTextBox1.Text = "Transaction failed: " + ex.Message;
            }
        }

        private async void GenerateNewBlock_Click(object sender, EventArgs e)
        {
            if (blockchain.transactionPool.Count == 0)
            {
                richTextBox1.Text = "No pending transactions to mine.";
                return;
            }

            GenerateNewBlock.Enabled = false;
            button4.Enabled = false;
            richTextBox1.Text = "Mining block... please wait.";

            try
            {
                if (comboBox1.SelectedItem == null)
                {
                    richTextBox1.Text = "Please select a mining preference.";
                    GenerateNewBlock.Enabled = true;
                    button4.Enabled = true;
                    return;
                }

                string mode = comboBox1.SelectedItem.ToString();
                List<Transaction> transactions = blockchain.GetPendingTransactions(mode, textBox2.Text);
                double oldDifficulty = blockchain.currentDifficultyValue;

                Block newBlock = await Task.Run(() =>
                {
                    Block block = new Block(
                        blockchain.GetLastBlock(),
                        transactions,
                        blockchain.currentDifficultyValue,
                        textBox2.Text
                    );

                    block.hash = block.MineParallel(Environment.ProcessorCount);
                    return block;
                });

                blockchain.Blocks.Add(newBlock);
                blockchain.AdjustDifficulty(newBlock.miningTimeMs);

                double newDifficulty = blockchain.currentDifficultyValue;

                richTextBox1.Text =
                    "Block mined successfully.\n\n" +
                    "Mining Preference: " + mode + "\n" +
                    "Index: " + newBlock.index + "\n" +
                    "Hash: " + newBlock.hash + "\n" +
                    "Nonce: " + newBlock.nonce + "\n" +
                    "Mining Time: " + newBlock.miningTimeMs + " ms\n" +
                    "Target Block Time: " + blockchain.targetBlockTimeMs + " ms\n" +
                    "Old Difficulty: " + oldDifficulty.ToString("F2") + "\n" +
                    "New Difficulty: " + newDifficulty.ToString("F2") + "\n" +
                    "Required Leading Zeros: " + ((int)Math.Round(newDifficulty)) + "\n" +
                    "Transactions: " + newBlock.transactionList.Count;
            }
            catch (Exception ex)
            {
                richTextBox1.Text = "Mining failed: " + ex.Message;
            }
            finally
            {
                GenerateNewBlock.Enabled = true;
                button4.Enabled = true;
            }
        }

        private void amount_TextChanged(object sender, EventArgs e)
        {
            if (!(sender is TextBox tb))
                return;

            const string pattern = @"^-?\d*\.?\d*$";
            bool isValid = Regex.IsMatch(tb.Text, pattern);
            tb.BackColor = isValid ? SystemColors.Window : Color.MistyRose;
        }

        private void reciever_TextChanged(object sender, EventArgs e)
        {
            if (!(sender is TextBox tb))
                return;

            bool hasText = !string.IsNullOrWhiteSpace(tb.Text);
                if (comboBox1.SelectedItem == null)
                {
                    richTextBox1.Text = "Please select a mining preference.";
                    GenerateNewBlock.Enabled = true;
                    button4.Enabled = true;
                    return;
                }

                string mode = comboBox1.SelectedItem.ToString();
                List<Transaction> transactions = blockchain.GetPendingTransactions(mode, textBox2.Text);
                double oldDifficulty = blockchain.currentDifficultyValue;

                Block newBlock = await Task.Run(() =>
                {
                    Block block = new Block(
                        blockchain.GetLastBlock(),
                        transactions,
                        blockchain.currentDifficultyValue,
                        textBox2.Text
                    );

                    block.hash = block.MineParallel(Environment.ProcessorCount);
                    return block;
                });

                blockchain.Blocks.Add(newBlock);
                blockchain.AdjustDifficulty(newBlock.miningTimeMs);

                double newDifficulty = blockchain.currentDifficultyValue;

                richTextBox1.Text =
                    "Block mined successfully.\n\n" +
                    "Mining Preference: " + mode + "\n" +
                    "Index: " + newBlock.index + "\n" +
                    "Hash: " + newBlock.hash + "\n" +
                    "Nonce: " + newBlock.nonce + "\n" +
                    "Mining Time: " + newBlock.miningTimeMs + " ms\n" +
                    "Target Block Time: " + blockchain.targetBlockTimeMs + " ms\n" +
                    "Old Difficulty: " + oldDifficulty.ToString("F2") + "\n" +
                    "New Difficulty: " + newDifficulty.ToString("F2") + "\n" +
                    "Required Leading Zeros: " + ((int)Math.Round(newDifficulty)) + "\n" +
                    "Transactions: " + newBlock.transactionList.Count;
            }
            catch (Exception ex)
            {
                richTextBox1.Text = "Mining failed: " + ex.Message;
            }
            finally
            {
                GenerateNewBlock.Enabled = true;
                button4.Enabled = true;
            }
        }

        private void amount_TextChanged(object sender, EventArgs e)
        {
            if (!(sender is TextBox tb))
                return;

            const string pattern = @"^-?\d*\.?\d*$";
            bool isValid = Regex.IsMatch(tb.Text, pattern);
            tb.BackColor = isValid ? SystemColors.Window : Color.MistyRose;
        }

        private void reciever_TextChanged(object sender, EventArgs e)
        {
            if (!(sender is TextBox tb))
                return;

            bool hasText = !string.IsNullOrWhiteSpace(tb.Text);
            tb.BackColor = hasText ? SystemColors.Window : Color.MistyRose;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (blockchain.Blocks.Count == 1)
            {
                richTextBox1.Text = "Blockchain is valid.";
                return;
            }

            for (int i = 1; i < blockchain.Blocks.Count; i++)
            {
                if (blockchain.Blocks[i].prevHash != blockchain.Blocks[i - 1].hash)
                {
                    richTextBox1.Text = "Blockchain is invalid.";
                    return;
                }

                if (!blockchain.validateMerkleRoot(blockchain.Blocks[i]))
                {
                    richTextBox1.Text = "Blockchain is invalid: Merkle root mismatch at block " + i;
                    return;
                }
            }

            richTextBox1.Text = "Blockchain is valid.";
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                richTextBox1.Text = "Enter a wallet address first.";
                return;
            }

            richTextBox1.Text = blockchain.GetBalance(textBox2.Text).ToString("F4") + " Assignment Coin";
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            GenerateNewBlock.Enabled = false;
            button4.Enabled = false;
            richTextBox1.Text = "Running Task 1 benchmark...";

            try
            {
                string result = await Task.Run(() =>
                {
                    int difficulty = 4;
                    int samples = 5;
                    int processorCount = Environment.ProcessorCount;

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Task 1: Proof-of-Work Benchmark");
                    sb.AppendLine("--------------------------------");
                    sb.AppendLine("Difficulty: " + difficulty);
                    sb.AppendLine("Samples per test: " + samples);
                    sb.AppendLine("Processor count: " + processorCount);
                    sb.AppendLine();

                    sb.AppendLine(RunBenchmarkSet(difficulty, samples, 1));

                    if (processorCount >= 2)
                        sb.AppendLine(RunBenchmarkSet(difficulty, samples, 2));

                    if (processorCount >= 4)
                        sb.AppendLine(RunBenchmarkSet(difficulty, samples, 4));

            tb.BackColor = hasText ? SystemColors.Window : Color.MistyRose;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (blockchain.Blocks.Count == 1)
            {
                richTextBox1.Text = "Blockchain is valid.";
                return;
            }

            for (int i = 1; i < blockchain.Blocks.Count; i++)
            {
                if (blockchain.Blocks[i].prevHash != blockchain.Blocks[i - 1].hash)
                {
                    richTextBox1.Text = "Blockchain is invalid.";
                    return;
                }

                if (!blockchain.validateMerkleRoot(blockchain.Blocks[i]))
                {
                    richTextBox1.Text = "Blockchain is invalid: Merkle root mismatch at block " + i;
                    return;
                }
            }

            richTextBox1.Text = "Blockchain is valid.";
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                richTextBox1.Text = "Enter a wallet address first.";
                return;
            }

            richTextBox1.Text = blockchain.GetBalance(textBox2.Text).ToString("F4") + " Assignment Coin";
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            GenerateNewBlock.Enabled = false;
            button4.Enabled = false;
            richTextBox1.Text = "Running Task 1 benchmark...";

            try
            {
                string result = await Task.Run(() =>
                {
                    int difficulty = 4;
                    int samples = 5;
                    int processorCount = Environment.ProcessorCount;

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Task 1: Proof-of-Work Benchmark");
                    sb.AppendLine("--------------------------------");
                    sb.AppendLine("Difficulty: " + difficulty);
                    sb.AppendLine("Samples per test: " + samples);
                    sb.AppendLine("Processor count: " + processorCount);
                    sb.AppendLine();

                    sb.AppendLine(RunBenchmarkSet(difficulty, samples, 1));

                    if (processorCount >= 2)
                        sb.AppendLine(RunBenchmarkSet(difficulty, samples, 2));

                    if (processorCount >= 4)
                        sb.AppendLine(RunBenchmarkSet(difficulty, samples, 4));

                    if (processorCount > 1)
                        sb.AppendLine(RunBenchmarkSet(difficulty, samples, processorCount));

                    return sb.ToString();
                });

                richTextBox1.Text = result;
            }
            catch (Exception ex)
            {
                richTextBox1.Text = "Benchmark failed: " + ex.Message;
            }
            finally
            {
                GenerateNewBlock.Enabled = true;
                button4.Enabled = true;
            }
        }

        private string RunBenchmarkSet(int difficulty, int sampleCount, int threadCount)
        {
            StringBuilder sb = new StringBuilder();
            long totalMs = 0;

            sb.AppendLine("Threads: " + threadCount);

            for (int i = 1; i <= sampleCount; i++)
            {
                Block testBlock = new Block();
                testBlock.difficultyValue = difficulty;

                Stopwatch sw = Stopwatch.StartNew();

                if (threadCount == 1)
                {
                    testBlock.hash = testBlock.MineSingleThread();
                }
                else
                {
                    testBlock.hash = testBlock.MineParallel(threadCount);
                }

                sw.Stop();
                totalMs += sw.ElapsedMilliseconds;

                sb.AppendLine("Run " + i + ": " + sw.ElapsedMilliseconds + " ms, nonce=" + testBlock.nonce);
            }

            double average = totalMs / (double)sampleCount;
            sb.AppendLine("Average: " + average.ToString("F2") + " ms");
            sb.AppendLine();

            return sb.ToString();
        }
    }
}

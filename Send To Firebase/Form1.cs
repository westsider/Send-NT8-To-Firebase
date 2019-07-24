using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Send_To_Firebase
{
    public partial class Form1 : Form
    {
        static readonly string rootFolder = @"C:\Users\trade\OneDrive\Documents\NinjaTrader 8\";
        static readonly string textFile = @"C:\Users\trade\OneDrive\Documents\NinjaTrader 8\log\log.20190723.00001.txt";
        List<double> entries = new List<double>();  // entries.Add(value);
        List<double> exits = new List<double>();
        List<double> tradeResult = new List<double>(); 
        private int mp = 0;
        private int tradeCount = 0;
        private string strategy = "None";
        private string date = "None";
        private string time = "None";
        private string action = "None";
        private string state = "None";
        private double TickSize = 0.25;
        string account = "none";
        string instrument = "None";

        public Form1()
        {
            InitializeComponent();
        }

        private void ReadFileBttn_Click(object sender, EventArgs e)
        {
            ReadFile();
        }

        private void ReadFile()
        {
            
            if (File.Exists(textFile))
            {
                // Read file using StreamReader. Reads file line by line
                using (StreamReader file = new StreamReader(textFile))
                {
                    int counter = 0;
                    string ln;

                    while ((ln = file.ReadLine()) != null)
                    { 
                        char[] delimiterChars = { ' ', ',', '\t', '|' }; //'.', ':'
                        string[] words = ln.Split(delimiterChars);
                        string orders = "32";
                        string flat = "64";

                        //if (words[3] == flat)
                        //{
                        //    //mp = 0;
                        //    //richTextBox1.AppendText("Flat");
                        //}

                        if (words[3] == orders) {
                            //richTextBox1.AppendText("\n\n");
                            //foreach (var word in words)
                            //{
                            //    richTextBox1.AppendText($"<{word}>");
                            //}

                            ParseOrder(words);
                            
                        }
                        counter++;

                        
                    }
                    file.Close();
                }
            }

        }

        /*
         *  [X] orders == 32 && name = SuperDOM
	            get dom strategy

            [X] Name='Entry' &&  state='Filled' 
	            loop thru contracts for entry List
                <2019-07-23><11:39:06:665><1><32><Order='1945737690/Y4398'><Name='Entry'><New><state='Filled'><Instrument='MES><09-19'><Action='Buy'><Limit><price=2992.5><Stop><price=0><Quantity=3><Type='Limit'><Time><in><force=GTC><Oco=''><Filled=3><Fill><price=2992.5><Error='No><error'><Native><error=''>

            [X] Name='Target1' && state='Filled'
	            add to exit list
                2019-07-23 11:10:46:079|1|32|Order='1945574946/Y4398' Name='Target1' New state='Filled' Instrument='MES 09-19' Action='Sell' Limit price=2994.25 Stop price=0 Quantity=1 Type='Limit' Time in force=GTC Oco='8dafd3f17009429a9734a8cb72ccdfd0' Filled=1 Fill price=2994.25 Error='No error' Native error=''
                2019-07-23 11:15:12:520|1|32|Order='1945574947/Y4398' Name='Target2' New state='Filled' Instrument='MES 09-19' Action='Sell' Limit price=2995 Stop price=0 Quantity=1 Type='Limit' Time in force=GTC Oco='68b5b569df46428e9b1fb1e3850f53e9' Filled=1 Fill price=2995 Error='No error' Native error=''
                2019-07-23 11:17:03:834|1|32|Order='1945574948/Y4398' Name='Target3' New state='Filled' Instrument='MES 09-19' Action='Sell' Limit price=2995.75 Stop price=0 Quantity=1 Type='Limit' Time in force=GTC Oco='c209b92f908544f3a5d99ede8b833885' Filled=1 Fill price=2995.75 Error='No error' Native error=''

                2nd trade 
                <2019-07-23><11:39:06:665><1><32><Order='1945737690/Y4398'><Name='Entry'><New><state='Filled'><Instrument='MES><09-19'><Action='Buy'><Limit><price=2992.5><Stop><price=0><Quantity=3><Type='Limit'><Time><in><force=GTC><Oco=''><Filled=3><Fill><price=2992.5><Error='No><error'><Native><error=''>

                T1
                <2019-07-23><11:39:32:841><1><32><Order='1945845341/Y4398'><Name='Target1'><New><state='Filled'><Instrument='MES><09-19'><Action='Sell'><Limit><price=2993.25><Stop><price=0><Quantity=1><Type='Limit'><Time><in><force=GTC><Oco='31a247382aeb46688c13331005205e8a'><Filled=1><Fill><price=2993.25><Error='No><error'><Native><error=''>

                Stop
                <2019-07-23><11:42:02:820><1><32><Order='1945649931/Y4398'><Name='Stop2'><New><state='Filled'><Instrument='MES><09-19'><Action='Sell'><Limit><price=0><Stop><price=2992.5><Quantity=1><Type='Stop><Market'><Time><in><force=GTC><Oco='4785e6e564dc4f1ab6d900e34e7a569e'><Filled=1><Fill><price=2992.5><Error='No><error'><Native><error=''>
                <2019-07-23><11:42:02:820><1><32><Order='1945649931/Y4398'><Name='Stop2'><New><state='Filled'><Instrument='MES><09-19'><Action='Sell'><Limit><price=0><Stop><price=2992.5><Quantity=1><Type='Stop><Market'><Time><in><force=GTC><Oco='4785e6e564dc4f1ab6d900e34e7a569e'><Filled=1><Fill><price=2992.5><Error='No><error'><Native><error=''>
            [X] Name='Stop1' && state='Filled'
	            add to exit list
                --->	Name='Stop2'
 				--->	state='Filled'
 				--->	Name='Stop3'
 				--->	state='Filled'

            [X] if exit list == entry list then
	            calc profit based on mp 
                show on UI

            [ ] version control
            [ ] refactor for repeated if target stop ect
            [ ] short trade

            [ ] sent result to firebase

            [ ] detect filename
                detect change in file and run parse trades
    */

        private void ParseOrder(String[] words)
        {
   
           if (words[5] == "SuperDOM")
            {
                richTextBox1.AppendText("\n\t\t\t\t---> Set Strategy to " + words[10] + " <-----\n");
                strategy = words[10];
                return;
            }
            date = words[0];
            time = words[1]; 
            action = ParseInSingleQuotes(words[5]); // entry
            state = ParseInSingleQuotes(words[7]);  // filled
            if ( state != "Filled") { return; }
            //richTextBox1.AppendText("\n \t\t\t\t--->\t");
            richTextBox1.AppendText(action);
            //richTextBox1.AppendText("\n \t\t\t\t--->\t");
            richTextBox1.AppendText(state);

            if (action == "Entry" && state == "Filled")
            {
                ParseAllExecutions(words);
            }

            if ((action == "Target1" || action == "Target2" || action == "Target3" || action == "Stop1" || action == "Stop2" || action == "Stop3") && (state == "Filled"))
            {
                    richTextBox1.AppendText("\n---> Exit found " + words[5] + " action " + action + " <-----\n");
                    ParseAllExecutions(words);
                }

            // MARK: -TODO - add calc totals clear array
        }

        private void ParseAllExecutions(String[] words)
        {
            //richTextBox1.AppendText("\n\n\n\n \t\t\t\t---> Entery filled! <-----\n\n\n\n");
            string justAccount = ParseInSingleQuotes(words[4]);
            account = justAccount.Substring(justAccount.IndexOf('/') + 1);
            instrument = words[8].Substring(words[8].IndexOf("'") + 1);
            string quantity = words[15].Substring(9);
            int quantityInt = int.Parse(quantity);

            string price = words[12].Substring(6); // Price=2993.25 if stop its 14
            if ( action == "Stop1" || action == "Stop2" || action == "Stop3")
            {
                price = words[14].Substring(6);
            }

            double priceD = double.Parse(price, System.Globalization.CultureInfo.InvariantCulture);
            string marketPosition = ParseInSingleQuotes(words[10]); // Action='Buy'
            if ((action == "Entry" && state == "Filled") || ((action == "Target1" || action == "Target2" || action == "Target3" || action == "Stop1" || action == "Stop2" || action == "Stop3") && (state == "Filled")))
            {
                ProcessExecutions(priceD: priceD, quantity: quantityInt, marketPos: marketPosition, action: action);
            }

            richTextBox1.AppendText("\n---------------------------------------------------------------------------------------------------\n");
            string message = (string.Format("{0}  {1} \t {2} {3} {4} \tPrice: {5} Quant: {6} \tMP: {7} \tStrategy: {8}",
                    date,
                    time,
                    account,
                    action,
                    instrument,
                    priceD,
                    quantityInt,
                    marketPosition,
                    strategy
                    ));
            richTextBox1.AppendText(message);
            richTextBox1.AppendText("\n----------------------------------------------------------------------------------------------------\n");

        }

        private void ProcessExecutions(double priceD, int quantity, string marketPos, string action)
        {
            // enter long
            if (marketPos == "Buy" && action == "Entry") {
                mp = 1;
                for (int i = 0; i < quantity; i++)
                {
                        entries.Add(priceD); 
                }
                richTextBox1.AppendText(marketPos + " added to entry array: \t" + String.Join("; ", entries) + "\n");
            }
            // exit long targets or stops
            if ((marketPos == "Sell" &&( action == "Target1" || action == "Target2" || action == "Target3" || action == "Stop1" || action == "Stop2" || action == "Stop3"))) {
                
                for (int i = 0; i < quantity; i++)
                {
                    exits.Add(priceD);
                }
                richTextBox1.AppendText(marketPos + " added to exit array: \t" + String.Join("; ", exits) + "\n");
            }

            // MARK: - TODO - short entry
            TradeExit();
        }

        private void TradeExit()
        {
            if (entries.Count != exits.Count)
            {
                return;
            }
            // loop through trade arrays and clac gain & loss
            for (int index = 0; index < entries.Count; index++)
            {
                if (mp == 1)
                {
                    tradeResult.Add(exits[index] - entries[index]);
                }
                if (mp == -1)
                {
                    tradeResult.Add(entries[index] - exits[index]);
                }
            }
            string direction = "Long";
            if (mp == -1) { direction = "Short"; }
            richTextBox1.AppendText("record " + direction + " exit: \t" + String.Join("; ", tradeResult));
            double tradeSum = tradeResult.Sum();
            double ticks = tradeSum / TickSize; 
            int contracts = entries.Count();
            tradeCount += 1;
            Guid obj = Guid.NewGuid();

            richTextBox1.AppendText("\n------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\n");
            richTextBox1.AppendText("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\n");
            string message = (string.Format("{0} {1} {2} Direction: {3} Gain: {4} Ticks: {5} Contracts: {6} Trade #: {7} uuid {8} strat {9}",
                date,
                time,
                instrument,
                mp,
                tradeSum.ToString(),
                ticks.ToString(),
                contracts.ToString(),
                tradeCount.ToString(),
                obj,
                strategy
                ));
            richTextBox1.AppendText(message);
            richTextBox1.AppendText("\n------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\n");
            richTextBox1.AppendText("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\n");

            entries.Clear();
            exits.Clear();
            tradeResult.Clear();
            mp = 0;

        }
        private string ParseInSingleQuotes(String word)
        {
            int start = word.IndexOf("'") + 1;
            int end = word.IndexOf("'", start);
            // richTextBox1.AppendText("start " + start + " end " + end + "\n");
            string answer = "Parse Fail on " + word;
            if (start > end)
            {
                return answer;
            }
            else
            {
                return word.Substring(start, end - start);
            }
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}

using System.Text.Json;
using System;

namespace CheckMatches
{
    public class Programa
    {

        /// <summary>
        /// Represents a single bet with its row number and selected numbers.
        /// </summary>
        public class Bets
        {
            /// <summary>
            /// Row number of the bet.
            /// </summary>
            public int Row { get; set; }

            /// <summary>
            /// List of numbers in the bet.
            /// </summary>
            public List<int>? Numbers { get; set; }
        }

        /// <summary>
        /// Report for bet result operations, including metadata and results summary.
        /// </summary>
        public class BetReport
        {
            public string DevelopedBy { get; set; } = "Nucleus MAP, Machines, Automation, and Programming";
            public string DevelopedOnDate { get; set; } = "Monday, December 09, 2024";
            public string DeveloperURL { get; set; } = "http://nucleomap.com.br";
            public string DeveloperEmail { get; set; } = "nucleomap@nucleomap.com.br";
            public string Version { get; set; } = "1.0";
            public string GeneratedBy { get; set; } = Environment.UserName;
            public string GeneratedOnDate { get; set; } = DateTimeOffset.Now.ToString("F");
            public string StationName { get; set; } = Environment.MachineName;
            public string OSVersion { get; set; } = Environment.OSVersion.ToString();

            /// <summary>
            /// Total number of results processed.
            /// </summary>
            public int TotalResults { get; set; }

            /// <summary>
            /// Total number of exact matches (all numbers matched).
            /// </summary>
            public int TotalMatches { get; set; }

            /// <summary>
            /// List of bets with matched numbers.
            /// </summary>
            public List<Bets> BettingResult { get; set; } = [];
        }

        /// <summary>
        /// Entry point of the program.
        /// Loads bets, compares them with the game result, and generates a report.
        /// </summary>
        public static void Main(string[] args)
        {
            string startFolder = "Bets";
            // Console.Clear();
            DirectoryInfo dir = new(startFolder);
            var fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);
            var fileQuery = from file in fileList
                            where file.Extension == ".json"
                            orderby file.Name
                            select file;

            foreach (FileInfo fi in fileQuery)
            {
                // Load bets from a JSON file
                List<Bets> bets = LoadBets(fi.FullName);

                // Define the winning numbers
                List<int> gameResult = [3, 12, 19, 26, 33, 47];

                // Initialize results
                List<Bets> bettingResult = [];
                int match = 0;
                string outFileName = "Results/MatchResult" + fi.Name;


                // Process each bet
                foreach (var bet in bets!)
                {
                    // Compare bet numbers with the game result
                    List<int> matchNumbers = CompareNumbers(bet, gameResult);

                    if (matchNumbers.Count > 0)
                    {
                        // Add bet to the result list if numbers matched
                        bettingResult.Add(new Bets
                        {
                            Row = bet.Row,
                            Numbers = matchNumbers,
                        });

                        Console.WriteLine($"Result in row {bet.Row} matches these numbers: {string.Join(", ", matchNumbers)}");
                    }
                    else
                    {
                        Console.WriteLine($"Result in row {bet.Row} doesn't match any number.");
                    }

                    // Increment the match counter for perfect matches
                    if (matchNumbers.Count == gameResult.Count)
                    {
                        match++;
                    }
                }

                Console.WriteLine($"\nTotal of winning bets: {match} in {fi.Name}\n");

                // Save results as JSON
                SaveDeleteResultsAsJson(new BetReport
                {
                    TotalResults = bettingResult.Count,
                    TotalMatches = match,
                    BettingResult = bettingResult
                }, outFileName);
            }
        }

        /// <summary>
        /// Saves the betting results to a JSON file.
        /// </summary>
        /// <param name="bets">BetReport containing all results and metadata.</param>
        public static void SaveDeleteResultsAsJson(BetReport bets, string outFileName)
        {
            string fileName = string.Concat(outFileName, ".json");
            string json = JsonSerializer.Serialize(bets);
            File.WriteAllText(fileName, json);
        }

        /// <summary>
        /// Loads bets from a JSON file.
        /// </summary>
        /// <param name="fileName">Path to the JSON file containing the bets.</param>
        /// <returns>List of bets loaded from the file.</returns>
        public static List<Bets> LoadBets(string fileName)
        {
            string json = File.ReadAllText(fileName);
            Dictionary<string, List<Bets>>? bets = JsonSerializer.Deserialize<Dictionary<string, List<Bets>>>(json);
            return bets!["Bets"];
        }

        /// <summary>
        /// Compares a bet's numbers with the game result and returns the matched numbers.
        /// </summary>
        /// <param name="bet">A bet to be compared.</param>
        /// <param name="gameResult">List of winning numbers.</param>
        /// <returns>List of numbers matched with the game result.</returns>
        public static List<int> CompareNumbers(Bets bet, List<int> gameResult)
        {
            List<int> matchedBets = [];

            // Check each number in the bet against the game result
            foreach (var number in bet!.Numbers!)
            {
                if (gameResult.Contains(number))
                {
                    matchedBets.Add(number);
                }
            }
            return matchedBets;
        }
    }
}

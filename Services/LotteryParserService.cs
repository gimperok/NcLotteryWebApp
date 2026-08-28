using HtmlAgilityPack;
using NcLotteryWebApp.Models;

namespace NcLotteryWebApp.Services
{
    public class LotteryParserService
    {
        private readonly HttpClient _httpClient;
        public LotteryParserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
        }

        private async Task<LotteryResult> ParsePowerballAsync(string lotteryType)
        {
            string type = lotteryType.ToLower();

            string url = "https://nclottery.com";
            string name;
            string bonusName;
            string whiteBallsIdPattern = "ctl00_MainContent_lbl";
            string bonusBallId = "ctl00_MainContent_lbl";
            string jackpotXPath = "ctl00_MainContent_Header";

            List<int> fallbackNumbers = new List<int> { 0, 0, 0, 0, 0 };
            int fallbackBonus = 0;
            string fallbackJackpot = "$0";

            if (type == "1" || type == "powerball")
            {
                url = $"{url}/powerball";
                name = "Powerball";
                bonusName = "Power";
                whiteBallsIdPattern = $"{whiteBallsIdPattern}Ball";
                bonusBallId = $"{bonusBallId}Powerball";
                jackpotXPath = $"//span[@id='{jackpotXPath}Powerball1_JackpotPowerball_lblPBJackpot']";
            }
            else if (type == "2" || type == "megamillions")
            {
                url = $"{url}/mega-millions";
                name = "Mega Millions";
                bonusName = "Mega";
                whiteBallsIdPattern = $"{whiteBallsIdPattern}Num";
                bonusBallId = $"{bonusBallId}Megaball";
                jackpotXPath = $"//span[@id='{jackpotXPath}MegaMillions_JackpotMegaMillions_lblMMPrize']";
            }
            else return null;

            try
            {
                var html = await _httpClient.GetStringAsync(url);
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var winningNumbers = new List<int>();
                int bonusNumber = 0;
                string jackpot = "Unknown";

                // Fill in white balls
                for (int i = 1; i <= 5; i++)
                {
                    var numNode = doc.DocumentNode.SelectSingleNode($"//span[@id='{whiteBallsIdPattern}{i}']");
                    if (numNode != null && int.TryParse(numNode.InnerText.Trim(), out int val))
                    {
                        winningNumbers.Add(val);
                    }
                }

                // Fill in bonus ball
                var bonusNode = doc.DocumentNode.SelectSingleNode($"//span[@id='{bonusBallId}']");
                if (bonusNode != null)
                {
                    int.TryParse(bonusNode.InnerText.Trim(), out bonusNumber);
                }


                // Fill  in Jackpot
                var jackpotNode = doc.DocumentNode.SelectSingleNode(jackpotXPath);
                if (jackpotNode != null)
                    jackpot = jackpotNode.InnerText.Trim();



                if (winningNumbers.Count == 5)
                {
                    return new LotteryResult(name, winningNumbers, bonusNumber, bonusName, jackpot, isArchiveData: true);
                }
            }
            catch 
            {
                //TODO: Add logging!!!!
            }

            return new LotteryResult(name, fallbackNumbers, fallbackBonus, fallbackJackpot, isArchiveData: true);
        }
    }
}
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
        public async Task<LotteryResult> ParseArchiveDataAsync(Lottery lottery)
        {
            string url = "https://nclottery.com";
            string whiteBallsIdPattern = "ctl00_MainContent_lbl";
            string bonusBallId = "ctl00_MainContent_lbl";
            string jackpotXPath = "ctl00_MainContent_Header";

            if (lottery is PowerballLottery)
            {
                url = $"{url}/powerball";
                whiteBallsIdPattern = $"{whiteBallsIdPattern}Ball";
                bonusBallId = $"{bonusBallId}Powerball";
                jackpotXPath = $"//span[@id='{jackpotXPath}Powerball1_JackpotPowerball_lblPBJackpot']";
            }
            else if (lottery is MegaMillionsLottery)
            {
                url = $"{url}/mega-millions";
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
                        winningNumbers.Add(val);
                }

                // Fill in bonus ball
                var bonusNode = doc.DocumentNode.SelectSingleNode($"//span[@id='{bonusBallId}']");
                if (bonusNode != null)
                    int.TryParse(bonusNode.InnerText.Trim(), out bonusNumber);

                // Fill  in Jackpot
                var jackpotNode = doc.DocumentNode.SelectSingleNode(jackpotXPath);
                if (jackpotNode != null)
                    jackpot = jackpotNode.InnerText.Trim();

                if (winningNumbers.Count == 5)
                    return new LotteryResult(lottery.Name, winningNumbers, bonusNumber, lottery.BonusBallName, jackpot, isArchiveData: true);
            }
            catch
            {
                //TODO: Add logging!!!!
            }
            return null;
        }
    }
}
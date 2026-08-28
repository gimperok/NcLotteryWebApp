namespace NcLotteryWebApp.Models
{
    public class LotteryResult
    {
        public string LotteryName { get; set; }
        public List<int> MainNumbers { get; set; }
        public int BonusNumber { get; set; }
        public string BonusName { get; set; }

        public LotteryResult(string lotteryName, List<int> mainNumbers, int bonusNumber, string bonusName)
        {
            LotteryName = lotteryName;
            MainNumbers = mainNumbers;
            BonusNumber = bonusNumber;
            BonusName = bonusName;
        }
    }
}

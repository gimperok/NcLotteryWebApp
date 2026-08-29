namespace NcLotteryWebApp.Models
{
    public class LotteryResult
    {
        public LotteryType Type { get; set; }
        public string LotteryName { get; set; }
        public List<int> MainNumbers { get; set; }
        public int BonusNumber { get; set; }
        public string BonusName { get; set; }
        public  string? Jackpot { get; set; } // Null for generation, populated for archive
        public bool IsArchiveData { get; set; } // Flag: this is an archive or a generated ticket.

        public LotteryResult(LotteryType lotteryType, string lotteryName, List<int> mainNumbers, int bonusNumber, 
            string bonusName, string? jackpot = null, bool isArchiveData = false)
        {
            Type = lotteryType;
            LotteryName = lotteryName;
            MainNumbers = mainNumbers;
            BonusNumber = bonusNumber;
            BonusName = bonusName;
            Jackpot = jackpot;
            IsArchiveData = isArchiveData;
        }
    }
}

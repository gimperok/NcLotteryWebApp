using NcLotteryWebApp.Models;

namespace NcLotteryWebApp.Services.Factories
{
    public class LotteryFactory
    {
        public Lottery? CreateLottery(LotteryType lotteryType)
        {
            return lotteryType switch
            {
                LotteryType.Powerball => new PowerballLottery(),
                LotteryType.MegaMillions => new MegaMillionsLottery(),
                _ => throw new ArgumentException($"Unsupported lottery type: {lotteryType}", nameof(lotteryType))
            };
        }
    }
}
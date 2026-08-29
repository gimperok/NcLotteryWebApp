using NcLotteryWebApp.Models;

namespace NcLotteryWebApp.Services.Factories
{
    public class LotteryFactory
    {
        public Lottery? CreateLottery(string lotteryType)
        {
            return lotteryType.ToLower() switch
            {
                "1" or "powerball" => new PowerballLottery(),
                "2" or "megamillions" => new MegaMillionsLottery(),
                _ => null
            };
        }
    }
}

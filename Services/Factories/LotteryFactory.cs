using NcLotteryWebApp.Models;

namespace NcLotteryWebApp.Services.Factories
{
    public class LotteryFactory
    {
        public Lottery? CreateLottery(string choice)
        {
            return choice.ToLower() switch
            {
                "1" or "powerball" => new PowerballLottery(),
                "2" or "megamillions" => new MegaMillionsLottery(),
                _ => null
            };
        }
    }
}

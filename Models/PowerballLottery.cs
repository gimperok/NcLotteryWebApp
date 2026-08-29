namespace NcLotteryWebApp.Models
{
    public class PowerballLottery : Lottery
    {
        public override LotteryType Type => LotteryType.Powerball;
        public override string Name => "Powerball";
        protected override int MainNumbersCount => 5;
        protected override int MaxMainNumber => 69;
        protected override int MaxBonusNumber => 26;
        public override string BonusBallName => "Powerball";

        public override string UrlSuffix => "powerball";
        public override string WhiteBallsIdPattern => "ctl00_MainContent_lblBall";
        public override string BonusBallId => "ctl00_MainContent_lblPowerball";
        public override string JackpotXPath => "//span[@id='ctl00_MainContent_HeaderPowerball_JackpotPowerball_lblPBJackpot' " +
                                                   "or @id='ctl00_MainContent_HeaderPowerball1_JackpotPowerball_lblPBJackpot']";
    }
}

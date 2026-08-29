namespace NcLotteryWebApp.Models
{
    public class MegaMillionsLottery : Lottery
    {
        public override LotteryType Type => LotteryType.MegaMillions;
        public override string Name => "MegaMillions";
        protected override int MainNumbersCount => 5;
        protected override int MaxMainNumber => 70;
        protected override int MaxBonusNumber => 25;
        public override string BonusBallName => "Mega Ball";

        public override string UrlSuffix => "mega-millions";
        public override string WhiteBallsIdPattern => "ctl00_MainContent_lblNum";
        public override string BonusBallId => "ctl00_MainContent_lblMegaball";
        public override string JackpotXPath => "//span[@id='ctl00_MainContent_HeaderMegaMillions_JackpotMegaMillions_lblMMPrize']";
    }
}
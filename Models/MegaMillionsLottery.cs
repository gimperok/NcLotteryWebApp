namespace NcLotteryWebApp.Models
{
    public class MegaMillionsLottery : Lottery
    {
        public override string Name => "MegaMillions";
        protected override int MainNumbersCount => 5;
        protected override int MaxMainNumber => 70;
        protected override int MaxBonusNumber => 25;
        public override string BonusBallName => "Mega Ball";
    }
}

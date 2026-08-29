namespace NcLotteryWebApp.Models
{
    public class PowerballLottery : Lottery
    {
        public override string Name => "Powerball";
        protected override int MainNumbersCount => 5;
        protected override int MaxMainNumber => 69;
        protected override int MaxBonusNumber => 26;
        public override string BonusBallName => "Powerball";
    }
}

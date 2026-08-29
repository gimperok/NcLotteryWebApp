namespace NcLotteryWebApp.Models
{
    public abstract class Lottery
    {
        public abstract string Name { get; }
        public abstract string BonusBallName { get; }
        protected abstract int MainNumbersCount { get; }
        protected abstract int MaxMainNumber { get; }
        protected abstract int MaxBonusNumber { get; }

        private readonly Random _random = new Random();

        public LotteryResult GenerateTicket()
        {
            var mainNumbers = new HashSet<int>();

            while (mainNumbers.Count < MainNumbersCount)
            {
                mainNumbers.Add(_random.Next(1, MaxMainNumber + 1));
            }

            int bonusNumber = _random.Next(1, MaxBonusNumber + 1);

            return new LotteryResult(Name, mainNumbers.OrderBy(n => n).ToList(), bonusNumber, BonusBallName);
        }

    }
}

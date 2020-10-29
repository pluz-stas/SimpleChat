namespace SimpleChat.Shared.Contracts
{
    public class Pagination
    {
        private const int MinTop = 20;
        private const int MaxTop = 100;
        private const int MinSkip = 0;
        private const int MaxSkip = int.MaxValue;

        private int skip = MinSkip;
        private int top = MinTop;

        public int Skip 
        { 
            get => skip;
            set 
            {
                if (value < MinSkip)
                    skip = MinSkip;
                else if (value > MaxSkip)
                    skip = MaxSkip;
                else
                    skip = value;
            }
        }

        public int Top
        {
            get => top;
            set
            {
                if (value < MinTop)
                    top = MinTop;
                else if (value > MaxTop)
                    top = MaxTop;
                else
                    top = value;
            }
        }
    }
}

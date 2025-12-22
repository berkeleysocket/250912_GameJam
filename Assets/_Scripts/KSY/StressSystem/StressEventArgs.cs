namespace Ksy.Scripts.StressSystem
{
    public struct StressEventArgs
    {
        public StressEventArgs (int currnetValue, int applyValue)
        {
            this.currnetValue = currnetValue;
            this.applyValue = applyValue;
        }
        public int currnetValue;
        public int applyValue;
    }
}
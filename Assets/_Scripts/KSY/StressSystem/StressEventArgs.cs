namespace Ksy.Scripts.StressSystem
{
    public struct StressEventArgs
    {
        public StressEventArgs (int currnetValue, int applyValue)
        {
            this.currnetValue = currnetValue;
            this.applyValue = applyValue;
        }
        int currnetValue;
        int applyValue;
    }
}
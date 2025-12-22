
namespace Ksy.Scripts.TraceSystem
{
    public interface ITraceReactive
    {
        public void Reactive()
        {
            TraceReactiveEffect();
        }
        public void TraceReactiveEffect();
    }
}
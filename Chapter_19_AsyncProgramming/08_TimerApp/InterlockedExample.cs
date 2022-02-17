using System;
using System.Threading;

namespace _08_TimerApp
{
    public class InterlockedExample
    {
        private static int _incValue = 0;

        public int IncValue => _incValue;
        public void AddOne() => _incValue = Interlocked.Increment(ref _incValue);

        public void SubOne() => _incValue = Interlocked.Decrement(ref _incValue);

        public void SafeExchange() => Interlocked.Exchange(ref _incValue, 82);
    }
}

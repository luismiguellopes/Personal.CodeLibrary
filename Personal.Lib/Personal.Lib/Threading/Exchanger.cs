using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Personal.Lib.Threading
{
    public sealed class Exchanger<T>
    {
        private T _t;
        private bool _hasValue;
        private SemaphoreSlim _semaphore = new SemaphoreSlim(2);

        public T Exchange(T t)
        {
            _semaphore.Wait();
            lock (this)
            {
                if (_hasValue)
                {
                    T taux = _t;
                    _t = t;
                    Monitor.Pulse(this);
                    return taux;
                }
                else
                {
                    _t = t;
                    _hasValue = true;
                    Monitor.Wait(this);
                    _hasValue = false;
                    _semaphore.Release(2);
                    return _t;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;

namespace Personal.Lib.Threading
{
    public class PipelineWorker<T, X>
    {
        ConcurrentQueue<T> _sourceQueue;
        ConcurrentQueue<X> _destQueue;

        private volatile bool _stop = false;
        private volatile bool _running = false;
        private Func<T, X> _worker;

        public PipelineWorker(ConcurrentQueue<T> sourceQueue, ConcurrentQueue<X> destQueue, Func<T, X> worker)
        {
            _sourceQueue = sourceQueue;
            _destQueue = destQueue;
            _worker = worker;
        }

        public void Stop()
        {
            _stop = true;
            while (_running)
            {
                Thread.Yield();
            }
        }

        public PipelineWorker<T, X> Start()
        {
            _stop = false;
            var thread = new Thread(Run);

            thread.Start();
            return this;
        }

        private void Run()
        {
            try
            {
                _running = true;
                while (!_stop)
                {
                    T t;
                    if (_sourceQueue.TryDequeue(out t))
                    {
                        _destQueue.Enqueue(_worker(t));
                    }
                    else
                    {
                        Thread.Sleep(10);
                    }
                }
            }
            finally
            {
                _running = false;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Personal.Lib.Threading
{
    /// <summary>
    /// Realize a classe BoundedQueue<T> com a implementação duma la de comunicação de objectos entre threads,
    /// com dimensão de armazenamento limitada. Este classe possui os seguintes metodos e propriedades
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BoundedQueue<T>
    {
        readonly static int MAX_SIZE = 10;
        readonly Queue<T> _queue;

        public BoundedQueue()
        {
            _queue = new Queue<T>();
        }
        /// <summary>
        /// Metodo T Get(int timetout), que retira um elemento da fila, bloqueando-se no maximo t milisegundos
        /// caso esse elemento não esteja imediatamente disponvel
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public T Get(int timeout)
        {
            lock (this)
            {
                int lastTime = timeout == Timeout.Infinite ? 0 : Environment.TickCount;
                while (_queue.Count == 0)
                {
                    Monitor.Wait(this, timeout);

                    if (SyncUtils.AdjustTimeout(lastTime, ref timeout) == 0)
                    {
                        throw new TimeoutException();
                    }
                }
                T t = _queue.Dequeue();
#if DEBUG
                Console.WriteLine("dequeued [{0}] queue size={1}", t, _queue.Count());
#endif
                Monitor.PulseAll(this);
                return t;
            }
        }

        /// <summary>
        /// Metodo bool Put(T t, int timeout), que insere t na fila, bloquando-se no maximo t milisegundos caso
        /// não exista espaco disponvel
        /// </summary>
        /// <param name="t"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool Put(T t, int timeout)
        {
            lock (this)
            {
                if (_closing || _closed)
                    throw new InvalidOperationException("Queue is no longuer acepting items");
                int lastTime = timeout == Timeout.Infinite ? 0 : Environment.TickCount;
                while (_queue.Count == MAX_SIZE)
                {
                    Monitor.Wait(this, timeout);
                    if (SyncUtils.AdjustTimeout(lastTime, ref timeout) == 0)
                    {
                        return false;
                    }
                }
                if (_closing || _closed)
                    throw new InvalidOperationException("Queue is no longuer acepting items");
                _queue.Enqueue(t);
#if DEBUG
                Console.WriteLine("enqueued [{0}] queue size={1}", t, _queue.Count());
#endif
                Monitor.PulseAll(this);
                return true;
            }
        }

        /// <summary>
        /// Metodo void Close(), que coloca a fila no estado closing.
        /// Neste estado, todas as operações de inserção são rejeitadas atraves do lancamento duma excepção. As
        /// operações de remoção terminam com sucesso enquanto existerem elementos na fila. Quando o ultimo
        /// elemento for retirado da fila, esta evolui para o estado closed. Neste estado todas as operações de remoção
        /// terminam com o lancamento duma excepção a sinalizar que a fila esta no estado closed.
        /// </summary>
        public void Close()
        {
            lock (this)
            {
#if DEBUG
                Console.WriteLine("QUEUE IS OFFICIALLY CLOSED!! queue size={0}", _queue.Count());
#endif
                if (_closing || _closed) throw new InvalidOperationException("Queue is already closing");
                _closing = true;
            }
        }
        volatile bool _closing = false;
        public bool IsClosing
        {
            get
            {
                return _closing;
            }
        }

        volatile bool _closed = false;
        public bool IsClosed
        {
            get
            {
                return _closed;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Personal.Lib.Threading
{
    public static class SyncUtils
    {
        public static int AdjustTimeout(int timeRef, ref int timeout)
        {
            if (timeout != Timeout.Infinite)
            {
                int now = Environment.TickCount;
                int elapsed = now == timeRef ? 1 : now - timeRef;
                elapsed = elapsed < timeout ? elapsed : timeout;
                timeout -= elapsed;
            }
            return timeout;
        }

        public static void Wait(object mlock, object condition, int timeout)
        {
            if (mlock == condition)
            {
                Monitor.Wait(mlock, timeout);
                return;
            }
            Monitor.Enter(condition);
            Monitor.Exit(mlock);
            try
            {
                Monitor.Wait(condition, timeout);
            }
            finally
            {
                Monitor.Exit(condition);
                bool interrupted = false;
                do
                {
                    try
                    {
                        Monitor.Enter(mlock);
                        break;
                    }
                    catch (ThreadInterruptedException)
                    {
                        interrupted = true;
                    }
                } while (true);
                if (interrupted)
                    throw new ThreadInterruptedException();
            }
        }

        public static void Wait(object mlock, object condidion)
        {
            Wait(mlock, condidion, Timeout.Infinite);
        }

        public static void Notify(object mlock, object condition)
        {
            if (mlock == condition)
            {
                Monitor.Pulse(mlock);
                return;
            }
            bool interrupted = false;
            do
            {
                try
                {
                    Monitor.Enter(condition);
                    break;
                }
                catch (ThreadInterruptedException)
                {
                    interrupted = true;
                }
            } while (true);
            Monitor.Pulse(condition);
            Monitor.Exit(condition);
            if (interrupted)
                Thread.CurrentThread.Interrupt();
        }

        public static void Broadcast(object mlock, object condidion)
        {
            if (mlock == condidion)
            {
                Monitor.PulseAll(mlock);
                return;
            }
            bool interrupted = false;
            do
            {
                try
                {
                    Monitor.Enter(condidion);
                    break;
                }
                catch (ThreadInterruptedException)
                {
                    interrupted = true;
                }
            } while (true);
            Monitor.PulseAll(condidion);
            Monitor.Exit(condidion);
            if (interrupted)
                Thread.CurrentThread.Interrupt();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Personal.Lib
{
    public class AsyncLoader<T>
    {
        public AsyncLoader(Func<T> loadMethod)
        {
            _task = Task.Factory.StartNew<T>(loadMethod);
        }
        private Task<T> _task;

        public T Value
        {
            get
            {
                return _task.Result;
            }
        }
    }
}

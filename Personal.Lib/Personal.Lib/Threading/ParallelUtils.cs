using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Lib.Threading
{
    public class ParallelUtils
    {
        /// <summary>
        /// Executa o conjunto de funções e junta os resultados 
        /// com a função indicada
        /// </summary>
        /// <typeparam name="T">o tipo de dados utilizado</typeparam>
        /// <param name="joinFunc">a função utilizada para agrupar resultados</param>
        /// <param name="funcs">as funções a executar em paralelo</param>
        /// <returns>o resultado final</returns>
        public static T ParallelExecuteAndJoin<T>(Func<T, T, T> joinFunc, params Func<T>[] funcs)
        {
            var tasks = new Task<T>[funcs.Length];
            for (int i = 0; i < funcs.Length; i++)
            {
                tasks[i] = Task.Factory.StartNew<T>(funcs[i]);
            }
            T res = default(T);
            var tr = Task.Factory.ContinueWhenAll<T>(tasks, (t) =>
            {
                for (int i = 0; i < t.Length; i++)
                {
                    res = joinFunc(res, ((Task<T>)t[i]).Result);
                }
                return res;
            });
            tr.Wait();
            return tr.Result;
        }

        /// <summary>
        /// Executa o conjunto de funções e junta os resultados 
        /// com a função indicada
        /// </summary>
        /// <typeparam name="T">o tipo de dados utilizado</typeparam>
        /// <param name="joinFunc">a função utilizada para agrupar resultados</param>
        /// <param name="funcs">as funções a executar em paralelo</param>
        /// <returns>o resultado final</returns>
        public static T BeginExecuteAndAdd<T>(Func<T, T, T> joinFunc, params Func<T>[] funcs)
        {
            var ar = new IAsyncResult[funcs.Length];
            for (int i = 0; i < funcs.Length; i++)
            {
                var act = funcs[i];
                ar[i] = act.BeginInvoke((o) => { }, null);
            }
            T res = default(T);
            for (int i = 0; i < ar.Length; i++)
            {
                ar[i].AsyncWaitHandle.WaitOne();
                res = joinFunc(res, funcs[i].EndInvoke(ar[i]));
            }
            return res;
        }
    }
}

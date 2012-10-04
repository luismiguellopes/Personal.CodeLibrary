using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Personal.Lib
{
    public class ContextUtils
    {
        /// <summary>
        /// permite a utilização em linha de um objecto disposable
        /// como por exemplo um contexto de EntityFramework:
        /// var x = ContextUtils.Execute(ctx => ctx.Products.Where(p => p.UnitPrice < 10));
        /// </summary>
        /// <typeparam name="T">o tipo de retorno</typeparam>
        /// <typeparam name="Y">o tipo do objecto IDisposable a utilizar</typeparam>
        /// <param name="f">a função a executar</param>
        /// <returns>o valor de retorno</returns>
        static public T Execute<T, Y>(Func<Y, T> f)
             where Y : IDisposable, new()
        {
            using (var ctx = new Y())
            {
                return f(ctx);
            }
        }

        /// <summary>
        /// permite a utilização em linha de um objecto disposable
        /// </summary>
        /// <typeparam name="T">o tipo do objecto IDisposable a utilizar</typeparam>
        /// <param name="f">a função a executar</param>
        static public void Execute<T>(Action<T> f)
             where T : IDisposable, new()
        {
            using (var ctx = new T())
            {
                f(ctx);
            }
        }
    }
}

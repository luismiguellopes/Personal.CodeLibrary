using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace Personal.Lib.Threading
{
    public static class StreamLib
    {
        private static readonly int BUFFER_SIZE = 1 << 12;

        public static WaitHandle AsyncCopy(this Stream src, Stream dst)
        {
            var evt = new ManualResetEvent(false);
            var buffer = new byte[BUFFER_SIZE];

            AsyncCallback cbk = null;
            cbk = (asyncReadResult) =>
            {
                int bytesRead = src.EndRead(asyncReadResult);
                if (bytesRead > 0)
                {
                    //Console.Write("r");
                    dst.BeginWrite(buffer, 0, bytesRead,
                        (asyncWriteResult) =>
                        {
                            //Console.Write("w");
                            dst.EndWrite(asyncWriteResult);
                            src.BeginRead(buffer, 0, buffer.Length, cbk, null);
                        }, null);
                }
                else
                {
                    Console.WriteLine();
                    dst.Flush();
                    dst.Position = 0;
                    evt.Set();
                }
            };
            src.BeginRead(buffer, 0, buffer.Length, cbk, buffer);
            return evt;
        }


        //public static WaitHandle StreamCopySync(Stream src, Stream dst)
        //{
        //    //sync
        //    var evt = new EventWaitHandle(false, EventResetMode.AutoReset);
        //    var buffer = new byte[BUFFER_SIZE];
        //    int bytesRead = 0;
        //    while ((bytesRead = src.Read(buffer, 0, BUFFER_SIZE)) > 0)
        //    {
        //        dst.Write(buffer, 0, bytesRead);
        //    }
        //    dst.Flush();
        //    dst.Position = 0;
        //    evt.Set();
        //    return evt;
        //}
    }
}

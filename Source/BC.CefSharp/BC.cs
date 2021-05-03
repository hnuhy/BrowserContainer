using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BC.CefSharp
{
    public class BC
    {
        private System.Threading.SynchronizationContext context;
        public BC(System.Threading.SynchronizationContext context)
        {
            this.context = context;
        }
        public string ShowMsg(string msg)
        {
            Console.WriteLine(msg);

            return "come from csharp:" + msg;
        }
        public void UpdateTime(string timeStr)
        {
            context.Send(x => {
                Console.WriteLine(timeStr);
            }, null);

        }
    }
}

using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

using System.IO;

namespace BC.CefSharp
{
    public class BoundObject
    {
        private System.Threading.SynchronizationContext context;

        [JavascriptIgnoreAttribute]
        private Form form { get; set; }

        public BoundObject() { }


        public BoundObject(Form form) { this.form = form; }

        public BoundObject(System.Threading.SynchronizationContext context)
        {
            this.context = context;
        }
        public string ShowMsg(string msg)
        {
            FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory+"T.txt", FileMode.Open, FileAccess.Read);
            TextReader tr = new StreamReader(fs, Encoding.Default);
            string str = tr.ReadToEnd();

            return form.ToString() + ":"+ msg + " come from csharp:" + str;
        }

        public void jsCallback(IJavascriptCallback javascriptCallback)
        {
            const int taskDelay = 1500;

            Task.Factory.StartNew(() =>
            {
                System.Threading.Thread.Sleep(taskDelay);

                using (javascriptCallback)
                {
                    javascriptCallback.ExecuteAsync("This callback from C# was delayed " + taskDelay + "ms");
                }
            });
        }


        public void UpdateTime(string timeStr)
        {
            context.Send(x => {
                Console.WriteLine(timeStr);
            }, null);

        }
    }
}

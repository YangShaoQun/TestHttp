using System;
using System.Collections;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime;
using Utils;

namespace MyService 
{
    public class HttpService : IService
    {
        const int _PORT_ = 9333;
        const string _PATH_ = "/test/";
        const string _WEB_PAGE_ = "<HTML><BODY>My web page.<br>{0}<br>{1}</BODY></HTML>";
        const string _WEB_PAGE_PATH_ = "search.htm";

        HttpListener _listener;
        bool _stop;
        Task<HttpListenerContext> _preContextTask;

        protected override void Start()
        {
            base.Start();

			_listener = new HttpListener();
            _listener.Prefixes.Add(string.Format("http://*:{0}{1}", _PORT_, _PATH_));
            _listener.Prefixes.Add(string.Format("https://*:{0}{1}", _PORT_ + 1, _PATH_));
			_listener.Start();
			Console.WriteLine(string.Format("Http Listener started on port: '{0}' at path: '{1}'"
											, _PORT_, _PATH_));

            Console.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory);

            StartCoroutine(StartHttpListener());
        }

        protected override void OnRetired()
        {
            base.OnRetired();

			Console.WriteLine("Http Listener stopped");
			_listener.Stop();
            _stop = true;
        }

        IEnumerator StartHttpListener()
        {
			while(!_stop)
			{
				Task<HttpListenerContext> ctxTask = _listener.GetContextAsync();
                if (_preContextTask != ctxTask)
                {
                    while (!ctxTask.IsCompleted)
                    {
                        yield return null;
                    }
                    _preContextTask = ctxTask;
                    try
                    {
                        if (ctxTask.IsCompleted)
                        {
                            HttpListenerContext ctx = ctxTask.Result;
                            Console.WriteLine(ctx.Request.RemoteEndPoint);
                            //string rstr = FileUtil.ReadText(_WEB_PAGE_PATH_);
                            string rstr = string.Format(_WEB_PAGE_, ctx.Request.Url.AbsolutePath, DateTime.Now);
                            byte[] buf = Encoding.UTF8.GetBytes(rstr);
                            ctx.Response.ContentLength64 = buf.Length;
                            ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    finally
                    {
                        ctxTask.Result.Response.OutputStream.Close();
                    }
                }

                yield return null;
            }
        }
    }
}
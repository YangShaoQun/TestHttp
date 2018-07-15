using System;
using MyServer;

namespace TestHttp
{
    class MainClass
    {
        const string EXIT_COMMAND = "EXIT";

        static HttpServer _server;

        public static void Main(string[] args)
        {
			AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

			// Do something here
			_server = new HttpServer();
			_server.Start();

			string command;
			while((command = Console.ReadLine()).ToUpper() != EXIT_COMMAND)
			{
			 Console.WriteLine(string.Format("command '{0}' is not support", command));
			}
			_server.Stop();
			_server = null;
		}

		static void OnProcessExit(object sender, EventArgs e)
		{
            if (_server != null)
                _server.Stop();
		}
    }
}

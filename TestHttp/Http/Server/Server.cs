using System;
using System.Threading;
using System.Collections.Generic;
using MyService;

namespace MyServer
{
    public abstract class Server
    {
		Thread _thread;
		bool _abortThread;

        public void Start()
        {
			_thread = new Thread(RunLoop);
			_thread.Start();
        }

        public void Stop()
        {
            if (_thread == null)
                return;
            _abortThread = true;
            _thread = null;
        }

        protected abstract List<IService> StartService(Server server);

        void RunLoop()
        {
            List<IService> services = StartService(this);
            if(services != null && services.Count > 0)
            {
                foreach(var service in services)
                {
                    service.Init();
                }
                while(!_abortThread)
                {
                    foreach(var service in services)
                    {
                        service.MainLoop();
                    }
                }
				foreach (var service in services)
				{
					service.Retire();
				}
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace MyService
{
    public abstract class IYieldObject : IEnumerator
    {
        public object Current 
        {
            get {
                return this;
            }
        }

        public bool MoveNext()
        {
            return Do();
        }

        public void Reset()
        {
            throw new NotSupportedException();
		}

        protected abstract bool Do();
    }

    public class WaitOneMessage : IYieldObject
    {
        protected override bool Do()
        {
            //Console.WriteLine("WaitOneMessage --- Do");
            return false;
        }
    }

    public class WaitForSeconds : IYieldObject
    {
        float _secondsToWait;
        DateTime _startTime;

        public WaitForSeconds(float seconds)
        {
            _secondsToWait = seconds;
            _startTime = DateTime.Now;
        }

        protected override bool Do()
        {
            DateTime now = DateTime.Now;
            TimeSpan time = now - _startTime;
            //Console.WriteLine(string.Format("WaitForSeconds --- Do --- time elapsed: {0}", time.TotalSeconds));
            if (time.TotalSeconds >= _secondsToWait)
            {
                //Console.WriteLine("WaitForSeconds --- Do --- time up");
                return false;
            }
            return true;
        }
    }
}
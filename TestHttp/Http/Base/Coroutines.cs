using System;
using System.Collections;
using System.Collections.Generic;

namespace MyService
{
    public class Coroutine
    {
        IEnumerator _current { get; set; }

        Stack<IEnumerator> _stack = new Stack<IEnumerator>();
        
        public Coroutine(IEnumerator iterator)
        {
            _current = iterator;
        }

        public bool MoveNext()
        {
            if (_current.MoveNext())
            {
                IEnumerator next = _current.Current as IEnumerator;
                if (next != null && _current != next)
                {
                    _stack.Push(_current);
                    _current = next;
                }
                return true;
            }
            else
			{
				if (_stack.Count == 0)
					return false;
                
                _current = _stack.Pop();
                    
                return true;
            }
        }
    }
}

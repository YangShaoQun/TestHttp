using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace MyService
{
    public abstract class IService
    {
        Dictionary<int, Coroutine> _coroutines = new Dictionary<int, Coroutine>();
        Queue<int> _reuseCoroutineIndex = new Queue<int>();
        int _coroutineIndex;

        public void Init()
        {
            Console.WriteLine(string.Format("[{0}] start", GetType().Name));
            Start();
        }

        public void Retire()
        {
			StopAllCoroutine();
			OnRetired();
			Console.WriteLine(string.Format("[{0}] stop", GetType().Name));
        }

        protected virtual void Start() {}

        protected virtual void OnRetired() {}

        protected void StartCoroutine(IEnumerator iterator)
        {
            _coroutines.Add(QueryCoroutineIndex(), new Coroutine(iterator));
		}

        protected void StopCoroutine(int index)
        {
            RemoveCoroutine(index);
        }

        void RemoveCoroutine(int index)
        {
			ReuseCoroutineIndex(index);
			_coroutines.Remove(index);
        }

        void StopAllCoroutine()
        {
            _coroutineIndex = 0;
			_reuseCoroutineIndex.Clear();
            _coroutines.Clear();
        }

        int QueryCoroutineIndex()
        {
            if (_reuseCoroutineIndex.Count > 0)
                return _reuseCoroutineIndex.Dequeue();
            else
                return ++_coroutineIndex;
        }

        void ReuseCoroutineIndex(int index)
        {
            _reuseCoroutineIndex.Enqueue(index);
        }

        public void MainLoop()
        {
			List<int> deleteList = new List<int>();
			foreach (var corPair in _coroutines)
			{
				int index = corPair.Key;
				Coroutine coroutine = corPair.Value;
				if (!coroutine.MoveNext())
				{
					deleteList.Add(index);
				}
			}
			foreach (var index in deleteList)
			{
				RemoveCoroutine(index);
			}

            Update();
        }

        protected virtual void Update() {}
    }
}
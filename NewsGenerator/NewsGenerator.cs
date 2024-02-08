using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsGenerator
{
    abstract public class NewsGenerator: IObservable<List<News>>
    {
        public List<News> NewsList { get; set; }
        public abstract void GenerateNews();

        protected List<IObserver<List<News>>> observers;
        protected virtual string Category { get; } 

        public NewsGenerator()
        {
            NewsList = new List<News>();
            observers = new List<IObserver<List<News>>>();
        }

        public IDisposable Subscribe(IObserver<List<News>> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        protected void NotifyObservers(List<News> listNews)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(listNews);
            }
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<List<News>>> _observers;
            private IObserver<List<News>> _observer;

            public Unsubscriber(List<IObserver<List<News>>
                > observers, IObserver<List<News>> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }
}

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsGenerator
{
    public abstract class NewsGenerator: IObservable
    {
        public List<News> NewsList { get; set; }
        protected string[] CategoryMas = { "sport", "technology" };
        protected const string FileName = "News.json";

        protected List<IObserver> observers;

        public NewsGenerator()
        {
            NewsList = new List<News>();
            observers = new List<IObserver>();
        }
        public abstract void GenerateNews();

        public void AddObserver(IObserver o)
        {
            observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            observers.Remove(o);
        }

        public void NotifyObservers(object obj)
        {
            foreach (IObserver observer in observers)
                observer.Update(obj);
        }
    }
}

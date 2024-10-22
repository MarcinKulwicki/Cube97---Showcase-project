using System;

namespace Cube.Network
{
    public abstract class AbstractService<T> : INet<T>
    {
        protected string Ep;
        protected abstract string Url { get; }

        public AbstractService(string ep) => Ep = ep;

        public abstract void Get(Action<T[]> OnSuccess, Action<string> OnFail);
        public abstract void Post(T item, Action<string> OnSuccess, Action<string> OnFail);
    }
}
using System;

namespace Cube.Network
{
    public interface INet<T>
    {
        public void Get(Action<T[]> OnSuccess, Action<string> OnFail);
        public void Post(T item, Action<string> OnSuccess, Action<string> OnFail);
    }
}

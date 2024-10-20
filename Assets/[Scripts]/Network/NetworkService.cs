using System;
using Cube.Controllers;

namespace Cube.Network
{
    /// <summary>
    ///     Actualy Online mode is not implemented
    /// </summary>
    public abstract class NetworkService<T> : INet<T>
    {
        protected abstract string OnlineEndPoint { get; }
        protected abstract string OfflineEndPoint { get; }
        public void Get(Action<T[]> OnSuccess, Action<string> OnFail)
        {
            if (NetworkController.Instance.IsOnline)
                new OnlineService<T>(OnlineEndPoint).Get(OnSuccess, OnFail);
            else
                new LocalService<T>(OfflineEndPoint).Get(OnSuccess, OnFail);
        }

        public void Post(T item, Action<string> OnSuccess, Action<string> OnFail)
        {
            if (NetworkController.Instance.IsOnline)
                new OnlineService<T>(OnlineEndPoint).Post(item, OnSuccess, OnFail);
            else
                new LocalService<T>(OfflineEndPoint).Post(item, OnSuccess, OnFail);
        }
    }
}
using System;

namespace Cube.Network
{
    public class OnlineService<T> : AbstractService<T>
    {
        public OnlineService(string ep) : base(ep) {}

        protected override string Url => NetworkConfig.URL;

        public override void Get(Action<T[]> OnSuccess, Action<string> OnFail)
        {
            throw new NotImplementedException();
        }

        public override void Post(T item, Action<string> OnSuccess, Action<string> OnFail)
        {
            throw new NotImplementedException();
        }
    }
}
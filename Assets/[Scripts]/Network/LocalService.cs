using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Cube.Network
{
    public class LocalService<T> : AbstractService<T>
    {
        public LocalService(string ep) : base(ep) {}
        protected override string Url => Application.persistentDataPath;

        public override void Get(Action<T[]> OnSuccess, Action<string> OnFail)
        {
            if (File.Exists(Url + Ep))
            {
                string json = File.ReadAllText(Url + Ep);
                try
                {
                    var item = JsonHelper.FromJson<T>(json);
                    OnSuccess?.Invoke(item);
                } 
                catch (Exception e)
                {
                    File.Delete(Url + Ep);
                    OnFail?.Invoke($"[{typeof(NetworkService<>)}] {e.Message}");
                }
            }
            else
            {
                OnFail?.Invoke($"[{typeof(NetworkService<>)}] File does not exist");
            }
        }

        public override void Post(T item, Action<string> OnSuccess, Action<string> OnFail)
        {
            if (File.Exists(Url + Ep))
            {
                string json = File.ReadAllText(Url + Ep);
                try
                {
                    var items = JsonHelper.FromJson<T>(json);
                    List<T> list;
                    
                    if (item == null)
                        list = new List<T>();
                    else
                        list = items.ToList();
                        
                    list.Add(item);

                    File.WriteAllText(Url + Ep, JsonHelper.ToJson(list.ToArray()));

                    OnSuccess?.Invoke($"[{typeof(NetworkService<>)}] Successfully save data in local drive.");
                } 
                catch (Exception e)
                {
                    File.Delete(Url + Ep);
                    OnFail?.Invoke($"[[{typeof(NetworkService<>)}] {e.Message}");
                }
            }
            else
            {
                List<T> list = new() { item };
                File.WriteAllText(Url + Ep, JsonHelper.ToJson(list.ToArray()));
            }
        }
    }
}
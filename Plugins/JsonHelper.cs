using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JAnalytics
{
    public static class JsonHelper
    {

        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>
            {
                Items = array
            };
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(List<T> list)
        {
            Wrapper<T> wrapper = new Wrapper<T>
            {
                Items = list.ToArray()
            };
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>
            {
                Items = array
            };
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        public static string ToJson<TKey, TValue>(Dictionary<TKey, TValue> dic)
        {
            MyDictionary<TKey, TValue> myDictionary = new MyDictionary<TKey, TValue>(dic);
            return JsonUtility.ToJson(myDictionary);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }



        // List<T>
        [Serializable]
        public class MyDictionary<T>
        {
            [SerializeField]
            List<T> target;
            public List<T> ToList() { return target; }

            public MyDictionary(List<T> target)
            {
                this.target = target;
            }
        }
        [Serializable]
        public class MyDictionary<TKey, TValue> : ISerializationCallbackReceiver
        {
            [SerializeField]
            List<TKey> keys;
            [SerializeField]
            List<TValue> values;

            [SerializeField]
            List<TKey> keysNull;

            public Dictionary<TKey, TValue> target;
            public Dictionary<TKey, TValue> ToDictionary() { return target; }

            public MyDictionary(Dictionary<TKey, TValue> target)
            {
                this.target = target;
            }

            public void OnBeforeSerialize()
            {
                Debug.Log("OnBeforeSerialize-");

                keys = new List<TKey>();
                values = new List<TValue>();
                keysNull = new List<TKey>();
                foreach (KeyValuePair<TKey, TValue> kv in target)
                {
                    TKey k = kv.Key;
                    TValue v = kv.Value;
                    if(null != v)
                    {
                        keys.Add(k);
                        values.Add(v);
                    }
                    else
                    {
                        keysNull.Add(k);
                    }

                }

            }

            public void OnAfterDeserialize()
            {
                Debug.Log("OnAfterDeserialize-");
                var count = Math.Min(keys.Count, values.Count);
                target = new Dictionary<TKey, TValue>(count);
                for (var i = 0; i < count; ++i)
                {
                    target.Add(keys[i], values[i]);
                }

                for(var i = 0; i < keysNull.Count; i++)
                {
                    target.Add(keysNull[i], default(TValue));
                }
            }
        }
    }
}

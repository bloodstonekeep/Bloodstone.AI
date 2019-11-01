using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bloodstone.Utility
{
    [Serializable]
    public class Reactive<T>
    {
        public delegate void ReactiveCallback(T value);

        private readonly List<ReactiveCallback> _subscribers = new List<ReactiveCallback>();

        [SerializeField]
        private T _value;

        public Reactive(T value)
        {
            Value = value;
        }

        public T Value
        {
            get => _value;
            set
            {
                if (EqualityComparer<T>.Default.Equals(_value, value))
                {
                    return;
                }

                _value = value;

                NotifySubscribers(value);
            }
        }

        public static implicit operator T(Reactive<T> lhs)
        {
            return lhs.Value;
        }

        public void Subscribe(ReactiveCallback callback, bool notifyOnSubscribe = true)
        {
            _subscribers.Add(callback);

            if (notifyOnSubscribe)
            {
                callback?.Invoke(Value);
            }
        }

        public void Unsubscribe(ReactiveCallback callback)
        {
            _subscribers.Remove(callback);
        }

        private void NotifySubscribers(T value)
        {
            for (int i = _subscribers.Count - 1; i >= 0; --i)
            {
                if (_subscribers[i] == null)
                {
                    Unsubscribe(_subscribers[i]);

                    continue;
                }

                _subscribers[i].Invoke(value);
            }
        }
    }
}
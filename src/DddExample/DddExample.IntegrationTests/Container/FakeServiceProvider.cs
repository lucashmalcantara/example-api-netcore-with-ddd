using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DddExample.IntegrationTests.Container
{
    public sealed class FakeServiceProvider
    {
        private static readonly FakeServiceProvider _instance = new FakeServiceProvider();
        public static FakeServiceProvider Instance
        {
            get { return _instance; }
        }

        static FakeServiceProvider() { }
        private FakeServiceProvider() { }

        private Dictionary<Type, object> fakers = new Dictionary<Type, object>();

        public void Add<T>(T faker) where T : class
        {
            Remove<T>();
            fakers.Add(typeof(T), faker);
        }

        public T Add<T>() where T : class
        {
            var fake = A.Fake<T>();
            Add(fake);
            return fake;
        }

        public T GetOrAddIfNotExists<T>() where T : class
        {
            var instance = Get<T>();

            if (instance != null)
                return instance;

            var fake = A.Fake<T>();

            Add(fake);

            return fake;
        }

        public T Get<T>() where T : class => Contains<T>() ? (T)fakers[typeof(T)] : null;

        public bool Contains<T>() where T : class => fakers.ContainsKey(typeof(T));

        public void Remove<T>() where T : class
        {
            if (Contains<T>())
                fakers.Remove(typeof(T));
        }

        public void Clear() => fakers = new Dictionary<Type, object>();

        public void Clear(params Type[] excludeType)
        {

            foreach (var key in fakers.Keys.Except(excludeType).ToList())
                fakers.Remove(key);
        }
    }
}

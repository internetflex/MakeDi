using System;
using System.Collections.Generic;

namespace MakeDi
{
    public class Make : IMake, IDisposable
    {
        private readonly Dictionary<Type, Func<object>> _factories;
        private readonly List<IDisposable> _disposables;

        // List of injectable dependencies
        public Make()
        {
            _disposables = new List<IDisposable>();
            _factories = new Dictionary<Type, Func<object>>
            {
                {typeof(IMake), () => this},
                {typeof(IFactory), () => new Factory(this)},
                {typeof(IServiceB), MakeSingleton(() => new ServiceB(Get<IServiceC>()))},
                {typeof(IServiceC), AddDisposable<IServiceC>(() => new ServiceC(Get<IServiceE>()))},
                {typeof(IServiceE), MakeLazy(() => new ServiceE())},
                {typeof(IServiceA), () => new ServiceA(Get<IServiceB>())},
                {typeof(IServiceD), () => new ServiceD(Get<IServiceA>(), Get<IServiceC>())},
                {typeof(IProduct), () => new Product()},
                {typeof(ServiceF), () => new ServiceF()}
            };
        }

        public IEnumerable<Type> Types => new List<Type>(_factories.Keys);

        public T Get<T>()
        {
            var type = typeof(T);
            return (T)(_factories.ContainsKey(type) ? _factories[type]() : null);
        }

        public object Get(Type type)
        {
            if (type == null)
                throw new ApplicationException($"Cannot resolve a type of null");
            if (!_factories.ContainsKey(type))
                throw new ApplicationException($"Unregistered type {type.FullName}");
            return _factories[type]();
        }

        private Func<T> AddDisposable<T>(Func<T> func)
        {
            return () =>
            {
                var obj = func();
                if (obj is IDisposable disposable)
                {
                    _disposables.Add(disposable);
                }

                return obj;
            };
        }

        private Func<T> MakeSingleton<T>(Func<T> creator)
        {
            var cachedValue = default(T);
            var initialised = false;

            return AddDisposable(() =>
            {
                if (!initialised)
                {
                    cachedValue = creator();
                    initialised = true;
                }
                return cachedValue;
            });
        }

        private Func<T> MakeLazy<T>(Func<T> creator)
        {
            var cachedValue = new Lazy<T>(creator);
            return () => cachedValue.Value;
        }

        private void ReleaseUnmanagedResources()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~Make()
        {
            ReleaseUnmanagedResources();
        }
    }
}

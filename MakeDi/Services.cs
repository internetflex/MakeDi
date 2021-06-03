using System.Diagnostics;

namespace MakeDi
{
    public class ServiceA : IServiceA
    {
        public ServiceA(IServiceB serviceB)
        {
        }

        public void DoSomethingA()
        {
            throw new System.NotImplementedException();
        }
    }

    public class ServiceB : IServiceB
    {
        private readonly IServiceC _serviceC;

        public ServiceB(IServiceC serviceC)
        {
            _serviceC = serviceC;
        }

        public void DoSomethingA()
        {
            throw new System.NotImplementedException();
        }

        public void DoSomethingB()
        {
            throw new System.NotImplementedException();
        }

        public void DoSomethingC()
        {
            throw new System.NotImplementedException();
        }
    }

    public class ServiceC : IServiceC
    {
        private readonly IServiceE _serviceE;

        public ServiceC(IServiceE serviceE)
        {
            _serviceE = serviceE;
        }

        public void DoSomethingA()
        {
            Debug.WriteLine("DoSomethingA");
        }

        public void DoSomethingB()
        {
            Debug.WriteLine("DoSomethingB");
        }
    }
    public class ServiceD : IServiceD
    {
        private readonly IServiceA _serviceA;
        private readonly IServiceC _serviceC;

        public ServiceD(IFactory factory)
        {
        }

        public ServiceD(IServiceA serviceA, IServiceC serviceC)
        {
            _serviceA = serviceA;
            _serviceC = serviceC;
        }

        public void DoSomethingB()
        {
            throw new System.NotImplementedException();
        }
    }

    public class Product : IProduct
    {
    }

    public class ServiceE : IServiceE
    {
        public ServiceE()
        {
        }

        public void DoSomethingE()
        {
            throw new System.NotImplementedException();
        }
    }

    public class ServiceF
    {
        public ServiceF()
        {
            Debug.WriteLine("ServiceF Created");
        }
    }

    public class Factory : IFactory
    {
        private readonly IMake _make;

        public Factory(IMake make)
        {
            _make = make;
        }

        public IProduct Create(string param)
        {
            return _make.Get<IProduct>();
        }
    }
}
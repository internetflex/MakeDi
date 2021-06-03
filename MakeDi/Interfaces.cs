namespace MakeDi
{
    public interface IMake
    {
        T Get<T>();
    }

    public interface IServiceA
    {
        void DoSomethingA();
    }

    public interface IServiceB
    {
        void DoSomethingA();
        void DoSomethingB();
        void DoSomethingC();
    }

    public interface IServiceC
    {
        void DoSomethingA();
        void DoSomethingB();
    }

    public interface IServiceD
    {
        void DoSomethingB();
    }

    public interface IServiceE
    {
        void DoSomethingE();
    }

    public interface IFactory
    {
        IProduct Create(string param);
    }

    public interface IProduct
    {
    }
}
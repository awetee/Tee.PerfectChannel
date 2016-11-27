namespace Tee.PerfectChannel.WebApi.Services
{
    public interface IDataService<T>
    {
        T Get(int id);

        void Update();
    }
}
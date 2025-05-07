using WebApiClass.Model;

namespace WebApiClass.IServices
{
    public interface INumberCheckService
    {
        Task<Root> GetCountryNumber(string number);
    }
}

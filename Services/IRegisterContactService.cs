using Site.ServiceModels;
using System.Threading.Tasks;


namespace Site.Services
{
    public interface IRegisterContactService
    {
        Task Register(Contact json);
    }
}

using Template.Business.Repositories;
using Template.Business.Services.Generics;

namespace Template.Business.Services
{
    public class HomeService : ServiceBase
    {
        public HomeRepository HomeRepository { get; set; }

        public HomeService(HomeRepository homeRepository)
        {
            HomeRepository = homeRepository;
        }
    }
}

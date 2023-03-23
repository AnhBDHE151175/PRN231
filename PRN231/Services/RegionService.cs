using PRN231.Entities;

namespace PRN231.Services
{
    public class RegionService : BaseService<Region>
    {
        private ApplicationContext _context;

        public RegionService(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}

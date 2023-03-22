using PRN231.Models;

namespace PRN231.Services
{
    public class RegionService : BaseService<Region>
    {
        private Prn231Context _context;

        public RegionService(Prn231Context context) : base(context)
        {
            _context = context;
        }
    }
}

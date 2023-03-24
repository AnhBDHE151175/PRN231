using PRN231.Entities;

namespace PRN231.Services
{
    public class StageService : BaseService<Stage>
    {
        private ApplicationContext _context;

        public StageService(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}

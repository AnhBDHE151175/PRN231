using PRN231.Entities;

namespace PRN231.Services
{
    public class SkillService : BaseService<Skill>
    {
        private ApplicationContext _context;

        public SkillService(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}

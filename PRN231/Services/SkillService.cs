using PRN231.DTOs.RequestModels;
using PRN231.DTOs.ResponseModels;
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
        public async Task<ListDataOutput<Skill>> GetFilter(Pager pager)
        {
            var res = new ListDataOutput<Skill>();
            try
            {
                var data = _context.Skills.ToList();
                res.TotalRecord = data.Count;
                res.Data = data.Skip((pager.PageIndex - 1) * pager.PageSize).Take(pager.PageSize).ToList();
                res.IsError = false;

            }
            catch (Exception ex)
            {
                res.IsError = true;
                return null;
            }
            return res;
        }
    }
}

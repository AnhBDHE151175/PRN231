using Microsoft.EntityFrameworkCore;
using PRN231.DTOs.Enum;
using PRN231.DTOs.ResponseModels;
using PRN231.Entities;
using PRN231.Interfaces;

namespace PRN231.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        public readonly ApplicationContext _context;
        public BaseService(ApplicationContext context)
        {
            this._context = context;
        }
        public async Task<Response> Delete(int id)
        {
            var res = new Response();
            try
            {
                var data = await GetByID(id);
                if (data != null)
                {
                    _context.Remove<TEntity>(data);
                    var affected = await _context.SaveChangesAsync();
                    res.StatusCode = affected > 0 ? ResponseStatusCode.Success : ResponseStatusCode.Error;
                }
            }
            catch (Exception ex)
            {
                res.StatusCode = ResponseStatusCode.Error;
            }
            return res;
        }

        public async Task<ListDataOutput<TEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetByID(int id)
        {
            try
            {
                return await _context.FindAsync<TEntity>(id);
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public async Task<Response> Insert(TEntity entity)
        {
            var response = new Response();
            try
            {
                await _context.AddAsync<TEntity>(entity);
                var affected = await _context.SaveChangesAsync();
                response.StatusCode = affected > 0 ? ResponseStatusCode.Success : ResponseStatusCode.Error;
            }
            catch (Exception ex)
            {
                return null;
            }
            return response;
        }

        public async Task<Response> Update(TEntity entity)
        {
            var response = new Response();
            try
            {
                var e = await GetByID(entity.Id);
                _context.Entry(e).State = EntityState.Detached;
                _context.Update<TEntity>(entity);
                var affected = await _context.SaveChangesAsync();
                response.StatusCode = affected > 0 ? ResponseStatusCode.Success : ResponseStatusCode.Error;
            }
            catch (Exception ex)
            {
                return null;
            }
            return response;
        }
    }
}

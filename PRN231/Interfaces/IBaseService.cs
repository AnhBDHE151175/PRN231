using PRN231.DTOs.ResponseModels;
using PRN231.Models;

namespace PRN231.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        Task<ListDataOutput<TEntity>> GetAll();
        Task<TEntity> GetByID(int id);
        Task<Response> Insert(TEntity entity);
        Task<Response> Update(TEntity entity);

        Task<Response> Delete(int id);


    }
}

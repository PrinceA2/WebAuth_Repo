using WebApiAuth.Models;

namespace WebApiAuth.Services
{
    public interface IRecordService
    {
        Task<bool> CreateRegister(TblUser user);

        Task<List<TblUser>> GetAll();

        public Task DeleteById(int id);
    }
}

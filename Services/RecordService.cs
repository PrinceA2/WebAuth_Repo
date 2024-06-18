using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAuth.Data;
using WebApiAuth.Models;

namespace WebApiAuth.Services
{
    public class RecordService : IRecordService
    {
        private readonly AppDbContext _context;
      

        public RecordService(AppDbContext context)
        {
            _context = context;
           
        }

        public async Task<bool> CreateRegister(TblUser user)
        {
            var EmailAlreadyExists = _context.TblUsers.SingleOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            if (EmailAlreadyExists != null)
            {
                return false;
            }
            var userObj = new TblUser()
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Designation = user.Designation

            };
            _context.TblUsers.Add(userObj);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TblUser>> GetAll()
        {
            return await _context.TblUsers.ToListAsync();
        }

        public async Task DeleteById(int id)
        {
            var record = _context.TblUsers.FirstOrDefault(p => p.ID==id);
            if (record != null)
            {
                _context.TblUsers.Remove(record);
            }
            await Task.CompletedTask;
        }

    }
}

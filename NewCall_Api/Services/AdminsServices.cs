using Microsoft.EntityFrameworkCore;
using NewCall_Api.Database;
using NewCall_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http.HttpResults;
using NewCall_Api.Helpers;
namespace NewCall_Api.Services
{
    public class AdminsServices
    {
        private readonly ApplicationDBContext _dbContext;

        public AdminsServices(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Admins>> GetAllAdmins()
        {
            return await _dbContext.Admins.ToListAsync();
        }

        public async Task<Admins?> GetAdminById(int id)
        {
            var admin = await _dbContext.Admins
                .FirstOrDefaultAsync(m => m.id == id);
            if (admin == null)
            {
                return null;
            }
            return admin;
        }

        

        public async Task CreateAdmin([Bind("id,identifiant,password")] Admins admins)
        {

            string hashedPassword = PasswordHasher.HashPassword(admins.password);

            admins.password = hashedPassword;
    
            _dbContext.Admins.Add(admins);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAdmin(int id, Admins admin)
        {
            var existingAdmin = await _dbContext.Admins.FindAsync(id);

            if (existingAdmin != null)
            {
                existingAdmin.identifiant = admin.identifiant;
                // Mettez à jour d'autres propriétés si nécessaire
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAdmin(int id)
        {
            var admin = await _dbContext.Admins.FindAsync(id);

            if (admin != null)
            {
                _dbContext.Admins.Remove(admin);
                await _dbContext.SaveChangesAsync();
            }
        }


    }
}

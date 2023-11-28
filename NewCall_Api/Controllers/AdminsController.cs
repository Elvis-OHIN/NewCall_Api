using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewCall_Api.Database;
using NewCall_Api.Models;
using NewCall_Api.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace NewCall_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : Controller
    {
        private readonly ApplicationDBContext _context;

        private readonly IConfiguration _configuration;

        public AdminsController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Index")]
        // GET: Admins
        public async Task<IActionResult> Index()
        {
              return _context.Admins != null ? 
                          Ok(await _context.Admins.ToListAsync()) :
                          Problem("Entity set 'ApplicationDBContext.Admins'  is null.");
        }

        // GET: Admins/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Admins == null)
            {
                return NotFound();
            }

            var admins = await _context.Admins
                .FirstOrDefaultAsync(m => m.id == id);
            if (admins == null)
            {
                return NotFound();
            }

            return Ok(admins);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([Bind("identifiant,password")] Login login)
        {
            var admins = await _context.Admins
                .FirstOrDefaultAsync(m => m.identifiant == login.identifiant);
            if (admins == null)
            {
                return NotFound();
            }

            if (PasswordHasher.VerifyPassword(login.password, admins.password))
            {

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login.identifiant),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }


        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("id,identifiant,password,passwordHash,passwordSalt")] Admins admins)
        {
            if (ModelState.IsValid)
            {

                _context.Add(admins);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(admins);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("id,identifiant,password,passwordHash,passwordSalt")] Admins admins)
        {
            if (id != admins.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admins);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminsExists(admins.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return Ok(admins);
        }

        // GET: Admins/Delete/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Admins == null)
            {
                return NotFound();
            }

            var admins = await _context.Admins
                .FirstOrDefaultAsync(m => m.id == id);
            if (admins == null)
            {
                return NotFound();
            }

            return Ok(admins);
        }


        private bool AdminsExists(int id)
        {
          return (_context.Admins?.Any(e => e.id == id)).GetValueOrDefault();
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr"));

            var token = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:4200",
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}

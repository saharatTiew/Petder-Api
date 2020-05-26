using Microsoft.AspNetCore.Mvc;
using petder.data;
using petder.Models.ViewModels;
using System.Linq;
using petder.Services;
using petder.Messages;
using AutoMapper;
using petder.Models.DataModels;
using System.Threading.Tasks;
using System;
using petder.Providers;

namespace petder.Controllers.LoginControllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(ApplicationDbContext _db, IMapper _mapper) : base(_db, _mapper) {}

        [HttpGet("addresses")]
        public IActionResult Addresses()
        {
            var addresses = _db.Addresses.Select(x => new
                                              {
                                                 AddressId = x.address_id,
                                                 Name = x.name
                                              });
            return Json(addresses);
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginViewModel model)
        {
            var user = _db.User.FirstOrDefault(x => x.username == model.username
                                               && x.password == model.password);
            
            return Json(new {UserId = user.user_id});
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var hasUser = _db.User.Any(x => x.username == model.Username);
            if (hasUser) return BadRequest(ErrorMessage.UserNameExist());

            var user = _mapper.Map<RegisterViewModel, User>(model);
            var address = _db.Addresses.FirstOrDefault(x => x.name == model.Address);
            if (address == null) return BadRequest(ErrorMessage.AddressNotFound());
            user.address_id = address.address_id;
            
            _db.User.Add(user);
            await _db.SaveChangesAsync();
            return Json(user);
        }
    }
}
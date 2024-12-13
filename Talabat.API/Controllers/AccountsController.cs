using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using talabat.Core.Models.Identity;
using talabat.Core.Services;
using Talabat.API.DTO;
using Talabat.API.Errors;
using Talabat.API.Extension;
using Talabat.Service;

namespace Talabat.API.Controllers
{

    public class AccountsController : APIBaseController
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _service;
        private readonly IMapper mapper;

        public AccountsController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,ITokenService service,IMapper mapper)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._service = service;
            this.mapper = mapper;
        }

        //[HttpGet]
        //public ActionResult Index()
        //{
        //    return Ok();
        //}
        // Register

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {

            if(IsEmailExists(model.email).Result.Value)
                return BadRequest(new ApiRespones(400,"This Email Is Already Exist"));
            var user = new AppUser()
            {
                DisplayName = model.displayName,
                Email = model.email,
                UserName = model.email.Split('@')[0],
               // PhoneNumber = model.PhoneNumber,
            };
            var Result = await _userManager.CreateAsync(user, model.password);
            if (!Result.Succeeded) return BadRequest(new ApiRespones(400));
            var returnData = new UserDto()
            {
                displayName = model.displayName,
                email = model.email,
                token = await _service.CreateTokenAsync(user, _userManager)
                //Token="ThisIsToken"
            };
            return Ok(returnData);

        }



        //login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
         var user=await _userManager.FindByEmailAsync(login.email);
         if (user == null) return Unauthorized(new ApiRespones(401));
         var result = await _signInManager.CheckPasswordSignInAsync(user, login.password, false);
         if(!result.Succeeded) return Unauthorized(new ApiRespones(401));
        var returnedData = new UserDto()
        {
             displayName = user.DisplayName,
             email = user.Email,
            token = await _service.CreateTokenAsync(user, _userManager)
        };
             return Ok(returnedData);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var Email=User.FindFirstValue(ClaimTypes.Email);
            var user=await _userManager.FindByEmailAsync(Email);
            var ReturnedData = new UserDto()
            {
                displayName = user.DisplayName,
                email = user.Email,
                token = await _service.CreateTokenAsync(user, _userManager)
            };
            return Ok(ReturnedData);
        }
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>>GetCurrentUserByAddress()
        {
           // var Email = User.FindFirstValue(ClaimTypes.Email);
            var user=await _userManager.FindUserWithAddressAsync(User);
            var mappedAddress= mapper.Map<Address,AddressDto>(user.address);
            return Ok(mappedAddress);

        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto Updatedaddress)
        {
            var user = await _userManager.FindUserWithAddressAsync(User);
            if(user == null) return Unauthorized(new ApiRespones(401));
            var address = mapper.Map<AddressDto, Address>(Updatedaddress);
            address.Id=user.address.Id;
            var Result = await _userManager.UpdateAsync(user);
            if(Result.Succeeded) return BadRequest(new ApiRespones(400));
            return Ok(Updatedaddress);


        }
        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>>IsEmailExists(string  Email)
        {
            return await _userManager.FindByEmailAsync(Email) is not null;
        }
    }
}

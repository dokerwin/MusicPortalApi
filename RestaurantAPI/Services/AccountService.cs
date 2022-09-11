using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using RestaurantAPI.RestaurantRoot.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RestaurantAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly RestaurantDbContextcs _restaurantDb;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authentication;
        public AccountService(RestaurantDbContextcs restaurantDb, IMapper mapper, IPasswordHasher<User> passwordHasher, AuthenticationSettings authentication)
        {
            _restaurantDb = restaurantDb;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _authentication = authentication;
        }

        public string GenerateJwt(LoginDto user)
        {
            var searchUser = _restaurantDb.Users.Include(u=> u.Role).FirstOrDefault(u =>u.Email == user.Email);
            if (searchUser is null)
            {
                throw new BadRequestException("Invalid user name or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(searchUser, searchUser.PasswordHash, user.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid user name or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, searchUser.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{ searchUser.Firstname }{searchUser.LastName}"),
                new Claim(ClaimTypes.Role, $"{ searchUser.Role.Name}"),
                new Claim("DateOfBirth", searchUser.DateOfBirth.Value.ToString("yyyy-MM-dd")),
                new Claim("Nationality", searchUser.Nationality)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_authentication.JwtKey));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddDays(_authentication.JwtExpireDays);

            var token = new JwtSecurityToken(_authentication.JwtIssuer, _authentication.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        public void RegisterUser(RegistereUserDto userDto)
        {
            var newUser = _mapper.Map<User>(userDto);
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, userDto.Password);
            _restaurantDb.Add(newUser);
            _restaurantDb.SaveChanges();
        }
    }
}

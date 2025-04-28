using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<bool> ChangeEmailAsync(string userId, string newEmail)
        {
            var identityUser = await _userManager.FindByIdAsync(userId);
            if (identityUser == null)
                return false;

            identityUser.Email = newEmail;
            identityUser.UserName = newEmail;

            var result = await _userManager.UpdateAsync(identityUser);
            return result.Succeeded;
        }

        public async Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var identityUser = await _userManager.FindByIdAsync(userId);
            if (identityUser == null)
                return false;

            var result = await _userManager.ChangePasswordAsync(identityUser, currentPassword, newPassword);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var identityUser = await _userManager.FindByIdAsync(userId);
            if (identityUser == null)
                return false;

            var result = await _userManager.DeleteAsync(identityUser);
            return result.Succeeded;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var identityUsers = _userManager.Users.ToList();

            var users = _mapper.Map<IEnumerable<User>>(identityUsers);

            return users;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);

            if (identityUser == null)
                return null;

            var user = _mapper.Map<User>(identityUser);

            return user;
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            var identityUser = await _userManager.FindByIdAsync(userId);

            if (identityUser == null)
                return null;

            var user = _mapper.Map<User>(identityUser);

            return user;
        }

        public async Task<bool> LoginUserAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return false;

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);

            return result.Succeeded;
        }

        public async Task RegisterUserAsync(User user, string password)
        {
            var identityUser = _mapper.Map<ApplicationUser>(user);
            identityUser.UserName = user.Email;

            await _userManager.CreateAsync(identityUser, password);
        }

        public async Task<bool> ResetPasswordAsync(string userId, string newPassword)
        {
            var identityUser = await _userManager.FindByIdAsync(userId);
            if (identityUser == null)
                return false;

            var token = await _userManager.GeneratePasswordResetTokenAsync(identityUser);
            var result = await _userManager.ResetPasswordAsync(identityUser, token, newPassword);
            return result.Succeeded;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var identityUser = await _userManager.FindByIdAsync(user.Id);
            if (identityUser == null)
                return false;

            identityUser.Email = user.Email;
            identityUser.UserName = user.Email;

            var result = await _userManager.UpdateAsync(identityUser);
            return result.Succeeded;
        }
    }
}

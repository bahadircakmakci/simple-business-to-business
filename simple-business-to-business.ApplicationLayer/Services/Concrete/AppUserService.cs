using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using simple_business_to_business.ApplicationLayer.Modes.DTOs;
using simple_business_to_business.ApplicationLayer.Services.Interfaces;
using simple_business_to_business.DomainLayer.Entities.Concrete;
using simple_business_to_business.DomainLayer.Enums;
using simple_business_to_business.DomainLayer.UnitOfWork;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace simple_business_to_business.ApplicationLayer.Services.Concrete
{
    public class AppUserService : IAppUserService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUsers> _userManager;
        private readonly SignInManager<AppUsers> _signInManager;

        public AppUserService(IUnitOfWork unitOfWork,
                              IMapper mapper,
                              UserManager<AppUsers> userManager,
                              SignInManager<AppUsers> signInManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public Task DeleteUser(params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public async Task EditUser(EditProfileDTO editProfileDTO)
        {
            var user = await _unitOfWork.AppUser.GetById(editProfileDTO.Id);
            if (user != null)
            {
                if (editProfileDTO.Image != null)
                {
                    using var image = Image.Load(editProfileDTO.Image.OpenReadStream());
                    image.Mutate(x => x.Resize(256, 256));
                    string newName = Guid.NewGuid().ToString();
                    image.Save($"wwwroot/images/users/{newName}.jpg");
                    user.ImagePath = ($"/images/users/{newName}.jpg");
                }

                if (editProfileDTO.Password != null)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, editProfileDTO.Password);
                    await _userManager.UpdateAsync(user);
                }

                if (editProfileDTO.UserName != null)
                {
                    var isUserNameExist = await _userManager.FindByNameAsync(editProfileDTO.UserName);

                    if (isUserNameExist == null) await _userManager.SetUserNameAsync(user, editProfileDTO.UserName);
                }

                if (editProfileDTO.Email != user.Email)
                {
                    var isEmailExist = await _userManager.FindByEmailAsync(editProfileDTO.Email);
                    if (isEmailExist == null) await _userManager.SetEmailAsync(user, editProfileDTO.Email);
                }

                user.FullName = editProfileDTO.FullName;
                _unitOfWork.AppUser.Update(user);
                await _unitOfWork.Commit();

            }
        }

        public async Task<EditProfileDTO> GetById(int id)
        {
            return _mapper.Map<EditProfileDTO>(await _unitOfWork.AppUser.GetById(id));
        }

        public async Task<SignInResult> Login(LoginDTO loginDTO)
        {
            var usr = await _unitOfWork.AppUser.GetFilteredFirstOrDefault(selector: x => x.Status, expression: x => x.UserName == loginDTO.UserName);
            if (usr != null)
            {
                if (usr != Status.Passive)
                {
                    return await _signInManager.PasswordSignInAsync(loginDTO.UserName.ToUpper(), loginDTO.Password, loginDTO.RememberMe, false);
                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterDTO registerDTO)
        {

            var user = _mapper.Map<AppUsers>(registerDTO);

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            //if (result.Succeeded)  await _signInManager.SignInAsync(user, false);

            return result;
        }

        public async Task<List<SearchUserDTO>> SearchUser(string keyword, int pageIndex)
        {
            var users = await _unitOfWork.AppUser.GetFilteredList(
               selector: x => new SearchUserDTO
               {
                   Id = x.Id,
                   FullName = x.FullName,
                   UserName = x.UserName,
                   ImagePath = x.ImagePath
               },
               expression: x => x.UserName.Contains(keyword) || x.FullName.Contains(keyword),
               pageIndex: pageIndex,
               pageSize: 10);

            return users;
        }

        public async Task<List<ListUserDTO>> ListUser(int pageIndex)
        {
            var users = await _unitOfWork.AppUser.GetFilteredList(
               selector: x => new ListUserDTO
               {
                   Id = x.Id,
                   FullName = x.FullName,
                   UserName = x.UserName,
                   ImagePath = x.ImagePath,
                   CompanyId=x.CompanyId,
                   Companies= _mapper.Map<CompaniesDto>(x.Companies),
                   Email=x.Email,
                   PhoneNumber=x.PhoneNumber,
                   PlasiyerCode=x.PlasiyerCode,
                   Status=x.Status
               },
               expression:null,
               include:x=>x.Include(z=>z.Companies),
               pageIndex: pageIndex,
               pageSize: 10);

            return users;
        }

        public async Task<int> UserIdFromName(string userName)
        {
            return await _unitOfWork.AppUser.GetFilteredFirstOrDefault(selector: x => x.Id, expression: x => x.UserName == userName);
        }
    }
}

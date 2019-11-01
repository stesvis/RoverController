using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using RoverController.Lib;
using RoverController.Web.DTOs;
using RoverController.Web.Mapper;
using RoverController.Web.Models;
using RoverController.Web.Models.Helpers;
using RoverController.Web.Services.Base;
using RoverController.Web.ViewModels;
using RoverController.Web.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoverController.Web.Services.Users
{
    public class UserService : BaseService, IUserService
    {
        protected IAppMapper AppMapper { get; }

        public UserService(IAppMapper appMapper)
            : base()
        {
            AppMapper = appMapper;
        }

        /// <summary>
        /// Returns a list of all the users without their list of clients
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserDTO> All()
        {
            using (var userManager = UserManager)
            {
                var userDTOs = new List<UserDTO>();
                foreach (var user in userManager.Users)
                {
                    //var userDTO = Mapper.MapUser(user);
                    var userDTO = AutoMapper.Mapper.Map<UserDTO>(user);
                    userDTOs.Add(userDTO);
                }

                return userDTOs;
            }
        }

        /// <summary>
        /// Creates a new app user and assigns it to the provided client
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<UserResult> Create(RegisterUserViewModel model, string currentUserId)
        {
            using (var userManager = UserManager)
            {
                var userResult = new UserResult();
                var user = AutoMapper.Mapper.Map<ApplicationUser>(model);
                user.CreatedDate = DateTime.Now;

                userResult.Result = await userManager.CreateAsync(user, model.Password);
                if (userResult.Result.Succeeded)
                {
                    userResult.User = user;

                    // Put user in role
                    await userManager.AddToRoleAsync(user.Id, model.RoleName);
                }

                return userResult;
            }
        }

        /// <summary>
        /// Returns the ApplicationUser by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserDTO Get(string userId)
        {
            var applicationUser = new ApplicationUser();
            using (var userManager = UserManager)
            {
                applicationUser = userManager.FindById(userId);
            }

            var userDTO = AutoMapper.Mapper.Map<UserDTO>(applicationUser);

            return userDTO;
        }

        /// <summary>
        /// Returns the list of users by ClientId
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="includeExternalUsers">
        /// TRUE: include the users that have access to this client even if it's not their primary client
        /// </param>
        /// <returns></returns>
        //public IEnumerable<UserDTO> Filter(
        //    int clientId,
        //    bool includeExternalUsers,
        //    bool includeSuperAdmin,
        //    out int recordsTotal,
        //    int? takeFrom = null,
        //    int? takeCount = null,
        //    string searchValue = null,
        //    int? orderByIndex = (int)UserColumns.UserName,
        //    string orderDirection = "asc",
        //    bool includeDeleted = false)
        //{
        //    var applicationUsersList = new List<ApplicationUser>();
        //    //get all the users except SuperAdmin and Sales
        //    using (var unitOfWork = new UnitOfWork())
        //    {
        //        var applicationUsers = unitOfWork.ClientUsers.GetAllUsersByClientId(clientId, includeExternalUsers, includeSuperAdmin, searchValue, includeDeleted, orderByIndex, orderDirection);

        // // Why does this not work? //var userDTOs =
        // AutoMapper.Mapper.Map<IEnumerable<UserDTO>>(users); recordsTotal = applicationUsers.Count();

        // if (takeFrom.HasValue) { applicationUsers = applicationUsers.Skip(takeFrom.Value); }

        // if (takeCount.HasValue) { applicationUsers = applicationUsers.Take(takeCount.Value); }

        // applicationUsersList = applicationUsers.ToList(); }

        // var userDTOs = AutoMapper.Mapper.Map<IEnumerable<UserDTO>>(applicationUsersList);

        // foreach (var userDTO in userDTOs) { userDTO.PrimaryClient = GetPrimaryClient(userDTO.Id);
        // userDTO.PrimaryClientId = userDTO.PrimaryClient?.Id > 0 ? userDTO.PrimaryClient.Id : 0; }

        //    return userDTOs;
        //}

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="model"></param>
        /// <param name="currentUserId">The Id of the user performing the update</param>
        /// <returns></returns>
        public async Task<UserResult> Update(EditUserViewModel model, string currentUserId, ApplicationSignInManager signInManager)
        {
            var userResult = new UserResult();

            using (var userManager = UserManager)
            {
                var provider = new DpapiDataProtectionProvider("RoverController");
                userManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("ChangePassword"));

                //find the user
                ApplicationUser applicationUser = await userManager.FindByIdAsync(model.Id);

                // If we could not find the user, throw an exception
                if (applicationUser == null)
                {
                    throw new Exception("Could not find the User");
                }

                AppMapper.MapUser(model, ref applicationUser);

                // Lets check if the account needs to be unlocked
                if (userManager.IsLockedOut(applicationUser.Id))
                {
                    // Unlock user
                    await userManager.ResetAccessFailedCountAsync(applicationUser.Id);
                }

                userResult.Result = await userManager.UpdateAsync(applicationUser);

                if (!userResult.Result.Succeeded)
                {
                    return userResult;
                }

                //------------------ User Roles
                //if (model.Id != Global.CurrentUserId)
                //{
                var roles = await userManager.GetRolesAsync(applicationUser.Id);
                var removeAllRolesResult = await userManager.RemoveFromRolesAsync(applicationUser.Id, roles.ToArray());
                //var removeAllRolesResult = await RemoveAllRoles(applicationUser.Id);

                if (removeAllRolesResult.Succeeded)
                {
                    foreach (var roleDTO in model.UserRoles)
                    {
                        await userManager.AddToRoleAsync(applicationUser.Id, roleDTO.Name);
                    }
                }
                //}

                //------------------ User Identity
                // Was a password sent across?
                if (!model.Password.IsEmpty())
                {
                    IdentityResult changePasswordResult = null;

                    try
                    {
                        if (await userManager.IsInRoleAsync(currentUserId, UserRoles.Admin))
                        {
                            // To reset the password for others we need a token
                            var token = await userManager.GeneratePasswordResetTokenAsync(model.Id);
                            changePasswordResult = await userManager.ResetPasswordAsync(model.Id, token, model.Password);
                        }
                        else
                        {
                            changePasswordResult = userManager.ChangePassword(model.Id, model.OldPassword, model.Password);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                    if (changePasswordResult.Errors.Count() > 0)
                    {
                        throw new Exception(changePasswordResult.Errors.FirstOrDefault());
                    }
                    else
                    {
                        if (model.Id == currentUserId)
                        {
                            applicationUser = await userManager.FindByIdAsync(applicationUser.Id);
                            await signInManager.SignInAsync(applicationUser, isPersistent: true, rememberBrowser: true);
                        }
                    }
                }

                userResult.User = applicationUser;
            }

            return userResult;
        }

        public IEnumerable<RoleDTO> GetAllRoles()
        {
            using (var roleManager = RoleManager)
            {
                var roles = roleManager.Roles.OrderBy(r => r.Order);
                var roleDTOs = AutoMapper.Mapper.Map<IEnumerable<RoleDTO>>(roles);

                return roleDTOs;
            }
        }

        public async Task<IdentityResult> RemoveAllRoles(string userId)
        {
            using (var userManager = UserManager)
            {
                var roles = await userManager.GetRolesAsync(userId);
                var result = await userManager.RemoveFromRolesAsync(userId, roles.ToArray());

                return result;
            }
        }
    }
}
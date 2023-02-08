using Azzuha.Data.Context;
using Azzuha.Data.DTOs;
using Azzuha.DataViewModels.Response.AdminResponse;
using Azzuha.Services.Interface.admin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Azzuha.Services.Implementation.admin
{
  public  class AdminAccountService:IAdminAccountService
    {
        public List<UserList> GetUsers(string SearchFilter, int skip, int take)
        {
            try
            {
                IQueryable<UserList> query;
                using (var db = new AzzuhaDbContext())
                {
                    query = db.AspNetUsers.Select(x => new UserList
                    {
                        FullName = x.FullName,
                        UserName = x.UserName,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber,
                        Designation = x.Designation,
                        Id = x.Id,
                        EnableFlag=x.EnableFlag,
                        CreatedDtg=x.CreationDate
                        
                    }).AsQueryable();

                    if (!string.IsNullOrEmpty(SearchFilter))
                    {
                        int totalCases = -1;
                        var isNumber = Int32.TryParse(SearchFilter, out totalCases);
                        if (isNumber)
                        {
                            //query = query.Where(
                            //    x => x.TotalCases == totalCases
                            //);
                        }
                        else
                        {
                            query = query.Where(
                            x => x.FullName.ToLower().Contains(SearchFilter.ToLower())
                        );
                        }
                    }
                    return query.Skip(skip).Take(take).ToList();
                }


                using (var db = new AzzuhaDbContext())
                {
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserList GetUser(string id)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    return db.AspNetUsers.Where(x => x.Id == id).Select(x => new UserList
                    {
                        FullName = x.FullName,
                        UserName = x.UserName,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber,
                        Designation = x.Designation,
                        Password = x.UnhashPassword,
                        Id = x.Id,
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckUserStatus(string userName, string password)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    return (bool)db.AspNetUsers.FirstOrDefault(x => x.UserName == userName && x.UnhashPassword == password).EnableFlag;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string FindUserByEmail(string email)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    var user = db.AspNetUsers.FirstOrDefault(x => x.Email.Equals(email));

                    return user == null ? "" : user.Id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string FindUserByUsername(string username)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    var user = db.AspNetUsers.FirstOrDefault(x => x.UserName.Equals(username));

                    return user == null ? "" : user.Id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
   
        public void UpdatePassword(string email, string password)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    var user = db.AspNetUsers.FirstOrDefault(x => x.Email == email);
                    user.UnhashPassword = password;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CountryCodes GetPhoneNumberByDialCode(string dialCode)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    return db.PhoneCountryCode.Where(x => x.DialCode.Equals(dialCode)).Select(x => new CountryCodes { Code = x.DialCode, Name = x.CountryName, Url = "/Content/Flags/" + x.LowResulationImageUrl1 }).FirstOrDefault();
                }
                }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RoleList> getRoleList()
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    return db.AspNetRoles.Select(x => new RoleList { Name = x.Name, Id = x.Id }).ToList();
                }
                }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetRightByRoleId(string role)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    return db.AssignedRights.Where(x => x.RoleId == role).Select(x => x.Rights.Name).ToList();
                }
                }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetRoleIdByUsername(string username)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    return db.AspNetUsers.FirstOrDefault(x => x.UserName == username).AspNetUserRoles.FirstOrDefault().RoleId;
                }
                }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsRightExist(string roleId, int rightId)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    return db.AssignedRights.FirstOrDefault(x => x.RoleId == roleId && x.RightsId == rightId) == null ? false : true;
                }
                }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UserCountByRoleName(string roleName)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    var role = db.AspNetRoles.Where(x => x.Name.Equals(roleName)).FirstOrDefault(); ;

                    return role.AspNetUserRoles.Count();
                }
                }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CountryCodes> GetCountryCode()
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    return db.PhoneCountryCode.Select(x => new CountryCodes { Code = x.DialCode, Name = x.CountryName, Url = "/img/Flags/" + x.LowResulationImageUrl1 }).ToList();
                }
                }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SaveRight(string roleId, int rightId)
        {
            try
            {
                bool res = true;
                using (var db = new AzzuhaDbContext())
                {
                    AssignedRights model = new AssignedRights { RightsId = rightId, RoleId = roleId };
                db.AssignedRights.Add(model);
                db.SaveChanges();
}
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RemoveRight(string roleId, int rightId)
        {
            try
            {
                bool res = true;
                using (var db = new AzzuhaDbContext())
                {
                    db.AssignedRights.Remove(db.AssignedRights.FirstOrDefault(x => x.RoleId == roleId && x.RightsId == rightId));
                    db.SaveChanges();
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckUserName(string UserName)
        {
            bool res = false;
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    var count = db.AspNetUsers.Where(x => x.UserName.Equals(UserName)).Count();
                    if (count > 0)
                    {
                        res = false;
                    }
                    else
                    {
                        res = true;
                    }
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string FindUserIdByEnrolId(int enrolId)
        {
            throw new NotImplementedException();
        }
    }

}
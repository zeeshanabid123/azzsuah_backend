using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Azzuha.AdminPanel.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
      
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
       
    }

    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Designation { get; set; }
        public bool EnableFlag { get; set; }
        public string UnhashPassword { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationBy { get; set; }
        public DateTime UpdationDate { get; set; }
        public string UpdationBy { get; set; }
        public bool EnalbleFalg { get; set; }
        public bool IsBlock { get; set; }
    }
}

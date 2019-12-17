using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonorTraceAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace DonorTraceAPI.Data
{
    public class DbSeeder
    {
        public static void Seed(DataContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            // Create default Users(if there are none)
            // if (!dbContext.Users.Any())
            //{
             CreateUsers(dbContext, roleManager, userManager).GetAwaiter().GetResult();
            //}
           
            CreateOfficers(dbContext).GetAwaiter().GetResult();

            // if(!dbContext.Facilities.Any()) CreateFacility(dbContext).GetAwaiter().GetResult();

            // if (!dbContext.Roles.Any())
            // CreateRoles(dbContext, roleManager).GetAwaiter().GetResult();

        }
        private static async Task CreateUsers(DataContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            // local variables
            DateTime createdDate = DateTime.Now;
            DateTime lastModifiedDate = DateTime.Now;

            string role_Administrator = "Administrator";
            string role_RegisteredUser = "Registered User";
            string role_MedicalOfficer = "Medical Officer";

            //Create Roles (if they doesn't exist yet)
            if (!await roleManager.RoleExistsAsync(role_Administrator))
            {
                await roleManager.CreateAsync(new
                  IdentityRole(role_Administrator));
            }
            if (!await roleManager.RoleExistsAsync(role_RegisteredUser))
            {
                await roleManager.CreateAsync(new
                   IdentityRole(role_RegisteredUser));
            }
            if (!await roleManager.RoleExistsAsync(role_MedicalOfficer))
            {
                await roleManager.CreateAsync(new
                    IdentityRole(role_MedicalOfficer));
            }

            // Create the "Admin" User account (if it doesn't exist already)
            var userAdmin = new User()
            {

                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "dtadmin",
                Email = "sa16agw@herts.ac.uk",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate,

            };

            // Insert the Admin user into the Database
            if (await userManager.FindByNameAsync(userAdmin.UserName) == null)
            {
                await userManager.CreateAsync(userAdmin, "Lynabena@1");
                //await userManager.AddToRoleAsync(user_Admin, role_RegisteredUser);
                await userManager.AddToRoleAsync(userAdmin, role_Administrator);

            }


            // Create some sample registered user accounts (if they don't exist already)
            var userOfficer = new User()
            {

                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "john.manu",
                
               // Email = "selormkwe@yahoo.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };

            //// Insert sample registered users into the Database
            if (await userManager.FindByNameAsync(userOfficer.UserName) == null)
            {
                await userManager.CreateAsync(userOfficer, "Password@1");
                await userManager.AddToRoleAsync(userOfficer, role_MedicalOfficer);

            }

           // await dbContext.SaveChangesAsync();
        }

        private static async Task CreateFacility(DataContext dbContext)
        {
            var data = new Facility()
            {
                Name = "Nyaho Medical Centre",
                ContactNo = "0307086490",
                RegistrationNo = "NMC101",
                Created = DateTime.Now,
                CreatedBy = "traceadmin",
                LastUpdated = DateTime.Now,
                Address = "Airport Residentail Area, 35 Kofi Annan ST, Accra"
            };
            dbContext.Facilities.Add(data);

            var data2 = new Facility()
            {
                Name = "Medifem Hospital",
                ContactNo = "0302433963",
                RegistrationNo = "MEDH101",
                Created = DateTime.Now,
                CreatedBy = "traceadmin",
                LastUpdated = DateTime.Now,
                Address = "Margaret Anderson Ave, Accra"
            };
            dbContext.Facilities.Add(data2);


            await dbContext.SaveChangesAsync();
        }
        private static async Task CreateOfficers(DataContext dbContext)
        {
            var data = new MedicalOfficer()
            {
                UserName = "john.manu",
                Firstname = "John",
                Lastname = "Manu",
                Department = "GP",
                FacilityId = 1,
                ContactNo = "0273344096",
                Created = DateTime.Now,
                CreatedBy = "traceadmin"
            };
           dbContext.Officers.Add(data);

      

           await dbContext.SaveChangesAsync();
        }

    }
}

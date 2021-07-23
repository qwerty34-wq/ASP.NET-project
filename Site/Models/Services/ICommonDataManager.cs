using Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Models.Services
{
    public interface ICommonDataManager
    {
        //User GetUserById(Guid id);

        Guid? LogIn(string login, string password);


        // User work
        IList<User> GetUsers();
        void AddUser(User user);
        User GetUserById(Guid Id);
        void DeleteUser(Guid Id);
        void _UpdateUser(Guid Id, User user);
        IList<User> GetAdmins();


        // Vehicle work
        IList<Vechicle> GetVechicles();
        void AddVechicle(Vechicle vechicle);
        Vechicle GetVechicleById(Guid Id);
        void UpdateVechicle(Guid Id, Vechicle vechicle);
        void DeleteVechicle(Guid Id);

        // All
        IList<VechicleUser> GetAllData();
        VechicleUser GetAllDataForVechicle(Guid Id);
    }
}

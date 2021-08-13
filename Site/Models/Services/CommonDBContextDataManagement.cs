using Site.DAL;
using Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Site.Models.Services
{
    public class CommonDBContextDataManagement : ICommonDataManager
    {

        private readonly ApplicationDBContext _context;
        private readonly IHashDataManager _hashDataManager;

        public CommonDBContextDataManagement(ApplicationDBContext context, IHashDataManager hashDataManager)
        {
            _context = context;
            _hashDataManager = hashDataManager;
        }

        private string GetHashByString(string password)
        {
            string hash = String.Empty;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                hash = _hashDataManager.GetHash(sha256Hash, password);
            }
            return hash;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void AddVechicle(Vechicle vechicle)
        {
            _context.Vechicles.Add(vechicle);
            _context.SaveChanges();
        }

        public void DeleteUser(Guid Id)
        {
            var user = _context.Users.Find(Id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public void DeleteVechicle(Guid Id)
        {
            var vechicle = _context.Vechicles.Find(Id);
            if (vechicle != null)
            {
                _context.Vechicles.Remove(vechicle);
                _context.SaveChanges();
            }
        }

        public IList<User> GetAdmins()
        {
            return _context.Users.AsQueryable().Where(x => x.isAdmin == true).ToList();
        }

        public IList<VechicleUser> GetAllData()
        {
            IList<VechicleUser> data = new List<VechicleUser>();
            foreach (var vechicle in GetVechicles())
            {
                if (vechicle.UserId == default)
                {
                    data.Add(new VechicleUser() { Vechicle = vechicle, User = new User() { Name = "No User", Surname = "" } });
                    continue;
                }

                var user = GetUserById(vechicle.UserId);
                data.Add(new VechicleUser() { Vechicle = vechicle, User = user });
            }
            return data;
        }

        public VechicleUser GetAllDataForVechicle(Guid Id)
        {
            var vechicle = GetVechicleById(Id);

            User user;

            if (vechicle.UserId != default)
            {
                user = GetUserById(vechicle.UserId);
            }
            else
            {
                user = new User() { Name = "No User", Surname = "" };
            }

            return new VechicleUser() { Vechicle = vechicle, User = user };
        }

        public User GetUserById(Guid Id)
        {
            return _context.Users.Find(Id);
        }

        public IList<User> GetUsers()
        {
            return _context.Users.AsQueryable().ToList();
        }

        public Vechicle GetVechicleById(Guid Id)
        {
            return _context.Vechicles.Find(Id);
        }

        public IList<Vechicle> GetVechicles()
        {
            return _context.Vechicles.AsQueryable().ToList();
        }

        public Guid? LogIn(string login, string password)
        {
            string hash = String.Empty;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                hash = _hashDataManager.GetHash(sha256Hash, password);
            }

            var user = _context.Users.SingleOrDefault(x => x.Login == login && x.Hash == hash && x.isAdmin == true);

            if (user != null)
            {
                return user.Id;
            }

            return null;
        }

        public bool UpdateIsAdmin(Guid id, bool isAdmin)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user is null)
            {
                return false;
            }

            user.isAdmin = !user.isAdmin;
            _context.SaveChanges();
            return true;
        }

        public void UpdateVechicle(Guid Id, Vechicle vechicle)
        {
            var v = GetVechicleById(Id);
            v.Name = vechicle.Name;
            v.Model = vechicle.Model;
            v.Country = vechicle.Country;
            v.VechicleType = vechicle.VechicleType;
            v.Manufactured = vechicle.Manufactured;
            v.VechicleState = vechicle.VechicleState;
            v.Mileage = vechicle.Mileage;
            v.Price = vechicle.Price;
            v.UserId = vechicle.UserId;
            _context.SaveChanges();
        }

        public bool UpdateVechicleState(Guid id, VechicleState state)
        {
            var vechicle = _context.Vechicles.FirstOrDefault(x => x.Id == id);

            if (vechicle is null)
            {
                return false;
            }

            vechicle.VechicleState = state;
            _context.SaveChanges();
            return true;
        }

        public bool UpdateVechicleType(Guid id, VechicleType type)
        {
            var vechicle = _context.Vechicles.FirstOrDefault(x => x.Id == id);

            if (vechicle is null)
            {
                return false;
            }

            vechicle.VechicleType = type;
            _context.SaveChanges();
            return true;
        }

        public void _UpdateUser(Guid Id, User user)
        {
            var u = GetUserById(Id);
            u.Name = user.Name;
            u.Surname = user.Surname;
            u.Age = user.Age;
            u.Login = user.Login;
            u.isAdmin = user.isAdmin;
            u.Country = user.Country;
            u.City = user.City;
            u.PhoneNumber = user.PhoneNumber;
            u.Email = user.Email;
            _context.SaveChanges();
        }
    }
}

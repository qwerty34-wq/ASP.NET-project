﻿using Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Site.Models.Services
{
    public class CommonDataManager : ICommonDataManager
    {

        private IList<User> _users = new List<User>();
        private IList<Vechicle> _vechicles= new List<Vechicle>();
        private readonly IHashDataManager _hashDataManager;
      

        public CommonDataManager(IHashDataManager hashDataManager)
        {

            _hashDataManager = hashDataManager;


            _users.Add(new User()
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Surname = "Doe",
                Age = 20,
                Login = "1",
                Hash = GetHashByString("1"),
                isAdmin = true,
            });


            _users.Add(new User()
            {
                Id = Guid.NewGuid(),
                Name = "Bob",
                Surname = "Smal",
                Age = 12,
                Login = "1",
                Hash = GetHashByString("q"),
                isAdmin = false,
                Country = "USA",
                City = "NY",
                PhoneNumber = "+123456789",
                Email = "bob@gmail.com"
            });


            _users.Add(new User()
            {
                Id = Guid.NewGuid(),
                Name = "Tom",
                Surname = "Fil",
                Age = 19,
                Login = "1",
                Hash = GetHashByString("w"),
                isAdmin = false,
                Country = "German",
                City = "Berlin",
                PhoneNumber = "+123456789",
                Email = "tom@gmail.com"
            });

            _users.Add(new User()
            {
                Id = Guid.NewGuid(),
                Name = "Sam",
                Surname = "Huil",
                Age = 32,
                Login = "1",
                Hash = GetHashByString("e"),
                isAdmin = false,
                Country = "England",
                City = "London",
                PhoneNumber = "+123456789",
                Email = "sam@gmail.com"
            });

            _vechicles.Add(new Vechicle()
            {
                Id = Guid.NewGuid(),
                Name = "Tesla",
                Model = "Model X",
                Country = "USA",
                VechicleType = VechicleType.Car,
                Manufactured = new DateTime(2010, 01, 01),
                VechicleState = VechicleState.Good,
                Mileage = 100000,
                Price = 90000,
                User = _users[0]
            });

            _vechicles.Add(new Vechicle()
            {
                Id = Guid.NewGuid(),
                Name = "Tesla",
                Model = "Model Y",
                Country = "USA",
                VechicleType = VechicleType.Car,
                Manufactured = new DateTime(2018, 01, 01),
                VechicleState = VechicleState.Good,
                Mileage = 100000,
                Price = 50000,
                User = _users[1]
            });

            _vechicles.Add(new Vechicle()
            {
                Id = Guid.NewGuid(),
                Name = "Tesla",
                Model = "Model S",
                Country = "USA",
                VechicleType = VechicleType.Car,
                Manufactured = new DateTime(2009, 01, 01),
                VechicleState = VechicleState.Good,
                Mileage = 200000,
                Price = 85000,
                User = _users[2]
            });

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

        public Guid? LogIn(string login, string password)
        {
            string hash = String.Empty;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                hash = _hashDataManager.GetHash(sha256Hash, password);
            }

            var user = _users.SingleOrDefault(x => x.Login == login && x.Hash == hash && x.isAdmin == true);

            if (user != null)
            {
                return user.Id;
            }

            return null;
        }


        // User
        public IList<User> GetUsers()
        {
            return _users;
        }

        public void AddUser(User user)
        {
            _users.Add(user);   
        }


        public User GetUserById(Guid Id)
        {
            return _users.SingleOrDefault(x => x.Id == Id);
        }

        public void DeleteUser(Guid Id)
        {
            var user = GetUserById(Id);
            _users.Remove(user);
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
        }

        // Vehicle work
        public IList<Vechicle> GetVechicles()
        {
            return _vechicles;
        }

        public void AddVechicle(Vechicle vechicle)
        {
            _vechicles.Add(vechicle);
        }

        public Vechicle GetVechicleById(Guid Id)
        {
            return _vechicles.SingleOrDefault(x => x.Id == Id);
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
        }

        public void DeleteVechicle(Guid Id)
        {
            var vechicle = GetVechicleById(Id);
            _vechicles.Remove(vechicle);
        }
    }
}
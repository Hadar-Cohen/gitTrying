using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ex2.Models.DAL;

namespace Ex2.Models
{
    public class User
    {
        int id;
        string firstName;
        string lastName;
        string email;
        string password;
        string phoneNum;
        string gender;
        int yearOfBirth;
        string genre;
        string address;

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string PhoneNum { get => phoneNum; set => phoneNum = value; }
        public string Gender { get => gender; set => gender = value; }
        public int YearOfBirth { get => yearOfBirth; set => yearOfBirth = value; }
        public string Genre { get => genre; set => genre = value; }
        public string Address { get => address; set => address = value; }
        public int Id { get => id; set => id = value; }

        public User() { }
        public User(string firstName, string lastName, string email, string password, string phoneNum, string gender, int yearOfBirth, string genre, string address)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.password = password;
            this.phoneNum = phoneNum;
            this.gender = gender;
            this.yearOfBirth = yearOfBirth;
            this.genre = genre;
            this.address = address;
            Id = -1;
        }

        public User(string email)
        {
            this.email = email;
        }
        public int Insert()
        {
            UserDataServices us = new UserDataServices();
            return us.Insert(this); //return 1/-1;
        }
        public User checkLogin(string email, string pass)
        {
            UserDataServices us = new UserDataServices();
            return us.checkLogIn(email, pass);
        }
    }
}
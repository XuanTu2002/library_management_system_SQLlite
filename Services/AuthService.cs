using System;
using System.Linq;
using LibraryManagement.Models;
using LibraryManagement.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services
{
    public class AuthService
    {
        public Staff? CurrentUser { get; private set; }

        public bool Login(string username, string password)
        {
            var staffs = DataStore.Instance.Context.Staffs;
            var staff = staffs.FirstOrDefault(s => s.Username == username);

            if (staff != null && staff.ValidatePassword(password))
            {
                CurrentUser = staff;
                return true;
            }

            return false;
        }

        public void Logout()
        {
            CurrentUser = null;
        }

        public bool IsLoggedIn()
        {
            return CurrentUser != null;
        }

        public bool ChangePassword(string oldPassword, string newPassword)
        {
            if (!IsLoggedIn()) return false;

            if (CurrentUser.ValidatePassword(oldPassword))
            {
                CurrentUser.ChangePassword(newPassword);
                DataStore.Instance.SaveChanges();
                return true;
            }

            return false;
        }
    }
}

using System;

namespace LibraryManagement.Models
{
    public class Staff : Person
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Role { get; set; }

        public Staff(int Id, string Name, string Phone, string Username, string Password, string Role)
            : base(Id, Name, Phone)
        {
            this.Username = Username;
            this.Password = Password;
            this.Role = Role;
        }

        public bool ValidatePassword(string inputPassword)
        {
            return Password == inputPassword;
        }

        public void ChangePassword(string newPassword)
        {
            if (!string.IsNullOrEmpty(newPassword) && newPassword.Length >= 6)
            {
                Password = newPassword;
            }
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"[Nhan Vien] ID: {Id} | Ten: {Name} | User: {Username} | Vai tro: {Role}");
        }
    }
}

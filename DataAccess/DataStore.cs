using System;
using System.Linq;
using LibraryManagement.Models;

namespace LibraryManagement.DataAccess
{
    public class DataStore
    {
        private static DataStore _instance;
        public static DataStore Instance => _instance ??= new DataStore();

        // Thay vì List, ta dùng Context
        public LibraryContext Context { get; private set; }

        private DataStore()
        {
            Context = new LibraryContext();

            // Tự động tạo DB nếu chưa có
            Context.Database.EnsureCreated();

            // Seed Data (Dữ liệu mẫu) - Chỉ thêm nếu DB trống
            if (!Context.Staffs.Any())
            {
                SeedData();
            }
        }

        // Phương thức lưu thay đổi xuống DB
        // Các Service cần gọi hàm này sau khi Add/Update/Delete
        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        private void SeedData()
        {
            // Thêm tài khoản Admin mặc định
            // Username: admin, Password: 123456
            Context.Staffs.Add(new Staff(1, "Administrator", "0909000999", "admin", "123456", "Admin"));

            // Thêm dữ liệu mẫu cho Sách (Tùy chọn)
            Context.Books.Add(new Book("Clean Code", "9780132350884", "Prentice Hall", 2008, "Robert C. Martin", "Technology"));

            // Quan trọng: Lưu lại sau khi Seed
            Context.SaveChanges();
        }
    }
}
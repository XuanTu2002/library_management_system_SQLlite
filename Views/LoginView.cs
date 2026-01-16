using System;
using LibraryManagement.Services;
using LibraryManagement.Utilities;
using LibraryManagement.Models;

namespace LibraryManagement.Views
{
    /// <summary>
    /// Lớp giao diện xử lý quá trình đăng nhập.
    /// Đây là điểm tiếp xúc đầu tiên của người dùng với hệ thống.
    /// </summary>
    public class LoginView
    {
        private AuthService _authService = new AuthService();

        /// <summary>
        /// Hiển thị màn hình đăng nhập và xử lý vòng lặp xác thực.
        /// </summary>
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("    HỆ THỐNG QUẢN LÝ THƯ VIỆN (CLI)     ");
                Console.WriteLine("========================================");

                Console.Write("Tên đăng nhập: ");
                string username = Console.ReadLine();

                Console.Write("Mật khẩu: ");
                // Sử dụng tiện ích InputHelper để ẩn mật khẩu
                string password = InputHelper.ReadPassword();

                Console.WriteLine("\nĐang kiểm tra thông tin...");

                // Gọi Service để xử lý logic đăng nhập
                if (_authService.Login(username, password))
                {
                    Console.WriteLine("\nĐăng nhập thành công! Nhấn phím bất kỳ để tiếp tục...");
                    Console.ReadKey();

                    // Chuyển hướng sang Menu chính
                    MainMenuView mainMenu = new MainMenuView(_authService.CurrentUser);
                    mainMenu.Show();

                    // Khi thoát khỏi Menu chính (Đăng xuất), vòng lặp này sẽ chạy lại để đăng nhập người khác
                }
                else
                {
                    Console.WriteLine("\nLỗi: Sai tên đăng nhập hoặc mật khẩu!");
                    Console.WriteLine("Nhấn phím bất kỳ để thử lại...");
                    Console.ReadKey();
                }
            }
        }
    }
}
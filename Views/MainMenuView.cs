using System;
using LibraryManagement.Models;
using LibraryManagement.Services;
using LibraryManagement.Utilities; // Để dùng InputHelper
using LibraryManagement.Views.SubMenus;

namespace LibraryManagement.Views
{
    /// <summary>
    /// Lớp giao diện Menu chính của chương trình.
    /// Điều hướng người dùng đến các chức năng quản lý cụ thể.
    /// </summary>
    public class MainMenuView
    {
        private Staff _currentUser;
        private AuthService _authService = new AuthService();

        public MainMenuView(Staff user)
        {
            _currentUser = user;
        }

        /// <summary>
        /// Hiển thị các lựa chọn chức năng chính.
        /// </summary>
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==============================================");
                Console.WriteLine($"  HỆ THỐNG QUẢN LÝ THƯ VIỆN (Người dùng: {_currentUser.Username})");
                Console.WriteLine("==============================================");
                Console.WriteLine("1. Quản lý Sách / Tài liệu");
                Console.WriteLine("2. Quản lý Độc giả");
                Console.WriteLine("3. Quản lý Mượn / Trả");
                Console.WriteLine("4. Báo cáo thống kê");
                Console.WriteLine("0. Đăng xuất");
                Console.WriteLine("----------------------------------------------");

                // Sử dụng InputHelper để nhập số an toàn
                int choice = InputHelper.ReadInt("Chọn chức năng: ");

                try
                {
                    switch (choice)
                    {
                        case 1:
                            // Gọi Menu con quản lý Sách
                            new BookMenu().Show();
                            break;
                        case 2:
                            // Gọi Menu con quản lý Độc giả
                            new ReaderMenu().Show();
                            break;
                        case 3:
                            // Gọi Menu con quản lý Mượn Trả (Đã tạo ở bước trước)
                            new BorrowMenu().Show();
                            break;
                        case 4:
                            // Gọi Menu con Báo cáo (Đã tạo ở bước trước)
                            new ReportMenu().Show();
                            break;
                        case 0:
                            _authService.Logout();
                            return; // Thoát khỏi hàm Show(), quay về LoginView
                        default:
                            Console.WriteLine("Lựa chọn không hợp lệ! Vui lòng chọn lại.");
                            Console.ReadKey();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[LỖI HỆ THỐNG]: {ex.Message}");
                    Console.WriteLine("Nhấn phím bất kỳ để quay lại menu...");
                    Console.ReadKey();
                }
            }
        }
    }
}
using System;
using System.Text;
using LibraryManagement.Views;

namespace LibraryManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            // Thiết lập hiển thị Tiếng Việt có dấu cho Console
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.Unicode;

            // Đặt tiêu đề cho cửa sổ Console
            Console.Title = "HE THONG QUAN LY THU VIEN - OOP PROJECT";

            // Khởi chạy ứng dụng bắt đầu từ màn hình Đăng nhập
            LoginView app = new LoginView();
            app.Show();
        }
    }
}
using System;
using System.Text;

namespace LibraryManagement.Utilities
{
    /// <summary>
    /// Lớp tiện ích hỗ trợ các thao tác nhập liệu từ bàn phím Console.
    /// Giúp chuẩn hóa dữ liệu đầu vào và tăng trải nghiệm người dùng (UX).
    /// </summary>
    public static class InputHelper
    {
        /// <summary>
        /// Đọc mật khẩu từ bàn phím và hiển thị dấu '*' thay vì ký tự thực.
        /// Giúp bảo mật thông tin khi người dùng nhập password.
        /// </summary>
        /// <returns>Chuỗi mật khẩu người dùng đã nhập</returns>
        public static string ReadPassword()
        {
            StringBuilder password = new StringBuilder();

            while (true)
            {
                // Đọc một phím, tham số 'true' để KHÔNG hiển thị ký tự đó ra màn hình ngay lập tức
                ConsoleKeyInfo info = Console.ReadKey(true);

                // Nếu nhấn Enter -> Kết thúc nhập
                if (info.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine(); // Xuống dòng
                    break;
                }
                // Nếu nhấn Backspace -> Xóa ký tự cuối
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        // Xóa ký tự trong bộ nhớ
                        password.Remove(password.Length - 1, 1);

                        // Xóa dấu * trên màn hình: Lùi con trỏ lại 1, ghi đè khoảng trắng, lùi lại lần nữa
                        Console.Write("\b \b");
                    }
                }
                // Các ký tự bình thường -> Thêm vào password và in ra dấu *
                else if (!char.IsControl(info.KeyChar))
                {
                    password.Append(info.KeyChar);
                    Console.Write("*");
                }
            }

            return password.ToString();
        }

        /// <summary>
        /// Yêu cầu người dùng nhập một số nguyên (int).
        /// Hàm sẽ lặp lại cho đến khi người dùng nhập đúng định dạng số.
        /// </summary>
        /// <param name="message">Thông báo nhắc người dùng nhập gì</param>
        /// <returns>Số nguyên hợp lệ</returns>
        public static int ReadInt(string message)
        {
            int result;
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (int.TryParse(input, out result))
                {
                    return result;
                }

                Console.WriteLine("Loi: Vui long nhap mot so nguyen hop le!");
            }
        }

        /// <summary>
        /// Yêu cầu người dùng nhập một chuỗi không được để trống.
        /// </summary>
        /// <param name="message">Thông báo nhắc nhập</param>
        /// <returns>Chuỗi hợp lệ</returns>
        public static string ReadString(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }

                Console.WriteLine("Loi: Du lieu khong duoc de trong!");
            }
        }
    }
}
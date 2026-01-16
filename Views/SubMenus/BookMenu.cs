using System;
using LibraryManagement.Services;
using LibraryManagement.Models;
using LibraryManagement.Utilities;

namespace LibraryManagement.Views.SubMenus
{
    /// <summary>
    /// Menu con quản lý các chức năng liên quan đến Tài liệu (Sách, Báo, Luận văn).
    /// </summary>
    public class BookMenu
    {
        private BookService _bookService = new BookService();

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== QUẢN LÝ SÁCH VÀ TÀI LIỆU ===");
                Console.WriteLine("1. Xem danh sách tài liệu");
                Console.WriteLine("2. Thêm mới tài liệu (Factory Pattern)");
                Console.WriteLine("3. Xóa tài liệu");
                Console.WriteLine("4. Tìm kiếm tài liệu");
                Console.WriteLine("0. Quay lại Menu chính");
                Console.WriteLine("--------------------------------");

                int choice = InputHelper.ReadInt("Chọn tác vụ: ");

                try
                {
                    switch (choice)
                    {
                        case 1:
                            ShowAllBooks();
                            break;
                        case 2:
                            AddNewItem();
                            break;
                        case 3:
                            DeleteBook();
                            break;
                        case 4:
                            SearchBook();
                            break;
                        case 0:
                            return;
                        default:
                            Console.WriteLine("Lựa chọn không hợp lệ.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[LỖI HỆ THỐNG]: {ex.Message}");
                }

                if (choice != 0)
                {
                    Console.WriteLine("\nNhấn phím bất kỳ để tiếp tục...");
                    Console.ReadKey();
                }
            }
        }

        private void ShowAllBooks()
        {
            var list = _bookService.GetAll();
            Console.WriteLine($"\n--- DANH SÁCH TÀI LIỆU ({list.Count}) ---");

            // Vẽ tiêu đề bảng
            string header = $"| {"LOẠI",-10} | {"ISBN",-15} | {"TIÊU ĐỀ",-30} | {"NXB",-15} | {"NĂM",-4} | {"THÔNG TIN KHÁC",-30} |";
            string line = new string('-', header.Length);

            Console.WriteLine(line);
            Console.WriteLine(header);
            Console.WriteLine(line);

            foreach (var item in list)
            {
                item.ShowInfo(); // Gọi phương thức đa hình
            }
            Console.WriteLine(line);
        }

        private void AddNewItem()
        {
            Console.WriteLine("\n--- THÊM MỚI TÀI LIỆU ---");
            Console.WriteLine("Chọn loại: [1] Sách (book) | [2] Luận Văn (thesis) | [3] Tạp Chí (magazine)");
            string type = InputHelper.ReadString("Nhập loại tài liệu: ").ToLower();

            // Xử lý trường hợp người dùng nhập số thay vì chữ
            if (type == "1") type = "book";
            if (type == "2") type = "thesis";
            if (type == "3") type = "magazine";

            // Nhập thông tin chung
            string isbn = InputHelper.ReadString("Nhập ISBN/Mã số: ");
            string title = InputHelper.ReadString("Nhập Tiêu đề: ");
            string pub = InputHelper.ReadString("Nhập NXB: ");
            int year = InputHelper.ReadInt("Nhập Năm XB: ");

            // Nhập thông tin riêng dựa trên loại (để truyền vào Factory)
            LibraryItem item = null;

            if (type == "book")
            {
                string author = InputHelper.ReadString("Nhập Tác giả: ");
                string genre = InputHelper.ReadString("Nhập Thể loại: ");
                // Gọi Factory với params
                item = LibraryItemFactory.CreateItem("book", isbn, title, pub, year, author, genre);
            }
            else if (type == "thesis")
            {
                string supervisor = InputHelper.ReadString("Nhập GVHD: ");
                string major = InputHelper.ReadString("Nhập Chuyên ngành: ");
                item = LibraryItemFactory.CreateItem("thesis", isbn, title, pub, year, supervisor, major);
            }
            else if (type == "magazine")
            {
                int issue = InputHelper.ReadInt("Nhập Số phát hành: ");
                int month = InputHelper.ReadInt("Nhập Tháng phát hành: ");
                item = LibraryItemFactory.CreateItem("magazine", isbn, title, pub, year, issue, month);
            }
            else
            {
                Console.WriteLine("Loại tài liệu không hỗ trợ!");
                return;
            }

            // Gọi Service để lưu vào DataStore
            _bookService.Add(item);
            Console.WriteLine("Thêm mới thành công!");
        }

        private void DeleteBook()
        {
            string isbn = InputHelper.ReadString("Nhập ISBN tài liệu cần xóa: ");
            _bookService.Delete(isbn);
            Console.WriteLine("Đã xóa thành công.");
        }

        private void SearchBook()
        {
            string keyword = InputHelper.ReadString("Nhập từ khóa (Tên hoặc ISBN): ");
            var results = _bookService.Search(keyword);

            Console.WriteLine($"\nTìm thấy {results.Count} kết quả:");
            foreach (var item in results)
            {
                item.ShowInfo();
            }
        }
    }
}
using System;
using LibraryManagement.Services;
using LibraryManagement.Models;
using LibraryManagement.Utilities;

namespace LibraryManagement.Views.SubMenus
{
    /// <summary>
    /// Menu con quản lý các chức năng liên quan đến Độc giả.
    /// </summary>
    public class ReaderMenu
    {
        private ReaderService _readerService = new ReaderService();

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== QUẢN LÝ ĐỘC GIẢ ===");
                Console.WriteLine("1. Xem danh sách độc giả");
                Console.WriteLine("2. Thêm độc giả mới");
                Console.WriteLine("3. Gia hạn thẻ (Cập nhật)");
                Console.WriteLine("0. Quay lại Menu chính");
                Console.WriteLine("-----------------------");

                int choice = InputHelper.ReadInt("Chọn tác vụ: ");

                try
                {
                    switch (choice)
                    {
                        case 1:
                            ShowAllReaders(); break;
                        case 2:
                            AddReader();
                            break;
                        case 3:
                            ExtendCard();
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
                    Console.WriteLine($"\nLỗi: {ex.Message}");
                }

                if (choice != 0)
                {
                    Console.WriteLine("\nNhấn phím bất kỳ để tiếp tục...");
                    Console.ReadKey();
                }
            }
        }

        private void ShowAllReaders()
        {
            var list = _readerService.GetAll();
            Console.WriteLine($"\n--- DANH SÁCH ĐỘC GIẢ ({list.Count}) ---");
            string header = $"| {"LOẠI",-10} | {"ID",-6} | {"TÊN",-30} | {"MÃ THẺ",-15} | {"TRẠNG THÁI",-10} |";
            string line = new string('-', header.Length);

            Console.WriteLine(line);
            Console.WriteLine(header);
            Console.WriteLine(line);
            foreach (var r in list) r.ShowInfo();
            Console.WriteLine(line);

        }

        private void AddReader()
        {
            Console.WriteLine("\n--- THÊM ĐỘC GIẢ ---");
            // Id tự tăng đơn giản (trong thực tế DB sẽ tự làm việc này)
            int newId = new Random().Next(1000, 9999);

            string name = InputHelper.ReadString("Nhập Họ tên: ");
            string phone = InputHelper.ReadString("Nhập SĐT: ");
            string cardId = InputHelper.ReadString("Nhập Mã thẻ (VD: THE001): ");

            // Mặc định thẻ có hạn 6 tháng
            DateTime expiry = DateTime.Now.AddMonths(6);

            Reader newReader = new Reader(newId, name, phone, cardId, expiry);
            _readerService.Add(newReader);
            Console.WriteLine($"Thêm độc giả thành công! Thẻ hết hạn ngày: {expiry:dd/MM/yyyy}");
        }

        private void ExtendCard()
        {
            string cardId = InputHelper.ReadString("Nhập Mã thẻ cần gia hạn: ");
            var reader = _readerService.GetById(cardId);

            if (reader != null)
            {
                // Gia hạn thêm 6 tháng
                reader.ExpiryDate = reader.ExpiryDate.AddMonths(6);
                // Gọi Update để đảm bảo tính nhất quán (dù tham chiếu đã đổi, nhưng logic service có thể có validation)
                _readerService.Update(cardId, reader);
                Console.WriteLine($"Đã gia hạn thẻ. Ngày hết hạn mới: {reader.ExpiryDate:dd/MM/yyyy}");
            }
            else
            {
                Console.WriteLine("Không tìm thấy độc giả.");
            }
        }
    }
}
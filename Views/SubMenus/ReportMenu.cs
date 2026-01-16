using System;
using LibraryManagement.Services;
using LibraryManagement.Utilities;

namespace LibraryManagement.Views.SubMenus
{
    public class ReportMenu
    {
        private ReportService _reportService = new ReportService();

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== BÁO CÁO THỐNG KÊ ===");
                Console.WriteLine("1. Sách đang được mượn (Chưa trả)");
                Console.WriteLine("2. Sách quá hạn (Cần thu hồi)");
                Console.WriteLine("3. Thống kê sách theo Thể loại");
                Console.WriteLine("0. Quay lại");
                Console.WriteLine("--------------------------------");

                int choice = InputHelper.ReadInt("Chọn tác vụ: ");

                switch (choice)
                {
                    case 1:
                        var borrowed = _reportService.GetBorrowedBooksReport();
                        Console.WriteLine($"\n>> Có {borrowed.Count} phiếu đang mượn:");
                        foreach (var r in borrowed)
                            Console.WriteLine($"- {r.RecordId}: {r.Borrower.Name} (Hạn: {r.DueDate:dd/MM})");
                        break;
                    case 2:
                        var overdue = _reportService.GetOverdueReport();
                        Console.WriteLine($"\n>> Có {overdue.Count} phiếu quá hạn:");
                        foreach (var r in overdue)
                            Console.WriteLine($"- {r.RecordId}: {r.Borrower.Name} (Quá hạn từ: {r.DueDate:dd/MM})");
                        break;
                    case 3:
                        // Giả định phương thức ReportBooksByGenre() trong ReportService đã bao gồm Console.WriteLine
                        _reportService.ReportBooksByGenre();
                        break;
                    case 0: return;
                    default: Console.WriteLine("Lựa chọn không hợp lệ"); break;
                }
                Console.WriteLine("\nNhấn phím bất kỳ để tiếp tục...");
                Console.ReadKey(true);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using LibraryManagement.Services;
using LibraryManagement.Utilities;

namespace LibraryManagement.Views.SubMenus
{
    public class BorrowMenu
    {
        private BorrowService _borrowService = new BorrowService();

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== QUẢN LÝ MƯỢN / TRẢ SÁCH ===");
                Console.WriteLine("1. Tạo phiếu mượn mới");
                Console.WriteLine("2. Trả sách (Tính phạt)");
                Console.WriteLine("3. Xem lịch sử mượn trả");
                Console.WriteLine("0. Quay lại");
                Console.WriteLine("-------------------------------");

                int choice = InputHelper.ReadInt("Chọn tác vụ: ");

                try
                {
                    switch (choice)
                    {
                        case 1: BorrowBooks(); break;
                        case 2: ReturnBooks(); break;
                        case 3: ShowAllRecords(); break;
                        case 0: return;
                        default: Console.WriteLine("Lựa chọn không hợp lệ."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[LỖI]: {ex.Message}");
                }

                if (choice != 0)
                {
                    Console.WriteLine("\nNhấn phím bất kỳ để tiếp tục...");
                    Console.ReadKey();
                }
            }
        }

        private void BorrowBooks()
        {
            Console.WriteLine("\n--- TẠO PHIẾU MƯỢN ---");
            string readerId = InputHelper.ReadString("Nhập Mã Thẻ Độc Giả: ");

            List<string> isbns = new List<string>();
            Console.WriteLine("Nhập ISBN sách mượn (Nhập 'ok' để kết thúc):");
            while (true)
            {
                string isbn = InputHelper.ReadString("- ISBN: ");
                if (isbn.ToLower() == "ok") break;
                isbns.Add(isbn);
            }

            if (isbns.Count > 0)
            {
                _borrowService.BorrowItems(readerId, isbns);
                Console.WriteLine("Tạo phiếu mượn thành công!");
            }
            else
            {
                Console.WriteLine("Hủy thao tác: Chưa nhập sách nào.");
            }
        }

        private void ReturnBooks()
        {
            Console.WriteLine("\n--- TRẢ SÁCH ---");
            string recordId = InputHelper.ReadString("Nhập Mã Phiếu Mượn (VD: PM005): ");

            // Gọi Service để trả và tính phạt
            decimal fine = _borrowService.ReturnItems(recordId);

            if (fine > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"SÁCH QUÁ HẠN! Số tiền phạt: {fine:N0} VND");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Trả sách đúng hạn. Không có tiền phạt.");
                Console.ResetColor();
            }
        }

        private void ShowAllRecords()
        {
            var list = _borrowService.GetAllRecords();
            Console.WriteLine($"\n--- DANH SÁCH PHIẾU MƯỢN ({list.Count}) ---");
            foreach (var r in list)
            {
                string status = r.ReturnDate == null ? "ĐANG MƯỢN" : "ĐÃ TRẢ";
                Console.WriteLine($"Phiếu: {r.RecordId} | ĐG: {r.Borrower.Name} | Ngày mượn: {r.BorrowDate:dd/MM} | Trạng thái: {status}");
            }
        }
    }
}
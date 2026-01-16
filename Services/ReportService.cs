using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Models;
using LibraryManagement.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services
{
    /// <summary>
    /// Lớp chuyên trách việc tạo các báo cáo thống kê.
    /// Sử dụng LINQ để truy vấn dữ liệu từ DataStore.
    /// </summary>
    public class ReportService
    {
        // Báo cáo danh sách tài liệu đang được mượn (chưa trả)
        public List<BorrowRecord> GetBorrowedBooksReport()
        {
            return DataStore.Instance.Context.BorrowRecords
                            .Include(r => r.Borrower)
                            .Include(r => r.Items)
                            .Where(r => r.ReturnDate == null)
                            .ToList();
        }

        // Báo cáo các phiếu mượn đã quá hạn
        public List<BorrowRecord> GetOverdueReport()
        {
            return DataStore.Instance.Context.BorrowRecords
                            .Include(r => r.Borrower)
                            .Include(r => r.Items)
                            .Where(r => r.ReturnDate == null && DateTime.Now > r.DueDate)
                            .ToList();
        }

        // Thống kê số lượng sách theo từng thể loại (Grouping)
        public void ReportBooksByGenre()
        {
            // Chỉ lấy các item là Book
            var books = DataStore.Instance.Context.LibraryItems.OfType<Book>();

            var report = books.GroupBy(b => b.Genre)
                              .Select(g => new { Genre = g.Key, Count = g.Count() })
                              .ToList(); // Execute query against DB

            Console.WriteLine("--- THỐNG KÊ SÁCH THEO THỂ LOẠI ---");
            foreach (var item in report)
            {
                Console.WriteLine($"Thể loại: {item.Genre} - Số lượng: {item.Count}");
            }
        }
    }
}
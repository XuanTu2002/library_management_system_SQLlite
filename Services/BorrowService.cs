using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Models;
using LibraryManagement.DataAccess;
using LibraryManagement.Utilities; // Để dùng FineCalculator
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services
{
    /// <summary>
    /// Lớp xử lý nghiệp vụ Mượn - Trả sách.
    /// Chứa logic kiểm tra điều kiện mượn và tính toán phạt khi trả.
    /// </summary>
    public class BorrowService
    {
        private DbSet<BorrowRecord> _records => DataStore.Instance.Context.BorrowRecords;

        // Giới hạn số sách được mượn tối đa
        private const int MAX_BORROW_LIMIT = 5;

        /// <summary>
        /// Thực hiện mượn sách.
        /// </summary>
        public void BorrowItems(string readerCardId, List<string> itemISBNs)
        {
            // 1. Kiểm tra độc giả
            var reader = DataStore.Instance.Context.Readers.FirstOrDefault(r => r.ReaderCardId == readerCardId);
            if (reader == null) throw new Exception("Khong tim thay doc gia.");

            // 2. Kiểm tra thẻ hết hạn
            if (reader.ExpiryDate < DateTime.Now) throw new Exception("The doc gia da het han.");

            // 3. Kiểm tra số lượng đang mượn (chưa trả)
            int currentBorrowed = _records.Count(r => r.Borrower.ReaderCardId == readerCardId && r.ReturnDate == null);
            if (currentBorrowed + itemISBNs.Count > MAX_BORROW_LIMIT)
            {
                throw new Exception($"Doc gia da muon {currentBorrowed} cuon. Khong duoc muon them qua {MAX_BORROW_LIMIT} cuon.");
            }

            // 4. Tìm các sách cần mượn
            List<LibraryItem> itemsToBorrow = new List<LibraryItem>();
            foreach (var isbn in itemISBNs)
            {
                var item = DataStore.Instance.Context.LibraryItems.FirstOrDefault(i => i.ISBN == isbn);
                if (item == null) throw new Exception($"Khong tim thay sach voi ISBN: {isbn}");

                // (Nâng cao: Có thể kiểm tra xem sách này có đang bị ai khác mượn không)
                itemsToBorrow.Add(item);
            }

            // 5. Tạo phiếu mượn và lưu vào DataStore
            string recordId = "PM" + DateTime.Now.Ticks.ToString().Substring(10); // Tạo ID ngẫu nhiên
            var newRecord = new BorrowRecord(recordId, reader, itemsToBorrow);
            _records.Add(newRecord);
            DataStore.Instance.SaveChanges();
        }

        /// <summary>
        /// Thực hiện trả sách.
        /// </summary>
        /// <returns>Số tiền phạt (nếu có)</returns>
        public decimal ReturnItems(string recordId)
        {
            var record = _records.FirstOrDefault(r => r.RecordId == recordId);
            if (record == null) throw new Exception("Khong tim thay phieu muon.");

            if (record.ReturnDate != null) throw new Exception("Phieu muon nay da duoc tra truoc do.");

            // Cập nhật ngày trả
            record.ReturnDate = DateTime.Now;

            // Sử dụng FineCalculator để tính phạt
            decimal fine = FineCalculator.Calculate(record.DueDate, record.ReturnDate.Value);

            DataStore.Instance.SaveChanges();
            return fine;
        }

        public List<BorrowRecord> GetAllRecords() => _records
                                                        .Include(r => r.Borrower)
                                                        .Include(r => r.Items)
                                                        .ToList();
    }
}
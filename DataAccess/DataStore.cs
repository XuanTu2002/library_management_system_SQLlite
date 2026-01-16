using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Models;

namespace LibraryManagement.DataAccess
{
    public class DataStore
    {
        private static DataStore _instance;
        public static DataStore Instance => _instance ??= new DataStore();

        public LibraryContext Context { get; private set; }

        private DataStore()
        {
            Context = new LibraryContext();
            Context.Database.EnsureCreated();

            // Luôn gọi hàm Seed để kiểm tra và nạp dữ liệu thiếu (nếu có)
            SeedMassiveData();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        private void SeedMassiveData()
        {
            Console.WriteLine("Dang tao du lieu mau hang loat... Vui long doi...");

            // 1. Tạo 3 Nhân viên
            if (!Context.Staffs.Any())
            {
                Context.Staffs.AddRange(
                    new Staff(1, "Nguyen Quan Ly", "0901234567", "admin", "123456", "Manager"),
                    new Staff(2, "Tran Thu Thu", "0902345678", "staff", "123", "Librarian")
                );
            }

            // 2. Tạo 20 Độc giả tự động
            var readers = Context.Readers.ToList();
            if (!readers.Any())
            {
                for (int i = 1; i <= 20; i++)
                {
                    readers.Add(new Reader(
                        100 + i,
                        $"Doc Gia {i}",
                        $"09{i:00000000}",
                        $"DG{i:000}",
                        DateTime.Now.AddMonths(i % 5 + 1)
                    ));
                }
                Context.Readers.AddRange(readers);
            }

            // 3. Tạo 30 Sách giáo trình/Tiểu thuyết tự động
            var books = Context.Books.ToList();
            if (!books.Any())
            {
                var newBooks = new List<Book>();
                string[] genres = { "CNTT", "Kinh te", "Van hoc", "Ngoai ngu", "Ky nang" };

                for (int i = 1; i <= 30; i++)
                {
                    string genre = genres[i % genres.Length];
                    newBooks.Add(new Book(
                        $"978-604-{i:0000}",
                        $"Sach Demo Tieu De So {i}",
                        "NXB Giao Duc",
                        2020 + (i % 4),
                        $"Tac Gia {i}",
                        genre
                    ));
                }
                Context.LibraryItems.AddRange(newBooks);
                books.AddRange(newBooks);
            }

            // 4. Tạo 5 Luận văn
            if (!Context.Theses.Any())
            {
                for (int i = 1; i <= 5; i++)
                {
                    Context.LibraryItems.Add(new Thesis(
                        $"LV-2024-{i:00}",
                        $"Nghien cuu khoa hoc so {i}",
                        "DH Bach Khoa",
                        2024,
                        "TS. Huong Dan",
                        "Cong nghe phan mem"
                    ));
                }
            }

            // 6. Tạo 30 phiếu mượn (Mock Data) để test báo cáo
            if (!Context.BorrowRecords.Any() && readers.Any() && books.Any())
            {
                var borrowRecords = new List<BorrowRecord>();
                var rnd = new Random();

                for (int i = 1; i <= 30; i++)
                {
                    var reader = readers[rnd.Next(readers.Count)];
                    var randomBooks = books.OrderBy(x => rnd.Next()).Take(rnd.Next(1, 3)).Cast<LibraryItem>().ToList();

                    string recordId = $"PM-MOCK-{i:000}";
                    var record = new BorrowRecord(recordId, reader, randomBooks);

                    if (i <= 10)
                    {
                        record.BorrowDate = DateTime.Now.AddDays(-rnd.Next(30, 60));
                        record.DueDate = record.BorrowDate.AddDays(14);
                        record.ReturnDate = record.BorrowDate.AddDays(rnd.Next(5, 12));
                    }
                    else if (i <= 20)
                    {
                        record.BorrowDate = DateTime.Now.AddDays(-rnd.Next(1, 10));
                        record.DueDate = record.BorrowDate.AddDays(14);
                    }
                    else
                    {
                        record.BorrowDate = DateTime.Now.AddDays(-rnd.Next(20, 40));
                        record.DueDate = record.BorrowDate.AddDays(14);
                    }
                    borrowRecords.Add(record);
                }
                Context.BorrowRecords.AddRange(borrowRecords);
            }

            // 5. Lưu toàn bộ xuống SQLite
            Context.SaveChanges();
            Console.WriteLine("Da nap xong du lieu! San sang demo.");
        }
    }
}
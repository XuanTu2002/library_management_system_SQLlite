using Microsoft.EntityFrameworkCore;
using LibraryManagement.Models;

namespace LibraryManagement.DataAccess
{
    public class LibraryContext : DbContext
    {
        // Khai báo các bảng dữ liệu
        public DbSet<Person> People { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<LibraryItem> LibraryItems { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Thesis> Theses { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }

        // Cấu hình kết nối tới file SQLite
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // File database sẽ tên là "library.db"
            optionsBuilder.UseSqlite("Data Source=library.db");
        }

        // Cấu hình Model (nếu cần thiết lập quan hệ đặc biệt)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // EF Core tự động xử lý kế thừa (Inheritance)
            // Person -> Staff, Reader sẽ chung bảng "People"
            // LibraryItem -> Book, Thesis... sẽ chung bảng "LibraryItems"

            // Thiết lập khóa chính nếu cần (thường EF tự nhận diện Id)
            modelBuilder.Entity<LibraryItem>().HasKey(l => l.ISBN);
            modelBuilder.Entity<BorrowRecord>().HasKey(b => b.RecordId);
        }
    }
}
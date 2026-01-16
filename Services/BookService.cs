using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Models;
using LibraryManagement.DataAccess;
using LibraryManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services
{
    /// <summary>
    /// Lớp xử lý nghiệp vụ liên quan đến Tài liệu (Sách, Báo, Luận văn...).
    /// Thực thi giao diện IManageable để đảm bảo chuẩn hóa các thao tác.
    /// </summary>
    public class BookService : IManageable<LibraryItem>
    {
        // Truy cập kho dữ liệu thông qua Singleton DataStore
        private DbSet<LibraryItem> _items => DataStore.Instance.Context.LibraryItems;

        public void Add(LibraryItem item)
        {
            // Kiểm tra trùng mã ISBN trước khi thêm
            if (GetById(item.ISBN) != null)
            {
                throw new Exception($"Tai lieu voi ISBN {item.ISBN} da ton tai.");
            }
            _items.Add(item);
            DataStore.Instance.SaveChanges();
        }

        public void Update(string isbn, LibraryItem newItem)
        {
            var existingItem = GetById(isbn);
            if (existingItem != null)
            {
                // Cập nhật các thông tin chung
                existingItem.Title = newItem.Title;
                existingItem.Publisher = newItem.Publisher;
                existingItem.Year = newItem.Year;

                // Lưu ý: Việc cập nhật các thuộc tính riêng (như Author của Book) 
                // cần ép kiểu (casting) nếu muốn thực hiện kỹ hơn.
                DataStore.Instance.SaveChanges();
            }
            else
            {
                throw new Exception("Khong tim thay tai lieu de cap nhat.");
            }
        }

        public void Delete(string isbn)
        {
            var item = GetById(isbn);
            if (item != null)
            {
                _items.Remove(item);
                DataStore.Instance.SaveChanges();
            }
            else
            {
                throw new Exception("Khong tim thay tai lieu de xoa.");
            }
        }

        public LibraryItem GetById(string isbn)
        {
            // Sử dụng LINQ để tìm kiếm theo ISBN (không phân biệt hoa thường)
            return _items.FirstOrDefault(i => i.ISBN.ToLower() == isbn.ToLower());
        }

        public List<LibraryItem> GetAll()
        {
            return _items.ToList();
        }

        /// <summary>
        /// Tìm kiếm tài liệu theo từ khóa (có thể là Tên hoặc ISBN).
        /// </summary>
        public List<LibraryItem> Search(string keyword)
        {
            keyword = keyword.ToLower();
            return _items.Where(i => i.Title.ToLower().Contains(keyword) ||
                                     i.ISBN.ToLower().Contains(keyword)).ToList();
        }
    }
}
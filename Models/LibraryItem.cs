using System;
namespace LibraryManagement.Models
{
    public abstract class LibraryItem
    {
        public string Title { get; set; }
        public string ISBN { get; set; }

        public string Publisher { get; set; }

        public int Year { get; set; }

        protected LibraryItem(string Title, string ISBN, string Publisher, int Year)
        {
            this.Title = Title;
            this.ISBN = ISBN;
            this.Publisher = Publisher;
            this.Year = Year;

        }
        public abstract void ShowInfo();

        // Helper để định dạng hiển thị dạng bảng (Cắt chuỗi nếu quá dài, thêm khoảng trắng nếu ngắn)
        protected string FormatCell(string value, int width)
        {
            if (string.IsNullOrEmpty(value)) value = "";
            if (value.Length > width)
            {
                return value.Substring(0, width - 3) + "...";
            }
            return value.PadRight(width);
        }
    }
}
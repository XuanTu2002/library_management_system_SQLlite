using System;

namespace LibraryManagement.Models
{
    public class Reader : Person
    {
        public string ReaderCardId { get; set; }

        public DateTime ExpiryDate { get; set; }

        public Reader(int Id, string Name, string Phone, string ReaderCardId, DateTime ExpiryDate)
          : base(Id, Name, Phone)
        {
            this.ReaderCardId = ReaderCardId;
            this.ExpiryDate = ExpiryDate;
        }

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
        public override void ShowInfo()
        {
            string status = DateTime.Now > ExpiryDate ? "HẾT HẠN" : "CÒN HẠN";
            Console.WriteLine($"| {FormatCell("Độc giả", 10)} | {FormatCell(Id.ToString(), 6)} | {FormatCell(Name, 30)} | {FormatCell(ReaderCardId, 15)} | {FormatCell(status, 10)} |");
        }
    }
}
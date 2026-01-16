using System;
using LibraryManagement.Models;

namespace LibraryManagement.Utilities
{
    /// <summary>
    /// Lớp tiện ích chịu trách nhiệm khởi tạo các đối tượng tài liệu (LibraryItem).
    /// Lớp này áp dụng mẫu thiết kế Factory Pattern (Mẫu Nhà máy) để che giấu logic khởi tạo phức tạp.
    /// Giúp tách biệt logic tạo đối tượng khỏi logic sử dụng đối tượng.
    /// </summary>
    public static class LibraryItemFactory
    {
        /// <summary>
        /// Phương thức tĩnh dùng để tạo ra một đối tượng LibraryItem cụ thể (Sách, Luận văn, hoặc Tạp chí).
        /// </summary>
        /// <param name="type">Loại tài liệu muốn tạo ("book", "thesis", "magazine")</param>
        /// <param name="isbn">Mã ISBN (Thông tin chung)</param>
        /// <param name="title">Tiêu đề (Thông tin chung)</param>
        /// <param name="publisher">Nhà xuất bản (Thông tin chung)</param>
        /// <param name="year">Năm xuất bản (Thông tin chung)</param>
        /// <param name="extraInfo">
        /// Mảng các tham số phụ thuộc vào từng loại tài liệu:
        /// - Book: [0] Tác giả (string), [1] Thể loại (string)
        /// - Thesis: [0] GVHD (string), [1] Chuyên ngành (string)
        /// - Magazine: [0] Số phát hành (int), [1] Tháng (int)
        /// </param>
        /// <returns>Trả về một đối tượng kế thừa từ LibraryItem</returns>
        /// <exception cref="ArgumentException">Ném ra lỗi nếu loại tài liệu không hợp lệ hoặc thiếu tham số</exception>
        public static LibraryItem CreateItem(string type, string isbn, string title, string publisher, int year, params object[] extraInfo)
        {
            // Chuẩn hóa chuỗi đầu vào về chữ thường để so sánh dễ dàng hơn
            string itemType = type.ToLower().Trim();

            switch (itemType)
            {
                case "book":
                    // Kiểm tra đủ số lượng tham số cho Sách
                    if (extraInfo.Length < 2)
                        throw new ArgumentException("Thieu thong tin cho Sach (Tac gia, The loai).");

                    return new Book(
                        title,
                        isbn,
                        publisher,
                        year,
                        extraInfo[0]?.ToString() ?? "", // Author
                        extraInfo[1]?.ToString() ?? ""  // Genre
                    );

                case "thesis":
                    // Kiểm tra đủ số lượng tham số cho Luận văn
                    if (extraInfo.Length < 2)
                        throw new ArgumentException("Thieu thong tin cho Luan van (GVHD, Chuyen nganh).");

                    return new Thesis(
                        title,
                        isbn,
                        publisher,
                        year,
                        extraInfo[0]?.ToString() ?? "", // Supervisor
                        extraInfo[1]?.ToString() ?? ""  // Major
                    );

                case "magazine":
                    // Kiểm tra đủ số lượng tham số cho Tạp chí
                    if (extraInfo.Length < 2)
                        throw new ArgumentException("Thieu thong tin cho Tap chi (So phat hanh, Thang).");

                    // Cần chuyển đổi kiểu dữ liệu từ object sang int cho Tạp chí
                    if (int.TryParse(extraInfo[0].ToString(), out int issue) &&
                        int.TryParse(extraInfo[1].ToString(), out int month))
                    {
                        return new Magazine(
                            title,
                            isbn,
                            publisher,
                            year,
                            issue, // IssueNumber
                            month  // Month
                        );
                    }
                    else
                    {
                        throw new ArgumentException("So phat hanh va Thang phai la so nguyen.");
                    }

                default:
                    // Ném lỗi nếu loại tài liệu không được hỗ trợ
                    throw new ArgumentException($"Loai tai lieu '{type}' khong ton tai hoac chua duoc ho tro.");
            }
        }
    }
}
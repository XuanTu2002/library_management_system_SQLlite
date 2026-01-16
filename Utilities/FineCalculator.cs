using System;

namespace LibraryManagement.Utilities
{
    /// <summary>
    /// Lớp tiện ích tĩnh hỗ trợ tính toán tiền phạt cho việc trả sách quá hạn.
    /// Tách biệt logic tính toán khỏi logic nghiệp vụ mượn trả.
    /// </summary>
    public static class FineCalculator
    {
        // Mức phạt cố định cho mỗi ngày quá hạn (Đơn vị: VNĐ)
        // Dùng hằng số (const) để dễ dàng quản lý và thay đổi
        private const decimal FINE_PER_DAY = 5000;

        /// <summary>
        /// Tính toán tổng tiền phạt dựa trên ngày hẹn trả và ngày trả thực tế.
        /// </summary>
        /// <param name="dueDate">Ngày hẹn trả (Hạn chót)</param>
        /// <param name="returnDate">Ngày trả sách thực tế</param>
        /// <returns>Số tiền phạt (VNĐ). Trả về 0 nếu trả đúng hạn hoặc trước hạn.</returns>
        public static decimal Calculate(DateTime dueDate, DateTime returnDate)
        {
            // Nếu ngày trả thực tế nhỏ hơn hoặc bằng ngày hẹn -> Không phạt
            if (returnDate <= dueDate)
            {
                return 0;
            }

            // Tính khoảng thời gian trễ (TimeSpan)
            TimeSpan lateSpan = returnDate - dueDate;

            // Lấy số ngày trễ, làm tròn lên (ví dụ trễ 1.5 ngày tính là 2 ngày)
            int daysLate = (int)Math.Ceiling(lateSpan.TotalDays);

            // Công thức: Số ngày trễ * Mức phạt mỗi ngày
            decimal totalFine = daysLate * FINE_PER_DAY;

            return totalFine;
        }
    }
}
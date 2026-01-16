using System;

namespace LibraryManagement.Models
{
    public class Magazine : LibraryItem
    {
        public int IssueNumber { get; set; }
        public int Month { get; set; }
        public Magazine(string Title, string ISBN, string Publisher, int Year, int IssueNumber, int Month)
            : base(Title, ISBN, Publisher, Year)
        {
            this.IssueNumber = IssueNumber;
            this.Month = Month;
        }
        public override void ShowInfo()
        {
            string extra = $"Số: {IssueNumber}, T{Month}";
            Console.WriteLine($"| {FormatCell("Tạp chí", 10)} | {FormatCell(ISBN, 15)} | {FormatCell(Title, 30)} | {FormatCell(Publisher, 15)} | {Year,-4} | {FormatCell(extra, 30)} |");
        }
    }
}
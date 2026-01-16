using System;
namespace LibraryManagement.Models
{
    public class Thesis : LibraryItem
    {
        public string Major { get; set; }
        public string Supervisor { get; set; }
        public Thesis(string Title, string ISBN, string Publisher, int Year, string Major, string Supervisor)
        : base(Title, ISBN, Publisher, Year)
        {
            this.Major = Major;
            this.Supervisor = Supervisor;
        }
        public override void ShowInfo()
        {
            string extra = $"GV: {Supervisor}";
            Console.WriteLine($"| {FormatCell("Luận văn", 10)} | {FormatCell(ISBN, 15)} | {FormatCell(Title, 30)} | {FormatCell(Publisher, 15)} | {Year,-4} | {FormatCell(extra, 30)} |");
        }
    }
}
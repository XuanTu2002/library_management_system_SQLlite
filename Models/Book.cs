using System;

namespace LibraryManagement.Models
{
    public class Book : LibraryItem
    {
        public string Author { get; set; }
        public string Genre { get; set; }

        public Book(string Title, string ISBN, string Publisher, int Year, string Author, string Genre)
          : base(Title, ISBN, Publisher, Year)
        {
            this.Author = Author;
            this.Genre = Genre;
        }
        public override void ShowInfo()
        {
            string extra = $"{Author} ({Genre})";
            Console.WriteLine($"| {FormatCell("Sách", 10)} | {FormatCell(ISBN, 15)} | {FormatCell(Title, 30)} | {FormatCell(Publisher, 15)} | {Year,-4} | {FormatCell(extra, 30)} |");
        }
    }
}
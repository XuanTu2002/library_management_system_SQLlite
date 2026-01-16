using System;
using System.Collections.Generic;

namespace LibraryManagement.Models
{
    public class BorrowRecord
    {
        public string RecordId { get; set; }

        public Reader Borrower { get; set; }

        public List<LibraryItem> Items { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        // Constructor for EF Core
        public BorrowRecord()
        {
            Items = new List<LibraryItem>();
        }

        public BorrowRecord(string RecordId, Reader Borrower, List<LibraryItem> Items)
        {
            this.RecordId = RecordId;
            this.Borrower = Borrower;
            this.Items = Items;
            this.BorrowDate = DateTime.Now;
            this.DueDate = DateTime.Now.AddDays(14);
            this.ReturnDate = null;
        }
    }
}

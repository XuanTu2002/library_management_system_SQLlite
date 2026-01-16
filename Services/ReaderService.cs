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
    /// Lớp xử lý nghiệp vụ quản lý Độc giả.
    /// </summary>
    public class ReaderService : IManageable<Reader>
    {
        public void Add(Reader reader)
        {
            // Kiểm tra trùng Mã thẻ hoặc ID cá nhân
            if (DataStore.Instance.Context.Readers.Any(r => r.ReaderCardId == reader.ReaderCardId || r.Id == reader.Id))
            {
                throw new Exception("Doc gia hoac Ma the da ton tai.");
            }
            DataStore.Instance.Context.Readers.Add(reader);
            DataStore.Instance.SaveChanges();
        }

        public void Update(string cardId, Reader newInfo)
        {
            var reader = GetById(cardId);
            if (reader != null)
            {
                reader.Name = newInfo.Name;
                reader.Phone = newInfo.Phone;
                reader.ExpiryDate = newInfo.ExpiryDate;
                DataStore.Instance.SaveChanges();
            }
            else
            {
                throw new Exception("Khong tim thay doc gia.");
            }
        }

        public void Delete(string cardId)
        {
            var reader = GetById(cardId);
            if (reader != null)
            {
                DataStore.Instance.Context.Readers.Remove(reader);
                DataStore.Instance.SaveChanges();
            }
            else
            {
                throw new Exception("Khong tim thay doc gia de xoa.");
            }
        }

        // Tìm theo Mã thẻ thư viện
        public Reader? GetById(string cardId)
        {
            return DataStore.Instance.Context.Readers.FirstOrDefault(r => r.ReaderCardId.ToLower() == cardId.ToLower());
        }

        public List<Reader> GetAll()
        {
            return DataStore.Instance.Context.Readers.ToList();
        }
    }
}
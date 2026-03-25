using Library_Manager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Library_Manager.Models;

namespace Library_Manager.Service
{
    public class BorrowService
    {
        private readonly BookRepository _bookRepo;
        private readonly BorrowRepository _borrowRepo;

        public BorrowService()
        {
            _bookRepo = new BookRepository();
            _borrowRepo = new BorrowRepository();
        }

        public bool BorrowBook(int userId, Book book)
        {
            if (book.Quantity <= 0)
                return false;

            book.Quantity--;
            _bookRepo.UpdateBook(book);

            _borrowRepo.AddBorrowing(new Borrowing
            {
                UserId = userId,
                BookId = book.BookId,
                BorrowDate = DateTime.Now
            });

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Library_Manager.Commands;

namespace Library_Manager.Models
{
    internal class EditBookViewModel
    {
        public Book EditingBook { get; set; }
        public Book OriginalBook { get; set; }

        public ICommand SaveChanges { get; }
        public ICommand CancelChanges { get; }

        private Window _window;

        public EditBookViewModel(Book selectedBook)
        {
            OriginalBook = selectedBook;
            EditingBook = new Book
            {
                BookId = selectedBook.BookId,
                Title = selectedBook.Title,
                Author = selectedBook.Author,
                Genre = selectedBook.Genre,
                Quantity = selectedBook.Quantity
            };
            SaveChanges = new RelayCommand(Save);
            CancelChanges = new RelayCommand(Cancel);
        }

        public void Save()
        {
            OriginalBook.Title = EditingBook.Title;
            OriginalBook.Author = EditingBook.Author;
            OriginalBook.Genre = EditingBook.Genre;
            OriginalBook.Quantity = EditingBook.Quantity;
            _window.DialogResult = true;
        }

        public void Cancel()
        {
            EditingBook.Title = OriginalBook.Title;
            EditingBook.Author = OriginalBook.Author;
            EditingBook.Genre = OriginalBook.Genre;
            EditingBook.Quantity = OriginalBook.Quantity;
            _window.DialogResult = false;
        }

        public void SetWindow(Window window)
        {
            _window = window;
        }

    }
}

using Library_Manager.Commands;
using Library_Manager.Data;
using Library_Manager.Models;
using Library_Manager.Service;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Library_Manager.ViewModels
{
    internal class UserViewModel
    {
        public ObservableCollection<Book> Books { get; set; }
        public ObservableCollection<Book> BorrowedBooks { get; set; }

        public Book SelectedBook { get; set; }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                SearchBooks();
            }
        }

        public ICommand BorrowCommand { get; }

        private readonly BorrowService _borrowService;
        private readonly BookRepository _bookRepo;
        private readonly BorrowRepository _borrowRepo;

        private readonly int _currentUserId;

        public UserViewModel(int userId)
        {
            _currentUserId = userId;

            Books = new ObservableCollection<Book>();
            BorrowedBooks = new ObservableCollection<Book>();

            _bookRepo = new BookRepository();
            _borrowRepo = new BorrowRepository();
            _borrowService = new BorrowService();

            BorrowCommand = new RelayCommand(BorrowBook, CanBorrowBook);

            LoadBooks();
            LoadBorrowedBooks();
        }

    
        private void LoadBooks()
        {
            Books.Clear();

            var booksFromDb = _bookRepo.GetAllBooks();

            foreach (var book in booksFromDb)
            {
                Books.Add(book);
            }
        }

        private void SearchBooks()
        {
            Books.Clear();

            var books = _bookRepo.GetAllBooks();

            var filtered = books.Where(b =>
                string.IsNullOrEmpty(SearchText) ||
                b.Title.ToLower().Contains(SearchText.ToLower()) ||
                b.Author.ToLower().Contains(SearchText.ToLower()));

            foreach (var book in filtered)
            {
                Books.Add(book);
            }
        }

  
        private void LoadBorrowedBooks()
        {
            BorrowedBooks.Clear();

            var borrowings = _borrowRepo.GetBorrowingsByUser(_currentUserId);

            foreach (var borrowing in borrowings)
            {
                var book = _bookRepo.GetAllBooks().FirstOrDefault(b => b.BookId == borrowing.BookId);
                if (book != null)
                {
                    BorrowedBooks.Add(book);
                }
            }
        }

        private void BorrowBook(object obj)
        {
            var book = obj as Book;

            if (book == null) return;

            bool success = _borrowService.BorrowBook(_currentUserId, book);

            if (success)
            {
                LoadBooks();
                LoadBorrowedBooks();
            }
        }

        private bool CanBorrowBook(object obj)
        {
            var book = obj as Book;
            return book != null && book.Quantity > 0;
        }
    }
}
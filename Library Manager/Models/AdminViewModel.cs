using Library_Manager.Commands;
using Library_Manager.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library_Manager.Models
{
    internal class AdminViewModel
    {

        public ObservableCollection<Book> Books { get; set; } =
            new ObservableCollection<Book>(new BookRepository().GetAllBooks());

        public ObservableCollection<User> Users { get; set; } =
            new ObservableCollection<User>(new UserRepository().GetAllUsers());

        public string NameInput { get; set; }
        public string IdNumberInput { get; set; }
        public string PasswordInput { get; set; }
        public bool IsAdminInput { get; set; }
        public string TitleInput { get; set; }
        public string AuthorInput { get; set; }
        public string GenreInput { get; set; }
        public int QuantityInput { get; set; }
        public User SelectedUser { get; set; }
        public Book SelectedBook { get; set; }

        public ICommand AddBookCommand { get; }
        public ICommand DeleteBookCommand { get; }
        public ICommand AddUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand UpdateBookCommand { get; }
        public ICommand UpdateUserCommand { get; }


        public AdminViewModel()
        {
            AddBookCommand = new RelayCommand(AddBook);
            DeleteBookCommand = new RelayCommand(DeleteBook);
            AddUserCommand = new RelayCommand(AddUser);
            DeleteUserCommand = new RelayCommand(DeleteUser);
            UpdateBookCommand = new RelayCommand(UpdateBook);
            UpdateUserCommand = new RelayCommand(UpdateUser);

                LoadBooks();
                LoadUsers();
        }

        private void LoadUsers()
        {
            Users.Clear();
            foreach (var user in new UserRepository().GetAllUsers())
            {
                Users.Add(user);
            }
        }

        private void LoadBooks()
        {
            Books.Clear();
            foreach (var book in new BookRepository().GetAllBooks())
            {
                Books.Add(book);
            }
        }

        private void AddBook()
        {
            var book = new Book
            {
                Title = TitleInput,
                Author = AuthorInput,
                Genre = GenreInput,
                Quantity = QuantityInput
            };
            new BookRepository().AddBook(book);
            LoadBooks();
        }

        private void DeleteBook()
        {
            if (SelectedBook != null)
            {
                new BookRepository().DeleteBook(SelectedBook.BookId);
                Books.Remove(SelectedBook);
            }
        }

        private void AddUser()
        {
            var user = new User
            {
                Name = NameInput,
                IdNumber = IdNumberInput,
                Password = PasswordInput,
                IsAdmin = IsAdminInput
            };
            new UserRepository().AddUser(user);
            LoadUsers();
        }

        private void DeleteUser()
        {
            if (SelectedUser != null)
            {
                new UserRepository().DeleteUser(SelectedUser.UserId);
                Users.Remove(SelectedUser);
            }
        }

        private void UpdateBook()
        {
            if (SelectedBook != null)
            {
                EditBook editBookWindow = new EditBook(SelectedBook);
                bool? result = editBookWindow.ShowDialog();
                if (result == true)
                {
                    new BookRepository().UpdateBook(SelectedBook);
                    LoadBooks();
                }
            }
        }

        private void UpdateUser()
        {
            if(SelectedUser != null)
            {
                EditUser editUserWindow = new EditUser(SelectedUser);
                bool? result = editUserWindow.ShowDialog();

                if (result == true)
                {
                    new UserRepository().UpdateUser(SelectedUser);
                    LoadUsers();
                }
            }
        }
    }
}

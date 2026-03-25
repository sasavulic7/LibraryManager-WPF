using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Manager.Models
{
    internal class Borrowing : INotifyPropertyChanged
    {
        public int _id;
        public int _userId;
        public int _bookId;
        public DateTime _borrowDate;
        public DateTime? _returnDate;

        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public int UserId
        {
            get => _userId;
            set
            {
                if (_userId != value)
                {
                    _userId = value;
                    OnPropertyChanged(nameof(UserId));
                }
            }
        }

        public int BookId
        {
            get => _bookId;
            set
            {
                if (_bookId != value)
                {
                    _bookId = value;
                    OnPropertyChanged(nameof(BookId));
                }
            }
        }

        public DateTime BorrowDate
        {
            get => _borrowDate;
            set
            {
                if (_borrowDate != value)
                {
                    _borrowDate = value;
                    OnPropertyChanged(nameof(BorrowDate));
                }
            }
        }

        public DateTime? ReturnDate
        {
            get => _returnDate;
            set
            {
                if (_returnDate != value)
                {
                    _returnDate = value;
                    OnPropertyChanged(nameof(ReturnDate));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}

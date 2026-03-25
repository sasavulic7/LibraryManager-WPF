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
    internal class EditUserViewModel
    {

        public User EditingUser { get; set; }

        public User OriginalUser { get; set; }

        public ICommand SaveChanges { get; }
        public ICommand CancelChanges { get; }

        private Window _window;

        public EditUserViewModel(User selectedUser)
        {
            OriginalUser = selectedUser;
            EditingUser = new User
            {
                UserId = selectedUser.UserId,
                Name = selectedUser.Name,
                IdNumber = selectedUser.IdNumber,
                IsAdmin = selectedUser.IsAdmin,
                Password = selectedUser.Password
            };
            SaveChanges = new RelayCommand(Save);
            CancelChanges = new RelayCommand(Cancel);
        }

        public void Save()
        {
            OriginalUser.Name = EditingUser.Name;
            OriginalUser.Password = EditingUser.Password;

            _window.DialogResult = true;
        }

        public void Cancel()
        {
            EditingUser.Name = OriginalUser.Name;
            EditingUser.Password = OriginalUser.Password;

            _window.DialogResult = false;
        }

        public void SetWindow(Window window)
        {
            _window = window;
        }
    }
}

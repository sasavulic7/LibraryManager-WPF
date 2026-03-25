using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Library_Manager.Models;

namespace Library_Manager
{
    /// <summary>
    /// Interaction logic for EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        public EditUser(User SelectedUser)
        {
            InitializeComponent();

            var vm = new EditUserViewModel(SelectedUser);
            vm.SetWindow(this);

            DataContext = vm;

        }
    }
}

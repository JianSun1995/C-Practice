using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DesktopContactApp.Classes;
using SQLite;

namespace DesktopContactApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Contact> _contacts;

        public MainWindow()
        {
            InitializeComponent();

            _contacts = new List<Contact>();

            ReadDatabase();
        }

        /// <summary>
        /// Open New Contact Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// <"(Window).Show()"/>
        /// The new window and the original window
        /// are both actived.
        /// However one can call  <(Window).ShowDialog/> method
        /// to only active the new window.
        /// The orinal Mainwindow is locked until the
        /// new window gives a call back method.
        /// </remarks>
        private void NewContact_OnClick(object sender, RoutedEventArgs e)
        {
            var newContactWindow = new NewContactWindow();
            newContactWindow.ShowDialog();

            ReadDatabase();
        }

        private void ReadDatabase()
        {
            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.CreateTable<Contact>();

                //! Note the .ToList() method used here.
                _contacts = connection.Table<Contact>().ToList();

                _contacts = (from contact in _contacts
                    orderby contact.Name
                    select contact).ToList();

                ContactListView.ItemsSource = _contacts;
            }
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchTextBox = sender as TextBox;

            //! Note the .ToLower() method used here to make the filter method more sensative
            var filteredList = (from contact in _contacts
                where searchTextBox != null && contact.Name.ToLower().Contains(searchTextBox.Text.ToLower())
                select contact).ToList();

            ContactListView.ItemsSource = filteredList;
        }
    }
}
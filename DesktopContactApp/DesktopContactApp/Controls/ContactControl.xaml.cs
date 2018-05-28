using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DesktopContactApp.Classes;

namespace DesktopContactApp.Controls
{
    /// <summary>
    /// Interaction logic for ContactControl.xaml
    /// </summary>
    public partial class ContactControl : UserControl
    {
        public Contact Contact
        {
            get => (Contact) GetValue(ContactProperty);
            set => SetValue(ContactProperty, value);
        }

        public static readonly DependencyProperty ContactProperty = DependencyProperty.Register("Contact",
            typeof(Contact), typeof(ContactControl),
            new PropertyMetadata(new Contact() {Name = "Name", Email = "Email", PhoneNumber = "Phone Number"},
                ContactChangedCallback));

        private static void ContactChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var contactControl = d as ContactControl;

            Debug.Assert(contactControl != null, nameof(contactControl) + " != null");
            Debug.Assert(e.NewValue != null, "e.NewValue != null");
            contactControl.Contact.Name = ((Contact) e.NewValue).Name;

            contactControl.Contact.Email = ((Contact) e.NewValue).Email;

            contactControl.Contact.PhoneNumber = ((Contact) e.NewValue).Email;
        }

        public ContactControl()
        {
            InitializeComponent();
        }
    }
}
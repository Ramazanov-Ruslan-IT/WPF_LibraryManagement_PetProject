using LibraryManagement.Models;
using System;
using System.Windows;

namespace LibraryManagement.Views
{
    public partial class AuthorWindow : Window
    {
        public Author? Author { get; private set; }

        public AuthorWindow(Author? author = null)
        {
            InitializeComponent();
            Author = author;
            if (Author != null)
            {
                FirstNameTextBox.Text = Author.FirstName;
                LastNameTextBox.Text = Author.LastName;
                BirthDatePicker.SelectedDate = Author.BirthDate;
                CountryTextBox.Text = Author.Country;
            }
            else
            {
                BirthDatePicker.SelectedDate = DateTime.Now;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(LastNameTextBox.Text) ||
                BirthDatePicker.SelectedDate == null ||
                string.IsNullOrWhiteSpace(CountryTextBox.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            if (Author == null)
            {
                Author = new Author();
            }

            Author.FirstName = FirstNameTextBox.Text;
            Author.LastName = LastNameTextBox.Text;
            Author.BirthDate = BirthDatePicker.SelectedDate.Value;
            Author.Country = CountryTextBox.Text;

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

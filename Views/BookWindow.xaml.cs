using LibraryManagement.Data;
using LibraryManagement.Models;
using System;
using System.Linq;
using System.Windows;

namespace LibraryManagement.Views
{
    public partial class BookWindow : Window
    {
        private readonly LibraryContext _context;
        private readonly Book? _book;

        public BookWindow(LibraryContext context, Book? book = null)
        {
            InitializeComponent();
            _context = context;
            _book = book;

            AuthorComboBox.ItemsSource = _context.Authors.ToList();
            GenreComboBox.ItemsSource = _context.Genres.ToList();

            if (_book != null)
            {
                TitleTextBox.Text = _book.Title;
                PublishYearTextBox.Text = _book.PublishYear.ToString();
                ISBNTextBox.Text = _book.ISBN;
                QuantityTextBox.Text = _book.QuantityInStock.ToString();
                AuthorComboBox.SelectedValue = _book.AuthorId;
                GenreComboBox.SelectedValue = _book.GenreId;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text) ||
                !int.TryParse(PublishYearTextBox.Text, out int publishYear) ||
                string.IsNullOrWhiteSpace(ISBNTextBox.Text) ||
                !int.TryParse(QuantityTextBox.Text, out int quantity) ||
                AuthorComboBox.SelectedValue == null ||
                GenreComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please fill all fields correctly.");
                return;
            }

            if (_book == null)
            {
                Book newBook = new Book
                {
                    Title = TitleTextBox.Text,
                    PublishYear = publishYear,
                    ISBN = ISBNTextBox.Text,
                    QuantityInStock = quantity,
                    AuthorId = (int)AuthorComboBox.SelectedValue,
                    GenreId = (int)GenreComboBox.SelectedValue
                };
                _context.Books.Add(newBook);
            }
            else
            {
                _book.Title = TitleTextBox.Text;
                _book.PublishYear = publishYear;
                _book.ISBN = ISBNTextBox.Text;
                _book.QuantityInStock = quantity;
                _book.AuthorId = (int)AuthorComboBox.SelectedValue;
                _book.GenreId = (int)GenreComboBox.SelectedValue;
                _context.Books.Update(_book);
            }

            _context.SaveChanges();
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Views;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;

namespace LibraryManagement
{
    public partial class MainWindow : Window
    {
        private readonly LibraryContext _context;

        public MainWindow()
        {
            InitializeComponent();
            _context = new LibraryContext();
            _context.Database.EnsureCreated();
            LoadFilters();
            LoadBooks();
        }

        private void LoadFilters()
        {
            var authors = _context.Authors.ToList();
            AuthorFilterComboBox.ItemsSource = authors;
            AuthorFilterComboBox.DisplayMemberPath = "FullName";
            AuthorFilterComboBox.SelectedValuePath = "Id";
            AuthorFilterComboBox.SelectedIndex = -1;

            var genres = _context.Genres.ToList();
            GenreFilterComboBox.ItemsSource = genres;
            GenreFilterComboBox.DisplayMemberPath = "Name";
            GenreFilterComboBox.SelectedValuePath = "Id";
            GenreFilterComboBox.SelectedIndex = -1;
        }

        private void LoadBooks()
        {
            var query = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                query = query.Where(b => b.Title.Contains(SearchTextBox.Text));
            }

            if (AuthorFilterComboBox.SelectedValue is int authorId)
            {
                query = query.Where(b => b.AuthorId == authorId);
            }

            if (GenreFilterComboBox.SelectedValue is int genreId)
            {
                query = query.Where(b => b.GenreId == genreId);
            }

            var books = query.ToList().Select(b => new
            {
                b.Id,
                b.Title,
                AuthorName = b.Author.FullName,
                b.PublishYear,
                b.ISBN,
                GenreName = b.Genre.Name,
                b.QuantityInStock
            }).ToList();

            BooksDataGrid.ItemsSource = books;
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            LoadBooks();
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            var bookWindow = new BookWindow(_context);
            if (bookWindow.ShowDialog() == true)
            {
                LoadBooks();
            }
        }

        private void EditBookButton_Click(object sender, RoutedEventArgs e)
        {
            dynamic selected = BooksDataGrid.SelectedItem;
            if (selected == null)
            {
                MessageBox.Show("Please select a book to edit.");
                return;
            }

            int bookId = selected.Id;
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                var bookWindow = new BookWindow(_context, book);
                if (bookWindow.ShowDialog() == true)
                {
                    LoadBooks();
                }
            }
        }

        private void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            dynamic selected = BooksDataGrid.SelectedItem;
            if (selected == null)
            {
                MessageBox.Show("Please select a book to delete.");
                return;
            }

            int bookId = selected.Id;
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this book?", "Confirm Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _context.Books.Remove(book);
                    _context.SaveChanges();
                    LoadBooks();
                }
            }
        }

        private void ManageAuthorsButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new AuthorManagementWindow(_context);
            window.ShowDialog();
            LoadFilters();
            LoadBooks();
        }

        private void ManageGenresButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new GenreManagementWindow(_context);
            window.ShowDialog();
            LoadFilters();
            LoadBooks();
        }
    }
}

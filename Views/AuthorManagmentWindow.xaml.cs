using LibraryManagement.Data;
using LibraryManagement.Models;
using System.Linq;
using System.Windows;

namespace LibraryManagement.Views
{
    public partial class AuthorManagementWindow : Window
    {
        private readonly LibraryContext _context;

        public AuthorManagementWindow(LibraryContext context)
        {
            InitializeComponent();
            _context = context;
            LoadAuthors();
        }

        private void LoadAuthors()
        {
            AuthorsDataGrid.ItemsSource = _context.Authors.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new AuthorWindow();
            if (window.ShowDialog() == true)
            {
                _context.Authors.Add(window.Author);
                _context.SaveChanges();
                LoadAuthors();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (AuthorsDataGrid.SelectedItem is Author selectedAuthor)
            {
                var window = new AuthorWindow(selectedAuthor);
                if (window.ShowDialog() == true)
                {
                    _context.Authors.Update(selectedAuthor);
                    _context.SaveChanges();
                    LoadAuthors();
                }
            }
            else
            {
                MessageBox.Show("Please select an author to edit.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (AuthorsDataGrid.SelectedItem is Author selectedAuthor)
            {
                if (MessageBox.Show("Are you sure you want to delete this author?", "Confirm Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _context.Authors.Remove(selectedAuthor);
                    _context.SaveChanges();
                    LoadAuthors();
                }
            }
            else
            {
                MessageBox.Show("Please select an author to delete.");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

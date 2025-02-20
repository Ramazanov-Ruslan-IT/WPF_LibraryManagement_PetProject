using LibraryManagement.Data;
using LibraryManagement.Models;
using System.Linq;
using System.Windows;

namespace LibraryManagement.Views
{
    public partial class GenreManagementWindow : Window
    {
        private readonly LibraryContext _context;

        public GenreManagementWindow(LibraryContext context)
        {
            InitializeComponent();
            _context = context;
            LoadGenres();
        }

        private void LoadGenres()
        {
            GenresDataGrid.ItemsSource = _context.Genres.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new GenreWindow();
            if (window.ShowDialog() == true)
            {
                _context.Genres.Add(window.Genre);
                _context.SaveChanges();
                LoadGenres();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (GenresDataGrid.SelectedItem is Genre selectedGenre)
            {
                var window = new GenreWindow(selectedGenre);
                if (window.ShowDialog() == true)
                {
                    _context.Genres.Update(selectedGenre);
                    _context.SaveChanges();
                    LoadGenres();
                }
            }
            else
            {
                MessageBox.Show("Please select a genre to edit.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (GenresDataGrid.SelectedItem is Genre selectedGenre)
            {
                if (MessageBox.Show("Are you sure you want to delete this genre?", "Confirm Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _context.Genres.Remove(selectedGenre);
                    _context.SaveChanges();
                    LoadGenres();
                }
            }
            else
            {
                MessageBox.Show("Please select a genre to delete.");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

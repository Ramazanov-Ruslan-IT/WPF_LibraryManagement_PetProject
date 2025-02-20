using LibraryManagement.Models;
using System.Windows;

namespace LibraryManagement.Views
{
    public partial class GenreWindow : Window
    {
        public Genre? Genre { get; private set; }

        public GenreWindow(Genre? genre = null)
        {
            InitializeComponent();
            Genre = genre;
            if (Genre != null)
            {
                NameTextBox.Text = Genre.Name;
                DescriptionTextBox.Text = Genre.Description;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Please fill in the name.");
                return;
            }

            if (Genre == null)
            {
                Genre = new Genre();
            }

            Genre.Name = NameTextBox.Text;
            Genre.Description = DescriptionTextBox.Text;

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

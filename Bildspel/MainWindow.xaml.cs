using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Bildspel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyCollectionChanged, INotifyPropertyChanged
    {
        public ObservableCollection<BitmapSource> AllImages { get; set; } = new ObservableCollection<BitmapSource>();
        public ObservableCollection<BitmapSource> ChosenImages { get; set; } = new ObservableCollection<BitmapSource>();

        private BitmapSource _image;
        public BitmapSource Image
        {
            get { return _image; }
            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged(nameof(Image));
                }
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PreviousImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NextImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StartSlideshow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = true;

            if (open.ShowDialog() == true)
            {
                string[] filepath = open.FileNames;
                foreach ( string s in  filepath )
                {
                    if (System.IO.Path.GetExtension(s) == ".jpg" || System.IO.Path.GetExtension(s) == ".png")
                    {
                        BitmapImage img = new BitmapImage();
                        img.BeginInit();
                        img.UriSource = new Uri(s);
                        img.EndInit();
                        AllImages.Add(img);
                    }
                }
            }
        }

        private void RemoveImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var b = sender as Border;
            if (b != null && b.Child is Image i)
            {
                if (b.BorderBrush == Brushes.Black)
                {
                    BitmapSource source = i.Source as BitmapSource;
                    ChosenImages.Add(source);
                    b.BorderBrush = Brushes.Red;
                }
                else
                {
                    BitmapSource source = i.Source as BitmapSource;
                    ChosenImages.Remove(source);
                    b.BorderBrush = Brushes.Black;
                }
            }
        }
    }
}
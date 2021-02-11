using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToursApp.Pages;

namespace ToursApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //alt+enter+enter
            MainFrame.Navigate(new MainMenu());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //App._context.Countries.ToList().FirstOrDefault().Name;
            //var listTours = App._context.Tours.ToList();
            // alt+enter+enter
            var d = new DirectoryInfo(@"C:\Users\br_mn\Desktop\Туры фото");
            var photos = d.GetFiles();
            foreach (var photo in photos)
            {
                var dataPhoto = File.ReadAllBytes(photo.FullName);
                var namePhoto = System.IO.Path.GetFileNameWithoutExtension(photo.FullName);
                //Получаем тур и затем записываем в него данные фотографии
                var tour = App._context.Tours.Where(t => t.Name == namePhoto).FirstOrDefault();
                tour.ImagePreview = dataPhoto;
                App._context.SaveChanges();

            }
        }


    }
}

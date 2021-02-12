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

            //Получаем информацию о папке
            var d = new DirectoryInfo(@"C:\Users\br_mn\Desktop\Туры фото");
            //Получаем фотографии (все кроме данных)
            var photos = d.GetFiles();
            //Проходимся по каждой из фотографий
            foreach (var photo in photos)
            {
                //Читаем данные фотографии
                var dataPhoto = File.ReadAllBytes(photo.FullName);
                //Получаем имя фотографии без расшширения
                var namePhoto = System.IO.Path.GetFileNameWithoutExtension(photo.FullName);
                //Получаем тур
                var tour = App._context.Tours.Where(t => t.Name == namePhoto).FirstOrDefault();
                // и затем записываем в него данные фотографии
                tour.ImagePreview = dataPhoto;
                //Сохраняем бд
                App._context.SaveChanges();

            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
                
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoForward)
            {
                MainFrame.GoForward();
            }
            
        }



        }
}

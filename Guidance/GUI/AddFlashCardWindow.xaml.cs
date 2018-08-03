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
using System.Windows.Shapes;
using Guidance.ViewModel;
namespace Guidance.GUI
{
    /// <summary>
    /// Interaction logic for AddFlashCardWindow.xaml
    /// </summary>
    public partial class AddFlashCardWindow : Window
    {
        public AddFlashCardWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            bool? result = dlg.ShowDialog();
            
            if (result == true)
            {
                string filename = dlg.FileName;
                var picture = File.ReadAllBytes(filename);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                FlashCardVM flashCardVM = new FlashCardVM();
                flashCardVM.Title = "Test1";
                flashCardVM.Tags = new List<string>();
                flashCardVM.Tags.Add("test1");
                flashCardVM.Tags.Add("test2");
                flashCardVM.Pictures = new List<byte[]>();
                flashCardVM.Pictures.Add(File.ReadAllBytes(@"C:\Users\BlueCompany\Desktop\pobrane.jpg"));
                flashCardVM.TextAnserws = new List<string>();
                flashCardVM.TextAnserws.Add("Testowa odpowiedź");
                flashCardVM.SaveToDB();
                Console.WriteLine("koniec zapisywania");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            //using (var db = new FlashCardsEntities())
            //{
            //    var flashCard = new FlashCard { Title = "SampleTitle1", Tags = "#tag1#tag2" };
            //    db.FlashCards.Add(flashCard);
            //    var pictureAnserw = new PictureAnserw { FlashCard = flashCard, Picture = File.ReadAllBytes(@"C:\Users\BlueCompany\Desktop\pobrane.jpg") };
            //    db.PictureAnserws.Add(pictureAnserw);
            //    db.SaveChanges();

            //    // Display all Blogs from the database 
            //    //var query = from b in db.FlashCards
            //    //            orderby b.Title
            //    //            select b;
            //    //foreach (var item in query)
            //    //{
            //    //    Console.WriteLine(item.Id);
            //    //    Console.WriteLine(item.Title);
            //    //}

            //    //Console.WriteLine("Press any key to exit...");
            //}
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //using (var db = new FlashCardsEntities())
            //{
            //    var query = from b in db.PictureAnserws
            //                select b.Picture;
            //    byte[] imageData = query.Single();
            //    var image = new BitmapImage();
            //    using (var mem = new MemoryStream(imageData))
            //    {
            //        mem.Position = 0;
            //        image.BeginInit();
            //        image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            //        image.CacheOption = BitmapCacheOption.OnLoad;
            //        image.UriSource = null;
            //        image.StreamSource = mem;
            //        image.EndInit();
            //    }
            //    using (var fileStream = new FileStream(@"C:\Users\BlueCompany\Desktop\pobrane1.png", FileMode.Create))
            //    {
            //        BitmapEncoder encoder = new PngBitmapEncoder();
            //        encoder.Frames.Add(BitmapFrame.Create(image));
            //        encoder.Save(fileStream);
            //    }
            //    image.Freeze();
            //}
        }

    }
}

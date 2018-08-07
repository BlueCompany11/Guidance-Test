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
using Guidance.IViewModel;
using Guidance.DataAccessLayer; //del
using Guidance.FlashCardModel;  //del
namespace Guidance.GUI
{
    /// <summary>
    /// Interaction logic for AddFlashCardWindow.xaml
    /// </summary>
    public partial class AddFlashCardWindow : Window
    {
        IAddFlashCard addFlashCard;
        public AddFlashCardWindow()
        {
            InitializeComponent();
        }

        public AddFlashCardWindow(IAddFlashCard addFlashCardVM):this()
        {
            addFlashCard = addFlashCardVM;
            this.DataContext = addFlashCard;
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
                Console.WriteLine(filename);
                addFlashCard.PictureAnserws.Add(picture);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FlashCardRepository flashCardRepository = new FlashCardRepository();
            var flashCard = new FlashCard { Title = "nowy test1", Tags = "#123" };
            flashCardRepository.Add(flashCard);
            //Console.WriteLine(addFlashCard.Title);
            //foreach (var item in addFlashCard.Tags)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine(addFlashCard.PictureAnserws.Count.ToString());
            //addFlashCard.Save();
        }

        private void AddNewTagToListBox(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                tagsList.Items.Add(tagInsertTb.Text);
                tagInsertTb.Text = "";
            }
        }

        private void DeleteTag(object sender, MouseButtonEventArgs e)
        {
            tagsList.Items.Remove(tagsList.SelectedItem);
        }
    }
}

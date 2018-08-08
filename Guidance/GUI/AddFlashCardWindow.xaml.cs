﻿using System;
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
        private void AddFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            //dlg.DefaultExt = ".png";
            //dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            bool? result = dlg.ShowDialog();
            
            if (result == true)
            {
                string filename = dlg.FileName;
                var file = File.ReadAllBytes(filename);
                addFlashCard.AddFileAnserw(filename, file, null);
            }
        }

        private void Button_Click_ConsolidateData(object sender, RoutedEventArgs e)
        {
            //zapisac textanserw i caly flashcard
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

        private void TestData(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(addFlashCard.FlashCard.Title);
            Console.WriteLine(addFlashCard.FlashCard.Tags);
            Console.WriteLine(addFlashCard.FileAnserws.Count);
            Console.WriteLine(addFlashCard.TextAnserws.Count);
        }

        private void RememberFileAnserw(object sender, RoutedEventArgs e)
        {

        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                addFlashCard.AddFile(dlg.FileName);
            }
        }
        private void RememberFileAnserw(object sender, RoutedEventArgs e)
        {
            addFlashCard.AttachAnnotationToFile(fileAnnotationTb.Text);   // dodanie annotation do pliku
            //addFlashCard.AddFile(); // zapis pliku i annotation w FlashCardzie
        }

        private void AddTextAnserw_Click(object sender, RoutedEventArgs e)
        {
            addFlashCard.AddTextAnserw(textAnserwTb.Text, textAnserwAnnotationTb.Text);   //pobranie annotation TextAnserw i TextAnerw z 2 tb i zapis do struktury FlashCard
        }
        private void Button_Click_SaveFlashCard(object sender, RoutedEventArgs e)
        {
            //zapisac textanserw i caly flashcard
            addFlashCard.SaveFlashCard();
        }

        private void AddNewTagToListBox(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                var tempList = tagsList.ItemsSource.Cast<string>().ToList();
                tempList.Add(tagInsertTb.Text);
                tagsList.ItemsSource = new ObservableCollection<string>(tempList);
                tagInsertTb.Text = "";
                //tagsList.DataContext = addFlashCard.Tags;
            }
        }

        private void DeleteTag(object sender, MouseButtonEventArgs e)
        {
            tagsList.Items.Remove(tagsList.SelectedItem);
        }

        private void TestData(object sender, RoutedEventArgs e)
        {
        }


    }
}

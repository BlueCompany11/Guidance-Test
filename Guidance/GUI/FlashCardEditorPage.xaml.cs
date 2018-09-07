﻿using System;
using System.Collections.Generic;
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
using Guidance.DataAccessLayer; //do usuniecia
using Guidance.FlashCardModel;  //do usuniecia
using Guidance.IViewModel;
using Guidance.ViewModel;

namespace Guidance.GUI
{
    /// <summary>
    /// Interaction logic for FlashCardEditorPage.xaml
    /// </summary>
    public partial class FlashCardEditorPage : Page
    {
        IFlashCardEntryPage mainViewModel;
        public FlashCardEditorPage()
        {
            InitializeComponent();
        }
        public FlashCardEditorPage(IFlashCardEntryPage newFlashCardView) : this()
        {
            this.DataContext = newFlashCardView;    //przy braku tej linijki selected item nie dziala binding
            mainViewModel = newFlashCardView;
        }

        private void AddFlashCardButton_Click(object sender, RoutedEventArgs e)
        {
            var addFlashCardWindow = new AddFlashCardWindow(new FlashCardAdd());
            addFlashCardWindow.ShowDialog();
        }

        private void test1(object sender, RoutedEventArgs e)
        {

        }

        private void EditFlashCardButton_Click(object sender, RoutedEventArgs e)
        {
            if (showFlashCardsDataGrid.SelectedItem == null)
            {
                return;
            }
            FlashCardPreview flashCardPreview = (FlashCardPreview)showFlashCardsDataGrid.SelectedItem;
            var addFlashCardWindow = new AddFlashCardWindow();  //uzyc drugiego konstruktora
            addFlashCardWindow.ShowDialog();
        }
        
    }

}

using Guidance.GUI;
using System;
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
using Guidance.ViewModel;
using Guidance.DataAccessLayer; //do usuniecia
using Guidance.FlashCardModel;

namespace Guidance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Puste kolumny dla warstw 0 i 1:
        ColumnDefinition column1CloneForLayer0;
        ColumnDefinition column2CloneForLayer0;
        ColumnDefinition column2CloneForLayer1;

        public MainWindow()
        {
            InitializeComponent();

            //flashCardRepository.Save(flashCard);
            // Inicjowanie pustych kolumn u¿ywanych przy dokowaniu:
            column1CloneForLayer0 = new ColumnDefinition();
            column1CloneForLayer0.SharedSizeGroup = "column1";
            column2CloneForLayer0 = new ColumnDefinition();
            column2CloneForLayer0.SharedSizeGroup = "column2";
            column2CloneForLayer1 = new ColumnDefinition();
            column2CloneForLayer1.SharedSizeGroup = "column2";
        }

        // Prze³¹czenie pomiêdzy stanem zadokowanym i niezadokowanym (panel 1)
        public void pane1Pin_Click(object sender, RoutedEventArgs e)
        {
            if (pane1Button.Visibility == Visibility.Collapsed)
                UndockPane(1);
            else
                DockPane(1);
        }

        // Prze³¹czenie pomiêdzy stanem zadokowanym i niezadokowanym (panel 2)
        public void pane2Pin_Click(object sender, RoutedEventArgs e)
        {
            if (pane2Button.Visibility == Visibility.Collapsed)
                UndockPane(2);
            else
                DockPane(2);
        }

        // Pokazanie panelu 1 przy umieszczeniu myszy nad przyciskiem
        public void pane1Button_MouseEnter(object sender, RoutedEventArgs e)
        {
            layer1.Visibility = Visibility.Visible;

            // Korekta kolejnoœci w osi Z, aby panel by³ na szczycie:
            parentGrid.Children.Remove(layer1);
            parentGrid.Children.Add(layer1);

            // Upewnienie siê, ¿e drugi panel jest ukryty, gdy jest niezadokowany
            if (pane2Button.Visibility == Visibility.Visible)
                layer2.Visibility = Visibility.Collapsed;
        }

        // Pokazanie panelu 2 przy umieszczeniu myszy nad przyciskiem
        public void pane2Button_MouseEnter(object sender, RoutedEventArgs e)
        {
            layer2.Visibility = Visibility.Visible;

            // Korekta kolejnoœci w osi Z, aby panel by³ na szczycie:
            parentGrid.Children.Remove(layer2);
            parentGrid.Children.Add(layer2);

            // Upewnienie siê, ¿e pierwszy panel jest ukryty, gdy jest niezadokowany
            if (pane1Button.Visibility == Visibility.Visible)
                layer1.Visibility = Visibility.Collapsed;
        }

        // Ukrycie niezadokowanych paneli, gdy mysz jest nad panelem 0
        public void layer0_MouseEnter(object sender, RoutedEventArgs e)
        {
            if (pane1Button.Visibility == Visibility.Visible)
                layer1.Visibility = Visibility.Collapsed;
            if (pane2Button.Visibility == Visibility.Visible)
                layer2.Visibility = Visibility.Collapsed;
        }

        // Ukrycie pozosta³ych niezadokowanych paneli gdy mysz jest nad panelem 1
        public void pane1_MouseEnter(object sender, RoutedEventArgs e)
        {
            // Upewniamy siê, ¿e pozosta³e panele s¹ ukryte, je¿eli nie s¹ zadokowane
            if (pane2Button.Visibility == Visibility.Visible)
                layer2.Visibility = Visibility.Collapsed;
        }

        // Ukrycie pozosta³ych niezadokowanych paneli gdy mysz jest nad panelem 2
        public void pane2_MouseEnter(object sender, RoutedEventArgs e)
        {
            // Upewniamy siê, ¿e pozosta³e panele s¹ ukryte, je¿eli nie s¹ zadokowane
            if (pane1Button.Visibility == Visibility.Visible)
                layer1.Visibility = Visibility.Collapsed;
        }

        // Dokowanie panelu, co ukrywa odpowiedni przycisk panelu
        public void DockPane(int paneNumber)
        {
            if (paneNumber == 1)
            {
                pane1Button.Visibility = Visibility.Collapsed;
                pane1PinImage.Source = new BitmapImage(new Uri("pin.gif", UriKind.Relative));

                // Dodanie do warstwy 0 sklonowanej kolumny po lewej stronie:
                layer0.ColumnDefinitions.Add(column1CloneForLayer0);
                // Dodanie do warstwy 1 sklonowanej kolumny, ale tylko gdy panel 2 jest zadokowany:
                if (pane2Button.Visibility == Visibility.Collapsed) layer1.ColumnDefinitions.Add(column2CloneForLayer1);
            }
            else if (paneNumber == 2)
            {
                pane2Button.Visibility = Visibility.Collapsed;
                pane2PinImage.Source = new BitmapImage(new Uri("pin.gif", UriKind.Relative));

                // Dodanie do warstwy 0 sklonowanej kolumny:
                layer0.ColumnDefinitions.Add(column2CloneForLayer0);
                // Dodanie do warstwy 1 sklonowanej kolumny, ale tylko gdy panel 1 jes zadokowany:
                if (pane1Button.Visibility == Visibility.Collapsed) layer1.ColumnDefinitions.Add(column2CloneForLayer1);
            }
        }

        // Usuniêcie dokowania panelu, co powoduje pokazanie odpowiedniego przycisku
        public void UndockPane(int paneNumber)
        {
            if (paneNumber == 1)
            {
                layer1.Visibility = Visibility.Collapsed;
                pane1Button.Visibility = Visibility.Visible;
                pane1PinImage.Source = new BitmapImage(new Uri("pinHorizontal.gif", UriKind.Relative));

                // Usuniêcie sklonowanych kolumn z warstw 0 i 1:
                layer0.ColumnDefinitions.Remove(column1CloneForLayer0);
                // Nie musi istnieæ, ale Remove ignoruje nieprawid³owe kolumny:
                layer1.ColumnDefinitions.Remove(column2CloneForLayer1);
            }
            else if (paneNumber == 2)
            {
                layer2.Visibility = Visibility.Collapsed;
                pane2Button.Visibility = Visibility.Visible;
                pane2PinImage.Source = new BitmapImage(new Uri("pinHorizontal.gif", UriKind.Relative));

                // Usuniêcie sklonowanych kolumn z warstw 0 i 1:
                layer0.ColumnDefinitions.Remove(column2CloneForLayer0);
                // Nie musi istnieæ, ale Remove ignoruje nieprawid³owe kolumny:
                layer1.ColumnDefinitions.Remove(column2CloneForLayer1);
            }
        }

        private void flashCardMenuListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new FlashCardEditorPage(new FlashCardEntry());
        }
    }
}

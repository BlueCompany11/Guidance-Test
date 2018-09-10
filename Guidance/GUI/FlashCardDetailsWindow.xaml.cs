using System;
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
using Guidance.IView;
namespace Guidance.GUI
{
    /// <summary>
    /// Interaction logic for AddFlashCardWindow.xaml
    /// </summary>
    public partial class FlashCardDetailsWindow : Window
    {
        public IFlashCardDetails addFlashCard;
        public FlashCardDetailsWindow()
        {
            InitializeComponent();
        }

        public FlashCardDetailsWindow(IFlashCardDetails addFlashCardVM):this()
        {
            addFlashCard = addFlashCardVM;
            this.DataContext = addFlashCard;
        }
    }
}

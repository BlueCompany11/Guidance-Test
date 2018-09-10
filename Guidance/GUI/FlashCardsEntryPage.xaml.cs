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
using Guidance.DataAccessLayer; //do usuniecia
using Guidance.FlashCardModel;  //do usuniecia
using Guidance.IView;
using Guidance.ViewModel;

namespace Guidance.GUI
{
    /// <summary>
    /// Interaction logic for FlashCardEditorPage.xaml
    /// </summary>
    public partial class FlashCardEntryPage : Page
    {
        IFlashCardEntryPage mainViewModel;
        public FlashCardEntryPage()
        {
            InitializeComponent();
        }
        public FlashCardEntryPage(IFlashCardEntryPage newFlashCardView) : this()
        {
            mainViewModel = newFlashCardView;
            this.DataContext = mainViewModel;
        }
    }
}

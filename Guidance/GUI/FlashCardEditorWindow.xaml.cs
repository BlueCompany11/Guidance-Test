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
using System.Windows.Shapes;

namespace Guidance.GUI
{
    /// <summary>
    /// Interaction logic for FlashCardEditorWindow.xaml
    /// </summary>
    public partial class FlashCardEditorWindow : Window
    {
        Action<Window> ReturnPrev;
        public FlashCardEditorWindow()
        {
            InitializeComponent();
        }
        public FlashCardEditorWindow(Window parentWindow):this()
        {

        }
    }
}

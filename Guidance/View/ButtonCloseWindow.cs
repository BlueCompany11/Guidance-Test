using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Guidance.View
{
    public class ButtonCloseWindow: Behavior<Window>
    {
        public static readonly DependencyProperty PrzyciskProperty =
         DependencyProperty.Register(
         "Przycisk",
         typeof(Button),
         typeof(ButtonCloseWindow),
         new PropertyMetadata(null, PrzyciskZmieniony)
         );
        public Button Przycisk
        {
            get { return (Button)GetValue(PrzyciskProperty); }
            set { SetValue(PrzyciskProperty, value); }
        }

        private static void PrzyciskZmieniony(DependencyObject d,
        DependencyPropertyChangedEventArgs e)
        {
            Window window = (d as ButtonCloseWindow).AssociatedObject;
            RoutedEventHandler button_Click =
            (object sender, RoutedEventArgs _e) => { window.Close(); };
            if (e.OldValue != null) ((Button)e.OldValue).Click -= button_Click;
            if (e.NewValue != null) ((Button)e.NewValue).Click += button_Click;
        }
    }
}

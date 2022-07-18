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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SequenceClicker.CustomControl
{
    /// <summary>
    /// Interaction logic for MButton.xaml
    /// </summary>
    public partial class MButton : UserControl
    {
        public MButton()
        {
            InitializeComponent();
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            FadeBarOpacity(1);
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            FadeBarOpacity(0);
        }

        private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            FadeBarOpacity(0.5f);
        }

        private void OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(OnClickEvent));
            FadeBarOpacity(1f);
        }

        private void FadeBarOpacity(float value)
        {
            var animation = new DoubleAnimation
            {
                From = Bar.Opacity,
                To = value,
                Duration = TimeSpan.FromSeconds(0.15f),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (o, e) => Bar.Opacity = value;

            Bar.BeginAnimation(UIElement.OpacityProperty, animation);
        }

        #region Dependency Property
        public float BarSize
        {
            get { return (float)GetValue(BarSizeProperty); }
            set { SetValue(BarSizeProperty, value); }
        }

        public static readonly DependencyProperty BarSizeProperty =
            DependencyProperty.Register("BarSize", typeof(float), typeof(MButton), new PropertyMetadata(3f));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MButton), new PropertyMetadata("Click"));


        public static readonly RoutedEvent OnClickEvent =
             EventManager.RegisterRoutedEvent(nameof(OnClick), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MButton));

        public event RoutedEventHandler OnClick
        {
            add { AddHandler(OnClickEvent, value); }
            remove { RemoveHandler(OnClickEvent, value); }
        }

        #endregion
    }
}

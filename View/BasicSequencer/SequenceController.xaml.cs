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

namespace SequenceClicker.View
{
    /// <summary>
    /// Interaction logic for SequenceController.xaml
    /// </summary>
    public partial class SequenceController : UserControl
    {
        SliderMode sliderMode = SliderMode.Open;

        Storyboard animator = new Storyboard();
        DoubleAnimation valueAnim = new DoubleAnimation();
        float slidePercent = 0;

        public SequenceController()
        {
            InitializeComponent();

            animator.Duration = TimeSpan.FromSeconds(1);
            animator.FillBehavior = FillBehavior.Stop;

            animator.Children.Add(valueAnim);

            valueAnim.Changed += (o, e) => UpdateSlider();
            valueAnim.Completed += (o, e) => UpdateSlider();
        }

        private void Ctrl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SlideCtrl(SliderMode.Close);
        }

        private void Ctrl_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SlideCtrl(SliderMode.Close);
        }

        private void Ctrl_MouseEnter(object sender, MouseEventArgs e)
        {
            SlideCtrl(SliderMode.Close);
        }

        private void Ctrl_MouseLeave(object sender, MouseEventArgs e)
        {
            SlideCtrl(SliderMode.Open);
        }

        private void SlideCtrl(SliderMode mode)
        {
            return;
            animator.Stop();

            valueAnim.From = slidePercent;
            valueAnim.To = (int)mode;

            animator.Begin();
        }

        private void UpdateSlider()
        {
            DLog.Log("Value : " + slidePercent);
        }

        private enum SliderMode
        {
            Open = 0,
            Close = 10,
            Semi = 5
        }

        private void MButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (sliderMode == SliderMode.Open)
            {
                sliderMode = SliderMode.Close;

                tButton.Text = "Pause";
            }
            else
            {
                sliderMode = SliderMode.Open;

                tButton.Text = "Play";
            }

            RaiseEvent(new RoutedEventArgs(OnClickEvent));
        }

        public static readonly RoutedEvent OnClickEvent =
            EventManager.RegisterRoutedEvent(nameof(OnClick), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SequenceController));

        public event RoutedEventHandler OnClick
        {
            add { AddHandler(OnClickEvent, value); }
            remove { RemoveHandler(OnClickEvent, value); }
        }
    }
}

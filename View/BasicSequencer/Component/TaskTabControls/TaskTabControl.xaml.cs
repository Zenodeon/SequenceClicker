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

namespace SequenceClicker.View.BasicSequencer.Component
{
    /// <summary>
    /// Interaction logic for TaskTabControl.xaml
    /// </summary>
    public partial class TaskTabControl : UserControl
    {
        public Action<int> OnActionClick;

        public TaskTabControl()
        {
            InitializeComponent();
        }

        private void MoveUp(object sender, RoutedEventArgs e)
        {
            OnActionClick?.Invoke(0);
        }

        private void RemoveSelf(object sender, RoutedEventArgs e)
        {
            OnActionClick?.Invoke(1);
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            OnActionClick?.Invoke(2);
        }

        private void DuplicateSelf(object sender, RoutedEventArgs e)
        {
            OnActionClick?.Invoke(3);
        }

        private void MoveDown(object sender, RoutedEventArgs e)
        {
            OnActionClick?.Invoke(4);
        }
    }
}

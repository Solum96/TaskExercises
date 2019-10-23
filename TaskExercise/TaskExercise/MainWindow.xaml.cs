using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace TaskExercise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ConcurrentBag<string> myList = new ConcurrentBag<string>();

        public MainWindow()
        {
            InitializeComponent();
            addString.Click += OnAddStringClick;
            addStringTimes10.Click += OnAddString10Click;
        }

        private void OnAddString10Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Dispatcher.Invoke(() =>
                    {
                        listBox.Items.Add("string");
                    });
                    Thread.Sleep(1000);
                }
            });
        }

        private void OnAddStringClick(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => { listBox.Items.Add("dis string"); });
        }
    }
}

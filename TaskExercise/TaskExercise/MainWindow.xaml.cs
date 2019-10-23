﻿using System;
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
        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken ct;

        public MainWindow()
        {
            InitializeComponent();
            ct = cts.Token;
            addString.Click += OnAddStringClick;
            addStringTimes10.Click += OnAddString10Click;
            stopButton.Click += CancelTasks;
        }

        private void CancelTasks(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
            stopButton.IsEnabled = false;
        }

        private void OnAddString10Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    addStringTimes10.IsEnabled = false;
                    stopButton.IsEnabled = true;
                });
                for(int i = 0; i < 10; i++)
                {
                    if(cts.IsCancellationRequested)
                    {
                        cts = new CancellationTokenSource();
                        Dispatcher.Invoke(() => { addStringTimes10.IsEnabled = true; });
                        return;
                    }
                    else
                    {
                        Dispatcher.Invoke(() =>
                        {
                            listBox.Items.Add("string");
                        });
                        Thread.Sleep(1000);
                    }
                }
            });
        }

        private void OnAddStringClick(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => { listBox.Items.Add("dis string"); });
        }
    }
}

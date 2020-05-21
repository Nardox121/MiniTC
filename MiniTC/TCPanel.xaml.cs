using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiniTC
{
    /// <summary>
    /// Logika interakcji dla klasy TCPanel.xaml
    /// </summary>
    public partial class TCPanel : UserControl
    {
        public TCPanel()
        {
            InitializeComponent();
        }
        #region properties
        public static readonly DependencyProperty PathProperty =
                DependencyProperty.Register(
                    "Path",
                    typeof(string),
                    typeof(TCPanel),
                    new FrameworkPropertyMetadata(null)
                );
        public string Path
        {
            private get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        public static readonly DependencyProperty FilesProperty =
                DependencyProperty.Register(
                    "Files",
                    typeof(string[]),
                    typeof(TCPanel),
                    new FrameworkPropertyMetadata(null)
                );
        public string[] Files
        {
            get { return (string[])GetValue(FilesProperty); }
            set { SetValue(FilesProperty, value); }
        }

        public static readonly DependencyProperty SelectedDiskProperty =
                DependencyProperty.Register(
                    "SelectedDisk",
                    typeof(string),
                    typeof(TCPanel),
                    new FrameworkPropertyMetadata(null)
                );
        public string SelectedDisk
        {
            get { return (string)GetValue(SelectedDiskProperty); }
            set { SetValue(SelectedDiskProperty, value); }
        }

        public static readonly DependencyProperty SelectedFileProperty =
                DependencyProperty.Register(
                    "SelectedFile",
                    typeof(string),
                    typeof(TCPanel),
                    new FrameworkPropertyMetadata(null)
                );
        public string[] SelectedFile
        {
            get { return (string[])GetValue(SelectedFileProperty); }
            set { SetValue(SelectedFileProperty, value); }
        }

        public static readonly DependencyProperty DisksProperty =
                DependencyProperty.Register(
                    "Disks",
                    typeof(string[]),
                    typeof(TCPanel),
                    new FrameworkPropertyMetadata(null)
                );
        public string[] Disks
        {
            get { return (string[])GetValue(DisksProperty); }
            set { SetValue(DisksProperty, value); }
        }
        #endregion

        public static readonly RoutedEvent MDClickEvent =
       EventManager.RegisterRoutedEvent(nameof(MDClick),
                    RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                    typeof(TCPanel));

        public event RoutedEventHandler MDClick
        {
            add { AddHandler(MDClickEvent, value); }
            remove { RemoveHandler(MDClickEvent, value); }
        }

        void RaiseMDClick()
        {
            RoutedEventArgs newEventArgs =
                    new RoutedEventArgs(MDClickEvent);
            RaiseEvent(newEventArgs);
        }

        private void MClick(object sender, RoutedEventArgs e)
        {
            this.RaiseMDClick();
        }
    }
}

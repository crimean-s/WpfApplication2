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
using WpfApplication2.ViewModel;
using Hardcodet.Wpf.TaskbarNotification.Interop;
using System.ComponentModel;
using System.Diagnostics;

namespace WpfApplication2
{
    
    public partial class MainWindow : Window
    {
      
        SuggestViewModel slist = new SuggestViewModel();
        CommandViewModel command = new CommandViewModel();


        private System.Windows.Forms.NotifyIcon notifyIcon;
        
        public MainWindow()
        {
            InitializeComponent();

            listBox.ItemsSource = slist.SuggestItems;
            listBox.Focusable = true;

            txt1.Focus();
            txt2.Text = command.CurrentCommand.Name;
            

            // создание notifyIcon //
            /////////////////////////

            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.BalloonTipText = "Приложение скрыто";
            notifyIcon.BalloonTipTitle = "Универсальный поиск";
            notifyIcon.Text = "Универсальный поиск";
            notifyIcon.Icon = new System.Drawing.Icon(System.IO.Directory.GetCurrentDirectory() + @"..\..\..\Icons\seacrh_tray.ico");
            notifyIcon.Click += new EventHandler(notifyIcon_Click);

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += HandleKeyPress;
        }
        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            var focusedControl = FocusManager.GetFocusedElement(this);

            if (e.Key == Key.Add & focusedControl == txt1)
            {
                // listBox.Focus();
                FocusManager.SetFocusedElement(userControl, listBox);
                listBox.Items.Refresh();
                
            }
            if (e.Key == Key.Enter && command.CurrentCommand.Name == "web")
            {
                WebSearchViewModel web = new WebSearchViewModel();
                web.Setup("");
                web.Search(txt1.Text);
                listBox.Items.Refresh();   
                txt2.Text = "go to web";                           
            }
            if (e.Key == Key.Enter && command.CurrentCommand.Name == "file")
            {
                FileSearchViewModel file = new FileSearchViewModel();
                file.Setup("");
                file.Search(txt1.Text);
                listBox.Items.Refresh();
                txt2.Text = "go to file";
            }
        }
        

        private void txt1_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            if(textBox.Text.Length == 0)
            {
                listBox.Visibility = Visibility.Collapsed;
            } else
            {
                listBox.Visibility = Visibility.Visible;
            }


            try
            {
                foreach (Suggest sitem in slist.SuggestItems)
                {
                    sitem.Query = textBox.Text;
                    string it = sitem.Command.Name;
                    sitem.Text = sitem.Command.Name + " " + sitem.Query;

                    listBox.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A handled exception just occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }
        
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            command.setCommandName(slist.SuggestItems.ElementAt(listBox.SelectedIndex).Command.Name);
            txt2.Text = command.getCommandName();
        }

        private void listBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Keyboard.ClearFocus();
            listBox.Focus();
        }



        // Управление notifyIcon //

        void OnClose(object sender, CancelEventArgs args)
        {
            notifyIcon.Dispose();
            notifyIcon = null;
        }

        private WindowState storedWindowState = WindowState.Normal;

        void OnStateChanged(object sender, EventArgs args)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
                if (notifyIcon != null)
                    notifyIcon.ShowBalloonTip(100);
            }
            else
                storedWindowState = WindowState;
        }

        void notifyIcon_Click(object sender, EventArgs e)
        {
            Show();
            this.Topmost = true;  // important
            this.Topmost = false; // important
            this.Focus();
            WindowState = storedWindowState;
        }

        void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            CheckTrayIcon();
        }
        
        void CheckTrayIcon()
        {
            ShowTrayIcon(!IsVisible);
        }

        void ShowTrayIcon(bool show)
        {
            if (notifyIcon != null)
                notifyIcon.Visible = show;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Hide();
        }
    }
}

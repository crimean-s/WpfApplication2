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

namespace WpfApplication2
{
    
    public partial class MainWindow : Window
    {
       

        SuggestViewModel slist = new SuggestViewModel();
        CommandViewModel command = new CommandViewModel();

        
        //private string command = "web";

        public MainWindow()
        {
            InitializeComponent();

            listBox.ItemsSource = slist.SuggestItems;
            listBox.Focusable = true;

            txt1.Focus();
            txt2.Text = command.CurrentCommand.Name;

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
                web.openWebbrowserQuery(txt1.Text, "");
                listBox.Items.Refresh();   
                txt2.Text = "go to web";                           
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

        private void Window_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Hide();
            //notifyIcon.Visibility = Visibility.Visible;
            //ShowInTaskbar = false;
            ////e..Cancel = true;
        }
    }
}

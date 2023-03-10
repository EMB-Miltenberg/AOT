using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Timers;

namespace AOT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        [DllImport("user32.dll")]
        static extern bool SetWindowText(IntPtr hWnd, string text);

        const int SWP_NOMOVE = 0x0002;
        const int SWP_NOSIZE = 0x0001;
        const int HWND_TOPMOST = -1;
        const int HWND_NOTOPMOST = -2;

        public MainWindow()
        {
            InitializeComponent();
            UpdateMainWindowCombobox();
            UpdateMainWindowButton();

            string _StartupValueWindowTitle = ((App)Application.Current)._WindowTitle;

            if(_StartupValueWindowTitle != null )
            {
                AoT_on(_StartupValueWindowTitle);
                this.Close();
            }
        }

        private void MainWindowButton_Click(object sender, RoutedEventArgs e)
        {
            if(MainWindowComboBox.Text.Contains("- AoT"))
            {
                AoT_off(MainWindowComboBox.Text);
                MainWindowComboBox.Text.Replace(" - AoT", "");
                UpdateMainWindowCombobox();
                UpdateMainWindowButton();
            }
            else
            {
                AoT_on(MainWindowComboBox.Text);
                MainWindowComboBox.Text += " - AoT";
                UpdateMainWindowCombobox();
                UpdateMainWindowButton();
            }
        }

        private void MainWindowButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            UpdateMainWindowCombobox();
        }

        private void MainWindowComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateMainWindowButton();
        }

        private void UpdateMainWindowButton()
        {

            if (MainWindowComboBox.Text.Contains("- AoT") == true)
            {
                MainWindowButton.Content = "Deaktivieren";
            }
            else
            {
                MainWindowButton.Content = "Aktivieren";
            }
        }



        private void UpdateMainWindowCombobox()
        {
            string SelectedItem = MainWindowComboBox.Text;
            
            MainWindowComboBox.Items.Clear();
            GetAllWindowsbyName();
            
            if (MainWindowComboBox.Items.Contains(SelectedItem) == true)
            {
                MainWindowComboBox.Text = SelectedItem;
            }
            else
            {
                MainWindowComboBox.SelectedIndex = 0;
            }
        }

        private void GetAllWindowsbyName()
        {
            Process[] processlist = Process.GetProcesses();
            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    MainWindowComboBox.Items.Add(process.MainWindowTitle.ToString());
                }
            }
        }

        public static void AoT_on(string title)
        {
            Process[] processes = Process.GetProcesses(".");
            foreach (var process in processes)
            {
                string mWinTitle = process.MainWindowTitle.ToString();
                if (mWinTitle == title)
                {
                    IntPtr handle = process.MainWindowHandle;
                    if (handle != IntPtr.Zero)
                    {
                        SetWindowPos(handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
                        string newTitle = title + " - AoT";
                        SetWindowText(handle, newTitle);
                    }
                }
            }
        }

        public static void AoT_off(string title)
        {
            Process[] processes = Process.GetProcesses(".");
            foreach (var process in processes)
            {
                string mWinTitle = process.MainWindowTitle.ToString();
                if (mWinTitle == title)
                {
                    IntPtr handle = process.MainWindowHandle;
                    if (handle != IntPtr.Zero)
                    {
                        SetWindowPos(handle, HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
                        string newTitle = title.Substring(0, title.Length - 6);
                        SetWindowText(handle, newTitle);
                    }
                }
            }
        }

        
    }
}

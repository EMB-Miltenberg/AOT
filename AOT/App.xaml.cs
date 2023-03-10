using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AOT
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public string _WindowTitle { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            for (int i = 0; i < e.Args.Length; i++)
            {
                if (e.Args[i].StartsWith("-Window="))
                {
                    _WindowTitle = e.Args[i].Substring("-Window=".Length);
                    break;
                }
            }
        }
    }
}

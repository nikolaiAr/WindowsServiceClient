using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using model;


namespace WinClient
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        protected override void OnStartup(StartupEventArgs e)
        {
            //App app = new App();
            MainWindow view = new MainWindow();
            ClassModel perf = new ClassModel();
            Presenter presenter = new Presenter(view, perf);
            //app.Run(view);
            //base.OnStartup(e);
        }
    }
}

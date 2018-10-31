﻿using System.Windows;
using ChromaX.Service;
using log4net;

namespace ChromaX
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public readonly ChromaService ChromaService = new ChromaService();

        protected override void OnStartup(StartupEventArgs e)
        {
            Log.Info("Application started");
            base.OnStartup(e);

            var window = new MainWindow(ChromaService);
            window.Show();
        }
    }
}

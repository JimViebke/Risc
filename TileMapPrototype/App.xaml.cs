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
using System.ComponentModel;
using GameLibrary;

using System.ServiceModel; //WCF Types
namespace TileMapPrototype
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, UseSynchronizationContext = false)]
    public partial class App : Application, ICallback
    {
        public static int myCallbackId = 0;
        public static IGame game = null;
        private MainWindow mainWindow;

        App() {
            DuplexChannelFactory<IGame> factory = new DuplexChannelFactory<IGame>(this, "GameService");
            game = factory.CreateChannel();
            myCallbackId = game.RegisterForCallBacks();

            // Create the ViewModel and expose it using the View's DataContext
            mainWindow = new MainWindow();
           
            mainWindow.Show();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {

         
        }

        private delegate void ClientUpdateDelegate(CallbackInfo info);
        public void UpdateGui(CallbackInfo info)
        {

            if (System.Threading.Thread.CurrentThread == this.Dispatcher.Thread)
            {
                // Update the GUI
                mainWindow.DataContext = info.Board;

            }
            else
            {
                this.Dispatcher.BeginInvoke(new ClientUpdateDelegate(UpdateGui), info);
            }
        }

    }
}

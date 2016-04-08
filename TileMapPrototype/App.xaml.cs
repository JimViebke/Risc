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
        public  bool IsReady { get; set; }
        private MainWindow mainWindow;
        private LobbyWindow lobbyWindow;
        App() {
        
            DuplexChannelFactory<IGame> factory = new DuplexChannelFactory<IGame>(this, "GameService");
            game = factory.CreateChannel();
            myCallbackId = game.RegisterForCallBacks();
            IsReady = false;
            mainWindow = new MainWindow();
            lobbyWindow = new LobbyWindow();

            if (myCallbackId != 1)
            {
                lobbyWindow.btnStart.IsEnabled = false;
            }

            lobbyWindow.Show();
            // mainWindow.Show();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {

         
        }

        private delegate void ClientUpdateDelegate(CallbackInfo info);
        public void UpdateGui(CallbackInfo info)
        {
            if (System.Threading.Thread.CurrentThread == this.Dispatcher.Thread)
            {

                if (info.GameStart != true)
                {
                    lobbyWindow.lbPlayers.Items.Clear();
                    foreach (Player player in info.Players)
                    {
                        lobbyWindow.lbPlayers.Items.Add(player.Name);
                    }
                }
                else if(info.GameStart == true)
                {
                    lobbyWindow.Close();

                    // Update the GUI
                    mainWindow.Show();
                }
                mainWindow.DataContext = info;
            }
            else
            {
                this.Dispatcher.BeginInvoke(new ClientUpdateDelegate(UpdateGui), info);
            }
        }

    }
}

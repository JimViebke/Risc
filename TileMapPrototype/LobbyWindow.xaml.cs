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
using System.Windows.Shapes;

namespace TileMapPrototype
{
    /// <summary>
    /// Interaction logic for LobbyWindow.xaml
    /// </summary>
    public partial class LobbyWindow : Window
    {
        public bool readyStatus = false;

        public LobbyWindow()
        {
            InitializeComponent();
            DataContext = App.game.Players;
        }

        private void btnAddPlayer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.game.new_player(tbAddPlayer.Text);
                btnAddPlayer.IsEnabled = false;
            }
            catch (Exception ex)
            { }
        }

        private void btnReady_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.game.StartGame();
                this.IsEnabled = false;
            }
            catch (Exception ex)
            { }
        }
    }
}

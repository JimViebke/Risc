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
	/// Interaction logic for MainWindow.xaml
	/// </summary>
    /// 
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, UseSynchronizationContext = false)]
	public partial class MainWindow : Window
	{
        private TileModel SelectedTile { get; set; }
        //private int myCallbackId = 0;
        //private IGame game = null;

		public MainWindow()
		{
			InitializeComponent();
            try
            {
               
                DataContext = App.game.Board;
            }
            catch (Exception ex)
            { 
            
            }
		}

		private void OnItemValueChanged(TileModel vm)
		{
			MessageBox.Show("Value Changed!\n" + "Row: " + vm.Row + "\nColumn: " + vm.Column + "\nValue: " + vm.Value);
		}


        private delegate void ClientUpdateDelegate(CallbackInfo info);

        public void UpdateGui(CallbackInfo info)
        {
            if (System.Threading.Thread.CurrentThread == this.Dispatcher.Thread)
            {
                // Update the GUI
                DataContext = info.Board;

            }
            else
            {
                this.Dispatcher.BeginInvoke(new ClientUpdateDelegate(UpdateGui), info);
            }
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			TileModel tileClicked = (e.Source as Button).DataContext as TileModel;
      
			if (tileClicked.Value != 0 && SelectedTile == null)
			{
				//tileClicked.Value = tileClicked.Value + 1;
				tileClicked.Background = Game.BLUE;
				SelectedTile = tileClicked;
				App.game.PaintSurroundingTiles(tileClicked.Row, tileClicked.Column, Game.WHITE);
             
			}
			else if ((tileClicked.Value == 0 && tileClicked.Background == Game.WHITE && SelectedTile.Value > 1) || (tileClicked.Background == Game.BLUE && tileClicked != SelectedTile))
			{
				if ((tileClicked.Value == 0))
				{
					tileClicked.Value = SelectedTile.Value - 1;
					tileClicked.Background = Game.BLUE;
					SelectedTile.Value = SelectedTile.Value - SelectedTile.Value + 1;
				}
				else
				{
					tileClicked.Value += SelectedTile.Value - 1;
					tileClicked.Background = Game.BLUE;
					SelectedTile.Value = SelectedTile.Value - SelectedTile.Value + 1;
                   
				}
				SelectedTile = null;
                App.game.PaintAllTiles();
               
			}
			else
			{
                App.game.PaintAllTiles();
                
			}
		}
	}
}

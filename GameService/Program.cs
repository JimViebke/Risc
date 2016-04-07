using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel; //WCF Types
using GameLibrary;

namespace GameService
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost servHost = null;

            try
            {
                //  Create a WCF service host object
                servHost = new ServiceHost(typeof(GameLibrary.Game));
                servHost.Open();
                Console.WriteLine("Game service started. Press a key to quit.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Wait for a keystroke then shut down the service host
                Console.ReadKey();
                if (servHost != null)
                    servHost.Close();
            }
        }
    }
}

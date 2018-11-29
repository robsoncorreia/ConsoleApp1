using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        private const int listenPort = 1234;

        static void Main(string[] args)
        {
            StartListener(); StartListener(); StartListener();
            Console.ReadKey();
        }

        private static void StartListener()
        {
            UdpClient listener = new UdpClient(listenPort);

            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

            List<string> list = new List<string>();

            bool done = true;


            try
            {
                while (done)
                {
                    // queued in the network buffer. 
                    byte[] data = listener.Receive(ref groupEP);

                    IPEndPoint newuser = new IPEndPoint(groupEP.Address, groupEP.Port);

                    string sData = (Encoding.ASCII.GetString(data));


                    var exist = list.FirstOrDefault(x => x.Equals(sData));

                    if (exist != null)
                    {
                        done = false;
                    }
                    else
                    {

                        Console.WriteLine("Waiting for broadcast");

                        Console.WriteLine($"Received broadcast from {groupEP} :");

                        Console.WriteLine($" {sData}");

                        list.Add(sData);
                    }

                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                listener.Close();
            }
        }
    }
}

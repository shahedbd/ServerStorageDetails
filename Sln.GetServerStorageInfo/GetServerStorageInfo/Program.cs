using System;

namespace GetServerStorageInfo
{
    class Program
    {
        //static string MyIPAddress = "192.168.1.111";
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Please enter valid server IP address: ");
                var readServerIPAddress = Console.ReadLine();

                if (Helper.IsIPv4(readServerIPAddress))
                {
                    if (Helper.GetServerPingStatus(readServerIPAddress) == "Success")
                    {
                        Helper.GetDriveInfo(readServerIPAddress);
                    }
                    else Console.Write("Server is down or not accessable!");
                }
                else Console.Write("Invalid IP address");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}

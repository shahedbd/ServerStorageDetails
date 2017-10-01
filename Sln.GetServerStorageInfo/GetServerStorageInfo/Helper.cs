using System;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace GetServerStorageInfo
{
    public static class Helper
    {
        public static void GetDriveInfo(string ComputerName)
        {
            try
            {
                ConnectionOptions conn = new ConnectionOptions();
                //conn.Username = "Administrator";
                //conn.Password = "admin";

                ManagementScope wmiScope = new ManagementScope(string.Format("\\\\{0}\\root\\cimv2", ComputerName), conn);
                ObjectQuery query = new ObjectQuery("select * from Win32_LogicalDisk where DriveType=3");
                ManagementObjectSearcher moSearcher = new ManagementObjectSearcher(wmiScope, query);

                foreach (ManagementObject mObj in moSearcher.Get())
                {
                    Console.WriteLine(" Volume Name: " + GetStorageVolumeName(mObj));
                    Console.WriteLine(" Description: " + GetStorageDescription(mObj));
                    Console.WriteLine(" File system: " + GetStorageFileSystem(mObj));

                    Console.WriteLine(" Available space to current user: " + GetStorageFreeSpace(mObj));
                    Console.WriteLine(" Total size of drive: " + GetStorageSize(mObj));

                    Console.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public static string GetStorageVolumeName(ManagementObject mObj)
        {
            return Convert.ToString(mObj["VolumeName"]);
        }

        public static string GetStorageDescription(ManagementObject mObj)
        {
            return Convert.ToString(mObj["Description"]);
        }

        public static string GetStorageFileSystem(ManagementObject mObj)
        {
            return Convert.ToString(mObj["FileSystem"]);
        }


        public static string GetStorageFreeSpace(ManagementObject mObj)
        {
            decimal freeSpace = Convert.ToDecimal(mObj["FreeSpace"]) / 1024 / 1024 / 1024;
            var FreeSpaceInGB = decimal.Round(freeSpace, 2);
            return FreeSpaceInGB.ToString() + "GB";
        }


        public static string GetStorageSize(ManagementObject mObj)
        {
            decimal freeSpace = Convert.ToDecimal(mObj["Size"]) / 1024 / 1024 / 1024;
            var FreeSpaceInGB = decimal.Round(freeSpace, 2);
            return FreeSpaceInGB.ToString() + "GB";
        }



        public static string GetServerPingStatus(string ComputerName)
        {
            var ping = new Ping();
            var reply = ping.Send(ComputerName, 60 * 1000);
            return reply.Status.ToString();
        }


        public static bool IsIPv4(string ComputerName)
        {
            IPAddress address;

            if (IPAddress.TryParse(ComputerName, out address))
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    return true;
                }
            }

            return false;
        }


    }
}

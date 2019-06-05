using System;
using System.Collections.Generic;

public static class Util
{
    public static string GetMachineNameFromIPAddress(string ipAdress)
    {
        string machineName = string.Empty;
        try
        {
            System.Net.IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(ipAdress);
            machineName = hostEntry.HostName;
        }
        catch (Exception ex)
        {
            //log here
        }
        return machineName;
    }

    public static string GetMacAddress(string ipAddress)
    {
        string macAddress = string.Empty;
        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
        pProcess.StartInfo.FileName = "arp";
        pProcess.StartInfo.Arguments = "-a " + ipAddress;
        pProcess.StartInfo.UseShellExecute = false;
        pProcess.StartInfo.RedirectStandardOutput = true;
        pProcess.StartInfo.CreateNoWindow = true;
        pProcess.Start();
        string strOutput = pProcess.StandardOutput.ReadToEnd();
        string[] substrings = strOutput.Split('-');
        if (substrings.Length >= 8)
        {
            macAddress = substrings[3].Substring(Math.Max(0, substrings[3].Length - 2))
                     + "-" + substrings[4] + "-" + substrings[5] + "-" + substrings[6]
                     + "-" + substrings[7] + "-"
                     + substrings[8].Substring(0, 2);
            return macAddress;
        }
        else
        {
            return "Not Found";
        }
    }

    public static string[] GetAllPCInLan()
    {
        string macAddress = string.Empty;
        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
        pProcess.StartInfo.FileName = "arp";
        pProcess.StartInfo.Arguments = "-a";
        pProcess.StartInfo.UseShellExecute = false;
        pProcess.StartInfo.RedirectStandardOutput = true;
        pProcess.StartInfo.CreateNoWindow = true;
        pProcess.Start();
        string strOutput = pProcess.StandardOutput.ReadToEnd();

        Console.WriteLine(strOutput);

        List<string> pcs = new List<string>();





        return pcs.ToArray();
    }
}

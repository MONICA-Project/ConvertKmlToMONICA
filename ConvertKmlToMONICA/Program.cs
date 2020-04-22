using System;

namespace ConvertKmlToMONICA
{
    class Program
    {
        static void Main(string[] args)
        {
            KMLToMonicaJson c = new KMLToMonicaJson();
            string endpoint = args[1];
            string filename = args[2];

            UpdateMonicaZones upmz = new UpdateMonicaZones();
            c.ConvertZones(".\\" + filename, ".\\" + filename + ".json");
            upmz.LoadZones(".\\" + filename + ".json", endpoint);

        }
    }
}

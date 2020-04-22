using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

using System.Xml.XPath;

namespace ConvertKmlToMONICA
{
    class KMLToMonicaJson
    {
        public bool ConvertZones(string infile, string outfile)
        {
            bool retval = true;
            using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(System.IO.File.Create(outfile)))
            {


                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(infile);
                string comma = "";
                XmlNodeList placeMarks = xDoc.SelectNodes(".//*[local-name()='Placemark']");
                file.WriteLine("[");
                foreach (XmlNode pm in placeMarks)
                {
                    file.WriteLine(comma);
                    string json = ParsePlaceMarkZones(pm);
                    file.WriteLine(json);
                    comma = ",";
                }
                file.WriteLine("]");
            }


            return true;
        }


     
        public bool ConvertThings(string infile, string outfile)
        {
            bool retval = true;
            using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(System.IO.File.Create(outfile)))
            {


                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(infile);
                XmlNodeList placeMarks = xDoc.SelectNodes(".//*[local-name()='Placemark']");
                foreach (XmlNode pm in placeMarks)
                {
                    string json = ParsePlaceMarkThing(pm);
                    file.WriteLine(json);
                }
            }


            return true;
        }

       
       

        public string ParsePlaceMarkZones(XmlNode pm)
        {
            string retVal = "";
            string name = pm.SelectSingleNode("./*[local-name()='name']").InnerText.Replace("\"", "\\\"");
            XmlNode xDesc = pm.SelectSingleNode("./*[local-name()='description']");
            string type = "";
            string description = "";
            if (xDesc != null)
                type = xDesc.InnerText.Replace("\"", "\\\"");
            string polygon = "";
            XmlNode xlookat = pm.SelectSingleNode("./*[local-name()='Point']");
            if (xlookat != null)
            {
                string point = xlookat.SelectSingleNode("./*[local-name()='coordinates']").InnerText;
                string[] coords = point.Split(',');
                polygon = "[[" + coords[1] + "," + coords[0] + "]]";
            }
            XmlNode outerBoundaryIs = pm.SelectSingleNode(".//*[local-name()='outerBoundaryIs']");
            XmlNode testPoly = pm.SelectSingleNode(".//*[local-name()='LineString']");
            if (outerBoundaryIs != null)
            {
                string posis = outerBoundaryIs.SelectSingleNode(".//*[local-name()='coordinates']").InnerText;
                posis = posis.Trim();
                posis = Regex.Replace(posis, @"\s+", " ");
                string[] points = posis.Split(' ');
              
  
                polygon = "[";
                string comma = "";
                foreach (string point in points)
                {
                    string[] coords = point.Split(',');
                    polygon += comma + "[" + coords[1] + "," + coords[0] + "]";
                    comma = ",";
                }
                polygon += "]";

            }
            else if (testPoly != null)
            {
                string posis = pm.SelectSingleNode(".//*[local-name()='coordinates']").InnerText;
                posis = posis.Trim();
                posis = Regex.Replace(posis, @"\s+", " ");
                string[] points = posis.Split(' ');
     
                
                polygon = "[";
                string comma = "";
                foreach (string point in points)
                {
                    string[] coords = point.Split(',');
                    polygon += comma + "[" + coords[1] + "," + coords[0] + "]";
                    comma = ",";
                }
                polygon += "]";

            }
            string prototype = @"{
              ""id"": 0,
              ""name"": ""###name###"",
              ""description"": ""###desc###"",
              ""metadata"": """",
              ""type"": ""###type##"",
             ""boundingPolygon"":###pol###
           }";
            prototype = prototype.Replace("###name###", name);
            prototype = prototype.Replace("###desc###", description);
            prototype = prototype.Replace("###type##", type);
            prototype = prototype.Replace("###pol###", polygon);
            return prototype;
        }


        public string ParsePlaceMarkThing(XmlNode pm)
        {
            string retVal = "";
            string name = pm.SelectSingleNode("./*[local-name()='name']").InnerText;
            XmlNode xDesc = pm.SelectSingleNode("./*[local-name()='description']");
            string lat = "";
            string lon = "";
            string type = "";
            string description = name;
            if (xDesc != null)
                type = xDesc.InnerText;
            string polygon = "";
            XmlNode xlookat = pm.SelectSingleNode("./*[local-name()='Point']");
            if (xlookat != null)
            {
                string point = xlookat.SelectSingleNode("./*[local-name()='coordinates']").InnerText;
                string[] coords = point.Split(',');
                polygon = "[[" + coords[1] + "," + coords[0] + "]]";
                lat = coords[1];
                lon = coords[0];
              
            }


            string prototype =
 @"{
  ""id"": 0,
  ""name"": ""###name###"",
  ""description"": ""###desc###"",
  ""thingtype"": 0,
  ""thingTemplate"": ""###type###"",
  ""status"": 0,
  ""lat"": ###lat###,
  ""lon"": ###lon###
}";
            prototype = prototype.Replace("###name###", name);
            prototype = prototype.Replace("###desc###", description);
            prototype = prototype.Replace("###type###", type);
            prototype = prototype.Replace("###lat###", lat);
            prototype = prototype.Replace("###lon###", lon);
            return prototype;
        }
    }
}

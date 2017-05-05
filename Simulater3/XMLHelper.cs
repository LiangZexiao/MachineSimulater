using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace Simulater3
{
    class XMLHelper
    {
        public static string ConvertDataTableToXML(DataTable xmlDS)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;
            try
            {
                stream = new MemoryStream();
                writer = new XmlTextWriter(stream, Encoding.Default);
                xmlDS.WriteXml(writer);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);
                UTF8Encoding utf = new UTF8Encoding();
                return utf.GetString(arr).Trim();
            }
            catch
            {
                return String.Empty;
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }
        public static DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch (Exception ex)
            {
                string strTest = ex.Message;
                return null;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        public static bool XMLToFile(string XMLString,string FileName)
        {
            try
            {
                XDocument xdoc = XDocument.Parse(XMLString);
                xdoc.Save(FileName);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string ConvertXmlToString(XmlDocument xmlDoc)
        {
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, null);
            writer.Formatting = Formatting.Indented;
            xmlDoc.Save(writer);
            StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
            stream.Position = 0;
            string xmlString = sr.ReadToEnd();
            sr.Close();
            stream.Close();
            return xmlString;
        }


        //保存机器列表
        public static void SaveConfigToXml(List<Machine>  MachineList)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);

            //机器列表
            XmlNode root = xmlDoc.CreateElement("MachineList");
            xmlDoc.AppendChild(root);

            for (int i = 0; i < MachineList.Count; i++)
            {
                XmlNode Machine = xmlDoc.CreateNode(XmlNodeType.Element, MachineList[i].Name, null);  
                CreateNode(xmlDoc, Machine, "id", MachineList[i].id.ToString());  
                CreateNode(xmlDoc, Machine, "Name", MachineList[i].Name);
                CreateNode(xmlDoc, Machine, "IpAddress", MachineList[i].IpAddress);
                CreateNode(xmlDoc, Machine, "ProductionCycle", MachineList[i].ProductionCycle.ToString());
                //CreateNode(xmlDoc, MachineNode, "isRun", MachineList[i].isRun.ToString());  

                XmlNode ParameterNames = xmlDoc.CreateNode(XmlNodeType.Element, "ParameterNames", null);  
                Machine.AppendChild(ParameterNames);

                foreach (var item in MachineList[i].ParameterNames)
                {
                    CreateNode(xmlDoc, ParameterNames, "Parameter", item.ToString());
                }
                root.AppendChild(Machine); 
            }
            try
            {
                xmlDoc.Save(AppDomain.CurrentDomain.BaseDirectory + @"MachineConfig\" + DateTime.Now.ToLongDateString().ToString() + "-" + DateTime.Now.ToLongTimeString().ToString().Replace(":", "-") + ".xml");
            }
            catch (Exception e)
            {
                throw e;
            }  
        }

        public static void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
        }  

        //读取机器列表
        public static void LoadMachineList(string XmlFile, out List<Machine> MachineList, out List<int>GlobalVariable)
        {
            MachineList = new List<Machine>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XmlFile);
            //机器列表
            XmlNode xmlNode = xmlDoc.SelectSingleNode("MachineList");
            XmlNodeList nodeList = xmlNode.ChildNodes;
            foreach (XmlNode item in nodeList)
            {
                Machine _machine = new Machine();
                _machine.id = Convert.ToInt32(item["id"].InnerText);
                _machine.Name = item["Name"].InnerText;
                _machine.IpAddress = item["IpAddress"].InnerText;
                _machine.ProductionCycle = Convert.ToInt32(item["ProductionCycle"].InnerText);
                XmlNodeList parameterList = item["ParameterNames"].ChildNodes;
                foreach (XmlNode parameter in parameterList)
                {
                    _machine.ParameterNames.Add(parameter.InnerText);
                }
                MachineList.Add(_machine);
            }
            //全局变量
            GlobalVariable = new List<int>();
            XmlNode lastNode = nodeList[nodeList.Count - 1];
            int machineID = Convert.ToInt32(lastNode["id"].InnerText)+1;

            string[] ip = lastNode["IpAddress"].InnerText.Split('.');
            int firstByte = Convert.ToInt32(ip[0]);
            int secondByte = Convert.ToInt32(ip[1]);
            int thirdByte = Convert.ToInt32(ip[2]);
            int fourByte = Convert.ToInt32(ip[3]);
            GlobalVariable.Add(machineID);
            GlobalVariable.Add(firstByte);
            GlobalVariable.Add(secondByte);
            GlobalVariable.Add(thirdByte);
            GlobalVariable.Add(fourByte);


        }
    }
}

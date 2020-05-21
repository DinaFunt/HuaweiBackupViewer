using System;
using System.Collections.Specialized;
using System.IO;
using System.Xml;

namespace BackupViewer
{
    public static class ParseXml
    {
        static object XmlGetColumnValue(XmlNode xmlNode)
        {
            XmlNode child = xmlNode.FirstChild;
            if (child.Name != "value")
            {
                Console.WriteLine("xml_get_column_value: entry has no values!");
                return null;
            }
            if (child.Attributes["Null"] != null)
            {
                return null;
            }
            if (child.Attributes["String"] != null)
            {
                return child.Attributes["String"].Value;
            }
            if (child.Attributes["Integer"] != null)
            {
                return Convert.ToInt32(child.Attributes["Integer"].Value);
            }
            
            Console.WriteLine("xml_get_column_value: unknown value attribute.");
            return null;
        }

        static void ParseBackupFilesTypeInfo(ref Decryptor decryptor, XmlDocument xmlEntry)
        {
            XmlNodeList elemList = xmlEntry.GetElementsByTagName("column");
            foreach (XmlNode entry in elemList)
            {
                string name = entry.Attributes["name"].Value;
                switch (name)
                {
                    case "e_perbackupkey":
                        decryptor.EPerBackupkey = XmlGetColumnValue(entry) as string;
                        break;
                    case "pwkey_salt":
                        decryptor.PwkeySalt = XmlGetColumnValue(entry) as string;
                        break;
                    case "type_attch":
                        decryptor.TypeAttch = Convert.ToInt32(XmlGetColumnValue(entry));
                        break;
                    case "checkMsg":
                        decryptor.CheckMsg = XmlGetColumnValue(entry) as string;
                        break;
                }
            }
        }

        static DecryptMaterial ParseBackupFileModuleInfo(XmlDocument xmlEntry)
        {
            string aString = xmlEntry.FirstChild.Attributes["table"].Value;
            DecryptMaterial decm = new DecryptMaterial(aString);

            XmlNodeList elemList = xmlEntry.GetElementsByTagName("column");
            foreach (XmlNode entry in elemList)
            {
                string name = entry.Attributes["name"].Value;
                if (name == "encMsgV3")
                {
                    decm.EncMsgV3 = XmlGetColumnValue(entry) as string;
                }
                else if (name == "checkMsgV3")
                {
                    // TBR: reverse this double sized checkMsgV3.
                }
                else if (name == "name")
                {
                    decm.Name = XmlGetColumnValue(entry) as string;
                }
            }

            if (decm.DoCheck() == false)
            {
                return null;
            }

            return decm;
        }

        public static HybridDictionary ParseInfoXml(string filepath, ref Decryptor decryptor, HybridDictionary decryptMaterialDict)
        {
            XmlDocument infoDom = new XmlDocument();
            infoDom.Load(filepath);

            if (infoDom.GetElementsByTagName("info.xml").Count != 1)
            {
                Console.WriteLine("First tag should be 'info.xml', not '{0}'", infoDom.FirstChild.Name);
                decryptor = null;
                return null;
            }

            string parent = Directory.GetParent(filepath).FullName;

            XmlDocument doc;
            XmlNodeList elemList = infoDom.GetElementsByTagName("row");
            foreach (XmlNode entry in elemList)
            {
                string title = entry.Attributes["table"].Value;
                switch (title)
                {
                    case "BackupFilesTypeInfo":
                        doc = new XmlDocument();
                        doc.LoadXml(entry.OuterXml);
                        ParseBackupFilesTypeInfo(ref decryptor, doc);
                        break;
                    case "BackupFileModuleInfo":
                    case "BackupFileModuleInfo_Contact":
                    case "BackupFileModuleInfo_Media":
                    case "BackupFileModuleInfo_SystemData":
                        doc = new XmlDocument();
                        doc.LoadXml(entry.OuterXml);
                        DecryptMaterial decMaterial = ParseBackupFileModuleInfo(doc);
                        if (decMaterial != null)
                        {
                            string dkey = Path.Combine(parent, decMaterial.Name);
                            decryptMaterialDict[dkey] = decMaterial;
                        }
                        break;
                }
            }

            return decryptMaterialDict;
        }

        public static HybridDictionary ParseXML(string filepath, HybridDictionary decryptMaterialDict)
        {
            XmlDocument xmlDom = new XmlDocument();
            xmlDom.Load(filepath);

            string fullpath = Directory.GetParent(filepath).FullName;
            string parent = Path.Combine(fullpath, Path.GetFileNameWithoutExtension(filepath));

            XmlNodeList elemList = xmlDom.GetElementsByTagName("File");
            foreach (XmlNode node in elemList)
            {
                string path = node.SelectSingleNode("Path").InnerText;
                string iv = node.SelectSingleNode("Iv").InnerText;
                if (!String.IsNullOrEmpty(path) && !String.IsNullOrEmpty(iv))
                {
                    DecryptMaterial decMaterial = new DecryptMaterial(Path.GetFileNameWithoutExtension(filepath));
                    // XML files use Windows style path separator, backslash.
                    decMaterial.Path = path;
                    decMaterial.IV = iv;
                    string dkey = Path.Combine(parent, path.TrimStart(new char[] { '\\' }));
                    decryptMaterialDict[dkey] = decMaterial;
                }
            }

            return decryptMaterialDict;
        }
    }
}
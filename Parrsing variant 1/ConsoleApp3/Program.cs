using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp3
{
    class Program
    {

        static public void AnalyzStruct(XmlNode xRoot, ref int level, StreamWriter sw, ref string str_int)
        {
            if (level <= 2)
            {
                str_int = str_int + level + "Tag_" + xRoot.Name.ToUpper() + "; ";
                for (int i = 0; i < xRoot.Attributes.Count; i++)
                {
                    switch (xRoot.Attributes.Item(i).Name)
                    {
                        case "name":
                            {
                                str_int = str_int + xRoot.Attributes.Item(i).Value + ";  ";
                                break;
                            }
                        case "subcategories-loading":
                            {
                                str_int = str_int + xRoot.Attributes.Item(i).Value + "; ";
                                break;
                            }
                        case "objects-loading":
                            {
                                str_int = str_int + xRoot.Attributes.Item(i).Value + "; ";
                                break;
                            }
                        case "mdl-applying-result":
                            {
                                str_int = str_int + xRoot.Attributes.Item(i).Value + "; ";
                                break;
                            }
                        case "artificial-src-full-name":
                            {
                                str_int = str_int + xRoot.Attributes.Item(i).Value + "; ";
                                break;
                            }
                        default:
                            {
                                str_int = str_int + "; ";
                                break;
                            }
                    }
                }
            }

            foreach (XmlNode childnode in xRoot.ChildNodes)
            {
                level++;

                AnalyzStruct(childnode, ref level, sw, ref str_int);
            }

            if (level == 2)
            {
                sw.WriteLine(str_int);
            }

            string s = level + "Tag_" + xRoot.Name.ToUpper() + ";";
            if (str_int.LastIndexOf(s) != -1)
            {
                int r = str_int.LastIndexOf(s);
                str_int = str_int.Substring(0, r);
            }

            level--;
        } 

        static void Main(string[] args)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("E:/Ellucian/DIR_ELLUCIAN/target.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            int level = 0;
            string str_int = null;
            StreamWriter sw = new StreamWriter(@"E:/Ellucian/DIR_ELLUCIAN/target_csv.csv", false, System.Text.Encoding.Default);

             for(int i  = 0; i < xRoot.ChildNodes.Count; i++) 
             {
                 level = 0;
                 str_int = null;
                 XmlNode xnode = xRoot.ChildNodes.Item(i);
                 AnalyzStruct(xnode, ref level, sw, ref str_int);
            }

            sw.Close();

            Console.Read();
        }
    }
}

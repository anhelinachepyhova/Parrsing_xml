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
		public struct Head
		{
			public string head;
		}

        static public void AnalyzStruct(XmlNode xRoot, ref int level, StreamWriter sw, ref string str_int, List<string> hd )
        {
			bool test = false;
            if (level <= 2)
            {
                str_int = str_int + level + "Tag_" + xRoot.Name.ToUpper() + "; ";
				for(int j = 0; j < hd.Count; j++)
				{
					 test = false;
					for (int i = 0; i < xRoot.Attributes.Count; i++)
					{
						if (hd[j] == xRoot.Attributes.Item(i).Name)
							{
								str_int = str_int + xRoot.Attributes.Item(i).Value + ";  ";
								test = true;
							}
							/*switch (xRoot.Attributes.Item(i).Name)
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
							}*/
					}
					if  (test == false) 
						str_int = str_int + "; ";

				}
            }

            foreach (XmlNode childnode in xRoot.ChildNodes)
            {
                level++;

                AnalyzStruct(childnode, ref level, sw, ref str_int, hd);
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

		static string fullnamefile()
		{
			string f_path;
			do
			{
				Console.Write("Please, input full name file (with path) : ");
				 f_path = Console.ReadLine();
			}while (f_path is null);
			return f_path;

		}

		static XmlElement openXMLfile(XmlDocument xDoc)
		{
			bool flag_open = true;
			do
			{
				try
				{
					xDoc.Load(fullnamefile());
					return xDoc.DocumentElement;
				}
				catch
				{
					flag_open = false;
				}
			} while (flag_open == false);
			return null;
		}

		static StreamWriter saveXMLfile()
		{
			bool flag_open = true;	
			do
			{
				try
				{
					return new StreamWriter(fullnamefile(), false, System.Text.Encoding.Default);
				}
				catch
				{
					flag_open = false;
				}
			} while (flag_open == false);
			return null;
		}
		

        static void Main(string[] args)
        {

            XmlDocument xDoc = new XmlDocument();

			Console.WriteLine("Input date for work xml file");
			XmlElement xRoot = openXMLfile(xDoc);

			Console.WriteLine("Input date for save result parssing xml file");
			StreamWriter sw = saveXMLfile();

			List<string> hd = new List<string>();
			hd.Add("name");
			hd.Add("child-type");
			hd.Add("subcategories-loading");
			hd.Add("objects-loading"); 
			hd.Add("object-loading");
			hd.Add("mdl-applying-result");
			hd.Add("artificial-src-full-name");

            int level;
            string str_int;
           
             for(int i  = 0; i < xRoot.ChildNodes.Count; i++) 
             {
                 level = 0;
                 str_int = null;
                 XmlNode xnode = xRoot.ChildNodes.Item(i);
                 AnalyzStruct(xnode, ref level, sw, ref str_int, hd);
            }

            sw.Close();

            Console.Read();
        }
    }
}

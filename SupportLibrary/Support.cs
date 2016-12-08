/*
 * Vytvořeno aplikací SharpDevelop.
 * Uživatel: asus
 * Datum: 8. 12. 2016
 * Čas: 15:09
 * 
 * Tento template můžete změnit pomocí Nástroje | Možnosti | Psaní kódu | Upravit standardní hlavičky souborů.
 */
using System;

namespace SupportLibrary
{
<<<<<<< HEAD
	//jaja
	//using
=======
	//něco
>>>>>>> origin/master
	//bemdr
	//88
	
	public class Support
	{
		static string  name ="./Log/Log["+DateTime.Now.ToString("HH;mm;ss")+"].txt";
		public static void Log(string b)
        {
			string time = DateTime.Now.ToString("HH:mm");
			if (!System.IO.File.Exists(name))
			{
				
                System.Windows.Forms.MessageBox.Show("Test"+time);
                System.IO.Directory.CreateDirectory("./Log");
                System.IO.FileStream fs = System.IO.File.Create(name);
                fs.Close();
			}
			Beguning: try
        	{   System.IO.StreamWriter sw = new System.IO.StreamWriter(name, true);
				string createText = Environment.NewLine + time + " -> " + b;
				sw.Write(createText);
				sw.Close();
			}
        	catch 
        	{
        		goto Beguning;
        	}
		}
	}
}
/*
 * Created by SharpDevelop.
 * User: Asus
 * Date: 16. 5. 2017
 * Time: 20:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace CheckUpdate
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class Update
	{
		static String name = "/Game Launscher.exe";
		static String directory = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
		static System.Net.WebClient wClient = new System.Net.WebClient();
		static String subject = directory + name;
		static System.Net.WebClient web = new System.Net.WebClient();
		static System.IO.Stream stream = web.OpenRead("/test url/version.txt");
        static System.IO.StreamReader reader = new System.IO.StreamReader(stream);
        static String line = reader.ReadLine();
        static String version = line.Substring(line.Length - 4);
        	
		public static void Start(){
        	if (version.Contains("c"/*číslo aktuální verze*/)) {
				foreach (System.Diagnostics.Process proces in System.Diagnostics.Process.GetProcesses()) {
					if (proces.ProcessName == "Skipe"){
						//Jak zavřít nadřazený form
						Get();
					} else {
						Get();
					}
				}
        	}
		}
        
        static void Get(){
			if (System.IO.File.Exists(subject)){
				System.IO.File.Delete(subject);
			}
			
            wClient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(Completed);
            wClient.DownloadFileAsync(new Uri("link"), name);
        }

        static void Completed(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //System. MessageBox.Show("Download completed!");
            System.Diagnostics.Process.Start(directory + name);
			//Jak zavřít nadřazený form
        } 
	}
}

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
		String name = "/Game Launscher.exe";
		String directory = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
		System.Net.WebClient wClient = new System.Net.WebClient();
		String subject = directory + name;
		System.Net.WebClient web = new System.Net.WebClient();
		System.IO.Stream stream = web.OpenRead("/test url/version.txt");
        System.IO.StreamReader reader = new System.IO.StreamReader(stream);
        String version = /*(jen čísla "2.0.0." ) Size proměné reader.ReadLine()*/;
        	
		public static void Start(){
        	if (version.Contains(this.ProductVersion)) {
				foreach (System.Diagnostics.Process proces in System.Diagnostics.Process.GetProcesses()) {
					if (proces.ProcessName == "Skipe"){
						this.Close();
						getUpdate();
					} else {
						getUpdate();
					}
				}
        	}
		}
        
        private void Get(){
			if (System.IO.File.Exists(subject)){
				System.IO.File.Delete(subject);
			}
			
            wClient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(Completed);
            wClient.DownloadFileAsync(new Uri("link"), name);
        }

        private void Completed(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Download completed!");
            System.Diagnostics.Process.Start(directory + name);
            this.Close();
        } 
	}
}

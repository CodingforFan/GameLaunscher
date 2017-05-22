/*
 * Created by SharpDevelop.
 * User: Asus
 * Date: 29. 12. 2016
 * Time: 23:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Text;

namespace GameBox_v2
{
	
	/// <summary>
	/// Description of Karta_Hry.
	/// </summary>
	public partial class Karta_Hry : Form
	{
		WebBrowser wb1 = new WebBrowser();
		static string G_Name = "";
		string webText;
		bool tryAgain = true;
		static string img_utl = "";
		public Karta_Hry(string gamename = "")
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			G_Name = gamename;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		void Karta_HryLoad(object sender, EventArgs e)
		{
			while(tryAgain){
				try{
					using(System.Net.WebClient client = new System.Net.WebClient()){
						client.Encoding = Encoding.UTF8;
						client.Encoding = ASCIIEncoding.UTF8;
						string htmlcode = client.DownloadString("https://www.databaze-her.cz/hry/" + G_Name.Replace(" ","-"));
						webText = htmlcode;
						label1.Text = G_Name;
					}
					if(webText != ""){
						if(webText.IndexOf("<h1>Stránka nenalezena</h1>") == -1 || webText != null || webText != "" || webText != " " || webText != string.Empty){
							int posS = webText.IndexOf("<img src=\"/obrazky/hry_krabice/");
							string myCapturedText = webText.Substring(posS, webText.IndexOf('>',posS) - posS + 1).Remove(0, 10);
							int pos = myCapturedText.IndexOf("?_");
							img_utl = "https://www.databaze-her.cz" + myCapturedText.Remove(pos, (myCapturedText.Length - pos));
							pictureBox1.Load(img_utl);
							
							posS = webText.IndexOf("<div id=\"game-description\"");
							myCapturedText = webText.Substring(posS, webText.IndexOf("</div>",posS) - posS).Remove(0, 10);
							pos = myCapturedText.IndexOf('>', 0);
							label2.Text = myCapturedText.Remove(0, pos + 1);
							for(;label2.Text.Contains("<") && label2.Text.Contains(">");){
								int pos2 = label2.Text.IndexOf('<');
								int pos3 = label2.Text.IndexOf('>')  + 1 - pos2;
								label2.Text = label2.Text.Remove(pos2, pos3);
							}	
						}else{
							throw new Exception("");
						}
					}else{
						throw new Exception("");
					}
					tryAgain = false;
				}catch{
					if(new NameField(G_Name).ShowDialog() == DialogResult.OK){
						for(int i = 0; i < MainForm.frm.flowLayoutPanel1.Controls.Count; i++){
							if(MainForm.frm.flowLayoutPanel1.Controls[i].GetChildAtPoint(new Point(0,0)).Text.Contains(G_Name)){
								MainForm.frm.flowLayoutPanel1.Controls[i].GetChildAtPoint(new Point(0,0)).Text = NameField.name;
								int id = -1;
								if(int.TryParse(MainForm.frm.flowLayoutPanel1.Controls[i].Tag.ToString(),out id)){
									if(id < MainForm.frm.datas.Count && id != -1){
										int pos = MainForm.frm.datas[id].IndexOf(G_Name);
										MainForm.frm.datas[id] = MainForm.frm.datas[id].Substring(0, pos).Replace(G_Name, NameField.name);
										MainForm.frm.LauncherContent();
										MainForm.frm.GameReload();
									}
								}
							}
						}
						G_Name = NameField.name;
					}else{
						tryAgain = false;
						this.Close();
					}
				}
			}
		}
		
		void PictureBox1Click(object sender, MouseEventArgs e)
		{
				contextMenuStrip1.Show( MousePosition); 
		}
		
		void DownloadImageToolStripMenuItemClick(object sender, EventArgs e)
		{
			using(System.Net.WebClient client = new System.Net.WebClient())
			{
   				client.DownloadFile(img_utl, "./coverlib/" + G_Name + img_utl.Substring(img_utl.Length - 4));
   				MainForm.frm.GameReload();
			}
		}
		void Karta_HryMouseDown(object sender, MouseEventArgs e)
		{
	
		}
		void PictureBox1MouseDown(object sender, MouseEventArgs e)
		{
			contextMenuStrip1.Show( MousePosition); 
	
		}
	}
}

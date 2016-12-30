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

namespace Game_Launscher
{
	
	/// <summary>
	/// Description of Karta_Hry.
	/// </summary>
	public partial class Karta_Hry : Form
	{
		WebBrowser wb1 = new WebBrowser();
		static string G_Name = "";
		public Karta_Hry(string gamename = "")
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			G_Name = gamename;
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		void Karta_HryLoad(object sender, EventArgs e)
		{
			try{
				wb1.Url = new Uri("https://www.igdb.com/games/the-witcher-3-wild-hunt-game-of-the-year-edition");
				string gg = wb1.Document.Body.ToString();
				MessageBox.Show(gg);
			}catch{
			}
			label1.Text = G_Name;
			
			pictureBox1.Load("https://images.igdb.com/igdb/image/upload/t_cover_big/jusjbgw2hb80sgyltvlw.jpg");
	
		}
	}
}

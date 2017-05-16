/*
 * Created by SharpDevelop.
 * User: Asus
 * Date: 13. 3. 2017
 * Time: 19:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;


namespace GameBox_v2
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{		
		static string current_item_name = null;
		static Control current_item = null;
		public List <string> datas = new List<string>();
		public System.IO.StreamReader sr;
		public System.IO.StreamWriter sw;
		public List <string> saveTime;
		public bool gameIsRun = false;
		public int activeGame = -1;
		public string processName;
		public System.Diagnostics.Process process;
		public string listing = "";		
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			datas = new List<string>();
			if(File.Exists("./config/datalib.glconfig")){
				int countLines = File.ReadAllLines("./config/datalib.glconfig").Length;
				sr = new StreamReader("./config/datalib.glconfig");
				if(sr.ReadToEnd().Length > 0){
					sr.Close();
					sr = new StreamReader("./config/datalib.glconfig");
					for(int i = 0; i < countLines; i++){
						datas.Add(sr.ReadLine());
					}
					for(int g = 0; g < datas.Count; g++){
						string a = "";
						int num = 0;
						if(datas[g] != null){
							foreach(string d in datas[g].Split('|')){
								if(num == 2){
									a = d;
								}
								num ++;
							}
						}
						if(a != ""){
							
							additem(a);
						}
					}
				}
				sr.Close();
			}else{
				if(!Directory.Exists("./config")){
					Directory.CreateDirectory("./config");
				}
				File.CreateText("./config/datalib.glconfig").Close();
				datas = new List<string>();
			}
		}
		void Button1Click(object sender, EventArgs e)
		{
			openFileDialog1.ShowDialog();
		}
		void OpenFileDialog1FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			foreach(string a in openFileDialog1.FileNames)
			{
				additem(a);
			}
		}
		void additem (string a){
			
			foreach ( Control c1 in flowLayoutPanel1.Controls ) {
				if (c1.Name.Contains(a)){
					goto pidano;
				}
			}
				
			if (a.Contains(".exe")){
				FileVersionInfo fi = FileVersionInfo.GetVersionInfo(a);
				string name = fi.ProductName;
				if(name == "")
					name = Path.GetFileNameWithoutExtension(a);
				System.Text.StringBuilder newText = new System.Text.StringBuilder(name.Length * 2);
				newText.Append(name[0]);
				for (int i = 1; i < name.Length; i++)
        		{
            		if (char.IsUpper(name[i]))
                		if (name[i - 1] != ' ' && !char.IsUpper(name[i - 1]))
                    		newText.Append(' ');
            		newText.Append(name[i]);
        		}
				Image ii = Icon.ExtractAssociatedIcon(a).ToBitmap();
				if (File.Exists("./coverlib/" + newText + ".png" )){
					ii = Image.FromFile("./coverlib/" + newText + ".png" );
				} else if (File.Exists("./coverlib/" + newText + ".jpg" )){
					ii = Image.FromFile("./coverlib/" + newText + ".jpg" );
				} else if (File.Exists("./coverlib/" + newText + ".gif" )){
					ii = Image.FromFile("./coverlib/" + newText + ".gif" );
				}
				PictureBox pb = new PictureBox {Name = a,Size = new Size(180,254),BackgroundImageLayout = ImageLayout.Zoom, BackgroundImage = ii};
				pb.Tag = flowLayoutPanel1.Controls.Count.ToString();
				pb.MouseClick += Item_Click;
				Label lb = new Label {Name = a + "1", Text = newText.ToString()};
				lb.MouseClick += Item_Click;
				lb.Font = new Font(new FontFamily("Arial"), 10,FontStyle.Bold);
				lb.TextAlign = ContentAlignment.MiddleCenter;
				lb.Location = new Point(0,0);
				lb.Size = new Size(180,35);
				lb.AutoEllipsis = true;
				lb.BackColor = Color.Transparent;
				pb.Controls.Add(lb);
				
				flowLayoutPanel1.Controls.Add(pb);
					
			}
			goto mustek;
			pidano: MessageBox.Show("Položka: <b>" + Path.GetFileName(a).Replace(".exe", string.Empty) + "<b> je již přidána!");
			mustek:;
			
		}
		Control GetControlUnderMouse() {
    		foreach ( Control c in flowLayoutPanel1.Controls ) {
        		if ( c.Bounds.Contains(flowLayoutPanel1.PointToClient(MousePosition)) ) {
					return c;
					
         		}
    		}
			return null;
		}
		void Item_Click(object sender, MouseEventArgs e)
        {
			current_item = GetControlUnderMouse();
			current_item_name = current_item.Controls[0].Text;
			
			if (e.Button == MouseButtons.Left){
				ButtonSet(System.Diagnostics.Process.Start(current_item.Name), int.Parse(current_item.Tag.ToString()));
			}else if (e.Button == MouseButtons.Right){
				contextMenuStrip1.Show( MousePosition); 
				//MessageBox.Show(GetControlUnderMouse().Name);
				//new Karta_Hry(current_item_name).Show();
			}
		}
		void UpdateTick(object sender, EventArgs e)
		{
			//CreateGraphics().DrawRectangle(new Pen(Color.Red, 1), new Rectangle(0, 0+30, Width-1, Height-31));
			label2.Text = DateTime.Now.ToString("HH:mm");
		}
		
		public void ButtonSet(System.Diagnostics.Process processe, int ID){
			gameIsRun = true;
			process = processe;
			processName = process.ProcessName;
			activeGame = ID;
			saveTime = new List<string>();
			var sets = DateTime.Now.ToString("HH;mm;ss");
			saveTime.Add(sets.Split(';')[0]);
			saveTime.Add(sets.Split(';')[1]);
			saveTime.Add(sets.Split(';')[2]);
		}
		void OpenFileDialog2FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			string localfilenames = openFileDialog2.FileName;
			if(File.Exists(localfilenames)){
				Directory.CreateDirectory("./coverlib");
				MessageBox.Show(current_item_name + current_item.ToString());
				File.Copy(openFileDialog2.FileName, "./coverlib/" + current_item_name.Replace(".exe", string.Empty) + "." + localfilenames.Split('.')[1], true);

				current_item.BackgroundImage = Image.FromFile("./coverlib/"+ current_item_name.Replace(".exe", string.Empty) +"." + localfilenames.Split('.')[1]);
    		}	
		}
		void ZměnitObrázekToolStripMenuItemClick(object sender, EventArgs e)
		{
			openFileDialog2.ShowDialog();
		}
		void MainFormActivated(object sender, EventArgs e)
		{
			if(gameIsRun){
				if(process.HasExited){
					gameIsRun = false;
					if(datas.Count > activeGame){
						if(datas[activeGame] != null){
							int num = 0;
							foreach(string t in datas[activeGame].Split('|')){
								if(num == 2){
									datas[activeGame].Replace(t, HowLongPlay(t));
								}
								num++;
							}
						}else{
							datas[activeGame] = processName.Replace(".exe", string.Empty) + "|" + HowLongPlay("00;00;00");
						}
					}else{
						for(int i = 0; i <= activeGame; i++){
							if(i == activeGame){
								datas.Add(processName.Replace(".exe", string.Empty) + "|" + HowLongPlay("00;00;00"));
							}else{
								datas.Add(null);
							}
						}
					}
					activeGame = -1;
					SaveData();
				}
			}
		}
		public void SaveData(){
			if(System.IO.File.Exists("./config/datalib.glconfig")){
				sw = new System.IO.StreamWriter("./config/datalib.glconfig", false);
				for(int i = 0; i < datas.Count; i++){
					if(datas[i] != null){
						sw.Write(datas[i] + Environment.NewLine);
					}
				}
				sw.Close();
			}else{
				sw = System.IO.File.CreateText("./config/datalib.glconfig");
				for(int i = 0; i < datas.Count; i++){
					if(datas[i] != null){
						sw.Write(datas[i] + Environment.NewLine);
					}
				}
				sw.Close();
			}
		}
		
		public string HowLongPlay(string lastPlayeTime){
			List<string> nowTime = new List<string>();
			var sets = DateTime.Now.ToString("HH;mm;ss");
			nowTime.Add(sets.Split(';')[0]);
			nowTime.Add(sets.Split(';')[1]);
			nowTime.Add(sets.Split(';')[2]);
			int num = 0;
			string h = "00";
			string m = "00";
			string s = "00";
			foreach(string one in lastPlayeTime.Split(';')){
				int nt = int.Parse(nowTime[num]);
				int st = int.Parse(saveTime[num]);
				int o = int.Parse(one);
				int hp = int.Parse(h);
				int mp = int.Parse(m);
				if(num == 0){
					if(nt >= st){
						h = (o + (nt - st)).ToString();
					}else{
						h = (o + (24 - st + nt)).ToString();
					}
				}else if(num == 1){
					if(nt >= st){
						m = (o + (nt - st)).ToString();
					}else{
						m = (o + (60 - st + nt)).ToString();
						h = (hp - 1).ToString();
					}
				}else if(num == 2){
					if(nt >= st){
						s = (o + (nt - st)).ToString();
					}else{
						s = (o + (60 - st + nt)).ToString();
						m = (mp - 1).ToString();
					}
				}
				num++;
			}
			return (h + ";" + m + ";" + s);
		}
		void ToolStripMenuItem1Click(object sender, EventArgs e)
		{
			new Karta_Hry(current_item_name).Show();
		}
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			LauncherContent();
		}
		void LauncherContent(){
			foreach(Control a in flowLayoutPanel1.Controls){
				int sets = int.Parse(a.Tag.ToString());
				if(datas.Count > sets){
					if(datas[sets] != null){
						int num = 0;
						foreach(string s in datas[sets].Split('|')){num++;}
						if(num == 3){
							datas[sets] += "|" + a.Name;
						}else if(num == 2){
							datas[sets] +="00;00;00" + "|" + a.Name;
						}else if(num <= 1){
							datas[sets] = Path.GetFileName(a.Name).Replace(".exe", string.Empty) + "|" + "00;00;00" + "|" + a.Name;
						}
					}else{
						datas[sets] = Path.GetFileName(a.Name).Replace(".exe", string.Empty) + "|" + "00;00;00" + "|" + a.Name;
					}
				}else{
					for(int i = 0; i <= sets; i++){
						if(i == sets){
							datas.Add(Path.GetFileName(a.Name).Replace(".exe", string.Empty) + "|" + "00;00;00" + "|" + a.Name);
						}else{
							datas.Add(null);
						}
					}
				}
			}
			SaveData();
		}
		void FlowLayoutPanel1Paint(object sender, PaintEventArgs e)
		{
	
		}
		
		string NameCleaner(string name){
			if(name.Contains(".exe")){
				name.Replace(".exe", string.Empty);
			}
			//if(name.s){
				
			//}
			return name;
		}
	}
}

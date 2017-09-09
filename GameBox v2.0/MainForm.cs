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
		public StreamReader sr;
		public StreamWriter sw;
		public List <string> saveTime;
		public bool gameIsRun = false;
		public int activeGame = -1;
		public string processName;
		public Process process;
		public string listing = "";
		public static MainForm frm;
		public string[] sep = {"|"};
		
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
			frm = this;
			datas = new List<string>();
			if(File.Exists("./config/datalib.glconfig")){
				int countLines = File.ReadAllLines("./config/datalib.glconfig").Length;
				try{sr.Close();}catch{}
				sr = new StreamReader("./config/datalib.glconfig");
				if(sr.ReadToEnd().Length > 0){
					try{sr.Close();}catch{}
					sr = new StreamReader("./config/datalib.glconfig");
					for(int i = 0; i < countLines; i++){
						datas.Add(sr.ReadLine());
					}
					GameReload();
				}
				try{sr.Close();}catch{}
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
				try{
					additem(a);
				}catch{
					MessageBox.Show("Game cannot add " + NameCleaner(Path.GetFileNameWithoutExtension(a)));
				}
			}
		}
		void additem (string a, int ids = -1){
			if(flowLayoutPanel1.Controls.Count > 0){
				foreach (Control c1 in flowLayoutPanel1.Controls) {
					if (c1.Name.Contains(a)){
						goto pidano;
					}
				}
			}
			if(a.Contains(".exe")){
				string name = "";
				FileVersionInfo fi = FileVersionInfo.GetVersionInfo(a);
				name = fi.ProductName;
				if(name == "" || name == string.Empty || name == " " || name == null)
					name = Path.GetFileNameWithoutExtension(a);
				bool named = true;
				for(int i = 0; i < datas.Count; i++){
					if(name.Contains(datas[i].Split(sep, StringSplitOptions.RemoveEmptyEntries)[0])){
						named = false;
					}
				}
				if(named){
					name = NameCleaner(name);
				}
				if(ids != -1){
					if(datas.Count > ids){
						name = datas[ids].Split(sep, StringSplitOptions.RemoveEmptyEntries)[0];
					}
				}
				Image ii = Icon.ExtractAssociatedIcon(a).ToBitmap();
				if (File.Exists("./coverlib/" + name + ".png" )){
					ii = Image.FromFile("./coverlib/" + name + ".png" );
				} else if (File.Exists("./coverlib/" + name + ".jpg" )){
					ii = Image.FromFile("./coverlib/" + name + ".jpg" );
				} else if (File.Exists("./coverlib/" + name + ".gif" )){
					ii = Image.FromFile("./coverlib/" + name + ".gif" );
				}
				PictureBox pb = new PictureBox {Name = a,Size = new Size(180,254),BackgroundImageLayout = ImageLayout.Zoom, BackgroundImage = ii};
				pb.MouseClick += Item_Click;
				Label lb = new Label {Name = a, Text = name};
				lb.MouseClick += Item_Click;
				lb.Font = new Font(new FontFamily("Arial"), 10,FontStyle.Bold);
				lb.TextAlign = ContentAlignment.MiddleCenter;
				lb.Location = new Point(0,0);
				lb.Size = new Size(180,35);
				lb.AutoEllipsis = true;
				lb.BackColor = Color.Transparent;
				pb.Controls.Add(lb);
				pb.Tag = flowLayoutPanel1.Controls.Count.ToString();				
				flowLayoutPanel1.Controls.Add(pb);
				int id = -1;
				if(int.TryParse(pb.Tag.ToString(), out id)){
					LauncherContent(id);
				}
			}
			goto mustek;
			pidano: MessageBox.Show("Položka: <b>" + NameCleaner(Path.GetFileName(a)) + "<b> je již přidána!");
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
					int locInt;
					if(int.TryParse(current_item.Tag.ToString(), out locInt)){
						try{
							ButtonSet(Process.Start(current_item.Name), locInt);
						}catch{
						}
					}
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
			label2.Location =  new Point((Width / 2) - (label2.Width/2), label2.Location.Y);
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
				File.Copy(openFileDialog2.FileName, "./coverlib/" + current_item_name + "." + localfilenames.Split('.')[1], true);
				current_item.BackgroundImage = Image.FromFile("./coverlib/"+ current_item_name +"." + localfilenames.Split('.')[1]);
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
							foreach(string t in datas[activeGame].Split(sep, StringSplitOptions.RemoveEmptyEntries)){
								if(num == 2){
									datas[activeGame].Replace(t, HowLongPlay(t));
								}
								num++;
							}
						}else{
							datas[activeGame] = "|" + processName + "||" + HowLongPlay("00;00;00") + "|";
						}
					}else{
						for(int i = 0; i <= activeGame; i++){
							if(i == activeGame){
								datas.Add("|" + processName + "||" + HowLongPlay("00;00;00") + "|");
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
			try{sw.Close();}catch{}
			try{sr.Close();}catch{}
			if(File.Exists("./config/datalib.glconfig")){
				try{sw.Close();}catch{}
				sw = new StreamWriter("./config/datalib.glconfig", false);
				for(int i = 0; i < datas.Count; i++){
					if(datas[i] != null && datas[i] != "" && datas[i] != " " && datas[i] != string.Empty){
						sw.Write(datas[i] + Environment.NewLine);
					}
				}
				try{sw.Close();}catch{}
			}else{
				try{sw.Close();}catch{}
				sw = File.CreateText("./config/datalib.glconfig");
				for(int i = 0; i < datas.Count; i++){
					if(datas[i] != null && datas[i] != "" && datas[i] != " " && datas[i] != string.Empty){
						sw.Write(datas[i] + Environment.NewLine);
					}
				}
				try{sw.Close();}catch{}
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
				int nt;
				int st;
				int o;
				int hp;
				int mp;
				if(int.TryParse(nowTime[num], out nt) && int.TryParse(saveTime[num], out st) && int.TryParse(one, out o) && int.TryParse(h, out hp) && int.TryParse(m, out mp)){
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
					return (h + ";" + m + ";" + s);
				}
			}
			return string.Empty;
		}
		void ToolStripMenuItem1Click(object sender, EventArgs e)
		{
			new Karta_Hry(current_item_name).Show();
		}
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			LauncherContent();
		}
		public void LauncherContent(int id = -1){
			if(id == -1){
				foreach(Control a in flowLayoutPanel1.Controls){
					int sets = int.Parse(a.Tag.ToString());
					if(datas.Count > sets){
						if(datas[sets] != null && datas[sets] != "" && datas[sets] != " " && datas[sets] != string.Empty){
							int num = 0;
							foreach(string s in datas[sets].Split(sep, StringSplitOptions.RemoveEmptyEntries)){num++;}
							if(num == 2){
								datas[sets] += "|" + a.Name + "|";
							}else if(num == 1){
								datas[sets] += "|" +"00;00;00" + "||" + a.Name + "|";
							}else if(num <= 0){
								datas[sets] = "|" + a.GetChildAtPoint(new Point(0,0)).Text + "||" + "00;00;00" + "||" + a.Name + "|";
							}
						}else{
							datas[sets] = "|" + a.GetChildAtPoint(new Point(0,0)).Text + "||" + "00;00;00" + "||" + a.Name+ "|";
						}
					}else{
						for(int i = datas.Count; i <= sets; i++){
							if(datas.Count <= i){
								if(i == sets){
									datas.Add("|" + a.GetChildAtPoint(new Point(0,0)).Text + "||" + "00;00;00" + "||" + a.Name + "|");
								}else{
									datas.Add("");
								}
							}else{
								if(i == sets){
									datas[i] = "|" + a.GetChildAtPoint(new Point(0,0)).Text + "||" + "00;00;00" + "||" + a.Name+ "|";
								}else{
									datas[i] = "";
								}
							}
						}
					}
				}
				SaveData();
			}else{
				if(id < flowLayoutPanel1.Controls.Count & id >= 0){
					Control a = flowLayoutPanel1.Controls[id];
					if(datas.Count > id){
						if(datas[id] != null && datas[id] != "" && datas[id] != " " && datas[id] != string.Empty){
							int num = 0;
							foreach(string s in datas[id].Split(sep, StringSplitOptions.RemoveEmptyEntries)){num++;}
							if(num == 2){
								datas[id] += "|" + a.Name + "|";
							}else if(num == 1){
								datas[id] += "|" +"00;00;00" + "||" + a.Name + "|";
							}else if(num <= 0){
								datas[id] = "|" + a.GetChildAtPoint(new Point(0,0)).Text + "||" + "00;00;00" + "||" + a.Name + "|";
							}
						}else{
							datas[id] = "|" + a.GetChildAtPoint(new Point(0,0)).Text + "||" + "00;00;00" + "||" + a.Name+ "|";
						}
					}else{
						for(int i = datas.Count; i <= id; i++){
							if(datas.Count <= i){
								if(i == id){
									datas.Add("|" + a.GetChildAtPoint(new Point(0,0)).Text + "||" + "00;00;00" + "||" + a.Name + "|");
								}else{
									datas.Add("");
								}
							}else{
								if(i == id){
									datas[i] = "|" + a.GetChildAtPoint(new Point(0,0)).Text + "||" + "00;00;00" + "||" + a.Name+ "|";
								}else{
									datas[i] = "";
								}
							}
						}
					}
					SaveData();
				}
			}
		}
		void FlowLayoutPanel1Paint(object sender, PaintEventArgs e)
		{
	
		}
		
		string NameCleaner(string name){
			string[] notText = {".exe","launcher","Launcher"};
			string[] mezText = {"_","-",","};
			string[,] spojText = {{"(","{","["},{")","}","]"}};
			int pos;
			int pos2;
			int num = 0;
			foreach(string localText in notText){
				if(name.Contains(localText)){
					name = name.Replace(localText, string.Empty);
				}
			}
			foreach(string localText in mezText){
				if(name.Contains(localText)){
					name = name.Replace(localText, " ");
				}
			}
			foreach(string g in spojText){
				if((pos = name.IndexOf(g)) != -1){
					if((pos2 = name.IndexOf(spojText[1,num], pos)) != -1){
						name = name.Remove(pos - 1, pos2);
					}
				}
				num++;
			}
			System.Text.StringBuilder newText = new System.Text.StringBuilder(name.Length * 2);
			newText.Append(name[0]);
			for (int i = 1; i < name.Length; i++)
        	{
				if (char.IsUpper(name[i])){
					if (name[i - 1] != ' ' && !char.IsUpper(name[i - 1]) && name[i - 1] != '.'){
                    	newText.Append(' ');
					}
				}
            	newText.Append(name[i]);
        	}
			return newText.ToString();
		}
		void Label2Click(object sender, EventArgs e)
		{
	
		}
		public void GameReload(){
			if(flowLayoutPanel1.Controls.Count > 0){ flowLayoutPanel1.Controls.Clear(); }
			for(int g = 0; g < datas.Count; g++){
				string a = "";
				int num = 0;
				if(datas[g] != null){
					foreach(string d in datas[g].Split(sep, StringSplitOptions.RemoveEmptyEntries)){
						if(num == 2){
							a = d;
						}
						num ++;
					}
				}
				if(a != "" && a != " " && a != null && a != string.Empty){
					if(File.Exists(a)){
						additem(a, g);
					}
				}
			}
		}
		void ToolStripMenuItem2Click(object sender, EventArgs e)
		{
			int id;
			if(int.TryParse(current_item.Tag.ToString(), out id)){
				if(new NameField(current_item_name).ShowDialog() == DialogResult.OK){
					if(flowLayoutPanel1.Controls[id].GetChildAtPoint(new Point(0,0)).Text.Contains(current_item_name)){
						flowLayoutPanel1.Controls[id].GetChildAtPoint(new Point(0,0)).Text = NameField.name;
						if(id < datas.Count && id != -1){
							int pos = datas[id].IndexOf(current_item_name);
							datas[id] = datas[id].Substring(0, pos).Replace(current_item_name, NameField.name);
							LauncherContent();
							GameReload();
						}
					}
				}
			}
		}
		void TreeView1AfterSelect(object sender, TreeViewEventArgs e)
		{
	
		}
	}
}

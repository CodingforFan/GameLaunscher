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
using System.Text;


namespace GameBox_v2
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{		
		static string current_item_name = null;
		public string processName;
		public string listing = "";
		public string[] sep = {"|"};
		public List <string> datas = new List<string>();
		public List <string> gameWebSaveData = new List <string>();
		public List <string> saveTime;
		public int treeNodeClass = -1;
		public int activeGame = -1;
		public int nodeId;
		public bool gameIsRun = false;
		public bool tryAgain = false;
		static TreeNode current_item = null;
		public StreamReader sr;
		public StreamWriter sw;
		public Process process;
		public WebBrowser wb1 = new WebBrowser();
		
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
			if(treeView1.Nodes.Count <= 0){
				treeView1.Nodes.Add("Hry");
				treeNodeClass = 0;
			}
			if(File.Exists("./config/datalib.glconfig")){
				int contentLength = File.ReadAllText("./config/datalib.glconfig").Length;
				if(contentLength > 0){
					foreach(string line in File.ReadAllLines("./config/datalib.glconfig")){
						datas.Add(line);
					}
					for(int i = 0; i < treeView1.Nodes.Count; i++){
						GameReload(i);
						LauncherContent(i);
					}
					AddAllGameAsPictureBox();
				}
			}else{
				if(!Directory.Exists("./config")){
					Directory.CreateDirectory("./config");
				}
				File.CreateText("./config/datalib.glconfig").Close();
				datas = new List<string>();
			}
		}
		
		void OpenFileDialog1FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try{
				foreach(string a in openFileDialog1.FileNames)
				{
					foreach(TreeNode c1 in treeView1.Nodes){
						foreach(TreeNode c2 in c1.Nodes){
							if (c2.Name.Contains(a)){
								break;
							}
						}
					}
					additem(a);
				}
				for(int i = 0; i < treeView1.Nodes.Count; i++){
					for(int g = 0; g < treeView1.Nodes[i].Nodes.Count; g++){
						gameWebDataSave(treeView1.Nodes[i].Nodes[g]);
						LauncherContent(i);
					}
				}
				AddAllGameAsPictureBox();
			}catch{
				MessageBox.Show("Toto není hra!");
			}
		}
		void additem (string a, int ids = -1){
			if(treeView1.Nodes.Count <= 0){
				treeView1.Nodes.Add("Hry");
				treeNodeClass = 0;
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
				if (ids != -1)
				if (datas.Count > ids) {
					name = datas[ids].Split(sep, StringSplitOptions.RemoveEmptyEntries)[0];
				}
				treeView1.Nodes[treeNodeClass].Nodes.Add(name);
				TreeNode treeNode = treeView1.Nodes[treeNodeClass].Nodes[treeView1.Nodes[treeNodeClass].Nodes.Count - 1];
				if (!File.Exists("./coverlib/" + name + ".png" )){
					Icon.ExtractAssociatedIcon(a).ToBitmap().Save("./coverlib/" + name + ".png", System.Drawing.Imaging.ImageFormat.Png);
				}
				treeNode.Name = a;
				treeNode.Tag = treeView1.Nodes[treeNodeClass].Nodes.Count - 1;			
			}
			goto mustek;
			pidano: MessageBox.Show("Položka: " + NameCleaner(Path.GetFileName(a)) + " je již přidána!");
			mustek:;
		}
		Control GetControlUnderMouse() {
    		foreach ( Control c in treeView1.Nodes[treeNodeClass].Nodes ) {
        		if ( c.Bounds.Contains(treeView1.PointToClient(MousePosition)) ) {
					return c;
					
         		}
    		}
			return null;
		}
		void Item_Click(object sender, TreeNodeMouseClickEventArgs e)
        {
			current_item = e.Node;
			current_item_name = current_item.Text;
			for(int i = 0; i < treeView1.Nodes.Count; i++){
				if(treeView1.Nodes[i] == e.Node){
					treeNodeClass = i;
				}
			}
			if(e.Node.Parent != null && e.Node.Name != e.Node.Parent.Name){
				
				for(int i = 0; i < treeView1.Nodes.Count; i++){
					if(treeView1.Nodes[i] == e.Node.Parent){
						treeNodeClass = i;
					}
				}	
				
				if (e.Button == MouseButtons.Left){
						int locInt;
						if(int.TryParse(current_item.Tag.ToString(), out locInt)){
							try{
								ButtonSet(Process.Start(current_item.Name), locInt);
							}catch{
							}
						}
				}else if (e.Button == MouseButtons.Right){
					contextMenuStrip1.Show(MousePosition);
				}
			}
		}
		
		void gamePlay(object sender, MouseEventArgs e)
        {
			if (e.Button == MouseButtons.Left){
					int locInt;
					if(int.TryParse(current_item.Tag.ToString(), out locInt)){
						try{
							ButtonSet(Process.Start(current_item.Name), locInt);
						}catch{
						}
					}
			}
		}
		
		void pictureBoxClick(object sender, MouseEventArgs e, PictureBox gamePictureBox)
        {
			if (e.Button == MouseButtons.Left){
					int locInt;
					if(int.TryParse(gamePictureBox.Tag.ToString(), out locInt)){
						try{
							ButtonSet(Process.Start(gamePictureBox.Name), locInt);
						}catch{}
					}
			}
		}
		void UpdateTick(object sender, EventArgs e)
		{
			//CreateGraphics().DrawRectangle(new Pen(Color.Red, 1), new Rectangle(0, 0+30, Width-1, Height-31));
			Text = DateTime.Now.ToString("HH:mm");
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
			try{
				string localfilenames = openFileDialog2.FileName;
				if(File.Exists(localfilenames)){
					File.Copy(localfilenames, "./coverlib/" + current_item_name + ".png");
	    		}	
			}catch{
				MessageBox.Show("Změna obrázku zatím nění funkční!");
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
				sw = new StreamWriter("./config/datalib.glconfig", false);
				for(int i = 0; i < datas.Count; i++){
					if(datas[i] != null && datas[i] != "" && datas[i] != " " && datas[i] != string.Empty){
						sw.Write(datas[i] + Environment.NewLine);
					}
				}
				sw.Close();
			}else{
				sw = File.CreateText("./config/datalib.glconfig");
				for(int i = 0; i < datas.Count; i++){
					if(datas[i] != null && datas[i] != "" && datas[i] != " " && datas[i] != string.Empty){
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
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			for(int i = 0; i < treeView1.Nodes.Count; i++){
				LauncherContent(i);
			}
		}
		public void LauncherContent(int id){
			if(id < treeView1.Nodes.Count && id >= 0){
				foreach(TreeNode a in treeView1.Nodes[id].Nodes){
					nodeId = int.Parse(a.Tag.ToString());
					if(datas.Count > nodeId){
						if(datas[nodeId] != null && datas[nodeId] != "" && datas[nodeId] != " " && datas[nodeId] != string.Empty){
							int num = 0;
							foreach(string s in datas[nodeId].Split(sep, StringSplitOptions.RemoveEmptyEntries)){num++;}
							if(num == 2){
								datas[nodeId] += "|" + a.Name + "|";
							}else if(num == 1){
								datas[nodeId] += "|" +"00;00;00" + "||" + a.Name + "|";
							}else if(num <= 0){
								datas[nodeId] = "|" + a.Text + "||" + "00;00;00" + "||" + a.Name + "|";
							}
						}else{
							datas[nodeId] = "|" + a.Text + "||" + "00;00;00" + "||" + a.Name+ "|";
						}
					}else{
						for(int i = datas.Count; datas.Count <= nodeId; i++){
							if(datas.Count <= i){
								if(i == nodeId){
									datas.Add("|" + a.Text + "||" + "00;00;00" + "||" + a.Name + "|");
								}else{
									datas.Add("");
								}
							}
						}
					}
				}
				SaveData();
			}

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
			AddAllGameAsPictureBox();
			treeView1.SelectedNode = null;
		}
		
		public void AddAllGameAsPictureBox(){
			foreach(Control con in tableLayoutPanel1.Controls){
				if(con.Tag == "GameCard"){
					tableLayoutPanel1.Controls.Remove(con);
				}
			}
			FlowLayoutPanel panelAllGames = new FlowLayoutPanel{Dock = DockStyle.Fill, Tag = "GameCard", AutoScroll = true};
			for(int i = 0; i < datas.Count; i++){
				var data = datas[i].Split(sep, StringSplitOptions.RemoveEmptyEntries);
				PictureBox gamePictureBox = new PictureBox{Size = new Size(170,230), BackgroundImageLayout = ImageLayout.Zoom, Name = (data.Length > 2 ? data[2] : "NoName"), Tag = i};
				if (File.Exists("./coverlib/" + data[0] + ".png" )){
					gamePictureBox.BackgroundImage = Image.FromFile("./coverlib/" + data[0] + ".png" );
				}
				gamePictureBox.MouseClick += (se, e) => {pictureBoxClick(se, e, gamePictureBox);};
				Label gameLabel = new Label{AutoSize = true, TextAlign = ContentAlignment.MiddleCenter, Parent = gamePictureBox, Text = data[0], Dock = DockStyle.Top, BackColor = Color.Transparent};
				panelAllGames.Controls.Add(gamePictureBox);
			}
			tableLayoutPanel1.Controls.Add(panelAllGames);
			tableLayoutPanel1.SetColumn(panelAllGames as Control, 1);
			tableLayoutPanel1.SetRow(panelAllGames as Control, 0);
		}
		
		public void GameReload(int id)
		{
			treeNodeClass = id;
			if(treeView1.Nodes.Count > 0 && treeView1.Nodes[id].Nodes.Count > 0){ treeView1.Nodes[id].Nodes.Clear(); }
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
			int id = int.Parse(current_item.Tag.ToString());
			if(new NameField(current_item_name).ShowDialog() == DialogResult.OK){
				if(id < datas.Count && id > -1){
					int pos = datas[id].Split(sep, StringSplitOptions.RemoveEmptyEntries)[0].Length + 2;
					if(File.Exists("./coverlib/" + current_item_name + ".png") && !File.Exists("./coverlib/" + NameField.name + ".png")){
						File.Copy("./coverlib/" + current_item_name + ".png", "./coverlib/" + NameField.name + ".png", true);
						while(File.Exists("./coverlib/" + current_item_name + ".png")){
							try{ 
								File.Delete("./coverlib/" + current_item_name + ".png"); 
							}catch{}
						}
					}
					if(File.Exists("./data/" + current_item_name + ".GameData") && !File.Exists("./data/" + NameField.name + ".GameData")){
						File.Copy("./data/" + current_item_name + ".GameData", "./data/" + NameField.name + ".GameData", true);
						while(File.Exists("./data/" + current_item_name + ".GameData")){
							try{ 
								File.Delete("./data/" + current_item_name + ".GameData"); 
							}catch{}
						}
					}
					datas[id] = datas[id].Substring(0, pos).Replace(current_item_name, NameField.name);
					current_item.Text = NameField.name;
					for(int i = 0; i < treeView1.Nodes.Count; i++){
						LauncherContent(i);
						for(int g = 0; g < treeView1.Nodes[i].Nodes.Count; g++){
							gameWebDataSave(treeView1.Nodes[i].Nodes[g]);
						}
						LauncherContent(i);
						GameReload(i);
					}
					AddAllGameAsPictureBox();
				}
			}
		}
		void Item_Select(object sender, TreeNodeMouseClickEventArgs e)
        {
			current_item = e.Node;
			current_item_name = current_item.Text;
			for(int i = 0; i < treeView1.Nodes.Count; i++){
				if(treeView1.Nodes[i] == e.Node){
					treeNodeClass = i;
				}
			}
			if(e.Node.Parent != null && e.Node.Name != e.Node.Parent.Name){
				
				for(int i = 0; i < treeView1.Nodes.Count; i++){
					if(treeView1.Nodes[i] == e.Node.Parent){
						treeNodeClass = i;
					}
				}	
				
				if (e.Button == MouseButtons.Left){
					foreach(Control control in tableLayoutPanel1.Controls){
						if(control.Tag == "GameCard"){
							tableLayoutPanel1.Controls.Remove(control);
						}
					}
					Panel panel01 = new Panel{Tag = "GameCard", Dock = DockStyle.Fill};
					PictureBox pictureBox01 = new PictureBox{Dock = DockStyle.Top, BackgroundImageLayout = ImageLayout.Zoom, Size = new Size(panel01.Width,  250), Location = new Point(0,0)};
					if (File.Exists("./coverlib/" + current_item.Text + "_background.png" )){
						pictureBox01.BackgroundImage = Image.FromFile("./coverlib/" + current_item.Text + "_background.png" );
					}
					pictureBox01.Parent = panel01;
					Button gamePlayButton = new Button{Location = new Point(25, pictureBox01.Height + 25), Size = new Size(75,25), Text = "Spustit"};
					gamePlayButton.MouseClick += new MouseEventHandler(gamePlay);
					gamePlayButton.Parent = panel01;
					int localNodeId;
					if(int.TryParse(e.Node.Tag.ToString(), out localNodeId)){
						nodeId = localNodeId;
					}
					Panel panel02 = new Panel{AutoScroll = true, Dock = DockStyle.Bottom};
					panel02.Parent = panel01;
					Label gameLabel = new Label{Dock = DockStyle.Fill, Text = (gameWebSaveData.Count > nodeId ? gameWebSaveData[nodeId] : ""), BackColor = Color.Transparent};
					gameLabel.Parent = panel02;
					tableLayoutPanel1.Controls.Add(panel01);
					tableLayoutPanel1.SetColumn(panel01 as Control,1);
					tableLayoutPanel1.SetRow(panel01 as Control,0);
					panel02.Size = new Size(panel01.Width, panel01.Height - gamePlayButton.Location.Y - 50);
					treeView1.SelectedNode = e.Node;
				}else if (e.Button == MouseButtons.Right){
					treeView1.SelectedNode = e.Node;
					contextMenuStrip1.Show(MousePosition);
				}
			}else{
				if (e.Button == MouseButtons.Right){
					contextMenuStrip2.Show(MousePosition);
				}
			}
		}
		void ToolStripMenuItem3Click(object sender, EventArgs e)
		{
			openFileDialog1.ShowDialog();
			
		}
		
		public bool saveWebData(string data, string name){
			if(Directory.Exists("./data/")){
				if(File.Exists("./data/"+ name +".GameData")){
					if(File.Exists("./data/"+ name +".GameData")){
						string loadData = File.ReadAllText("./data/"+ name +".GameData");
						if(loadData == data){
							return true;
						}
					}
				}
			}
			return false;
		}
		
		public void gameWebDataSave(TreeNode nodeControl)
		{
			if(nodeControl != null){
				string webText;
				string img_utl;
				nodeId = int.Parse(nodeControl.Tag.ToString());
				if(Directory.Exists("./data")){
					if(File.Exists("./data/"+ nodeControl.Text +".GameData")){
						tryAgain = true;
					}else{
						File.Create("./data/"+ nodeControl.Text +".GameData").Close();
						tryAgain = true;
					}
				}else{
					Directory.CreateDirectory("./data");
					File.Create("./data/"+ nodeControl.Text +".GameData").Close();
					tryAgain = true;
				}
				while(tryAgain){
					try{
						using(System.Net.WebClient client = new System.Net.WebClient()){
							client.Encoding = Encoding.UTF8;
							client.Encoding = ASCIIEncoding.UTF8;
							string htmlcode = client.DownloadString("https://www.databaze-her.cz/hry/" + nodeControl.Text.Replace(" ","-") + "/");
							webText = htmlcode;
						}
						if(webText != ""){
							if(webText.IndexOf("<h1>Stránka nenalezena</h1>") == -1 && webText != null && webText != "" && webText != " " && webText != string.Empty){
								int posS = webText.IndexOf("<img src=\"/obrazky/hry_krabice/");
								string myCapturedText = webText.Substring(posS, webText.IndexOf('>',posS) - posS + 1).Remove(0, 10);
								int pos = myCapturedText.IndexOf("?_");
								img_utl = "https://www.databaze-her.cz" + myCapturedText.Remove(pos, (myCapturedText.Length - pos));
								//pictureBox1.Load(img_utl);
								if(!Directory.Exists("./coverlib")){
									Directory.CreateDirectory("./coverlib");
								}
								if(!File.Exists("./coverlib/" + nodeControl.Text + "_background.png")){
									using(System.Net.WebClient client2 = new System.Net.WebClient())
									{
						   				client2.DownloadFile(img_utl, "./coverlib/" + nodeControl.Text + "_background.png");
									}		
								}
								posS = webText.IndexOf("<div id=\"game-description\"");
								myCapturedText = webText.Substring(posS, webText.IndexOf("</div>",posS) - posS).Remove(0, 10);
								pos = myCapturedText.IndexOf('>', 0);
								while(gameWebSaveData.Count <= nodeId){
									gameWebSaveData.Add("");
								}
								gameWebSaveData[nodeId] = myCapturedText.Remove(0, pos + 1);
								for(;gameWebSaveData[nodeId].Contains("<") && gameWebSaveData[nodeId].Contains(">");){
									int pos2 = gameWebSaveData[nodeId].IndexOf('<');
									int pos3 = gameWebSaveData[nodeId].IndexOf('>')  + 1 - pos2;
									gameWebSaveData[nodeId] = gameWebSaveData[nodeId].Remove(pos2, pos3);
								}
								if(!saveWebData(gameWebSaveData[nodeId], nodeControl.Text)){
									try{sw.Close();}catch{}
									try{sr.Close();}catch{}
									if(File.Exists("./data/"+ nodeControl.Text +".GameData")){
										sw = new StreamWriter("./data/"+ nodeControl.Text +".GameData", false);
										sw.Write(gameWebSaveData[nodeId]);
										sw.Close();
									}
								}
							}else{
								throw new Exception("");
							}
						}else{
							throw new Exception("");
						}
						tryAgain = false;
					}catch{
						NameField nameField = new NameField(nodeControl.Text);
						if(nameField.ShowDialog() == DialogResult.OK){
							for(int g = 0; g < datas.Count; g++){
								int nameLenght = datas[g].Split(sep, StringSplitOptions.RemoveEmptyEntries)[0].Length + 2;
								if(File.Exists("./coverlib/" + nodeControl.Text + ".png") && !File.Exists("./coverlib/" + NameField.name + ".png")){
									File.Copy("./coverlib/" + nodeControl.Text + ".png", "./coverlib/" + NameField.name + ".png", true);
									while(File.Exists("./coverlib/" + nodeControl.Text + ".png")){
										File.Delete("./coverlib/" + nodeControl.Text + ".png");
									}
								}
								if(File.Exists("./data/" + nodeControl.Text + ".GameData") && !File.Exists("./data/" + NameField.name + ".GameData")){
									File.Copy("./data/" + nodeControl.Text + ".GameData", "./data/" + NameField.name + ".GameData", true);
									while(File.Exists("./data/" + nodeControl.Text + ".GameData")){
										File.Delete("./data/" + nodeControl.Text + ".GameData");
									}
								}
								datas[g] = datas[g].Substring(0, nameLenght).Replace(nodeControl.Text, NameField.name);
							}
							nodeControl.Text = NameField.name;
						}else if(nameField.ShowDialog() == DialogResult.Cancel){
							tryAgain = false;
						}
					}
				}
			}
		}
	}
}



		
		
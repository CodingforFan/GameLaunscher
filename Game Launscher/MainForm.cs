/*
 * Vytvořeno aplikací SharpDevelop.
 * Uživatel: asus
 * Datum: 7. 12. 2016
 * Čas: 14:59
 * 
 * Tento template můžete změnit pomocí Nástroje | Možnosti | Psaní kódu | Upravit standardní hlavičky souborů.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using SupportLibrary;
using System.Collections.Generic;

namespace Game_Launscher
{

	public partial class MainForm : Form // nasrat já nic srát nebudu
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

			InitializeComponent();

		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			Support.Log("Start");
			if(System.IO.File.Exists("./Data.glconfig")){
				sr = new System.IO.StreamReader("./Data.glconfig");
				if(sr.ReadToEnd().Length > 0){
					var poc = System.IO.File.ReadAllLines("./Data.glconfig").Length;
					for(int i = 0; i < poc; i++){
						if(datas.Count > i){
							datas[i] = sr.ReadLine();
						}else{
							datas.Add(sr.ReadLine());
						}
					}
				}
				sr.Close();
			}else{
				System.IO.File.CreateText("./Data.glconfig").Close();
			}
		}
		
		void Button1Click(object sender, EventArgs e)
		{

			openFileDialog1.ShowDialog();
			Support.Log("Open File Dialog 1");
			
		}
		void OpenFileDialog1FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			

					
		}
		
		void additem (string[] files){
			foreach(string a in files)
			{
				foreach ( Control c1 in flowLayoutPanel1.Controls ) {
					if (c1.Name.Contains(a)){
						goto pidano;
					}
				}
				
			if (a.Contains(".exe")){
			
				PictureBox pb = new PictureBox {Name = a,Size = new Size(300,150),BackgroundImageLayout = ImageLayout.Zoom, BackgroundImage = Icon.ExtractAssociatedIcon(a).ToBitmap()};
				pb.Tag = flowLayoutPanel1.Controls.Count.ToString();
				pb.MouseClick += Item_Click;
					
				Label lb = new Label {Name = a + "1", Text = Path.GetFileName(a).Replace(".exe", string.Empty)};
				lb.MouseClick += Item_Click;
				lb.Font = new Font(lb.Font.Name, 24,FontStyle.Bold);
				lb.AutoSize = true;
				lb.BackColor = Color.Transparent;
				pb.Controls.Add(lb);
				
				flowLayoutPanel1.Controls.Add(pb);
					
			}
					goto mustek;
					pidano: MessageBox.Show("Položka: <b>" + Path.GetFileName(a).Replace(".exe", string.Empty) + "<b> je již přidána!");
					mustek:;
			}
		}
		
		Control GetControlUnderMouse() {
    		foreach ( Control c in this.flowLayoutPanel1.Controls ) {
        		if ( c.Bounds.Contains(flowLayoutPanel1.PointToClient(MousePosition)) ) {
					return c;
					
         		}
    		}
			return null;
		}
		
		void Item_Click(object sender, MouseEventArgs e)
        {
			current_item = GetControlUnderMouse();
			current_item_name = Path.GetFileName(current_item.Name);
			
			if (e.Button == MouseButtons.Left){
				ButtonSet(System.Diagnostics.Process.Start(current_item.Name), int.Parse(current_item.Tag.ToString()));
			}else if (e.Button == MouseButtons.Right){
				contextMenuStrip1.Show( MousePosition); 
			}
		}
		void UpdateTick(object sender, EventArgs e)
		{
			//CreateGraphics().DrawRectangle(new Pen(Color.Red, 1), new Rectangle(0, 0+30, Width-1, Height-31));
			label2.Text = DateTime.Now.ToString("HH:mm");
		}
		void Button4Click(object sender, EventArgs e)
		{
			LauncherContent();
			Close();
		}
		void Button3Click(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Normal){
				this.WindowState = FormWindowState.Maximized;
				button3.BackgroundImage = Resource1.UI_4;
			}else if (this.WindowState == FormWindowState.Maximized) {
				this.WindowState = FormWindowState.Normal;
				button3.BackgroundImage = Resource1.UI_5;
			}
		}
		void Button2Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}
		void MenuToolStripMenuItemClick(object sender, EventArgs e)
		{
		}
		void OpenFileDialog2FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			string localfilenames = openFileDialog2.FileName.ToString();
			if(File.Exists(localfilenames)){
				Directory.CreateDirectory("./coverlib");
				MessageBox.Show(current_item_name + current_item.ToString());
				File.Copy(openFileDialog2.FileName, "./coverlib/" + current_item_name.Replace(".exe", string.Empty) + "." + localfilenames.Split('.')[1], true);

				current_item.BackgroundImage = Image.FromFile("./coverlib/"+ current_item_name.Replace(".exe", string.Empty) +"." + localfilenames.Split('.')[1]);

	
				
    		}	
		}
		void LauncherContent(){
			foreach(Control a in flowLayoutPanel1.Controls){
				var set = int.Parse(a.Tag.ToString());
				if(datas.Count > set){
					if(datas[set] != null){
						if(datas[set].Split('|').Length == 2){
							datas[set] += "|" + a.Name;
						}else if(datas[set].Split('|').Length == 1){
							datas[set] +="00;00;00" + "|" + a.Name;
						}else{
							datas[set] = Path.GetFileName(a.Name).Replace(".exe", string.Empty) + "|" + "00;00;00" + "|" + a.Name;
						}
					}else{
						datas[set] = Path.GetFileName(a.Name).Replace(".exe", string.Empty) + "|" + "00;00;00" + "|" + a.Name;
					}
				}else{
					for(int i = 0; i <= set; i++){
						if(i == set){
							datas.Add(Path.GetFileName(a.Name).Replace(".exe", string.Empty) + "|" + "00;00;00" + "|" + a.Name);
						}else{
							datas.Add("");
						}
					}
				}
			}
			SaveData();
		}
		
		void ToolStripMenuItem1Click(object sender, EventArgs e)
		{
			openFileDialog2.ShowDialog();
		}
		
		void Button5MouseDown(object sender, EventArgs e)
		{
			//resize.Start();
		}
		
		void Button5MouseUp(object sender, EventArgs e)
		{
			resize.Stop();
			Size = new Size (MainForm.MousePosition.X - Location.X , MainForm.MousePosition.Y - Location.Y);
			
		}
		
		void ResizeTick(object sender, EventArgs e)
		{
			Size = new Size (MainForm.MousePosition.X - Location.X , MainForm.MousePosition.Y - Location.Y);
		}

		void MainForm_Active(object sender, EventArgs e){
			if(gameIsRun){
				if(process.HasExited){
					gameIsRun = false;
					if(datas.Count > activeGame){
						if(datas[activeGame] != null){
							var t = datas[activeGame].Split('|')[1];
							datas[activeGame].Replace("|" + t, "|" + HowLongPlay(t));
						}else{
							datas[activeGame] = processName.Replace(".exe", string.Empty) + "|" + HowLongPlay("00;00;00");
						}
					}else{
						for(int i = 0; i <= activeGame; i++){
							if(i == activeGame){
								datas.Add(processName.Replace(".exe", string.Empty) + "|" + HowLongPlay("00;00;00"));
							}else{
								datas.Add("");
							}
						}
					}
					activeGame = -1;
					SaveData();
				}
			}
		}
		
		public void SaveData(){
			if(System.IO.File.Exists("./Data.glconfig")){
				sw = new System.IO.StreamWriter("./Data.glconfig", false);
				for(int i = 0; i < datas.Count; i++){
					sw.Write(datas[i] + Environment.NewLine);
				}
				sw.Close();
			}else{
				sw = System.IO.File.CreateText("./Data.glconfig");
				for(int i = 0; i < datas.Count; i++){
					sw.Write(datas[i] + Environment.NewLine);
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
	}
}

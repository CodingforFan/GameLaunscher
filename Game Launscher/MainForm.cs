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
//kaďák

namespace Game_Launscher
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		static string current_item_name = null;
		static Control current_item = null;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			Support.Log("Start");
		}
		
		void Button1Click(object sender, EventArgs e)
		{

			openFileDialog1.ShowDialog();
			Support.Log("Open File Dialog 1");
			
		}
		void OpenFileDialog1FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			foreach(string a in openFileDialog1.FileNames)
			{
				foreach ( Control c1 in flowLayoutPanel1.Controls ) {
					if (c1.Name.Contains(a)){
						goto pidano;
					}
				}
				
				if (a.Contains(".exe")){

					PictureBox pb = new PictureBox {Name = a,Size = new Size(300,150),BackgroundImageLayout = ImageLayout.Zoom, BackgroundImage = Icon.ExtractAssociatedIcon(a).ToBitmap()};
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
		
		void ListView1SelectedIndexChanged(object sender, EventArgs e)
		{
	
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
				System.Diagnostics.Process.Start(current_item.Name);	
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
		
		void ToolStripMenuItem1Click(object sender, EventArgs e)
		{
			openFileDialog2.ShowDialog();
		}

	}
}

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

namespace GameBox_v2._
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
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
}

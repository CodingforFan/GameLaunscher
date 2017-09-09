/*
 * Created by SharpDevelop.
 * User: Asus
 * Date: 13. 3. 2017
 * Time: 19:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace GameBox_v2
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		public System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Timer update;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem změnitObrázekToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openFileDialog2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.TextBox textBox1;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node8");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node9");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node10");
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node11");
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
			treeNode1,
			treeNode2,
			treeNode3,
			treeNode4});
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Node12");
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Node13");
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Node14");
			System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Node1", new System.Windows.Forms.TreeNode[] {
			treeNode6,
			treeNode7,
			treeNode8});
			System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Node15");
			System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Node16");
			System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Node17");
			System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Node2", new System.Windows.Forms.TreeNode[] {
			treeNode10,
			treeNode11,
			treeNode12});
			System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Node18");
			System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Node19");
			System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Node20");
			System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Node3", new System.Windows.Forms.TreeNode[] {
			treeNode14,
			treeNode15,
			treeNode16});
			System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Node21");
			System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Node22");
			System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Node23");
			System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Node4", new System.Windows.Forms.TreeNode[] {
			treeNode18,
			treeNode19,
			treeNode20});
			System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Node5");
			System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Node6");
			System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Node7");
			this.button1 = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.update = new System.Windows.Forms.Timer(this.components);
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.změnitObrázekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.flowLayoutPanel1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(3, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(61, 20);
			this.button1.TabIndex = 0;
			this.button1.Text = "Přidat";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			this.openFileDialog1.Multiselect = true;
			this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDialog1FileOk);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.AutoScroll = true;
			this.flowLayoutPanel1.Controls.Add(this.button1);
			this.flowLayoutPanel1.Controls.Add(this.label2);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(165, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(642, 359);
			this.flowLayoutPanel1.TabIndex = 0;
			this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.FlowLayoutPanel1Paint);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(70, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "label2";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label2.Click += new System.EventHandler(this.Label2Click);
			// 
			// update
			// 
			this.update.Enabled = true;
			this.update.Interval = 300;
			this.update.Tick += new System.EventHandler(this.UpdateTick);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.změnitObrázekToolStripMenuItem,
			this.toolStripMenuItem1,
			this.toolStripMenuItem2});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(157, 70);
			// 
			// změnitObrázekToolStripMenuItem
			// 
			this.změnitObrázekToolStripMenuItem.Name = "změnitObrázekToolStripMenuItem";
			this.změnitObrázekToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
			this.změnitObrázekToolStripMenuItem.Text = "Změnit obrázek";
			this.změnitObrázekToolStripMenuItem.Click += new System.EventHandler(this.ZměnitObrázekToolStripMenuItemClick);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(156, 22);
			this.toolStripMenuItem1.Text = "Informace";
			this.toolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(156, 22);
			this.toolStripMenuItem2.Text = "Přejmenovat";
			this.toolStripMenuItem2.Click += new System.EventHandler(this.ToolStripMenuItem2Click);
			// 
			// openFileDialog2
			// 
			this.openFileDialog2.FileName = "openFileDialog2";
			this.openFileDialog2.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDialog2FileOk);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(810, 365);
			this.tableLayoutPanel1.TabIndex = 3;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.treeView1);
			this.panel1.Controls.Add(this.textBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(156, 359);
			this.panel1.TabIndex = 3;
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.Location = new System.Drawing.Point(0, 20);
			this.treeView1.Name = "treeView1";
			treeNode1.Name = "Node8";
			treeNode1.Text = "Node8";
			treeNode2.Name = "Node9";
			treeNode2.Text = "Node9";
			treeNode3.Name = "Node10";
			treeNode3.Text = "Node10";
			treeNode4.Name = "Node11";
			treeNode4.Text = "Node11";
			treeNode5.Name = "Node0";
			treeNode5.Text = "Node0";
			treeNode6.Name = "Node12";
			treeNode6.Text = "Node12";
			treeNode7.Name = "Node13";
			treeNode7.Text = "Node13";
			treeNode8.Name = "Node14";
			treeNode8.Text = "Node14";
			treeNode9.Name = "Node1";
			treeNode9.Text = "Node1";
			treeNode10.Name = "Node15";
			treeNode10.Text = "Node15";
			treeNode11.Name = "Node16";
			treeNode11.Text = "Node16";
			treeNode12.Name = "Node17";
			treeNode12.Text = "Node17";
			treeNode13.Name = "Node2";
			treeNode13.Text = "Node2";
			treeNode14.Name = "Node18";
			treeNode14.Text = "Node18";
			treeNode15.Name = "Node19";
			treeNode15.Text = "Node19";
			treeNode16.Name = "Node20";
			treeNode16.Text = "Node20";
			treeNode17.Name = "Node3";
			treeNode17.Text = "Node3";
			treeNode18.Name = "Node21";
			treeNode18.Text = "Node21";
			treeNode19.Name = "Node22";
			treeNode19.Text = "Node22";
			treeNode20.Name = "Node23";
			treeNode20.Text = "Node23";
			treeNode21.Name = "Node4";
			treeNode21.Text = "Node4";
			treeNode22.Name = "Node5";
			treeNode22.Text = "Node5";
			treeNode23.Name = "Node6";
			treeNode23.Text = "Node6";
			treeNode24.Name = "Node7";
			treeNode24.Text = "Node7";
			this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
			treeNode5,
			treeNode9,
			treeNode13,
			treeNode17,
			treeNode21,
			treeNode22,
			treeNode23,
			treeNode24});
			this.treeView1.Size = new System.Drawing.Size(156, 339);
			this.treeView1.TabIndex = 4;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1AfterSelect);
			// 
			// textBox1
			// 
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox1.Location = new System.Drawing.Point(0, 0);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(156, 20);
			this.textBox1.TabIndex = 1;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(810, 365);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "MainForm";
			this.Text = "GameBox v2.0";
			this.Activated += new System.EventHandler(this.MainFormActivated);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}
	}
}

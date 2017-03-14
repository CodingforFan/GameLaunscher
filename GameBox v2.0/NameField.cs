/*
 * Created by SharpDevelop.
 * User: Lenovo
 * Date: 14. 3. 2017
 * Time: 14:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameBox_v2
{
	/// <summary>
	/// Description of Form1.
	/// </summary>
	public partial class NameField : Form
	{
		public static string name="";
		public NameField(string names = "")
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			textBox1.Text = names;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void TextBox1TextChanged(object sender, EventArgs e)
		{
	
		}
		void Button1Click(object sender, EventArgs e)
		{
			name = textBox1.Text;
			Dispose();
		}
		void NameFieldLoad(object sender, EventArgs e)
		{
			TopMost = true;
			TopMost = false;
		}
	}
}

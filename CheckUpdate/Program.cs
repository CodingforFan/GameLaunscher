/*
 * Created by SharpDevelop.
 * User: Asus
 * Date: 16. 5. 2017
 * Time: 20:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace CheckUpdate
{
	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Update World!");
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
			Update.Start();
		
			
		}
	}
}
using System;
using System.Windows.Forms;

namespace CircleApp
{
	class Program {

		[STAThread]
		public static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainWindowForm());
		}

	}
}
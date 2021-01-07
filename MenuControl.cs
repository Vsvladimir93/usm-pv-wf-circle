using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace CircleApp 
{
	class MenuControl : MenuStrip
	{
		public ToolStripMenuItem file;
		public ToolStripMenuItem view;
		public ToolStripMenuItem window;
		public ToolStripMenuItem help;
		// File
		public ToolStripMenuItem newItem;
		public ToolStripMenuItem open;
		public ToolStripMenuItem save;
		public ToolStripMenuItem saveAs;
		public ToolStripMenuItem exit;
		// View
		public ToolStripMenuItem toolBar;
		public ToolStripMenuItem statusBar;
		// Window
		public ToolStripMenuItem cascade;
		public ToolStripMenuItem horizontal;
		public ToolStripMenuItem vertical;
		// Help
		public ToolStripMenuItem about;

		public Dictionary<string, ToolStripMenuItem> circleWindows = new Dictionary<string, ToolStripMenuItem>();

		public MenuControl()
		{
			file = new ToolStripMenuItem();
			file.Text = "File";
			view = new ToolStripMenuItem();
			view.Text = "View";
			window = new ToolStripMenuItem();
			window.Text = "Window";
			help = new ToolStripMenuItem();
			help.Text = "Help";
			// File
			newItem = new ToolStripMenuItem();
			newItem.Text = "New";
			open = new ToolStripMenuItem();
			open.Text = "Open";
			save = new ToolStripMenuItem();
			save.Text = "Save";
			saveAs = new ToolStripMenuItem();
			saveAs.Text = "Save as ...";
			exit = new ToolStripMenuItem();
			exit.Text = "Exit";
			
			file.DropDownItems.AddRange(
				new ToolStripMenuItem[] 
				{
					newItem,
					open,
					save,
					saveAs,
					exit
				}
			);

			// View
			toolBar = new ToolStripMenuItem();
			toolBar.Text = "Toolbar";
			statusBar = new ToolStripMenuItem();
			statusBar.Text = "Status bar";

			view.DropDownItems.AddRange(
				new ToolStripMenuItem[] 
				{
					toolBar,
					statusBar
				}
			);

			// Window
			cascade = new ToolStripMenuItem();
			horizontal = new ToolStripMenuItem();
			vertical = new ToolStripMenuItem();
			cascade.Text = "Cascade";
			horizontal.Text = "Horizontal";
			vertical.Text = "Vertical";

			window.DropDownItems.AddRange(
				new ToolStripMenuItem[] 
				{
					cascade,
					horizontal,
					vertical
				}
			);

			// Help
			about = new ToolStripMenuItem();
			about.Text = "About";

			help.DropDownItems.AddRange(
				new ToolStripMenuItem[] 
				{
					about
				}
			);

			this.Items.AddRange(
				new ToolStripMenuItem[] 
				{
					file,
					view,
					window,
					help
				}
			);

		}

	}
}
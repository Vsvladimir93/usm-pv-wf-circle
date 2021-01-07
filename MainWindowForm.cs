using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace CircleApp 
{
	class MainWindowForm : Form
	{
		private MenuControl menu;
		private ToolBarControl toolBar;
		private StatusStripControl statusStrip;
		private Dictionary<string, CircleWindow> circleWindows = new Dictionary<string, CircleWindow>();

		public delegate void OnWindowClickHandler(string circleId);

		public MainWindowForm()
		{
			IsMdiContainer = true;

			InitToolBarControl();
			InitMainWindow();
			InitMenuControl();
			InitStatusStripControl();

			new WindowService(this, menu, toolBar, statusStrip);
			
		}

		private void InitMainWindow()
		{
			Text = "Circle application";
			Size = new Size(1024, 800);
			StartPosition = FormStartPosition.CenterScreen;
		}

		private void InitMenuControl()
		{
			menu = new MenuControl();
			menu.Location = new Point(0, 0);
			menu.Size = new Size(812, 24);
			menu.toolBar.Checked = false;
			menu.statusBar.Checked = true;

			Controls.Add(menu);
		}

		private void InitToolBarControl()
		{
			toolBar = new ToolBarControl();
			toolBar.Dock = DockStyle.Top;
			toolBar.Visible = false;

			Controls.Add(toolBar);
		}

		private void InitStatusStripControl()
		{
			statusStrip = new StatusStripControl();
			statusStrip.Visible = true;
			statusStrip.Size = new System.Drawing.Size(Size.Width, 24);
			statusStrip.Location = new System.Drawing.Point(0, (Size.Height - statusStrip.Size.Height));

			Controls.Add(statusStrip);
		}

	}
}
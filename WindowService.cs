using System;
using System.Windows.Forms;
using System.Collections.Generic;
using CircleApp.ReadWrite;
using System.IO;

namespace CircleApp 
{
	sealed class WindowService
	{
		private MainWindowForm mainWindow;
		private MenuControl menu;
		private ToolBarControl toolBar;
		private StatusStripControl statusStrip;
		private Dictionary<string, CircleWindow> circleWindows;
		public delegate void Action(string circleId);
		public WindowService(
			MainWindowForm mainWindow,
			MenuControl menu,
			ToolBarControl toolBar,
			StatusStripControl statusStrip
		)
		{
			this.mainWindow = mainWindow;
			this.menu = menu;
			this.toolBar = toolBar;
			this.statusStrip = statusStrip;
			this.circleWindows = new Dictionary<string, CircleWindow>();
			
			InitMainWindowEvents();
			InitMenuEvents();
			InitToolBarEvents();
		}

		private void InitMainWindowEvents()
		{
			mainWindow.MdiChildActivate += (s, e) => {
				if (mainWindow.ActiveMdiChild != null) {
					statusStrip.selectedCircleText.Text = ((CircleWindow)mainWindow.ActiveMdiChild).Id;
					statusStrip.filePathText.Text = ((CircleWindow)mainWindow.ActiveMdiChild).path;	
				}
			};
		}

		private void InitMenuEvents()
		{
			// File
			menu.newItem.Click += (s, e) => OnNewClick();
			menu.open.Click += (s, e) => OnOpenClick();
			menu.save.Click += (s, e) => OnSaveClick();
			menu.saveAs.Click += (s, e) => OnSaveAsClick();
			menu.exit.Click += (s, e) => OnExitClick();
			// View
			menu.toolBar.Click += (s, e) => OnToolBarClick();
			menu.statusBar.Click += (s, e) => OnStatusBarClick();
			// Window
			menu.cascade.Click += (s, e) => OnCascadeClick();
			menu.horizontal.Click += (s, e) => OnHorizontalClick();
			menu.vertical.Click += (s, e) => OnVerticalClick();
			//Help
			menu.about.Click += (s, e) => OnAboutClick();
		}
		private void InitToolBarEvents()
		{
			toolBar.ButtonClick += (s, e) => {
				if (e.Button == toolBar.newButton)
					OnNewClick();
				if (e.Button == toolBar.openButton)
					OnOpenClick();
				if (e.Button == toolBar.saveButton)
					OnSaveClick();

				if (e.Button == toolBar.cascadeButton)
					OnCascadeClick();
				if (e.Button == toolBar.horizontalButton)
					OnHorizontalClick();
				if (e.Button == toolBar.verticalButton)
					OnVerticalClick();
				
				if (e.Button == toolBar.aboutButton)
					OnAboutClick();
				if (e.Button == toolBar.exitButton)
					OnExitClick();
			};
		}

		private void OnNewClick()
		{
			CircleWindow cw = CreateNewCircleWindow();
			AddMenuCircleWindowData(cw.Id, OnCircleWindowClick);
		}

		private CircleWindow CreateNewCircleWindow()
		{
			string circleId = string.Format("Circle_{0}", (circleWindows.Count + 1));
			CircleWindow cw = new CircleWindow(circleId);
			circleWindows.Add(circleId, cw);

			cw.MdiParent = mainWindow;
			cw.Show();

			cw.Closing += (sender, closingEvent) => {
				if (OnCircleWindowClose(cw.Id))
				{
					closingEvent.Cancel = true;
				}
			};

			return cw;
		}
		
		private CircleWindow OpenNewCircleWindow(string fileName, string circleId, Circle circle)
		{
			CircleWindow cw = new CircleWindow(circleId, circle, fileName, true);
			circleWindows.Add(circleId, cw);

			cw.MdiParent = mainWindow;
			cw.Show();

			cw.Closing += (sender, closingEvent) => {
				if (OnCircleWindowClose(cw.Id))
				{
					closingEvent.Cancel = true;
				}
			};

			return cw;
		}

		private bool CheckIfAlreadyOpenedWindow(string circleId)
		{
			CircleWindow cw;
			if (circleWindows.TryGetValue(circleId, out cw))
			{
				cw.Activate();
				return true;
			}
			return false;
		}

		private string FileNameToCircleId(string fileName)
		{
			int ext = fileName.LastIndexOf(".app");

			string result = fileName;
			if(ext != -1) {
				result = fileName.Remove(ext, 4);
			}
			int absPath = fileName.LastIndexOf("\\");
			if(absPath != -1) {
				result = result.Substring(absPath + 1);
			}
			
			return result;

		}

		private bool SaveRequest(string title, string message)
		{
			DialogResult d = MessageBox
					.Show(
						message, 
						title, 
						MessageBoxButtons.YesNo, 
						MessageBoxIcon.Warning
					);
			return d == DialogResult.Yes;
		}

		private bool OnCircleWindowClose(string circleId) 
		{
			bool cancelEvent = false;
			CircleWindow cw;
			if (circleWindows.TryGetValue(circleId, out cw))
			{
				if (cw.isSaved)
				{
					RemoveMenuCircleWindowData(circleId);
					circleWindows.Remove(circleId);
				} 
				else 
				{
					if (SaveRequest("Unsaved changes", "You have unsaved changes. Are you sure?")) 
					{
						RemoveMenuCircleWindowData(circleId);
						circleWindows.Remove(circleId);
					} 
					else
					{
						cancelEvent = true;
					}
				}
			}
			return cancelEvent;
		}

		private void OnCircleWindowClick(string circleId) 
		{
			CircleWindow child;	
			if (circleWindows.TryGetValue(circleId, out child)) {
				child.Activate();
			}
		}

		private void OnOpenClick()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();

			openFileDialog.InitialDirectory = "./";
			openFileDialog.Filter = "Circle app files (*.app)|*.app";
			openFileDialog.FilterIndex = 2;
			openFileDialog.RestoreDirectory = true;

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string circleId = FileNameToCircleId(openFileDialog.FileName);
				if (CheckIfAlreadyOpenedWindow(circleId)) {
					return;
				}

				Circle circle = ObjectReader.ReadFromFile<Circle>(openFileDialog.FileName);
				AddMenuCircleWindowData(OpenNewCircleWindow(openFileDialog.FileName, circleId, circle).Id, OnCircleWindowClick);
			}
		}

		private void OnSaveClick()
		{
			CircleWindow cw = ((CircleWindow)mainWindow.ActiveMdiChild);
			if (cw == null)
				return;
			Save(cw);
		}

		private void Save(CircleWindow cw, string fileName = null)
		{
			if (fileName == null)
				fileName = "./" + cw.Id + ".app";
			
			ObjectWriter.WriteToFile<Circle>(cw.circle, fileName);
			cw.isSaved = true;
			statusStrip.filePathText.Text = Directory.GetCurrentDirectory() + "\\" + fileName;
		}

		private void OnSaveAsClick()
		{
			CircleWindow cw = ((CircleWindow)mainWindow.ActiveMdiChild);
			if (cw == null)
				return;	

			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.FileName = cw.Id;
			saveFileDialog.DefaultExt = ".app";
			saveFileDialog.Filter = "Circle app files (.app)|*.app";

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{		
				Save(cw, saveFileDialog.FileName);
			}
		}
		private void OnExitClick()
		{
			CircleWindow[] windows = new CircleWindow[circleWindows.Count];
			circleWindows.Values.CopyTo(windows, 0);

			foreach(CircleWindow cw in windows)
			{
				cw.Close();
			}
			if (circleWindows.Count == 0)
			{
				Environment.Exit(0);
			}
		}

		private void OnToolBarClick()
		{
			toolBar.Visible = !toolBar.Visible;
			menu.toolBar.Checked = toolBar.Visible;
		}

		private void OnStatusBarClick()
		{
			statusStrip.Visible = !statusStrip.Visible;
			menu.statusBar.Checked = statusStrip.Visible;
		}
		private void OnCascadeClick()
		{
			mainWindow.LayoutMdi(MdiLayout.Cascade);
		}

		private void OnHorizontalClick()
		{
			mainWindow.LayoutMdi(MdiLayout.TileHorizontal);
		}

		private void OnVerticalClick()
		{
			mainWindow.LayoutMdi(MdiLayout.TileVertical);
		}

		private void OnAboutClick()
		{
			MessageBox.Show(
				"GUI example app\n\n" +
				"Created by: Vladimir Vranceanu MIA 2002\n\n" +
				"Programmare vizuala\nPereteatcu Sergiu, doctor, conferențiar universitar\n\n" +
				"USM 2021", 
				"Circle application"
				);
		}

		public void AddMenuCircleWindowData(string circleId, Action<string> handler)
		{
			ToolStripMenuItem newWindow = new ToolStripMenuItem();
			newWindow.Text = circleId;
			newWindow.Name = circleId;
			newWindow.Click += (s, e) => { handler(circleId); };
			if (menu.circleWindows.Count == 0) {
				ToolStripSeparator separator = new ToolStripSeparator();
				separator.Name = "separator";
				menu.window.DropDownItems.Add(separator);
			}
			menu.circleWindows.Add(circleId, newWindow);
			menu.window.DropDownItems.Add(newWindow);
		}

		public void RemoveMenuCircleWindowData(string circleId) 
		{
			menu.window.DropDownItems.RemoveAt(menu.window.DropDownItems.IndexOfKey(circleId));
			menu.circleWindows.Remove(circleId);
			if (menu.circleWindows.Count == 0) {
				menu.window.DropDownItems.RemoveAt(menu.window.DropDownItems.IndexOfKey("separator"));
				statusStrip.selectedCircleText.Text = "";
			}
		}


	}
}
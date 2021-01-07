using System;
using System.Windows.Forms;
using System.Drawing;

namespace CircleApp 
{
	class ToolBarControl : ToolBar
	{
		private ImageList imageList;
		public ToolBarButton newButton;
		public ToolBarButton openButton;
		public ToolBarButton saveButton;
		public ToolBarButton separator_1;
		public ToolBarButton cascadeButton;
		public ToolBarButton horizontalButton;
		public ToolBarButton verticalButton;
		public ToolBarButton separator_2;
		public ToolBarButton aboutButton;
		public ToolBarButton exitButton;
		public ToolBarControl()
		{
			InitToolBar();
			InitImageList();
			InitButtons();
		}

		private void InitToolBar()
		{
			Appearance = ToolBarAppearance.Flat;
			BorderStyle = BorderStyle.None;
			ShowToolTips = true;
			ButtonSize = new Size(24, 24);		
			Visible = false;
		}

		private void InitImageList()
		{
			imageList = new ImageList();
			imageList.ColorDepth=ColorDepth.Depth24Bit;
			imageList.ImageSize=new System.Drawing.Size(24,24);

			imageList.Images.Add(Image.FromFile(
										Application.StartupPath+"\\Images\\new.png"));
			imageList.Images.Add(Image.FromFile(
										Application.StartupPath+"\\Images\\open.png"));
			imageList.Images.Add(Image.FromFile(
										Application.StartupPath+"\\Images\\save.png"));
			imageList.Images.Add(Image.FromFile(
										Application.StartupPath+"\\Images\\cascade.png"));
			imageList.Images.Add(Image.FromFile(
										Application.StartupPath+"\\Images\\horizontal.png"));			
			imageList.Images.Add(Image.FromFile(
										Application.StartupPath+"\\Images\\vertical.png"));
			imageList.Images.Add(Image.FromFile(
										Application.StartupPath+"\\Images\\about.png"));
			imageList.Images.Add(Image.FromFile(
										Application.StartupPath+"\\Images\\exit.png"));

			ImageList=imageList;
		}

		private void InitButtons()
		{
			newButton = new ToolBarButton();
			newButton.ToolTipText = "New";
			newButton.Style = ToolBarButtonStyle.PushButton;
			newButton.ImageIndex = 0;

			openButton = new ToolBarButton();
			openButton.ToolTipText = "Open";
			openButton.Style = ToolBarButtonStyle.PushButton;
			openButton.ImageIndex = 1;

			saveButton = new ToolBarButton();
			saveButton.ToolTipText = "Save";
			saveButton.Style = ToolBarButtonStyle.PushButton;
			saveButton.ImageIndex = 2;

			separator_1 = new ToolBarButton();
			separator_1.Style = ToolBarButtonStyle.Separator;

			cascadeButton = new ToolBarButton();
			cascadeButton.ToolTipText = "Cascade";
			cascadeButton.Style = ToolBarButtonStyle.PushButton;
			cascadeButton.ImageIndex = 3;

			horizontalButton = new ToolBarButton();
			horizontalButton.ToolTipText = "Horizontal";
			horizontalButton.Style = ToolBarButtonStyle.PushButton;
			horizontalButton.ImageIndex = 4;

			verticalButton = new ToolBarButton();
			verticalButton.ToolTipText = "Vertical";
			verticalButton.Style = ToolBarButtonStyle.PushButton;
			verticalButton.ImageIndex = 5;

			separator_2 = new ToolBarButton();
			separator_2.Style = ToolBarButtonStyle.Separator;

			aboutButton = new ToolBarButton();
			aboutButton.ToolTipText = "About";
			aboutButton.Style = ToolBarButtonStyle.PushButton;
			aboutButton.ImageIndex = 6;

			exitButton = new ToolBarButton();
			exitButton.ToolTipText = "Exit";
			exitButton.Style = ToolBarButtonStyle.PushButton;
			exitButton.ImageIndex = 7;

			Buttons.Add(newButton);
			Buttons.Add(openButton);
			Buttons.Add(saveButton);
			Buttons.Add(separator_1);
			Buttons.Add(cascadeButton);
			Buttons.Add(horizontalButton);
			Buttons.Add(verticalButton);
			Buttons.Add(separator_2);
			Buttons.Add(aboutButton);
			Buttons.Add(exitButton);
		}
			
	}
}
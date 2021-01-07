using System;
using System.Windows.Forms;
using System.Drawing;

namespace CircleApp 
{
	class StatusStripControl : StatusStrip
	{
		private ToolStripStatusLabel selectedCircleLabel;
		public ToolStripStatusLabel selectedCircleText;
		private ToolStripStatusLabel filePathLabel;
		public ToolStripStatusLabel filePathText;
		
		public StatusStripControl()
		{
			LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
			Padding = new Padding(10, 0, 20, 0);

			selectedCircleLabel = new ToolStripStatusLabel();
			selectedCircleLabel.Alignment = ToolStripItemAlignment.Left;
			selectedCircleLabel.Text = "Selected circle: ";

			selectedCircleText = new ToolStripStatusLabel();
			selectedCircleText.Alignment = ToolStripItemAlignment.Left;
			selectedCircleText.Text = "";
			selectedCircleText.BorderSides = ToolStripStatusLabelBorderSides.Right;
			selectedCircleText.BorderStyle = Border3DStyle.Etched;

			filePathLabel = new ToolStripStatusLabel();
			filePathLabel.Alignment = ToolStripItemAlignment.Left;
			filePathLabel.Text = "File: ";

			filePathText = new ToolStripStatusLabel();
			filePathText.Alignment = ToolStripItemAlignment.Left;
			filePathText.Text = "";

			Items.AddRange(new ToolStripItem[] {
				selectedCircleLabel,
				selectedCircleText,
				filePathLabel,
				filePathText
			});
		}

			
	}
}
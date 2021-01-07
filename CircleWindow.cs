using System;
using System.Windows.Forms;
using System.Drawing;

namespace CircleApp 
{
	class CircleWindow : Form
	{
		private SplitContainer splitContainer;
		private PropertiesControl properties;
		public Circle circle;
		public string path;
		public bool isSaved;

		public CircleWindow(String circleId, Circle circle, string path, bool isSaved = false) 
		{
			Text = circleId;
			Size = new Size(660, 450);
			InitCircleWindow();
			this.circle = circle;
			properties.SetProperties(this.circle);
			this.isSaved = isSaved;
			this.path = path;
		}

		public CircleWindow(String circleId)
		{
			Text = circleId;
			Size = new Size(660, 450);
			InitCircleWindow();
			this.circle = new Circle(100, 
				splitContainer.Panel2.Size.Width / 2, 
				splitContainer.Panel2.Size.Height / 2,
				Color.White,
				3,
				Color.Black
				);
			properties.SetProperties(this.circle);
			this.isSaved = false;
		}	

		public void InitCircleWindow()
		{
			InitSplitContainer();
			InitPropertiesControl();

			properties.PropertiesChanged += (Circle c) => {
				this.circle = c;
				splitContainer.Panel2.Refresh();
				this.isSaved = false;
			};

			splitContainer.Panel2.CreateGraphics();
			splitContainer.Panel2.Paint += Circle_Paint;
			splitContainer.Panel2.Click += (s, e) => {
				Point clickedPosition = splitContainer.Panel2.PointToClient(Cursor.Position);
				if (clickedPosition.X < 0)
					clickedPosition.X = 0;
				if (clickedPosition.X > 1500)
					clickedPosition.X = 1500;
				if (clickedPosition.Y < 0)
					clickedPosition.Y = 0;
				if (clickedPosition.Y > 1500)
					clickedPosition.Y = 1500;
				this.circle.CenterX = clickedPosition.X;
				this.circle.CenterY = clickedPosition.Y;
				properties.SetProperties(this.circle);
				splitContainer.Panel2.Refresh();
			};
		}
		private void InitSplitContainer()
		{
			splitContainer = new SplitContainer();
			splitContainer.Size = this.Size;
			splitContainer.Location = new Point(0, 0);
			splitContainer.SplitterDistance = 180;
			splitContainer.FixedPanel = FixedPanel.Panel1;
			
			splitContainer.BorderStyle = BorderStyle.FixedSingle;
			splitContainer.IsSplitterFixed = true;
			splitContainer.Panel1.BackColor = Color.FromArgb(170, 170, 170);
			Controls.Add(splitContainer);
		}

		private void InitPropertiesControl()
		{
			properties = new PropertiesControl();
			properties.Dock = DockStyle.Fill;
			properties.Location = new Point(0, 0);

			splitContainer.Panel1.Controls.Add(properties);
		}

		public string Id 
		{
			get { return Text; }
		}

		protected override void OnResize(EventArgs e)
		{
			if (splitContainer != null) 
			{
				splitContainer.Size = this.Size;
			}
		}
		private void Circle_Paint(object sender, PaintEventArgs e)
		{	
				DrawCircle(circle, e.Graphics);
		}

		public void DrawCircle(Circle circle, Graphics g)
		{
			SolidBrush brush = new SolidBrush(System.Drawing.Color.Red);
			Brush circleColorBrush = new SolidBrush(circle.CircleColor);
			Pen borderColorPen = new Pen(circle.BorderColor);
			Brush textColorBrush = new SolidBrush(circle.TextColor);
			borderColorPen.Width = circle.BorderWidth;
			Graphics graphics = g;

			int relativeCenterX = (int)(circle.CenterX - circle.Radius);
			int relativeCenterY = (int)(circle.CenterY - circle.Radius);
			int diameter = (int)circle.Radius * 2;

			Rectangle r = new Rectangle(relativeCenterX, relativeCenterY, diameter, diameter);

			graphics.FillEllipse(circleColorBrush, r);

			graphics.DrawEllipse(borderColorPen, r);

			Font drawFont = new Font("Arial", 16);
			StringFormat f = new StringFormat();
			f.LineAlignment = StringAlignment.Center;
			f.Alignment = StringAlignment.Center;
			graphics.DrawString(circle.Text, drawFont, textColorBrush, r, f);


			brush.Dispose();
			graphics.Dispose();
		}
	
			
	}
}
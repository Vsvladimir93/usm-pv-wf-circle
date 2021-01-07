using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace CircleApp 
{
	public delegate void Notify(Circle circle); 

	class PropertiesControl : ContainerControl
	{
		private Label radiusLabel;
		public NumericUpDown radiusInput;
		private Label centerXLabel;
		public NumericUpDown centerXInput;
		private Label centerYLabel;
		public NumericUpDown centerYInput;
		private Label circleColorLabel;
		public ComboBox circleColorSelect;
		private Label borderWidthLabel;
		public NumericUpDown borderWidthInput;
		private Label borderColorLabel;
		public ComboBox borderColorSelect;
		private Label textColorLabel;
		public ComboBox textColorSelect;
		private Label textLabel;
		public TextBox textInput;
		
		private Point startPoint = new Point(15, 0);
		private int width;
		private Size controlSize;
		private Size labelSize;

		// Event that fired if one of control values changed
		public event Notify PropertiesChanged;
		
		public PropertiesControl()
		{
			width = 150;
			controlSize = new Size(width, 20);
			labelSize = new Size(width, 15);
		
			radiusLabel = new Label();
			radiusLabel.AutoSize = true;
			radiusLabel.Location = NextPoint();
			radiusLabel.Size = labelSize;
			radiusLabel.Text = "Radius";

			radiusInput = new NumericUpDown();
			radiusInput.DecimalPlaces = 1;
			radiusInput.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			radiusInput.Location = NextPoint(false);
			radiusInput.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
			radiusInput.Size = controlSize;
			radiusInput.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});

			centerXLabel = new Label();
			centerXLabel.AutoSize = true;
			centerXLabel.Location = NextPoint();
			centerXLabel.Size = labelSize;
			centerXLabel.Text = "Center X";

			centerXInput = new NumericUpDown();
			centerXInput.DecimalPlaces = 1;
			centerXInput.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			centerXInput.Location = NextPoint(false);
			centerXInput.Maximum = new decimal(new int[] {
            1500,
            0,
            0,
            0});
			centerXInput.Size = controlSize;
			centerXInput.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});

			centerYLabel = new Label();
			centerYLabel.AutoSize = true;
			centerYLabel.Location = NextPoint();
			centerYLabel.Size = labelSize;
			centerYLabel.Text = "Center Y";

			centerYInput = new NumericUpDown();
			centerYInput.DecimalPlaces = 1;
			centerYInput.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			centerYInput.Location = NextPoint(false);
			centerYInput.Maximum = new decimal(new int[] {
            1500,
            0,
            0,
            0});
			centerYInput.Size = controlSize;
			centerYInput.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});

			circleColorLabel = new Label();
			circleColorLabel.AutoSize = true;
			circleColorLabel.Location = NextPoint();
			circleColorLabel.Size = labelSize;
			circleColorLabel.Text = "Circle color";

			circleColorSelect = new ComboBox();
			circleColorSelect.CausesValidation = false;
			circleColorSelect.FormattingEnabled = true;
			circleColorSelect.Location = NextPoint(false);
			circleColorSelect.Size = controlSize;		

			borderWidthLabel = new Label();
			borderWidthLabel.AutoSize = true;
			borderWidthLabel.Location = NextPoint();
			borderWidthLabel.Size = labelSize;
			borderWidthLabel.Text = "Border width";

			borderWidthInput = new NumericUpDown();
			borderWidthInput.DecimalPlaces = 1;
			borderWidthInput.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			borderWidthInput.Location = NextPoint(false);
			borderWidthInput.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
			borderWidthInput.Size = controlSize;
			borderWidthInput.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
		
			borderColorLabel = new Label();
			borderColorLabel.AutoSize = true;
			borderColorLabel.Location = NextPoint();
			borderColorLabel.Size = labelSize;
			borderColorLabel.Text = "Border color";

			borderColorSelect = new ComboBox();
			borderColorSelect.CausesValidation = false;
			borderColorSelect.FormattingEnabled = true;
			borderColorSelect.Location = NextPoint(false);
			borderColorSelect.Size = controlSize;		

			textColorLabel = new Label();
			textColorLabel.AutoSize = true;
			textColorLabel.Location = NextPoint();
			textColorLabel.Size = labelSize;
			textColorLabel.Text = "Text color";

			textColorSelect = new ComboBox();
			textColorSelect.CausesValidation = false;
			textColorSelect.FormattingEnabled = true;
			textColorSelect.Location = NextPoint(false);
			textColorSelect.Size = controlSize;

			textLabel = new Label();
			textLabel.AutoSize = true;
			textLabel.Location = NextPoint();
			textLabel.Size = labelSize;
			textLabel.Text = "Text";

			textInput = new TextBox();
			textInput.Location = NextPoint(false);
			textInput.MaxLength = 50;
			textInput.Multiline = true;
			textInput.Size = new Size(width, 110);			

			Controls.Add(radiusLabel);
			Controls.Add(radiusInput);
			Controls.Add(centerXLabel);
			Controls.Add(centerXInput);
			Controls.Add(centerYLabel);
			Controls.Add(centerYInput);
			Controls.Add(circleColorLabel);
			Controls.Add(circleColorSelect);
			Controls.Add(borderWidthLabel);
			Controls.Add(borderWidthInput);
			Controls.Add(borderColorLabel);
			Controls.Add(borderColorSelect);
			Controls.Add(textColorLabel);
			Controls.Add(textColorSelect);
			Controls.Add(textLabel);
			Controls.Add(textInput);

			InitColorValues();

			// Subscribe to properties changed events
			radiusInput.ValueChanged += (s, e) => PropertiesChanged.Invoke(GetProperties());
			centerXInput.ValueChanged += (s, e) => PropertiesChanged.Invoke(GetProperties());
			centerYInput.ValueChanged += (s, e) => PropertiesChanged.Invoke(GetProperties());
			borderWidthInput.ValueChanged += (s, e) => PropertiesChanged.Invoke(GetProperties());
			textInput.TextChanged += (s, e) => PropertiesChanged.Invoke(GetProperties());
			circleColorSelect.SelectedIndexChanged += (s, e) => PropertiesChanged.Invoke(GetProperties());
			borderColorSelect.SelectedIndexChanged += (s, e) => PropertiesChanged.Invoke(GetProperties());
			textColorSelect.SelectedIndexChanged += (s, e) => PropertiesChanged.Invoke(GetProperties());
		}

		public Circle GetProperties()
		{
			return new Circle(
				(double)radiusInput.Value,
				(double)centerXInput.Value,
				(double)centerYInput.Value,
				Color.FromName(circleColorSelect.Text),
				(uint)borderWidthInput.Value,
				Color.FromName(borderColorSelect.Text),
				textInput.Text,
				Color.FromName(textColorSelect.Text)
			);
		}
		public void SetProperties(Circle circle)
		{
			radiusInput.Value = (decimal)circle.Radius;
			centerXInput.Value = (decimal)circle.CenterX;
			centerYInput.Value = (decimal)circle.CenterY;
			circleColorSelect.SelectedItem = ColorToString(circle.CircleColor);
			borderWidthInput.Value = circle.BorderWidth;
			borderColorSelect.SelectedItem = ColorToString(circle.BorderColor);
			textColorSelect.SelectedItem = ColorToString(circle.TextColor);
			textInput.Text = circle.Text;
		}

		private Point NextPoint(bool isLabel = true)
		{
			startPoint = new Point(startPoint.X, startPoint.Y + (isLabel ? controlSize.Height : labelSize.Height));
			return startPoint;
		}

		private void InitColorValues()
		{
			Color[] colors = new Color[] { Color.White, Color.Black, Color.Blue, Color.Orange, Color.Green };
			String[] colorStrings = colors.Select(c => c.ToString().Substring(7).Replace("]", "")).ToArray();

			circleColorSelect.Items.AddRange(colorStrings);
			borderColorSelect.Items.AddRange(colorStrings);
			textColorSelect.Items.AddRange(colorStrings);

			circleColorSelect.SelectedItem = colorStrings[0];
			borderColorSelect.SelectedItem = colorStrings[0];
			textColorSelect.SelectedItem = colorStrings[0];
		}

		private string ColorToString(Color color)
		{
			return color.ToString().Substring(7).Replace("]", "");
		}
			
	}
}
using System;
using System.Drawing;
using CircleApp.ReadWrite;
using CircleApp.ReadWrite.Attributes;

namespace CircleApp
{

	public class Circle : SerializableData
	{
		private double radius;
		private double centerX;
		private double centerY;
		private Color circleColor;
		private uint borderWidth;
		private Color borderColor;
		private string text;
		private Color textColor;

		public Circle() { }

		public Circle(
			double radius,
			double centerX,
			double centerY,
			Color? circleColor = null,
			uint borderWidth = 0,
			Color? borderColor = null,
			string text = "",
			Color? textColor = null
			)
		{
			Radius = radius;
			CenterX = centerX;
			CenterY = centerY;
			CircleColor = circleColor ?? Color.White;
			BorderWidth = borderWidth;
			BorderColor = borderColor ?? Color.White;
			Text = text;
			TextColor = textColor ?? Color.Black;
		}

		[SerializableProperty]
		public double Radius
		{
			get
			{
				return radius;
			}
			set
			{
				radius = value < 0 ? 0 : value;
			}
		}

		[SerializableProperty]
		public double CenterX
		{
			get
			{
				return centerX;
			}
			set
			{
				centerX = value;
			}
		}

		[SerializableProperty]
		public double CenterY
		{
			get
			{
				return centerY;
			}
			set
			{
				centerY = value;
			}
		}

		[SerializableProperty]
		public Color CircleColor
		{
			get
			{
				return circleColor;
			}
			set
			{
				circleColor = value;
			}
		}

		[SerializableProperty]
		public uint BorderWidth
		{
			get
			{
				return borderWidth;
			}
			set
			{
				borderWidth = value < 0 ? 0 : value;
			}
		}

		[SerializableProperty]
		public Color BorderColor
		{
			get
			{
				return borderColor;
			}
			set
			{
				borderColor = value;
			}
		}

		[SerializableProperty]
		public string Text
		{
			get
			{
				return text;
			}
			set
			{
				text = value;
			}
		}

		[SerializableProperty]
		public Color TextColor
		{
			get
			{
				return textColor;
			}
			set
			{
				textColor = value;
			}
		}

		public override string ToString()
		{
			return string.Format("{0} {1} {2} {3} {4} {5} {6} {7}", 
				Radius, CenterX, CenterY, CircleColor.ToString(), BorderWidth, 
				BorderColor.ToString(), Text, TextColor.ToString()
				);
		}

	}
}

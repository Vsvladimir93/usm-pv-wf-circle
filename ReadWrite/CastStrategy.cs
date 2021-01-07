using System;
using System.Drawing;

namespace CircleApp.ReadWrite
{
	sealed class CastStrategy
	{
		private CastStrategy() { }		

		public static Func<string, Color> GetCastFunctionFor(string objectType)
		{
			switch (objectType)
			{
				case "System.Drawing.Color":
					Func<string, Color> func = (string color) => Color.FromName(color.Substring(7).Replace("]", "")); 
					return func;
				default:
					throw new InvalidCastException("Cast Strategy Not Found!");
			}
		}

	}
}

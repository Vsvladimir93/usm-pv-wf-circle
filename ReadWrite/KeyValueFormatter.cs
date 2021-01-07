using System;
using System.Collections.Generic;

namespace CircleApp.ReadWrite
{
	sealed class KeyValueFormatter
	{
		private const string SEPARATOR = " = ";
		private const string DEFAULT_FORMAT = "{0}" + SEPARATOR + "{1}\r\n";


		private KeyValueFormatter() { }

		public static string Assemble(string key, string value)
		{
			return String.Format(DEFAULT_FORMAT, key, value);
		}

		public static KeyValuePair<string, string> Disassemble(string keyValueUnit)
		{
			string[] result = keyValueUnit.Split(new string[] { SEPARATOR }, 2, StringSplitOptions.None);
			return new KeyValuePair<string, string>(result[0], result.Length == 1 ? "" : result[1]);
		}

	}
}

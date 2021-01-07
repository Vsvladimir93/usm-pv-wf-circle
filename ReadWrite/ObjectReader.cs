using CircleApp.ReadWrite.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CircleApp.ReadWrite
{
	sealed class ObjectReader
	{
		private ObjectReader() { }

		public static T ReadFromFile<T>(string path) where T : ISerializableData, new()
		{
			if (!File.Exists(path))
			{
				throw new FileNotFoundException(string.Format("File by path: {0} does not found.", path));
			}

			string fileContent = File.ReadAllText(path);

			Dictionary<string, string> parsedFileContent = ParseContent(fileContent);

			return TryCast<T>(parsedFileContent);

		}

		private static Dictionary<string, string> ParseContent(string fileContent)
		{
			Dictionary<string, string> keyValues = new Dictionary<string, string>();

			string[] linesArray = fileContent.Split('\n');
			foreach (string line in linesArray)
			{
				if (string.IsNullOrEmpty(line))
					continue;
				KeyValuePair<string, string> pair = KeyValueFormatter.Disassemble(line);
				keyValues.Add(pair.Key, pair.Value);
			}
			return keyValues;
		}

		private static T TryCast<T>(Dictionary<string, string> fileContent) where T : ISerializableData, new()
		{
			T obj = new T();

			obj.GetSerialiazableProperties().ForEach(property =>
			{
				if (fileContent.ContainsKey(property.Name))
				{
					try
					{
						property.SetValue(obj, Convert.ChangeType(fileContent[property.Name], property.PropertyType));
					}
					catch (InvalidCastException e)
					{
						object castedObj = CastStrategy.GetCastFunctionFor(property.PropertyType.FullName)(fileContent[property.Name].Trim());
						property.SetValue(obj, castedObj);
					}
					
				}
			});

			return obj;
		}	
	}
}

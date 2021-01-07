using CircleApp.ReadWrite.Interfaces;
using System;
using System.IO;
using System.Text;

namespace CircleApp.ReadWrite
{
	sealed class ObjectWriter
	{
		private ObjectWriter() { }

		public static bool WriteToFile<T>(T data, string path) where T : ISerializableData
		{
			using (FileStream stream = File.OpenWrite(path))
			{				
				data.GetSerialiazableProperties().ForEach(property =>
				{
					try
					{
						byte[] array = new UTF8Encoding(true)
							.GetBytes(KeyValueFormatter.Assemble(property.Name, property.GetValue(data).ToString()));
						stream.Write(array, 0, array.Length);
					}
					catch (InvalidCastException e)
					{
						Console.WriteLine("InvalidCastException: {0}. Cannot cast member: {1}", e.Message, property.Name);
					}
				});
			}

			return true;
		}

	}
}

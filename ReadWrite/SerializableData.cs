using CircleApp.ReadWrite.Attributes;
using CircleApp.ReadWrite.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CircleApp.ReadWrite
{
	public class SerializableData : ISerializableData
	{
		public List<PropertyInfo> GetSerialiazableProperties()
		{
			return this.GetType()
					.GetProperties(BindingFlags.Instance | BindingFlags.Public)
					.Where(property => IsPropertySerializable(property))
					.ToList();
		}
		public bool IsPropertySerializable(PropertyInfo property)
		{
			return property.GetCustomAttribute(typeof(SerializableProperty)) != null;
		}
	}
}

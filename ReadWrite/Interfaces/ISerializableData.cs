using System.Collections.Generic;
using System.Reflection;

namespace CircleApp.ReadWrite.Interfaces
{
	interface ISerializableData
	{
		bool IsPropertySerializable(PropertyInfo property);

		List<PropertyInfo> GetSerialiazableProperties();

	}
}

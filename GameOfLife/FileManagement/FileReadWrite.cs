using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace GameOfLife.FileManagement
{
	public static class FileReadWrite
	{

		// Writes itemObject to a file specified by path through XML serialization
		// itemObject must implement ISerializable
		public static void WriteLifeSerializableToFile<T>(T itemObject, string path) where T : ISerializable
		{
			try
			{
				XmlSerializer write = new XmlSerializer(itemObject.GetType());
				FileStream file = File.Create(path);

				write.Serialize(file, itemObject);
				file.Close();
			}
			catch
			{
				throw;
			}
		}

		// Reads information in a given XML file defined by path and turns it into a new object of the type specified by T
		// Can throw typical FileStream creation or read errors
		// T must implement ISerializable
		public static T ReadObjectFromXMLFile<T>(string path) where T : ISerializable
		{
			try
			{
				T item;
				XmlSerializer read = new XmlSerializer(typeof(T));
				StreamReader reader = new StreamReader(path);
				item = (T)read.Deserialize(reader);

				return item;
			}
			catch
			{
				throw;
			}
		}
	}
}

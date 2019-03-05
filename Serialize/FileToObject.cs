using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace StoicDreams.Serialize
{
	public class FileToObject<T> where T : class
	{
		public T Data { get; private set; }
		public ISerialize Serializer { get; set; } = new JSON();
		public string FolderPath { get; set; } = "./";
		/// <summary>
		/// Name of file to deserialize.
		/// File name only, do not include folder path.
		/// </summary>
		public string FileName { get; set; }
		public string FullFilePath { get => $"{Path.GetFullPath(FolderPath)}\\{FileName}"; }

		public async Task<bool> LoadAsync()
		{
			return await Task.Run(() =>
			{
				return Load();
			});
		}
		public bool Load()
		{
			Data = null;
			if (!File.Exists(FullFilePath)) { return false; }
			return Load(File.ReadAllText(FullFilePath));
		}
		internal async Task<bool> LoadAsync(string json)
		{
			return await Task.Run(() => {
				return Load(json);
			});
		}
		internal bool Load(string json)
		{
			Data = Serializer.Deserialize<T>(json);
			return Data != null;
		}
	}
}

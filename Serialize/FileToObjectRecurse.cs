using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace StoicDreams.Serialize
{
	public static class FileToObjectRecurse
	{
		/// <summary>
		/// Load data object from one or more files, starting from expected folder, then recursively checking parent folders up to the root folder.
		/// Use this method to have global - or default - settings or data in parent folders, and child folders containing overwrites.
		/// </summary>
		/// <typeparam name="T">Class to hold deserialized data</typeparam>
		/// <param name="fileToObject"></param>
		/// <returns></returns>
		public static async Task<bool> LoadRecursivelyAsync<T>(this FileToObject<T> fileToObject) where T:class
		{
			List<IDictionary> dataContent = new List<IDictionary>();
			string driveRoot = Path.GetFullPath("/");
			DirectoryInfo currentFolder = new DirectoryInfo(fileToObject.FolderPath);
			FileToObject<IDictionary> temp = new FileToObject<IDictionary>
			{
				FileName = fileToObject.FileName
			};
			while (currentFolder.Parent != null)
			{
				temp.FolderPath = currentFolder.FullName;
				currentFolder = currentFolder.Parent;
				await temp.LoadAsync();
				if(temp.Data == null) { continue; }
				dataContent.Add(temp.Data);
			}
			dataContent.Reverse();
			IDictionary result = new Dictionary<string, object>();
			foreach(IDictionary data in dataContent)
			{
				await result.MergeAsync(data);
			}
			JSON serializer = new JSON();
			string json = serializer.Serialize(result);
			await fileToObject.LoadAsync(json);
			return fileToObject.Data != null;
		}
		public static void Merge(this IDictionary destination, IDictionary source)
		{
			foreach (object key in source.Keys)
			{
				if (destination.Contains(key))
				{
					destination[key] = MergeOrOverwrite(destination[key], source[key]);
					continue;
				}
				destination.Add(key, source[key]);
			}
		}
		public static async Task MergeAsync(this IDictionary destination, IDictionary source)
		{
			await Task.Run(() =>
			{
				destination.Merge(source);
			});
		}
		public static T MergeOrOverwrite<T>(this T destination, T source) where T:class
		{
			if (destination is JToken dest)
			{
				string jsonDestination = Newtonsoft.Json.JsonConvert.SerializeObject(dest);
				string jsonSource = Newtonsoft.Json.JsonConvert.SerializeObject(source as JToken);
				IDictionary des = Newtonsoft.Json.JsonConvert.DeserializeObject<IDictionary>(jsonDestination);
				IDictionary src = Newtonsoft.Json.JsonConvert.DeserializeObject<IDictionary>(jsonSource);
				des.Merge(src);
				return des as T;
			}
			return source;
		}
	}
}

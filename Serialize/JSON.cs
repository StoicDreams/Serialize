using Newtonsoft.Json;
using System.Threading.Tasks;

namespace StoicDreams.Serialize
{
	public static class JSON
	{
		public static JsonSerializerSettings DefaultSerializerSettings { get; } = new JsonSerializerSettings()
		{
			FloatParseHandling = FloatParseHandling.Decimal
		};
		public static async Task<T> DeserializeAsync<T>(string json)
		{
			return await Task.Run(() => JsonConvert.DeserializeObject<T>(json, DefaultSerializerSettings));
		}
		public static async Task<string> SerializeAsync<T>(T input)
		{
			return await Task.Run(() => JsonConvert.SerializeObject(input, DefaultSerializerSettings));
		}
		public static T Deserialize<T>(string json)
		{
			var task = Task.Run(() => DeserializeAsync<T>(json));
			task.Wait();
			return task.Result;
		}
		public static string Serialize<T>(T input)
		{
			var task = Task.Run(() => SerializeAsync(input));
			task.Wait();
			return task.Result;
		}
	}
}

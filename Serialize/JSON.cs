using Newtonsoft.Json;
using System.Threading.Tasks;

namespace StoicDreams.Serialize
{
	public class JSON : ISerialize
	{
		private JsonSerializerSettings DefaultSerializerSettings { get; } = new JsonSerializerSettings()
		{
			FloatParseHandling = FloatParseHandling.Decimal
		};
		public async Task<T> DeserializeAsync<T>(string json)
		{
			return await Task.Run(() => JsonConvert.DeserializeObject<T>(json, DefaultSerializerSettings));
		}
		public async Task<string> SerializeAsync<T>(T input)
		{
			return await Task.Run(() => JsonConvert.SerializeObject(input, DefaultSerializerSettings));
		}
		public T Deserialize<T>(string json)
		{
			var task = Task.Run(() => DeserializeAsync<T>(json));
			task.Wait();
			return task.Result;
		}
		public string Serialize<T>(T input)
		{
			var task = Task.Run(() => SerializeAsync(input));
			task.Wait();
			return task.Result;
		}
	}
}

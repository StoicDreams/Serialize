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
		public static JSON Serializer { get; } = new JSON();
		public async Task<T> DeserializeAsync<T>(string json)
		{
			return await Task.Run(() => Deserialize<T>(json));
		}
		public async Task<string> SerializeAsync<T>(T input)
		{
			return await Task.Run(() => Serialize(input));
		}
		public T Deserialize<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json, DefaultSerializerSettings);
		}
		public string Serialize<T>(T input)
		{
			return JsonConvert.SerializeObject(input, DefaultSerializerSettings);
		}
	}
}

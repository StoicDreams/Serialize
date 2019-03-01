using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoicDreams.Serialize
{
	public interface ISerialize
	{
		Task<T> DeserializeAsync<T>(string json);
		Task<string> SerializeAsync<T>(T input);
		T Deserialize<T>(string json);
		string Serialize<T>(T input);
	}
}

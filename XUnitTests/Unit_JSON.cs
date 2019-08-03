using Xunit;
using StoicDreams.Serialize;

namespace XUnitTests
{
	public class Unit_JSON
	{
		[Fact]
		public void TestSerialize()
		{
			JSON serializer = new JSON();
			string jsonA = "{\"One\":\"1\",\"Two\":\"2\"}";
			TestA testA = new TestA() { One = "1", Two = "2" };
			Assert.Equal(jsonA, serializer.Serialize(testA));
			string jsonB = "{\"One\":1,\"Two\":2.0}";
			TestB testB = new TestB() { One = 1, Two = 2m };
			Assert.Equal(jsonB, serializer.Serialize(testB));
		}

		[Fact]
		public void TestDeSerialize()
		{
			JSON serializer = new JSON();
			string json = "{\"One\":1,\"Two\":2}";
			TestA testA = serializer.Deserialize<TestA>(json);
			Assert.Equal("1", testA.One);
			Assert.Equal("2", testA.Two);
			TestB testB = serializer.Deserialize<TestB>(json);
			Assert.Equal(1, testB.One);
			Assert.Equal(2.0m, testB.Two);
		}

		private class TestA
		{
			public string One { get; set; }
			public string Two { get; set; }
		}

		private struct TestB
		{
			public int One { get; set; }
			public decimal Two { get; set; }
		}
	}
}

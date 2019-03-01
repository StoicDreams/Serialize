using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoicDreams.Serialize;

namespace UnitTests
{
	[TestClass]
	public class UnitTest_JSON
	{
		[TestMethod]
		public void TestSerialize()
		{
			JSON serializer = new JSON();
			string jsonA = "{\"One\":\"1\",\"Two\":\"2\"}";
			TestA testA = new TestA() { One = "1", Two = "2" };
			Assert.AreEqual(jsonA, serializer.Serialize(testA));
			string jsonB = "{\"One\":1,\"Two\":2.0}";
			TestB testB = new TestB() { One = 1, Two = 2m };
			Assert.AreEqual(jsonB, serializer.Serialize(testB));
		}
		[TestMethod]
		public void TestDeSerialize()
		{
			JSON serializer = new JSON();
			string json = "{\"One\":1,\"Two\":2}";
			TestA testA = serializer.Deserialize<TestA>(json);
			Assert.AreEqual("1", testA.One);
			Assert.AreEqual("2", testA.Two);
			TestB testB = serializer.Deserialize<TestB>(json);
			Assert.AreEqual(1, testB.One);
			Assert.AreEqual(2.0m, testB.Two);
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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoicDreams.Serialize;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace UnitTests
{
	[TestClass]
	class UnitTest_FileToObjectRecurse
	{
		[TestMethod]
		public async Task UnitTestLoadCompleteFile()
		{
			FileToObject<Test> test = new FileToObject<Test>
			{
				FolderPath = "Parent",
				FileName = "Test.Config.JSON"
			};
			Assert.AreEqual(Path.GetFullPath("parent/test.config.json").ToLower(), test.FullFilePath.ToLower());
			Assert.AreEqual(true, await test.LoadRecursivelyAsync());
			Assert.AreNotEqual(null, test.Data);
			Assert.AreEqual("1", test.Data.One);
			Assert.AreEqual("2", test.Data.Two);
			Assert.AreEqual("8", test.Data.Nested["One"]);
			Assert.AreEqual("16", test.Data.Nested["Two"]);
		}
		[TestMethod]
		public async Task UnitTestLoadPartialFile()
		{
			FileToObject<Test> test = new FileToObject<Test>
			{
				FolderPath = "Parent/child",
				FileName = "test.config.json"
			};
			Assert.AreEqual(Path.GetFullPath("Parent/child/test.config.json"), test.FullFilePath);
			Assert.AreEqual(true, await test.LoadRecursivelyAsync());
			Assert.AreNotEqual(null, test.Data);
			Assert.AreEqual("1", test.Data.One);
			Assert.AreEqual("3", test.Data.Two);
			Assert.AreEqual("32", test.Data.Nested["One"]);
			Assert.AreEqual("16", test.Data.Nested["Two"]);
		}
		[TestMethod]
		public async Task UnitTestLoadInvalidDataFile()
		{
			FileToObject<Test> test = new FileToObject<Test>
			{
				FolderPath = "Parent/bad",
				FileName = "test.config.json"
			};
			Assert.AreEqual(Path.GetFullPath("Parent/bad/test.config.json"), test.FullFilePath);
			Assert.AreEqual(true, await test.LoadRecursivelyAsync());
			Assert.AreNotEqual(null, test.Data);
			Assert.AreEqual("1", test.Data.One);
			Assert.AreEqual("2", test.Data.Two);
		}
		private class Test
		{
			public string One;
			public string Two;
			public Dictionary<string, string> Nested = new Dictionary<string, string>();
		}
	}
}

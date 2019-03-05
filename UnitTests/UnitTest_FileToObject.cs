using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoicDreams.Serialize;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace UnitTests
{
	[TestClass]
	public class UnitTest_FileToObject
	{
		/// <summary>
		/// Make sure methods from base libraries function as expected
		/// </summary>
		[TestMethod]
		public void VerifyLibraryAssertions()
		{
			string driveRoot = Path.GetFullPath("/");//e.g. C:/
			string projectRoot = Path.GetFullPath("./");//e.g. C:/project/bin/
			Assert.AreEqual($@"{projectRoot}noexist", Path.GetFullPath("noexist"));
			Assert.AreEqual($@"{projectRoot}noexist", Path.GetFullPath("./noexist"));
			Assert.AreEqual($@"{driveRoot}noexist", Path.GetFullPath("/noexist"));
			Assert.AreEqual(driveRoot, Path.GetFullPath(driveRoot));
			Assert.AreEqual(projectRoot, Path.GetFullPath(projectRoot));
			Assert.AreNotEqual(driveRoot, projectRoot);
		}
		[TestMethod]
		public async Task UnitTestLoadSingleMissingFile()
		{
			FileToObject<Test> test = new FileToObject<Test>
			{
				FolderPath = "/",
				FileName = "test.config.json"
			};
			Assert.AreEqual(false, await test.LoadAsync());
		}
		[TestMethod]
		public async Task UnitTestLoadCompleteFile()
		{
			FileToObject<Test> test = new FileToObject<Test>
			{
				FolderPath = "Parent",
				FileName = "Test.Config.JSON"
			};
			Assert.AreEqual(Path.GetFullPath("parent/test.config.json").ToLower(), test.FullFilePath.ToLower());
			Assert.AreEqual(true, await test.LoadAsync());
			Assert.AreNotEqual(null, test.Data);
			Assert.AreEqual("1", test.Data.One);
			Assert.AreEqual("2", test.Data.Two);
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
			Assert.AreEqual(true, await test.LoadAsync());
			Assert.AreNotEqual(null, test.Data);
			Assert.AreEqual(null, test.Data.One);
			Assert.AreEqual("3", test.Data.Two);
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
			Assert.AreEqual(true, await test.LoadAsync());
			Assert.AreNotEqual(null, test.Data);
			Assert.AreEqual(null, test.Data.One);
			Assert.AreEqual(null, test.Data.Two);
		}
		private class Test
		{
			public string One;
			public string Two;
			public Dictionary<string, string> Nested = new Dictionary<string, string>();
		}
	}
}

using System.Collections.Generic;
using System.IO;
using StoicDreams.Serialize;
using Xunit;

namespace XUnitTests
{
	public class Unit_FileToObjectRecurse
	{
		[Fact]
		public void Verify_LoadCompleteFile()
		{
			FileToObject<Test> test = new FileToObject<Test>
			{
				FolderPath = "Parent",
				FileName = "Test.Config.JSON"
			};
			Assert.Equal(Path.GetFullPath("parent/test.config.json").ToLower(), test.FullFilePath.ToLower());
			Assert.True(test.LoadRecursivelyAsync().GetAwaiter().GetResult());
			Assert.NotNull(test.Data);
			Assert.Equal("1", test.Data.One);
			Assert.Equal("2", test.Data.Two);
			Assert.Equal("8", test.Data.Nested["One"]);
			Assert.Equal("16", test.Data.Nested["Two"]);
		}

		[Fact]
		public void Verify_LoadPartialFile()
		{
			FileToObject<Test> test = new FileToObject<Test>
			{
				FolderPath = "Parent/child",
				FileName = "test.config.json"
			};
			Assert.Equal(Path.GetFullPath("Parent/child/test.config.json"), test.FullFilePath);
			Assert.True(test.LoadRecursivelyAsync().GetAwaiter().GetResult());
			Assert.NotNull(test.Data);
			Assert.Equal("1", test.Data.One);
			Assert.Equal("3", test.Data.Two);
			Assert.Equal("32", test.Data.Nested["One"]);
			Assert.Equal("16", test.Data.Nested["Two"]);
		}

		[Fact]
		public void Verify_LoadInvalidDataFile()
		{
			FileToObject<Test> test = new FileToObject<Test>
			{
				FolderPath = "Parent/bad",
				FileName = "test.config.json"
			};
			Assert.Equal(Path.GetFullPath("Parent/bad/test.config.json"), test.FullFilePath);
			Assert.True(test.LoadRecursivelyAsync().GetAwaiter().GetResult());
			Assert.NotNull(test.Data);
			Assert.Equal("1", test.Data.One);
			Assert.Equal("2", test.Data.Two);
		}

		private class Test
		{
			public string One;
			public string Two;
			public Dictionary<string, string> Nested = new Dictionary<string, string>();
		}
	}
}

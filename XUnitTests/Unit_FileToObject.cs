using System.IO;
using System.Collections.Generic;
using StoicDreams.Serialize;
using Xunit;

namespace XUnitTests
{
	public class Unit_FileToObject
	{
		[Fact]
		public void Verify_LibraryAssertions()
		{
			string driveRoot = Path.GetFullPath("/");//e.g. C:/
			string projectRoot = Path.GetFullPath("./");//e.g. C:/project/bin/
			Assert.Equal($@"{projectRoot}noexist", Path.GetFullPath("noexist"));
			Assert.Equal($@"{projectRoot}noexist", Path.GetFullPath("./noexist"));
			Assert.Equal($@"{driveRoot}noexist", Path.GetFullPath("/noexist"));
			Assert.Equal(driveRoot, Path.GetFullPath(driveRoot));
			Assert.Equal(projectRoot, Path.GetFullPath(projectRoot));
			Assert.NotEqual(driveRoot, projectRoot);
		}

		[Fact]
		public void Verify_LoadSingleMissingFile()
		{
			FileToObject<Test> test = new FileToObject<Test>
			{
				FolderPath = "/",
				FileName = "test.config.json"
			};
			Assert.False(test.LoadAsync().GetAwaiter().GetResult());
		}

		[Fact]
		public void Verify_LoadCompleteFile()
		{
			FileToObject<Test> test = new FileToObject<Test>
			{
				FolderPath = "Parent",
				FileName = "Test.Config.JSON"
			};
			Assert.Equal(Path.GetFullPath("parent/test.config.json").ToLower(), test.FullFilePath.ToLower());
			Assert.True(test.LoadAsync().GetAwaiter().GetResult());
			Assert.NotNull(test.Data);
			Assert.Equal("1", test.Data.One);
			Assert.Equal("2", test.Data.Two);
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
			Assert.True(test.LoadAsync().GetAwaiter().GetResult());
			Assert.NotNull(test.Data);
			Assert.Null(test.Data.One);
			Assert.Equal("3", test.Data.Two);
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
			Assert.True(test.LoadAsync().GetAwaiter().GetResult());
			Assert.NotNull(test.Data);
			Assert.Null(test.Data.One);
			Assert.Null(test.Data.Two);
		}

		private class Test
		{
			public string One;
			public string Two;
			public Dictionary<string, string> Nested = new Dictionary<string, string>();
		}
	}
}

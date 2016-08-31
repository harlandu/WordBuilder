//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4200
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;

using Whee.WordBuilder.Helpers;
using Whee.WordBuilder.Model;
using Whee.WordBuilder.Exporters;

namespace test
{


	[TestFixture()]
	public class CsvExporterTest
	{

		private IFileSystem m_FileSystem;

		[Test()]
		public void TestExport()
		{
			m_FileSystem = Substitute.For<IFileSystem>();

			List<Context> selected = new List<Context>();
			
			Context result = new Context();
			result.Tokens.Add("a");
			result.Tokens.Add("b");
			result.Tokens.Add("c");
			
			Context branch = result.Branch("b1");
			branch.Tokens.Add("d");
			
			selected.Add(result);
			
			IExporter exporter = new CsvExporter();
			
			exporter.Export(selected, @"c:\abc.csv", m_FileSystem);
			
			m_FileSystem.Received().WriteAllText(@"c:\abc.csv", string.Format(".Word.;b1{0}abc;abcd{0}", Environment.NewLine));
		}
	}
}

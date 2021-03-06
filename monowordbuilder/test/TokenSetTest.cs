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
using Whee.WordBuilder.Helpers;
using Whee.WordBuilder.Model;

namespace test
{
	[TestFixture()]
	public class TokenSetTest
	{
		[SetUp()]
		public void Setup()
		{
			m_Random = Substitute.For<IRandom>();
			m_TokenSet = new TokenSet(m_Random);
			m_TokenSetCollection = new TokenSetCollection();
		}
		
		private TokenSet m_TokenSet;
		private IRandom m_Random;
		private TokenSetCollection m_TokenSetCollection;

		[Test()]
		public void TestGetToken()
		{
			m_TokenSet.Tokens.Add("a");
			m_Random.Next(1).Returns(0);
			Assert.AreEqual("a", m_TokenSet.GetToken());

			m_TokenSet.Tokens.Add("b");
			m_Random.Next(2).Returns(0);
			Assert.AreEqual("a", m_TokenSet.GetToken());

			m_Random.Next(2).Returns(1);
			Assert.AreEqual("b", m_TokenSet.GetToken());

			m_Random.Received(3).Next(Arg.Any<Int32>());
		}
		
		[Test()]
		public void TestCountByName()
		{
			TokenSet a = new TokenSet(m_Random);
			a.Name = "a";
			
			TokenSet a2 = new TokenSet(m_Random);
			a2.Name = "a";
			
			Assert.AreEqual(0, m_TokenSetCollection.CountByName("a"));

			m_TokenSetCollection.Add(a);
			Assert.AreEqual(1, m_TokenSetCollection.CountByName("a"));
			
			m_TokenSetCollection.Add(a2);
			Assert.AreEqual(2, m_TokenSetCollection.CountByName("a"));			
			
			Assert.AreEqual(0, m_TokenSetCollection.CountByName("b"));			
		}
		
		[Test()]
		public void TestGetByName()
		{
			TokenSet a = new TokenSet(m_Random);
			a.Name = "a";
			
			TokenSet a2 = new TokenSet(m_Random);
			a2.Name = "a";
			
			Assert.IsNull(m_TokenSetCollection.FindByName("a"));

			m_TokenSetCollection.Add(a);
			Assert.AreSame(a, m_TokenSetCollection.FindByName("a"));
			
			m_TokenSetCollection.Add(a2);
			Assert.AreSame(a, m_TokenSetCollection.FindByName("a"));
			
			Assert.IsNull(m_TokenSetCollection.FindByName("b"));			
		}
	}
}

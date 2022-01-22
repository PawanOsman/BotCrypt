using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BotCrypt.Test;

[TestClass]
public class HashTest
{
    [TestMethod]
    public void TestSha256()
    {
        Assert.AreEqual("e7cf3ef4f17c3999a94f2c6f612e8a888e5b1026878e4e19398b23bd38ec221a", Crypter.Sha256Hash("Password"));
    }
}
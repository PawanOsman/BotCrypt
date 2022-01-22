using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BotCrypt.Test;

[TestClass]
public class DecryptionTest
{
    [TestMethod]
    public void TestStringDecryption()
    {
        Assert.AreEqual("TestData", Crypter.DecryptString("Password", "WDmM9d8U5vvWx7Qn5fctKQ=="));
    }
}
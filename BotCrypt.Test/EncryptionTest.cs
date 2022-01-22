using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BotCrypt.Test;

[TestClass]
public class EncryptionTest
{
    [TestMethod]
    public void TestStringEncryption()
    {
        Assert.AreEqual("WDmM9d8U5vvWx7Qn5fctKQ==", Crypter.EncryptString("Password", "TestData"));
    }
}
using System;

namespace BotCrypt.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Crypter.DecryptString("TestPassword", "f5flrcfBt0sAHv3ZNU+krA=="));
            //Console.WriteLine(Crypter.Sha256Hash("TestPassword"));
        }
    }
}

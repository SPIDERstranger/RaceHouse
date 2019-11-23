using NUnit.Framework;
using System;
using System.IO;

namespace RaceHouse.Test
{
    [TestFixture]
    public class TestClass1
    {
        //[TestCaseSource(typeof(TestCaseProvider), nameof(TestCaseProvider.ProvideInputHousesNum))]
        //public void TestInputHousesNum(string id, string input, string ex)
        //{
        //    MemoryStream ms = new MemoryStream();
        //    StreamWriter writer = new StreamWriter(ms);
        //    TextReader reader = new StreamReader(ms);

        //    // todo 接下来还没完成

        //    Console.SetIn(reader);
        //    string[] inputs = input.Split(' ');
        //    foreach (string str in inputs)
        //    {
        //        writer.WriteLine(input);
        //    }
        //    int result = 0;

        //    try
        //    {
        //        result = Program.InputHousesNum();

        //        Assert.AreEqual(ex,result );
        //    }
        //    catch
        //    {
        //        if (ex == "" || ex == null)
        //        {
        //            Assert.Pass();
        //        }
        //        else
        //        {
        //            Assert.Fail();
        //        }
        //    }
        //}
        [TestCaseSource(typeof(TestCaseProvider), nameof(TestCaseProvider.ProvideHousePrintPos))]
        public void TestHousePrintPos(House house, string ex)
        {
            Assert.AreEqual(ex,house.PrintPos());
        }

        [TestCaseSource(typeof(TestCaseProvider), nameof(TestCaseProvider.ProvideHouseRun))]
        public void TestHouseRun(House house, int speed, float ex)
        {
            Assert.AreEqual(ex,house.Run(speed));
        }

        [TestCaseSource(typeof(TestCaseProvider), nameof(TestCaseProvider.ProvideGame))]
        public void TestHouseGame(NotRandom game,  int target,int money ,float ex)
        {
            float result = game.StartGame(target, money);
            Assert.AreEqual(ex,result);
        }
    }
}

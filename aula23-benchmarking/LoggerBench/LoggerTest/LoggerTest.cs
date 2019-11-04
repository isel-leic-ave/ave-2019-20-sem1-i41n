using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logger;

namespace LoggerTest
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Student st = new Student(154134, "Ze Manel", 5243, "ze", new DateTime(1990, 12, 7));
            var output = new LoggerToString();
            LoggerEmit emit = new LoggerEmit(output);
            Assert.AreEqual("", output.ToString());
            emit.Log(st);
            Console.WriteLine(output.ToString());
        }
    }
}

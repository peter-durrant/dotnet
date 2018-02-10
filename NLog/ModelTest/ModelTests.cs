using System;
using System.IO;
using NUnit.Framework;

namespace Hdd.ModelTest
{
    [TestFixture]
    public class ModelTests
    {
        private TextWriter _consoleOut;
        private StringWriter _writer;

        [SetUp]
        public void SetUp()
        {
            _consoleOut = Console.Out;
            _writer = new StringWriter();

            // grab the standard output stream
            Console.SetOut(_writer);
        }

        [TearDown]
        public void TearDown()
        {
            _writer.Dispose();
        }

        [Test]
        public void Model_Logging_NormalModelOperation()
        {
            // arrange
            var log = new Logger.Logger("Unit test logger");
            var uut = new Model.Model(log);

            // act
            var result = uut.Operate(false);

            // assert
            var output = _writer.ToString();
            var logMessages = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            Assert.IsTrue(result);
            Assert.AreEqual(2, logMessages.Length);
            StringAssert.EndsWith("|INFO|Unit test logger|About to operate...", logMessages[0]);
            StringAssert.EndsWith("|INFO|Unit test logger|Operate complete", logMessages[1]);

            // see log output in console - optional
            Console.SetOut(_consoleOut);
            Console.WriteLine(output);
        }

        [Test]
        public void Model_Logging_ErrorModelOperation()
        {
            // arrange
            var log = new Logger.Logger("Unit test logger");
            var uut = new Model.Model(log);

            // act
            var result = uut.Operate(true);

            // assert
            Assert.IsFalse(result);
            var output = _writer.ToString();
            var logMessages = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            Assert.AreEqual(2, logMessages.Length);
            StringAssert.EndsWith("|INFO|Unit test logger|About to operate...", logMessages[0]);
            StringAssert.EndsWith("|FATAL|Unit test logger|Error during operate: ERROR!", logMessages[1]);

            // see log output in console - optional
            Console.SetOut(_consoleOut);
            Console.WriteLine(output);
        }
    }
}

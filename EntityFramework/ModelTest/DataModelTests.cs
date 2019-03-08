using Model;
using NUnit.Framework;

namespace ModelTest
{
    [TestFixture]
    public class DataModelTests
    {
        [Test]
        public void DataModel_Ctor_CreatesDb()
        {
            var dataModel = new DataModel();
        }
    }
}
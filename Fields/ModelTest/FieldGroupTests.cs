using Model;
using Model.Converters;
using NUnit.Framework;

namespace ModelTest
{
    [TestFixture]
    public class FieldGroupTests
    {
        [Test]
        public void FieldGroup_Constructor_FieldGroupHasId()
        {
            const string expectedId = "id";
            var fieldGroup = new FieldGroup(expectedId);
            Assert.AreEqual(expectedId, fieldGroup.Id);
        }

        [Test]
        public void FieldGroup_Constructor_HasNoFields()
        {
            var fieldGroup = new FieldGroup("id");
            Assert.AreEqual(0, fieldGroup.Fields.Count);
        }

        [Test]
        public void FieldGroup_Constructor_HasNoFieldGroups()
        {
            var fieldGroup = new FieldGroup("id");
            Assert.AreEqual(0, fieldGroup.FieldGroups.Count);
        }

        [Test]
        public void FieldGroup_AddField_HasOneField()
        {
            var fieldGroup = new FieldGroup("id");
            fieldGroup.AddField(new Field<int, IntConverter>("field"));
            Assert.AreEqual(1, fieldGroup.Fields.Count);
        }

        [Test]
        public void FieldGroup_AddField_HasNoFieldGroups()
        {
            var fieldGroup = new FieldGroup("id");
            fieldGroup.AddField(new Field<int, IntConverter>("field"));
            Assert.AreEqual(0, fieldGroup.FieldGroups.Count);
        }

        [Test]
        public void FieldGroup_AddFieldGroup_HasOneFieldGroup()
        {
            var fieldGroup = new FieldGroup("id");
            fieldGroup.AddFieldGroup(new FieldGroup("fieldGroup"));
            Assert.AreEqual(1, fieldGroup.FieldGroups.Count);
        }

        [Test]
        public void FieldGroup_AddFieldGroup_HasNoFields()
        {
            var fieldGroup = new FieldGroup("id");
            fieldGroup.AddFieldGroup(new FieldGroup("fieldGroup"));
            Assert.AreEqual(0, fieldGroup.Fields.Count);
        }
    }
}
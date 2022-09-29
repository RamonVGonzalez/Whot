using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whot;

namespace WhotTest
{
    [TestClass]
    public class CardTest
    {
        [TestMethod]
        public void EqualsMethodTest()
        {
            //Arrange
            Card card = new Card(CardSymbol.CIRCLE, 1);
            Card otherCard = new Card(CardSymbol.CIRCLE, 1);

            //Act
            var expected = true;
            var actual = card.Equals(otherCard);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}

using NUnit.Framework;

namespace Test_ProductService
{
    [TestFixture]
// ReSharper disable InconsistentNaming
    public class Client_IntegrationTests
    {

        [Test]
        public void GetProductWithSku_ReturnsValidProduct()
        {
            //arrange

            //act
            var view = ProductClient.ProductServiceACL.GetProduct(1);

            //assert
            Assert.That(view.ProductCode, Is.EqualTo("ABC_1"));

        }

        [Test]
        public void GetProductWithUnknownSku_ReturnsNull()
        {
            //arrange

            //act
            var view = ProductClient.ProductServiceACL.GetProduct(6666);

            //assert
            Assert.That(view, Is.Null);

        }

        [Test]
        public void GetAllProducts_ReturnsAllProducts()
        {
            //arrange

            //act
            var collection = ProductClient.ProductServiceACL.GetAllProducts();

            //assert
            Assert.That(collection.Count, Is.GreaterThan(1));
            var view = collection[0];
            Assert.That(view.ProductCode, Is.EqualTo("ABC_1"));

        }


        [Test]
        public void GetProductsByQuery_ReturnsFilteredProducts()
        {
            //arrange

            //act
            var fullCollection = ProductClient.ProductServiceACL.GetAllProducts();
            var filteredCollection = ProductClient.ProductServiceACL.GetProductsByQuery("DEF");

            //assert
            Assert.That(filteredCollection.Count, Is.GreaterThan(0));
            Assert.That(filteredCollection.Count, Is.LessThan(fullCollection.Count));
            var view = filteredCollection[0];
            Assert.That(view.ProductCode, Is.EqualTo("DEF_1"));
            view = filteredCollection[1];
            Assert.That(view.ProductCode, Is.EqualTo("DEF_2"));

        }


    }
}

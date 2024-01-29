using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using ToTable.Controllers;
using ToTable.Models;
using ToTable.Services;
using System.Threading.Tasks;
using ToTable.Interfaces;

namespace ToTable_Backend.Tests
{
    [TestFixture]
    public class ProductControllerTests
    {
        [Test]
        public async Task GetProductObject_ReturnsOkResultWithData()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);

            var fakeProductList = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Product 1", ProductPrice = 10.99 },
                new Product { ProductId = 2, ProductName = "Product 2", ProductPrice = 20.99 },
            };

            productServiceMock.Setup(x => x.GetProductObject()).ReturnsAsync(fakeProductList);

            // Act
            var result = await controller.GetProductObject();

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okObjectResult = (OkObjectResult)result.Result;

            Assert.That(okObjectResult.Value, Is.InstanceOf<IEnumerable<Product>>());
            var returnedProducts = (IEnumerable<Product>)okObjectResult.Value;


            NUnit.Framework.Assert.That(() => fakeProductList.SequenceEqual(returnedProducts), Is.True);




        }

        [Test]
        public async Task GetProductObject_ReturnsNotFoundResult()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);

            productServiceMock.Setup(x => x.GetProductObject()).ReturnsAsync(new List<Product>());

            // Act
            var result = await controller.GetProductObject();

            // Assert
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }
    }
}

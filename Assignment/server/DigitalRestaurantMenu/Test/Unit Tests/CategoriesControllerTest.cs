using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Controllers;
using Application.Categories;
using Application.Categories.Dtos;
using Domain.models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Moq;
using Xunit;

namespace Test.UnitTests
{
    public class CategoriesControllerTest : BaseControllerTest
    {
        CategoriesController CategoriesController;
        public CategoriesControllerTest()
        {
            CategoriesController = new CategoriesController();
        }

        [Fact]
        public void CreateNewCategory_Success_Result()
        {
            var createNewCategoryRequestModel = new Create.Command();
            Mediator.Setup(x => x.Send(It.IsAny<Create.Command>(), new CancellationToken())).
                ReturnsAsync(ResponseWrapper<Create>.GetInstance((int)HttpStatusCode.OK, null, true, null));

            CategoriesController.SetMediatrForTest(Mediator.Object);

            //Action
            var result = CategoriesController.Create(createNewCategoryRequestModel);

            //Assert
            Assert.IsType<Task<ActionResult<ResponseWrapper<Create>>>>(result);
        }

        [Fact]
        public void EditDish_Success_Result()
        {
            var editCategoryRequestModel = new Edit.Command();
            Mediator.Setup(x => x.Send(It.IsAny<Edit.Command>(), new CancellationToken())).
                ReturnsAsync(ResponseWrapper<Edit>.GetInstance((int)HttpStatusCode.OK, null, true, null));

            CategoriesController.SetMediatrForTest(Mediator.Object);

            //Action
            var result = CategoriesController.Edit(new ObjectId().ToString(), editCategoryRequestModel);

            //Assert
            Assert.IsType<Task<ActionResult<ResponseWrapper<Edit>>>>(result);
        }

        [Fact]
        public void GetCategories_Success_Result()
        {
            CategoriesController.SetMediatrForTest(Mediator.Object);

            //Action
            var result = CategoriesController.List();

            //Assert
            Assert.IsType<Task<ActionResult<ResponseWrapper<List<CategoryDto>>>>>(result);
        }

        [Fact]
        public void GetCategoryById_Success_Result()
        {
            CategoriesController.SetMediatrForTest(Mediator.Object);

            //Action
            var result = CategoriesController.Details(new ObjectId().ToString());

            //Assert
            Assert.IsType<Task<ActionResult<ResponseWrapper<CategoryDto>>>>(result);
        }

        [Fact]
        public void DeleteCategoryById_Success_Result()
        {
            CategoriesController.SetMediatrForTest(Mediator.Object);

            //Action
            var result = CategoriesController.Delete(new ObjectId().ToString());

            //Assert
            Assert.IsType<Task<ActionResult<ResponseWrapper<Delete>>>>(result);
        }
    }
}
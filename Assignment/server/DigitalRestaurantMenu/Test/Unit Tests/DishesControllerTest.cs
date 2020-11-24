using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Controllers;
using Application.Dishes;
using Application.Dishes.Dtos;
using Domain.models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Moq;
using Xunit;

namespace Test.UnitTests
{
    public class DishesControllerTest: BaseControllerTest
    {
        DishesController DishesController;
        public DishesControllerTest()
        {
            DishesController = new DishesController();
        }

        [Fact]
        public void CreateNewDish_Success_Result()
        {
            var createNewDishRequestModel = new Create.Command();
            Mediator.Setup(x => x.Send(It.IsAny<Create.Command>(), new CancellationToken())).
                ReturnsAsync(ResponseWrapper<Create>.GetInstance((int)HttpStatusCode.OK, null, true, null));

            DishesController.SetMediatrForTest(Mediator.Object);

            //Action
            var result = DishesController.Create(createNewDishRequestModel);

            //Assert
            Assert.IsType<Task<ActionResult<ResponseWrapper<Create>>>>(result);
        }

        [Fact]
        public void GetDishes_Success_Result()
        {
            DishesController.SetMediatrForTest(Mediator.Object);

            //Action
            var result = DishesController.List();

            //Assert
            Assert.IsType<Task<ActionResult<ResponseWrapper<List<DishDto>>>>>(result);
        }

        [Fact]
        public void GetDishById_Success_Result()
        {
            DishesController.SetMediatrForTest(Mediator.Object);

            //Action
            var result = DishesController.Details(new ObjectId().ToString());

            //Assert
            Assert.IsType<Task<ActionResult<ResponseWrapper<DishDto>>>>(result);
        }

        [Fact]
        public void DeleteDishById_Success_Result()
        {
            DishesController.SetMediatrForTest(Mediator.Object);

            //Action
            var result = DishesController.Delete(new ObjectId().ToString());

            //Assert
            Assert.IsType<Task<ActionResult<ResponseWrapper<Delete>>>>(result);
        }

        [Fact]
        public void EditDish_Success_Result()
        {
            var getDishesRequestModel = new Edit.Command();
            Mediator.Setup(x => x.Send(It.IsAny<Edit.Command>(), new CancellationToken())).
                ReturnsAsync(ResponseWrapper<Edit>.GetInstance((int)HttpStatusCode.OK, null, true, null));

            DishesController.SetMediatrForTest(Mediator.Object);

            //Action
            var result = DishesController.Edit(new ObjectId().ToString(), getDishesRequestModel);

            //Assert
            Assert.IsType<Task<ActionResult<ResponseWrapper<Edit>>>>(result);
        }

        [Fact]
        public void SetDishActiveStatus_Success_Result()
        {
            DishesController.SetMediatrForTest(Mediator.Object);

            //Action
            var result = DishesController.SetActive(new ObjectId().ToString(), false);

            //Assert
            Assert.IsType<Task<ActionResult<ResponseWrapper<SetActive>>>>(result);
        }
    }
}
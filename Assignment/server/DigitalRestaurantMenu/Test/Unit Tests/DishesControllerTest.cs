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
        [Fact]
        public void CreateNewDish_Success_Result()
        {
            var createNewDishRequestModel = new Create.Command();
            Mediator.Setup(x => x.Send(It.IsAny<Create.Command>(), new CancellationToken())).
                ReturnsAsync(ResponseWrapper<Create>.GetInstance((int)HttpStatusCode.OK, null, true, null));

            var dishesController = new DishesController();
            dishesController.SetMediatrForTest(Mediator.Object);

            //Action
            var result = dishesController.Create(createNewDishRequestModel);

            //Assert
            Assert.IsType<Task<ActionResult<ResponseWrapper<Create>>>>(result);
        }

        [Fact]
        public void GetDishes_Success_Result()
        {
            var dishesController = new DishesController();
            dishesController.SetMediatrForTest(Mediator.Object);

            //Action
            var result = dishesController.List();

            //Assert
            Assert.IsType<Task<ActionResult<ResponseWrapper<List<DishDto>>>>>(result);
        }

        [Fact]
        public void GetDishById_Success_Result()
        {
            var getDishesRequestModel = new Details.Query();
            Mediator.Setup(x => x.Send(It.IsAny<Details.Query>(), new CancellationToken())).
                ReturnsAsync(ResponseWrapper<DishDto>.GetInstance((int)HttpStatusCode.OK, null, true, null));

            var dishesController = new DishesController();
            dishesController.SetMediatrForTest(Mediator.Object);

            //Action
            var result = dishesController.Details(new ObjectId().ToString());

            //Assert
            Assert.IsType<Task<ActionResult<ResponseWrapper<DishDto>>>>(result);
        }

        [Fact]
        public void DeleteDishById_Success_Result()
        {
            var dishesController = new DishesController();
            dishesController.SetMediatrForTest(Mediator.Object);

            //Action
            var result = dishesController.Delete(new ObjectId().ToString());

            //Assert
            Assert.IsType<Task<ActionResult<ResponseWrapper<Delete>>>>(result);
        }

        [Fact]
        public void EditDish_Success_Result()
        {
            var getDishesRequestModel = new Edit.Command();
            Mediator.Setup(x => x.Send(It.IsAny<Edit.Command>(), new CancellationToken())).
                ReturnsAsync(ResponseWrapper<Edit>.GetInstance((int)HttpStatusCode.OK, null, true, null));

            var dishesController = new DishesController();
            dishesController.SetMediatrForTest(Mediator.Object);

            //Action
            var result = dishesController.Edit(new ObjectId().ToString(), getDishesRequestModel);

            //Assert
            Assert.IsType<Task<ActionResult<ResponseWrapper<Edit>>>>(result);
        }

        [Fact]
        public void SetDishActiveStatus_Success_Result()
        {
            var dishesController = new DishesController();
            dishesController.SetMediatrForTest(Mediator.Object);

            //Action
            var result = dishesController.SetActive(new ObjectId().ToString(), false);

            //Assert
            Assert.IsType<Task<ActionResult<ResponseWrapper<SetActive>>>>(result);
        }
    }
}

using CarAPI.Entities;
using CarAPI.Models;
using CarAPI.Payment;
using CarAPI.Repositories_DAL;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace CarFactoryAPI_tests
{
    public class OwnerServiceTests : IDisposable
    {
        public OwnerServiceTests(ITestOutputHelper testOutput) {
            // Test setup
            // open connection with test Database
            // global setup for all test
            testOutput.WriteLine("test start");
        }
        public void Dispose()
        {
            // test clean up
            // reset database state
            // close connection with database
        }
        [Fact]
        public void BuyCar_CarId10_NotExist()
        {
            // Arrang

            IInMemoryContext context = new InMemoryContext();
            ICarsRepository carsRepo = new CarsRepository(context);
            IOwnersRepository ownersRepo = new OwnersRepository(context);
            ICashService cashService = new CashService();

            OwnersService ownersService = new OwnersService(
                carsRepo,ownersRepo,cashService
                );

            BuyCarInput carInput = new BuyCarInput()
            {
                CarId = 10, OwnerId = 5, Amount = 1000
            };

            // Act
           string result = ownersService.BuyCar(carInput);

            // Assert
            Assert.Equal("Car doesn't exist", result);
        }

        [Fact]
        public void BuyCar_CarId1_AlreadySold()
        {
            // Arrange

            // Create moq dependencies
            Mock<ICarsRepository> carRepoMock = new Mock<ICarsRepository>();
            Mock<IOwnersRepository> ownerRepoMock = new Mock<IOwnersRepository>();
            CashService cashServiceMock = new CashService();

            // Build moq Data
            Car car = new Car(1, CarType.Audi, 30);
            Owner owner1 = new Owner(1, "Hesham");
            car.OwnerId = 1;
            car.Owner = owner1;

            Owner owner = null;
            // Setup called methods
            carRepoMock.Setup(m=>m.GetCarById(It.IsAny<int>())).Returns(car);
            ownerRepoMock.Setup(m => m.GetOwnerById(It.IsAny<int>())).Returns(owner);

            // use fake dependencies to create object
            OwnersService ownersService = new OwnersService(carRepoMock.Object,
                ownerRepoMock.Object, cashServiceMock);

            BuyCarInput carInput = new BuyCarInput()
            {
                CarId = 1,
                OwnerId = 2,
                Amount = 1000
            };
            // Act
            string result = ownersService.BuyCar(carInput);

            // Assert
            Assert.Equal("Already sold", result);

        }

        [Fact]
        public void BuyCar_CarAvailabeOwnerNotFound_OwnerNotExist()
        {
            // Arrange
            // Crake moq dependencies 
            Mock<ICarsRepository> carRepoMock = new();
            Mock<IOwnersRepository> ownerRepoMock = new();
            ICashService cashService = new CashService();

            // Build moq data
            Car car = new Car(3,CarType.BMW,50);
            Owner owner = null;

            // setup called methods
            carRepoMock.Setup(m => m.GetCarById(It.IsAny<int>())).Returns(car);
            ownerRepoMock.Setup(m => m.GetOwnerById(It.IsAny<int>())).Returns(owner);

            // use fake dependencies to create object
            OwnersService ownersService = new(carRepoMock.Object,ownerRepoMock.Object,cashService);

            BuyCarInput buyCarInput = new BuyCarInput()
            {
                CarId= 2, OwnerId = 5, Amount = 1000
            };

            // Act
            string result = ownersService.BuyCar(buyCarInput);

            // Assert
            Assert.Equal("Owner doesn't exist", result);
        }

        [Fact]
        public void BuyCar_CarAvailabeOwnerHasCar_HaveCar()
        {
            // Arrange
            // Crake moq dependencies 
            Mock<ICarsRepository> carRepoMock = new();
            Mock<IOwnersRepository> ownerRepoMock = new();
            ICashService cashService = new CashService();

            // Build moq data
            Car car = new Car(3, CarType.BMW, 50);
            Owner owner = new Owner(5,"ali");
            owner.CarId = 5;
            owner.Car = new Car();

            // setup called methods
            carRepoMock.Setup(m => m.GetCarById(It.IsAny<int>())).Returns(car);
            ownerRepoMock.Setup(m => m.GetOwnerById(It.IsAny<int>())).Returns(owner);

            // use fake dependencies to create object
            OwnersService ownersService = new(carRepoMock.Object, ownerRepoMock.Object, cashService);

            BuyCarInput buyCarInput = new BuyCarInput()
            {
                CarId = 3,
                OwnerId = 5,
                Amount = 1000
            };

            // Act
            string result = ownersService.BuyCar(buyCarInput);

            // Assert
            Assert.Equal("Already have car", result);
        }

        
    }
}

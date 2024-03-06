using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactoryLibrary_Test
{
    public class CarStoreTests
    {
        [Fact]
        public void AddCars_listOFCars_carsNotEmpty()
        {
            // Arrange
            CarStore carStore = new CarStore();
            Toyota toyota = new Toyota()
            {
                velocity = 10
            };
            BMW mW = new BMW();
            List<Car> cars = new List<Car>()
            {
                new Toyota(){velocity = 10},
                new Toyota(){velocity = 20},
                new BMW(){velocity = 30},
            };

            // Act
            carStore.AddCars(cars);

            // collection Assert
            //Assert.Empty(carStore.cars);
            Assert.NotEmpty(carStore.cars);

            // check object equality
            Assert.Contains<Car>(toyota, carStore.cars);
            Assert.DoesNotContain<Car>(mW, carStore.cars);

            Assert.Contains(carStore.cars, car=>car.velocity == 10);
        }
    }
}

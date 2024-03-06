using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactoryLibrary_Test
{
    public  class CarFactoryTests
    {
        [Fact]
        public void NewCar_Honda_NotImplementedException()
        {
            // Arrange

            

            // Assert
            Assert.Throws<NotImplementedException>(() =>
            {
                // Act
                CarFactory.NewCar(CarTypes.Honda);
            });
        }
    }
}

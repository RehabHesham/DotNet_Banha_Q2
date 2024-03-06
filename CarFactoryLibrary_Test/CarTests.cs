namespace CarFactoryLibrary_Test
{
    public class CarTests
    {
        [Fact]
        public void IsStopped_velocity0_true()
        {
            //Arrange
            Car car = new Toyota() { velocity = 0 };

            //Act
            bool result = car.IsStopped();

            //Assert
            // Boolean Asserts
            Assert.True(result);
            //Assert.False(result);
        }

        [Theory]
        [InlineData(10, 5, 15)]
        [InlineData(12, 5, 17)]
        public void IncreaseVelocity_startSpeed_endisStartandincrease
            (double startSpeed, double increase, double endSpeed)

        {
            // Arrange 
            Car car = new Toyota() { velocity = startSpeed };

            // Act
            car.IncreaseVelocity(increase);
            var result = car.velocity;

            // Equality Assert
            Assert.Equal(endSpeed, result);
            Assert.NotEqual(startSpeed, result);
            Assert.NotEqual(increase, result);
        }

        [Fact]
        public void TimeToCoverDistance_Velocity20Distance40_time2()
        {
            // Arrange
            Car car = new Toyota() { velocity = 20 };

            // Act
            var result = car.TimeToCoverDistance(40);

            // Numeric Assert
            //Assert.Equal(2, result);
            Assert.InRange(result, 1, 3);
            Assert.NotInRange(result, 5, 7);
        }

        [Fact]
        public void GetDirection_DrivingModeForward_Forward()
        {
            // Arrange
            Car car = new Toyota() { drivingMode = DrivingMode.Forward };

            // Act
            var result = car.GetDirection();

            // String Asserts
            //Assert.Equal("Forward", result);
            Assert.StartsWith("F", result);
            Assert.EndsWith("d", result);
            Assert.Contains("wa", result);
            Assert.DoesNotContain("zod", result);
            //Assert.Matches("Regex", result);
            //Assert.DoesNotMatch("regex", result);
        }

        [Fact]
        public void GetMyCar_ToyotaObject_same()
        {
            //Arrange
            Car car = new Toyota() { velocity = 10, drivingMode = DrivingMode.Forward };

            // Act
            var result = car.GetMyCar();

            // Refrence Asserts
            //Assert.Null(result);
            Assert.NotNull(result);
            Assert.Same(car, result);
            // Assert.NotSame(car, result);


            // Type Assert
            Assert.IsType<Toyota>(result);
            Assert.IsAssignableFrom<Car>(result);
        }

    }
}


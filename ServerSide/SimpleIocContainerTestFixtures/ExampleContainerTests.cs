using System;
using SimpleIocContainer;
using SimpleIocContainerTestFixtures.TestInterfaces;
using Xunit;

namespace SimpleIocContainerTestFixtures
{
    public class ExampleContainerTests
    {
        [Fact]
        public void GivenAnInteraceAndClassThatUsesIt_ContainerReturnsInstance()
        {
            var container = new ExampleContainer();
            container.Register<ISimple, Simple>();

            var actualInstance = container.Create<ISimple>();
            actualInstance.Message = "test";

            Assert.IsType<Simple>(actualInstance);
        }

        [Fact]
        public void GivenAnInterfaceIsntRegistered_throwsASimpleContainerException()
        {
            var container = new ExampleContainer();

            Assert.Throws<SimpleContainerException>(() => container.Create<ISimple>());
        }

        [Fact]
        public void GivenTwoInterfacesAndOneImplementationHasADependencyOnTheOther_InstanceIsCreated()
        {
            var container = new ExampleContainer();
                container.Register<ISimpleTwo, SimpleTwo>();
                container.Register<ISimpleLogger, SimpleLogger>();

                var actualInstance = container.Create<ISimpleTwo>();

                Assert.IsType<SimpleTwo>(actualInstance);
                Assert.IsType<SimpleLogger>((actualInstance as SimpleTwo).CheckLogger);
        }

    }
}

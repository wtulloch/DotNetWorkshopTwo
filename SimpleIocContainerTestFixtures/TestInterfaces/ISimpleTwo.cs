namespace SimpleIocContainerTestFixtures.TestInterfaces
{
    public interface ISimpleTwo
    {
        void DoSomething();
    }

    class SimpleTwo : ISimpleTwo
    {
        private ISimpleLogger _logger;

        public SimpleTwo(ISimpleLogger logger)
        {
            _logger = logger;
        }

        public ISimpleLogger CheckLogger => _logger;

        public void DoSomething()
        {
            _logger.LogMessage($"Called DoSomething()");
        }
    }
}
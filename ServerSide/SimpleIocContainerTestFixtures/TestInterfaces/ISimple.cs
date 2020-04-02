namespace SimpleIocContainerTestFixtures.TestInterfaces
{
    public interface ISimple
    {
        string Message { get; set; }
    }

    class Simple : ISimple
    {
        public string Message { get; set; }
    }
}
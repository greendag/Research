using ConfigurationManager.ConcreteClasses;

namespace AdcServices
{
    public interface IAnalogReaderFactory
    {
        IAnalogReader OpenChannel(Channel channel);
    }
}
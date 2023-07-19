using ConfigurationManager.ConcreteClasses;

namespace ConfigurationManager
{
    public interface IConfiguration
    {
        WaterHeater WaterHeater { get; set; }
    }
}
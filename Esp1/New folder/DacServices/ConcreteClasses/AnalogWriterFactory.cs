using Esp32;
using NanoFrameworkWrapper.Dac;

namespace DacServices.ConcreteClasses
{
    public class AnalogWriterFactory : IAnalogWriterFactory
    {
        #region Fields

        private readonly IHardware _hardware;
        private readonly IAnalogWriterController _analogWriterController;
        private readonly IDacFactory _dacFactory;

        #endregion
        
        #region Public Methods

        public IAnalogWriter OpenChannel(int channelNumber)
        {
            return new AnalogWriter(_hardware, _analogWriterController, _dacFactory, channelNumber);
        }

        #endregion

        #region Initialization

        public AnalogWriterFactory(IHardware hardware, IAnalogWriterController analogWriterController, IDacFactory dacFactory)
        {
            _hardware = hardware;
            _analogWriterController = analogWriterController;
            _dacFactory = dacFactory;
        }

        #endregion
    }
}

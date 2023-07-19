using NanoFrameworkWrapper.Adc;

namespace AdcServices.ConcreteClasses
{
    public class AdcSensorController : IAdcSensorController
    {
        #region Fields

        private readonly IAdcController _adcController;

        #endregion

        #region Properties

        public int MinValue => _adcController.MinValue;

        public int MaxValue => _adcController.MaxValue;

        public int ResolutionInBits => _adcController.ResolutionInBits;

        public ILut Lut { get; set; }

        #endregion

        #region Initialization

        public AdcSensorController(IAdcController adcController)
        {
            _adcController = adcController;
        }

        #endregion
    }
}

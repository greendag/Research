namespace NanoFrameworkWrapper.Adc.ConcreteClasses
{
    public class AdcFactory : IAdcFactory
    {
        #region Fields

        private readonly IAdcController _adcController;

        #endregion

        #region Initialization

        public AdcFactory(IAdcController adcController)
        {
            _adcController = adcController;
        }

        #endregion

        #region Public Methods

        public IAdcChannel OpenChannel(int channelNumber)
        {
            return new AdcChannel(_adcController, channelNumber);
        }

        #endregion
    }
}

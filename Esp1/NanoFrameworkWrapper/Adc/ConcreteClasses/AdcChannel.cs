namespace NanoFrameworkWrapper.Adc.ConcreteClasses
{
    public class AdcChannel : IAdcChannel, IAdcChannelBase
    {
        #region Fields

        private readonly System.Device.Adc.AdcChannel _adcChannelBase;

        #endregion

        #region Properties

        System.Device.Adc.AdcChannel IAdcChannelBase.AdcChannel => _adcChannelBase;

        public IAdcController Controller { get; }

        public int ChannelNumber { get; }

        #endregion

        #region Initialization

        public AdcChannel(IAdcController adcController, int channelNumber)
        {
            Controller = adcController;
            ChannelNumber = channelNumber;

            if (adcController is IAdcControllerBase adcControllerBase)
            {
                _adcChannelBase = adcControllerBase.AdcController.OpenChannel(channelNumber);
            }
        }

        #endregion

        #region Public Methods

        public int ReadValue() => _adcChannelBase.ReadValue();

        public double ReadRatio() => _adcChannelBase.ReadRatio();

        public void Dispose()
        {
            _adcChannelBase.Dispose();
        }

        #endregion
    }
}

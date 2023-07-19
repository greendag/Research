using System.Device.Adc;

namespace NanoFrameworkWrapper.Adc.ConcreteClasses
{
    public class AdcController : IAdcController, IAdcControllerBase
    {
        #region Fields

        private readonly System.Device.Adc.AdcController _baseAdcController;

        #endregion

        #region Properties

        System.Device.Adc.AdcController IAdcControllerBase.AdcController => _baseAdcController;

        public int ChannelCount => _baseAdcController.ChannelCount;

        public AdcChannelMode ChannelMode
        {
            get => _baseAdcController.ChannelMode;
            set => _baseAdcController.ChannelMode = value;
        }

        public int MinValue => _baseAdcController.MinValue;

        public int MaxValue => _baseAdcController.MaxValue;

        public int ResolutionInBits => _baseAdcController.ResolutionInBits;

        #endregion

        #region Initialization

        public AdcController()
        {
            _baseAdcController = new System.Device.Adc.AdcController();
        }

        #endregion

        #region Public Methods

        public bool IsChannelModeSupported(AdcChannelMode channelMode) => _baseAdcController.IsChannelModeSupported(channelMode);

        #endregion
    }
}
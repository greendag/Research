namespace NanoFrameworkWrapper.Dac.ConcreteClasses
{
    public class DacController : IDacController, IDacControllerBase
    {
        #region Fields

        private readonly System.Device.Dac.DacController _baseDacController;

        #endregion

        #region Properties

        System.Device.Dac.DacController IDacControllerBase.DacController => _baseDacController;

        public int ChannelCount => _baseDacController.ChannelCount;

        public ushort MinValue => 0;

        public ushort MaxValue => GetMaxValue(_baseDacController);

        public int ResolutionInBits => _baseDacController.ResolutionInBits;

        #endregion

        #region Initialization

        public DacController()
        {
            _baseDacController = System.Device.Dac.DacController.GetDefault();
        }

        public DacController(System.Device.Dac.DacController baseDacController)
        {
            _baseDacController = baseDacController;
        }

        #endregion

        #region Private Methods

        private ushort GetMaxValue(System.Device.Dac.DacController dacController)
        {
            switch (dacController.ResolutionInBits)
            {
                case 0: return 0;
                case 1: return 1;
                case 2: return 3;
                case 3: return 7;
                case 4: return 15;
                case 5: return 31;
                case 6: return 63;
                case 7: return 127;
                case 8: return 255;
                case 9: return 511;
                case 10: return 1023;
                case 11: return 2047;
                case 12: return 4095;
                case 13: return 8191;
                case 14: return 16383;
                case 15: return 32767;
                case 16: return 65535;
                default: return 65535;
            }
        }

        #endregion
    }
}

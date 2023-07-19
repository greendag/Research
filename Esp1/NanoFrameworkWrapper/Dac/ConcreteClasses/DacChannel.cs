namespace NanoFrameworkWrapper.Dac.ConcreteClasses
{
    public class DacChannel : IDacChannel, IDacChannelBase
    {
        #region Fields

        private readonly System.Device.Dac.DacChannel _dacChannelBase;

        #endregion

        #region Properties

        System.Device.Dac.DacChannel IDacChannelBase.DacChannel => _dacChannelBase;

        public IDacController Controller { get; }

        public int ChannelNumber { get; }

        #endregion

        #region Initialization

        public DacChannel(IDacController dacController, int channelNumber)
        {
            Controller = dacController;
            ChannelNumber = channelNumber;

            if (dacController is IDacControllerBase dacControllerBase)
            {
                _dacChannelBase = dacControllerBase.DacController.OpenChannel(channelNumber);
            }
        }

        #endregion

        #region Public Methods

        public void WriteValue(ushort value) => _dacChannelBase.WriteValue(value);

        public void Dispose()
        {
            _dacChannelBase.Dispose();
        }

        #endregion
    }
}

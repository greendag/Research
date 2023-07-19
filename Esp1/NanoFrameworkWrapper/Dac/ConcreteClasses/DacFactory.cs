namespace NanoFrameworkWrapper.Dac.ConcreteClasses
{
    public class DacFactory : IDacFactory
    {
        #region Fields

        private readonly IDacController _dacController;

        #endregion

        #region Initialization

        public DacFactory(IDacController dacController)
        {
            _dacController = dacController;
        }

        #endregion

        #region Public Methods

        public IDacChannel OpenChannel(int channelNumber)
        {
            return new DacChannel(_dacController, channelNumber);
        }

        #endregion
    }
}

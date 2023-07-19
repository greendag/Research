using NanoFrameworkWrapper.Dac;

namespace DacServices.ConcreteClasses
{
    public class AnalogWriterController : IAnalogWriterController
    {
        #region Fields

        private readonly IDacController _dacController;

        #endregion

        #region Properties

        public ushort MinValue => _dacController.MinValue;

        public ushort MaxValue => _dacController.MaxValue;
        
        public int ResolutionInBits => _dacController.ResolutionInBits;

        #endregion

        #region Initialization

        public AnalogWriterController(IDacController controller)
        {
            _dacController = controller;
        }

        #endregion
    }
}

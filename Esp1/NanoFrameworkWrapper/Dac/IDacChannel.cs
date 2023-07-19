using System;

namespace NanoFrameworkWrapper.Dac
{
    public interface IDacChannel : IDisposable
    {
        IDacController Controller { get; }

        int ChannelNumber { get; }

        void WriteValue(ushort value);
    }
}
using System;

namespace Esp32.ConcreteClasses
{
    public static class Environment
    {
        public static string StackTrace
        {
            get
            {
                try
                {
                    throw new Exception();
                }
                catch (Exception e)
                {
                    return e.StackTrace;
                }
            }
        }
    }
}

using Demo_cs_Cart.Support;
using System;

namespace Demo_cs_Cart.Demo_cs_CartException
{

    /// <summary>
    /// Failure Exception. Print and log a custom message and the original Exception Message
    /// </summary>
    [Serializable]
    class TestFailedException : Demo_cs_CartException
    {
        public TestFailedException(string message)
        : base(message)
        {
            GeneralMethods generalMethods = new GeneralMethods();
            generalMethods.printAndLog(message, true);
        }

    }
}

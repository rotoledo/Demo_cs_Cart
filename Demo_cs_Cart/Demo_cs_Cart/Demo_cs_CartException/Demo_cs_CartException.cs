using System;

namespace Demo_cs_Cart.Demo_cs_CartException
{
    [Serializable]
    class Demo_cs_CartException : Exception
    {
        private string message;

        public Demo_cs_CartException(string message)
        {
            this.message = message;
        }
    }
}

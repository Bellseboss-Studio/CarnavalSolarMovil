using System;

namespace ServiceLocatorPath
{
    public class EnergiaInsuficienteException : Exception
    {
        public EnergiaInsuficienteException()
        {
        }

        public EnergiaInsuficienteException(string message) : base(message)
        {
        }
    }
}
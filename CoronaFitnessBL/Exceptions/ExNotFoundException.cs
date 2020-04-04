using System;

namespace CoronaFitnessBL.Exceptions
{
    public class ExNotFoundException<T> : ApplicationException where T: class
    {
        public ExNotFoundException() : base(typeof(T).ToString())
        {
        }
    }
}
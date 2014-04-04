using System;

namespace EsccWebTeam.Exceptions.Soap
{
    public class SoapExceptionDetails
    {
        #region private fields
        private string _message;
        private string _description;
        private SoapExceptionDetails _innerException;
        #endregion
        #region constructor
        public SoapExceptionDetails(string message, string description)
        {
            _message = message;
            _description = description;
        }
        #endregion
        #region public properties
        public string Message
        {
            get
            {
                return _message;
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
        }
        public SoapExceptionDetails InnerException
        {
            get
            {
                return _innerException;
            }
        }
        /// <param name="innerException">The exception's inner exception.</param>
        public void SetInnerException(SoapExceptionDetails innerException)
        {
            _innerException = innerException;
        }
        #endregion
    }
}

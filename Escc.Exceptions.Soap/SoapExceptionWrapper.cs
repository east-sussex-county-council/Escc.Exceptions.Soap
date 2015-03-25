using System.Web.Services.Protocols;
using System.Xml;

namespace Escc.Exceptions.Soap
{
    /// <summary>
    /// Access properties from the SOAP message contained in the wrapped <see cref="SoapException" />
    /// </summary>
    public class SoapExceptionWrapper
    {
        private readonly SoapException _soapexception;
        private readonly SoapExceptionDetails _innerException;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoapExceptionWrapper"/> class.
        /// </summary>
        /// <param name="soapException">The SOAP exception.</param>
        public SoapExceptionWrapper(SoapException soapException)
        {
            _soapexception = soapException;
            SoapExceptionDetails currentException = null;

            //Get all the innerexceptions
            XmlNode node = this.SoapException.Detail.SelectSingleNode("detail");


            while (node != null)
            {
                SoapExceptionDetails newException = new SoapExceptionDetails(node.SelectSingleNode("description").InnerText
                    , node.SelectSingleNode("message").InnerText);

                if (currentException == null)
                {
                    _innerException = newException;
                }
                else
                {
                    currentException.InnerException = newException;
                }
                currentException = newException;

                node = node.SelectSingleNode("detail");
            }
        }

        /// <summary>
        /// Gets the exception message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message
        {
            get
            {
                string retVal = string.Empty;
                if (this.SoapException.Detail != null && this.SoapException.Detail.HasChildNodes)
                {
                    retVal = this.SoapException.Detail.SelectSingleNode("message").InnerText;
                }
                return retVal;
            }
        }

        /// <summary>
        /// Gets the exception description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description
        {
            get
            {
                string retVal = string.Empty;
                if (this.SoapException.Detail != null && this.SoapException.Detail.HasChildNodes)
                {
                    retVal = this.SoapException.Detail.SelectSingleNode("description").InnerText;
                }
                return retVal;
            }
        }

        /// <summary>
        /// Gets the <see cref="SoapException"/>
        /// </summary>
        /// <value>
        /// The SOAP exception.
        /// </value>
        public SoapException SoapException
        {
            get
            {
                return _soapexception;
            }
        }

        /// <summary>
        /// Gets the inner exception.
        /// </summary>
        /// <value>
        /// The inner exception.
        /// </value>
        public SoapExceptionDetails InnerException
        {
            get
            {
                return _innerException;
            }
        }
    }
}

using System;
using System.Web.Services.Protocols;
using System.Xml;

namespace EsccWebTeam.Exceptions.Soap
{
    public class SoapExceptionHelper
    {
        #region private fields
        private SoapException _soapexception;
        private SoapExceptionDetails _innerException;
        #endregion
        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SoapExceptionHelper"/> class.
        /// </summary>
        /// <param name="soapException">The SOAP exception.</param>
        public SoapExceptionHelper(SoapException soapException)
        {
            _soapexception = soapException;
            SoapExceptionDetails newException;
            SoapExceptionDetails currentException = null;

            //Get all the innerexceptions
            XmlNode node = this.SoapeXception.Detail.SelectSingleNode("detail");


            while (node != null)
            {
                newException = new SoapExceptionDetails(node.SelectSingleNode("description").InnerText
                    , node.SelectSingleNode("message").InnerText);

                if (currentException == null)
                {
                    _innerException = newException;
                }
                else
                {
                    currentException.SetInnerException(newException);
                }
                currentException = newException;

                node = node.SelectSingleNode("detail");
            }
        }
        #endregion
        #region public properties
        public string Message
        {
            get
            {
                string retVal = string.Empty;
                if (this.SoapeXception.Detail != null && this.SoapeXception.Detail.HasChildNodes)
                {
                    retVal = this.SoapeXception.Detail.SelectSingleNode("message").InnerText;
                }
                return retVal;
            }
        }
        public string Description
        {
            get
            {
                string retVal = string.Empty;
                if (this.SoapeXception.Detail != null && this.SoapeXception.Detail.HasChildNodes)
                {
                    retVal = this.SoapeXception.Detail.SelectSingleNode("description").InnerText;
                }
                return retVal;
            }
        }
        public SoapException SoapeXception
        {
            get
            {
                return _soapexception;
            }
        }
        public SoapExceptionDetails InnerException
        {
            get
            {
                return _innerException;
            }
        }
        #endregion
    }
}

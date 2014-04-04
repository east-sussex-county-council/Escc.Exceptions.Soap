using System;
using System.Web.Services.Protocols;
using System.Xml;

namespace EsccWebTeam.Exceptions.Soap
{
    public class SoapExceptionEngine
    {
        /// <param name="message">The error message.</param>	
        /// <param name="description">A description of the error message.</param>
        /// <param name="innerException">The inner exception of the error.</param>
        /// <param name="actor">A valid actor.</param>
        /// <seealso cref="SoapException">
        /// SoapException class.
        /// </seealso>			
        public static SoapException GetSoapException(string message, string description, Exception innerException, string actor)
        {
            XmlDocument doc = new XmlDocument();

            //Create main node
            XmlNode mainnode = GetDetailNode(doc, message, description);

            //Add detail node for InnerExceptions
            if (innerException != null)
            {
                XmlNode innerExceptionDetail = GetDetailNode(doc, innerException.Message, innerException.StackTrace);
                mainnode.AppendChild(innerExceptionDetail);

                //Add detail nodes for InnerExceptions recursivly
                XmlNode currentDetail = innerExceptionDetail;
                innerException = innerException.InnerException;
                while (innerException != null)
                {
                    innerExceptionDetail = GetDetailNode(doc, innerException.Message, innerException.StackTrace.Trim());
                    currentDetail.AppendChild(innerExceptionDetail);
                    currentDetail = innerExceptionDetail;
                    innerException = innerException.InnerException;
                }
            }

            //Create the SoapException
            SoapException soapexception = new SoapException(message, SoapException.ServerFaultCode, actor, mainnode, innerException);

            return soapexception;
        }
        /// <param name="doc">An xml document containing the exception details.</param>
        /// <param name="message">The error message</param>
        /// <param name="description">A description of the error message.</param>		
        /// <seealso cref="XmlNode">
        /// </seealso>			
        public static XmlNode GetDetailNode(XmlDocument doc, string message, string description)
        {
            XmlNode detailNode =
                doc.CreateNode(XmlNodeType.Element,
                SoapException.DetailElementName.Name,
                SoapException.DetailElementName.Namespace);

            XmlNode messageNode = doc.CreateNode(XmlNodeType.Element, "message", null);
            XmlNode messageTextNode = doc.CreateNode(XmlNodeType.Text, "messagetext", null);
            messageTextNode.Value = message;
            messageNode.AppendChild(messageTextNode);

            XmlNode descrNode = doc.CreateNode(XmlNodeType.Element, "description", null);
            XmlNode descrTextNode = doc.CreateNode(XmlNodeType.Text, "descriptiontext", null);
            descrTextNode.Value = description;
            descrNode.AppendChild(descrTextNode);

            detailNode.AppendChild(messageNode);
            detailNode.AppendChild(descrNode);

            return detailNode;
        }
        /// <param name="message">The error message</param>
        /// <param name="description">A description of the error message.</param>		
        /// <param name="innerException">The inner exception of the error.</param>
        public static SoapException GetSoapException(string message, string description, Exception innerException)
        {
            return GetSoapException(message, description, innerException, string.Empty);
        }
        /// <param name="message">The error message</param>
        /// <param name="description">A description of the error message.</param>		
        public static SoapException GetSoapException(string message, string description)
        {
            return GetSoapException(message, description, null, string.Empty);
        }
        /// <param name="message">The error message</param>
        /// <param name="innerException">The inner exception of the error.</param>
        public static SoapException GetSoapException(string message, Exception innerException)
        {
            return GetSoapException(message, string.Empty, innerException);
        }
        /// <param name="message">The error message</param>
        public static SoapException GetSoapException(string message)
        {
            return GetSoapException(message, string.Empty);
        }
    }
}


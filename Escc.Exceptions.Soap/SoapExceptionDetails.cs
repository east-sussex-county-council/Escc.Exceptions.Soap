namespace Escc.Exceptions.Soap
{
    /// <summary>
    /// Exception details extracted from a SOAP message
    /// </summary>
    public class SoapExceptionDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SoapExceptionDetails"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="description">The description.</param>
        public SoapExceptionDetails(string message, string description)
        {
            Message = message;
            Description = description;
        }

        /// <summary>
        /// Gets the exception message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets the exception description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the inner exception.
        /// </summary>
        /// <value>
        /// The inner exception.
        /// </value>
        public SoapExceptionDetails InnerException { get; set; }
    }
}

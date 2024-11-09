namespace Smartwyre.Domain.Entities.ErrorHandler
{
    using Smartwyre.Domain.Entities.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The bonos exception.
    /// </summary>
    public class ExceptionGeneric : Exception
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public ExceptionGenericTypes Type { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public ErrorCodes ErrorCode { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionGeneric"/> class.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        public ExceptionGeneric(ExceptionGenericTypes type)
        {
            this.Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionGeneric"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public ExceptionGeneric(string message, ErrorCodes errorCode) : base(message)
        {
            this.Type = ExceptionGenericTypes.Generic;
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionGeneric"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public ExceptionGeneric(string message) : base(message)
        {
            this.Type = ExceptionGenericTypes.Generic;
        }

        public ExceptionGeneric(ExceptionGenericTypes exceptionType, string message, ErrorCodes errorCodes) : base(message)
        {
            if (exceptionType != ExceptionGenericTypes.Validations)
            {
                Console.WriteLine(message);
            }

            this.Type = exceptionType;
            this.ErrorCode = errorCodes;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionGeneric"/> class.
        /// </summary>
        /// <param name="exceptionType">
        /// The exception type.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        public ExceptionGeneric(ExceptionGenericTypes exceptionType, string message) : base(message)
        {
            if (exceptionType != ExceptionGenericTypes.Validations)
            {
                Console.WriteLine(message);
            }

            this.Type = exceptionType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionGeneric"/> class.
        /// </summary>
        /// <param name="exceptionType">
        /// The exception type.
        /// </param>
        /// <param name="messages">
        /// The messages.
        /// </param>
        public ExceptionGeneric(ExceptionGenericTypes exceptionType, List<string> messages) : base(string.Join("\r\n", messages.Select(r => $"*{r}")))
        {
            if (exceptionType != ExceptionGenericTypes.Validations)
            {
                Console.WriteLine(string.Join(" | ", messages));
            }

            this.Type = exceptionType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionGeneric"/> class.
        /// </summary>
        /// <param name="exceptionType">
        /// The exception type.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public ExceptionGeneric(ExceptionGenericTypes exceptionType, string message, Exception innerException) : base(message, innerException)
        {
            if (exceptionType != ExceptionGenericTypes.Validations)
            {
                Console.WriteLine($"error: {message} | innerException: {innerException?.Message}");
            }

            this.Type = exceptionType;
        }
    }
}

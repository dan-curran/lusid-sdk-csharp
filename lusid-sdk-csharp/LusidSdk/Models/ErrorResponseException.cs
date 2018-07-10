// <auto-generated>
// Copyright © 2018 FINBOURNE TECHNOLOGY LTD
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
// </auto-generated>

namespace Finbourne.Models
{
    using Microsoft.Rest;

    /// <summary>
    /// Exception thrown for an invalid response with ErrorResponse
    /// information.
    /// </summary>
    public partial class ErrorResponseException : RestException
    {
        /// <summary>
        /// Gets information about the associated HTTP request.
        /// </summary>
        public HttpRequestMessageWrapper Request { get; set; }

        /// <summary>
        /// Gets information about the associated HTTP response.
        /// </summary>
        public HttpResponseMessageWrapper Response { get; set; }

        /// <summary>
        /// Gets or sets the body object.
        /// </summary>
        public ErrorResponse Body { get; set; }

        /// <summary>
        /// Initializes a new instance of the ErrorResponseException class.
        /// </summary>
        public ErrorResponseException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ErrorResponseException class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public ErrorResponseException(string message)
            : this(message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ErrorResponseException class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">Inner exception.</param>
        public ErrorResponseException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

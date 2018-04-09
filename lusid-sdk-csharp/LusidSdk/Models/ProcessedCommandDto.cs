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
    using Newtonsoft.Json;
    using System.Linq;

    public partial class ProcessedCommandDto
    {
        /// <summary>
        /// Initializes a new instance of the ProcessedCommandDto class.
        /// </summary>
        public ProcessedCommandDto()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ProcessedCommandDto class.
        /// </summary>
        /// <param name="userId">The user that issued the command.</param>
        /// <param name="processedTime">The as at time of the events published
        /// by the processing of
        /// this command.</param>
        public ProcessedCommandDto(string description = default(string), string path = default(string), string userId = default(string), object processedTime = default(object))
        {
            Description = description;
            Path = path;
            UserId = userId;
            ProcessedTime = processedTime;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the user that issued the command.
        /// </summary>
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the as at time of the events published by the
        /// processing of
        /// this command.
        /// </summary>
        [JsonProperty(PropertyName = "processedTime")]
        public object ProcessedTime { get; set; }

    }
}

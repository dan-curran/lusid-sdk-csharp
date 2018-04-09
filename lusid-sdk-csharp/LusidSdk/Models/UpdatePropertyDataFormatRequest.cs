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
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class UpdatePropertyDataFormatRequest
    {
        /// <summary>
        /// Initializes a new instance of the UpdatePropertyDataFormatRequest
        /// class.
        /// </summary>
        public UpdatePropertyDataFormatRequest()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the UpdatePropertyDataFormatRequest
        /// class.
        /// </summary>
        /// <param name="formatType">Possible values include: 'Basic',
        /// 'Limited', 'Currency'</param>
        /// <param name="valueType">Possible values include: 'String', 'Int',
        /// 'Decimal', 'DateTime', 'Boolean', 'Map', 'PropertyArray',
        /// 'Percentage', 'Currency', 'BenchmarkType', 'Code', 'Id', 'Uri',
        /// 'ArrayOfIds'</param>
        public UpdatePropertyDataFormatRequest(string formatType, int order, string displayName, string valueType, IList<object> acceptableValues = default(IList<object>))
        {
            FormatType = formatType;
            Order = order;
            DisplayName = displayName;
            ValueType = valueType;
            AcceptableValues = acceptableValues;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets possible values include: 'Basic', 'Limited',
        /// 'Currency'
        /// </summary>
        [JsonProperty(PropertyName = "formatType")]
        public string FormatType { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "order")]
        public int Order { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets possible values include: 'String', 'Int', 'Decimal',
        /// 'DateTime', 'Boolean', 'Map', 'PropertyArray', 'Percentage',
        /// 'Currency', 'BenchmarkType', 'Code', 'Id', 'Uri', 'ArrayOfIds'
        /// </summary>
        [JsonProperty(PropertyName = "valueType")]
        public string ValueType { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "acceptableValues")]
        public IList<object> AcceptableValues { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (FormatType == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "FormatType");
            }
            if (DisplayName == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "DisplayName");
            }
            if (ValueType == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "ValueType");
            }
        }
    }
}

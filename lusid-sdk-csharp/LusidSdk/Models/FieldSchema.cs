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
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class FieldSchema
    {
        /// <summary>
        /// Initializes a new instance of the FieldSchema class.
        /// </summary>
        public FieldSchema()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the FieldSchema class.
        /// </summary>
        /// <param name="type">Possible values include: 'String', 'Int',
        /// 'Decimal', 'DateTime', 'Boolean', 'Map', 'List', 'PropertyArray',
        /// 'Percentage', 'Currency', 'BenchmarkType', 'Code', 'Id', 'Uri',
        /// 'ArrayOfIds', 'ArrayOfTxnAliases', 'ArrayofTxnMovements'</param>
        public FieldSchema(string scope = default(string), string name = default(string), string displayName = default(string), string type = default(string), bool? isMetric = default(bool?), int? displayOrder = default(int?), IDictionary<string, FieldSchema> propertySchema = default(IDictionary<string, FieldSchema>))
        {
            Scope = scope;
            Name = name;
            DisplayName = displayName;
            Type = type;
            IsMetric = isMetric;
            DisplayOrder = displayOrder;
            PropertySchema = propertySchema;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets possible values include: 'String', 'Int', 'Decimal',
        /// 'DateTime', 'Boolean', 'Map', 'List', 'PropertyArray',
        /// 'Percentage', 'Currency', 'BenchmarkType', 'Code', 'Id', 'Uri',
        /// 'ArrayOfIds', 'ArrayOfTxnAliases', 'ArrayofTxnMovements'
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "isMetric")]
        public bool? IsMetric { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "displayOrder")]
        public int? DisplayOrder { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "propertySchema")]
        public IDictionary<string, FieldSchema> PropertySchema { get; set; }

    }
}

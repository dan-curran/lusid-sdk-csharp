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

    /// <summary>
    /// This 'dto' contains target holdings. i.e. holding data that the
    /// system should match. When processed by the movement
    /// engine, it will create 'true-up' adjustments on the fly.
    /// </summary>
    public partial class HoldingAdjustment
    {
        /// <summary>
        /// Initializes a new instance of the HoldingAdjustment class.
        /// </summary>
        public HoldingAdjustment()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the HoldingAdjustment class.
        /// </summary>
        /// <param name="instrumentUid">Unique instrument identifier</param>
        /// <param name="taxLots">1 or more quantity amounts</param>
        /// <param name="subHoldingKeys">Key fields to uniquely index the sub
        /// holdings of a instrument</param>
        /// <param name="properties">Arbitrary properties to store with the
        /// holding</param>
        public HoldingAdjustment(string instrumentUid, IList<TargetTaxLot> taxLots, IList<PerpetualProperty> subHoldingKeys = default(IList<PerpetualProperty>), IList<PerpetualProperty> properties = default(IList<PerpetualProperty>))
        {
            InstrumentUid = instrumentUid;
            SubHoldingKeys = subHoldingKeys;
            Properties = properties;
            TaxLots = taxLots;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets unique instrument identifier
        /// </summary>
        [JsonProperty(PropertyName = "instrumentUid")]
        public string InstrumentUid { get; set; }

        /// <summary>
        /// Gets or sets key fields to uniquely index the sub holdings of a
        /// instrument
        /// </summary>
        [JsonProperty(PropertyName = "subHoldingKeys")]
        public IList<PerpetualProperty> SubHoldingKeys { get; set; }

        /// <summary>
        /// Gets or sets arbitrary properties to store with the holding
        /// </summary>
        [JsonProperty(PropertyName = "properties")]
        public IList<PerpetualProperty> Properties { get; set; }

        /// <summary>
        /// Gets or sets 1 or more quantity amounts
        /// </summary>
        [JsonProperty(PropertyName = "taxLots")]
        public IList<TargetTaxLot> TaxLots { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (InstrumentUid == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "InstrumentUid");
            }
            if (TaxLots == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "TaxLots");
            }
            if (SubHoldingKeys != null)
            {
                foreach (var element in SubHoldingKeys)
                {
                    if (element != null)
                    {
                        element.Validate();
                    }
                }
            }
            if (Properties != null)
            {
                foreach (var element1 in Properties)
                {
                    if (element1 != null)
                    {
                        element1.Validate();
                    }
                }
            }
            if (TaxLots != null)
            {
                foreach (var element2 in TaxLots)
                {
                    if (element2 != null)
                    {
                        element2.Validate();
                    }
                }
            }
        }
    }
}

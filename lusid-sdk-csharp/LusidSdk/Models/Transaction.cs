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

    public partial class Transaction
    {
        /// <summary>
        /// Initializes a new instance of the Transaction class.
        /// </summary>
        public Transaction()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the Transaction class.
        /// </summary>
        /// <param name="transactionId">Unique transaction identifier</param>
        /// <param name="type">LUSID transaction type code - Buy, Sell,
        /// StockIn, StockOut, etc</param>
        /// <param name="instrumentUid">Unique instrument identifier</param>
        /// <param name="transactionDate">Transaction date</param>
        /// <param name="settlementDate">Settlement date</param>
        /// <param name="units">Quantity of transaction in units of the
        /// instrument</param>
        /// <param name="transactionPrice">Execution price for the
        /// transaction</param>
        /// <param name="totalConsideration">Total value of the transaction in
        /// settlement currency</param>
        /// <param name="source">Where this transaction came from. Possible
        /// values include: 'System', 'Client'</param>
        /// <param name="exchangeRate">Rate between transaction and settle
        /// currency</param>
        /// <param name="transactionCurrency">Transaction currency</param>
        /// <param name="counterpartyId">Counterparty identifier</param>
        public Transaction(string transactionId, string type, string instrumentUid, System.DateTimeOffset transactionDate, System.DateTimeOffset settlementDate, double units, TransactionPrice transactionPrice, CurrencyAndAmount totalConsideration, string source, double? exchangeRate = default(double?), string transactionCurrency = default(string), IList<PerpetualProperty> properties = default(IList<PerpetualProperty>), string counterpartyId = default(string), string nettingSet = default(string))
        {
            TransactionId = transactionId;
            Type = type;
            InstrumentUid = instrumentUid;
            TransactionDate = transactionDate;
            SettlementDate = settlementDate;
            Units = units;
            TransactionPrice = transactionPrice;
            TotalConsideration = totalConsideration;
            ExchangeRate = exchangeRate;
            TransactionCurrency = transactionCurrency;
            Properties = properties;
            CounterpartyId = counterpartyId;
            Source = source;
            NettingSet = nettingSet;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets unique transaction identifier
        /// </summary>
        [JsonProperty(PropertyName = "transactionId")]
        public string TransactionId { get; set; }

        /// <summary>
        /// Gets or sets LUSID transaction type code - Buy, Sell, StockIn,
        /// StockOut, etc
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets unique instrument identifier
        /// </summary>
        [JsonProperty(PropertyName = "instrumentUid")]
        public string InstrumentUid { get; set; }

        /// <summary>
        /// Gets or sets transaction date
        /// </summary>
        [JsonProperty(PropertyName = "transactionDate")]
        public System.DateTimeOffset TransactionDate { get; set; }

        /// <summary>
        /// Gets or sets settlement date
        /// </summary>
        [JsonProperty(PropertyName = "settlementDate")]
        public System.DateTimeOffset SettlementDate { get; set; }

        /// <summary>
        /// Gets or sets quantity of transaction in units of the instrument
        /// </summary>
        [JsonProperty(PropertyName = "units")]
        public double Units { get; set; }

        /// <summary>
        /// Gets or sets execution price for the transaction
        /// </summary>
        [JsonProperty(PropertyName = "transactionPrice")]
        public TransactionPrice TransactionPrice { get; set; }

        /// <summary>
        /// Gets or sets total value of the transaction in settlement currency
        /// </summary>
        [JsonProperty(PropertyName = "totalConsideration")]
        public CurrencyAndAmount TotalConsideration { get; set; }

        /// <summary>
        /// Gets or sets rate between transaction and settle currency
        /// </summary>
        [JsonProperty(PropertyName = "exchangeRate")]
        public double? ExchangeRate { get; set; }

        /// <summary>
        /// Gets or sets transaction currency
        /// </summary>
        [JsonProperty(PropertyName = "transactionCurrency")]
        public string TransactionCurrency { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "properties")]
        public IList<PerpetualProperty> Properties { get; set; }

        /// <summary>
        /// Gets or sets counterparty identifier
        /// </summary>
        [JsonProperty(PropertyName = "counterpartyId")]
        public string CounterpartyId { get; set; }

        /// <summary>
        /// Gets or sets where this transaction came from. Possible values
        /// include: 'System', 'Client'
        /// </summary>
        [JsonProperty(PropertyName = "source")]
        public string Source { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "nettingSet")]
        public string NettingSet { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (TransactionId == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "TransactionId");
            }
            if (Type == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Type");
            }
            if (InstrumentUid == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "InstrumentUid");
            }
            if (TransactionPrice == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "TransactionPrice");
            }
            if (TotalConsideration == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "TotalConsideration");
            }
            if (Source == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Source");
            }
            if (Properties != null)
            {
                foreach (var element in Properties)
                {
                    if (element != null)
                    {
                        element.Validate();
                    }
                }
            }
        }
    }
}

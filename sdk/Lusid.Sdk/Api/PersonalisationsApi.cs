/* 
 * LUSID API
 *
 * # Introduction  This page documents the [LUSID APIs](https://api.lusid.com/swagger), which allows authorised clients to query and update their data within the LUSID platform.  SDKs to interact with the LUSID APIs are available in the following languages :  * [C#](https://github.com/finbourne/lusid-sdk-csharp) * [Java](https://github.com/finbourne/lusid-sdk-java) * [JavaScript](https://github.com/finbourne/lusid-sdk-js) * [Python](https://github.com/finbourne/lusid-sdk-python)  # Data Model  The LUSID API has a relatively lightweight but extremely powerful data model.   One of the goals of LUSID was not to enforce on clients a single rigid data model but rather to provide a flexible foundation onto which clients can streamline their data.   One of the primary tools to extend the data model is through using properties.  Properties can be associated with amongst others: - * Transactions * Instruments * Portfolios   The LUSID data model is exposed through the LUSID APIs.  The APIs provide access to both business objects and the meta data used to configure the systems behaviours.   The key business entities are: - * **Portfolios** A portfolio is the primary container for transactions and holdings.  * **Derived Portfolios** Derived portfolios allow portfolios to be created based on other portfolios, by overriding or overlaying specific items * **Holdings** A holding is a position account for a instrument within a portfolio.  Holdings can only be adjusted via transactions. * **Transactions** A Transaction is a source of transactions used to manipulate holdings.  * **Corporate Actions** A corporate action is a market event which occurs to a instrument, for example a stock split * **Instruments**  A instrument represents a currency, tradable instrument or OTC contract that is attached to a transaction and a holding. * **Properties** Several entities allow additional user defined properties to be associated with them.   For example, a Portfolio manager may be associated with a portfolio  Meta data includes: - * **Transaction Types** Transactions are booked with a specific transaction type.  The types are client defined and are used to map the Transaction to a series of movements which update the portfolio holdings.  * **Properties Types** Types of user defined properties used within the system.  This section describes the data model that LUSID exposes via the APIs.  ## Scope  All data in LUSID is segregated at the client level.  Entities in LUSID are identifiable by a unique code.  Every entity lives within a logical data partition known as a Scope.  Scope is an identity namespace allowing two entities with the same unique code to co-exist within individual address spaces.  For example, prices for equities from different vendors may be uploaded into different scopes such as `client/vendor1` and `client/vendor2`.  A portfolio may then be valued using either of the price sources by referencing the appropriate scope.  LUSID Clients cannot access scopes of other clients.  ## Schema  A detailed description of the entities used by the API and parameters for endpoints which take a JSON document can be retrieved via the `schema` endpoint.  ## Instruments  LUSID has its own built-in instrument master which you can use to master your own instrument universe.  Every instrument must be created with one or more unique market identifiers, such as [FIGI](https://openfigi.com/) or RIC code. For any non-listed instruments (eg OTCs), you can upload an instrument against a custom ID of your choosing.  In addition, LUSID will allocate each instrument a unique 'LUSID instrument identifier'. The LUSID instrument identifier is what is used when uploading transactions, holdings, prices, etc. The API exposes an `instrument/lookup` endpoint which can be used to lookup these LUSID identifiers using their market identifiers.  Cash can be referenced using the ISO currency code prefixed with \"`CCY_`\" e.g. `CCY_GBP`  ## Instrument Prices (Analytics)  Instrument prices are stored in LUSID's Analytics Store  | Field|Type|Description | | - --|- --|- -- | | InstrumentUid|string|Unique instrument identifier | | Value|decimal|Value of the analytic, eg price | | Denomination|string|Underlying unit of the analytic, eg currency, EPS etc. |   ## Instrument Data  Instrument data can be uploaded to the system using the [Instrument Properties](#tag/InstrumentProperties) endpoint.  | Field|Type|Description | | - --|- --|- -- | | LusidInstrumentId|string|Unique instrument identifier |   ## Portfolios  Portfolios are the top-level entity containers within LUSID, containing transactions, corporate actions and holdings.    The transactions build up the portfolio holdings on which valuations, analytics profit & loss and risk can be calculated.     Properties can be associated with Portfolios to add in additional model data.  Portfolio properties can be changed over time as well.  For example, to allow a Portfolio Manager to be linked with a Portfolio.  Additionally, portfolios can be securitised and held by other portfolios, allowing LUSID to perform \"drill-through\" into underlying fund holdings  ### Reference Portfolios Reference portfolios are portfolios that contain only weights, as opposed to transactions, and are designed to represent entities such as indices.  ### Derived Portfolios  LUSID also allows for a portfolio to be composed of another portfolio via derived portfolios.  A derived portfolio can contain its own transactions and also inherits any transactions from its parent portfolio.  Any changes made to the parent portfolio are automatically reflected in derived portfolio.  Derived portfolios in conjunction with scopes are a powerful construct.  For example, to do pre-trade what-if analysis, a derived portfolio could be created a new namespace linked to the underlying live (parent) portfolio.  Analysis can then be undertaken on the derived portfolio without affecting the live portfolio.  ### Portfolio Groups Portfolio groups allow the construction of a hierarchy from portfolios and groups.  Portfolio operations on the group are executed on an aggregated set of portfolios in the hierarchy.   For example:   * Global Portfolios _(group)_   * APAC _(group)_     * Hong Kong _(portfolio)_     * Japan _(portfolio)_   * Europe _(group)_     * France _(portfolio)_     * Germany _(portfolio)_   * UK _(portfolio)_   In this example **Global Portfolios** is a group that consists of an aggregate of **Hong Kong**, **Japan**, **France**, **Germany** and **UK** portfolios.  ### Movements Engine The Movements engine sits on top of the immutable event store and is used to manage the relationship between input trading actions and their associated portfolio holdings.     The movements engine reads in the following entity types:- * Posting Transactions * Applying Corporate Actions  * Holding Adjustments  These are converted to one or more movements and used by the movements engine to calculate holdings.  At the same time it also calculates running balances, and realised P&L.  The outputs from the movements engine are holdings and transactions.  ## Transactions  A transaction represents an economic activity against a Portfolio.  Transactions are processed according to a configuration. This will tell the LUSID engine how to interpret the transaction and correctly update the holdings. LUSID comes with a set of transaction types you can use out of the box, or you can configure your own set(s) of transactions.  For more details see the [LUSID Getting Started Guide for transaction configuration.](https://support.finbourne.com/hc/en-us/articles/360016737511-Configuring-Transaction-Types)  | Field|Type|Description | | - --|- --|- -- | | TransactionId|string|Unique transaction identifier | | Type|string|LUSID transaction type code - Buy, Sell, StockIn, StockOut, etc | | InstrumentUid|string|Unique instrument identifier | | TransactionDate|datetime|Transaction date | | SettlementDate|datetime|Settlement date | | Units|decimal|Quantity of transaction in units of the instrument | | TransactionPrice|tradeprice|Execution price for the transaction | | TotalConsideration|currencyandamount|Total value of the transaction in settlement currency | | ExchangeRate|decimal|Rate between transaction and settle currency | | TransactionCurrency|currency|Transaction currency | | CounterpartyId|string|Counterparty identifier | | Source|string|Where this transaction came from | | NettingSet|string|  |   From these fields, the following values can be calculated  * **Transaction value in Transaction currency**: TotalConsideration / ExchangeRate  * **Transaction value in Portfolio currency**: Transaction value in Transaction currency * TradeToPortfolioRate  ### Example Transactions  #### A Common Purchase Example Three example transactions are shown in the table below.   They represent a purchase of USD denominated IBM shares within a Sterling denominated portfolio.   * The first two transactions are for separate buy and fx trades    * Buying 500 IBM shares for $71,480.00    * A foreign exchange conversion to fund the IBM purchase. (Buy $71,480.00 for &#163;54,846.60)  * The third transaction is an alternate version of the above trades. Buying 500 IBM shares and settling directly in Sterling.  | Column |  Buy Trade | Fx Trade | Buy Trade with foreign Settlement | | - -- -- | - -- -- | - -- -- | - -- -- | | TransactionId | FBN00001 | FBN00002 | FBN00003 | | Type | Buy | FxBuy | Buy | | InstrumentUid | FIGI_BBG000BLNNH6 | CCY_USD | FIGI_BBG000BLNNH6 | | TransactionDate | 2018-08-02 | 2018-08-02 | 2018-08-02 | | SettlementDate | 2018-08-06 | 2018-08-06 | 2018-08-06 | | Units | 500 | 71480 | 500 | | TransactionPrice | 142.96 | 1 | 142.96 | | TradeCurrency | USD | USD | USD | | ExchangeRate | 1 | 0.7673 | 0.7673 | | TotalConsideration.Amount | 71480.00 | 54846.60 | 54846.60 | | TotalConsideration.Currency | USD | GBP | GBP | | Trade/default/TradeToPortfolioRate&ast; | 0.7673 | 0.7673 | 0.7673 |  [&ast; This is a property field]  #### A Forward FX Example  LUSID has a flexible transaction modelling system, and there are a number of different ways of modelling forward fx trades.  The default LUSID transaction types are FwdFxBuy and FwdFxSell. Other types and behaviours can be configured as required.  Using these transaction types, the holdings query will report two forward positions. One in each currency.   Since an FX trade is an exchange of one currency for another, the following two 6 month forward transactions are equivalent:  | Column |  Forward 'Sell' Trade | Forward 'Buy' Trade | | - -- -- | - -- -- | - -- -- | | TransactionId | FBN00004 | FBN00005 | | Type | FwdFxSell | FwdFxBuy | | InstrumentUid | CCY_GBP | CCY_USD | | TransactionDate | 2018-08-02 | 2018-08-02 | | SettlementDate | 2019-02-06 | 2019-02-06 | | Units | 10000.00 | 13142.00 | | TransactionPrice |1 | 1 | | TradeCurrency | GBP | USD | | ExchangeRate | 1.3142 | 0.760919 | | TotalConsideration.Amount | 13142.00 | 10000.00 | | TotalConsideration.Currency | USD | GBP | | Trade/default/TradeToPortfolioRate | 1.0 | 0.760919 |  ## Holdings  A holding represents a position in a instrument or cash on a given date.  | Field|Type|Description | | - --|- --|- -- | | InstrumentUid|string|Unique instrument identifier | | HoldingType|string|Type of holding, eg Position, Balance, CashCommitment, Receivable, ForwardFX | | Units|decimal|Quantity of holding | | SettledUnits|decimal|Settled quantity of holding | | Cost|currencyandamount|Book cost of holding in transaction currency | | CostPortfolioCcy|currencyandamount|Book cost of holding in portfolio currency | | Transaction|Transaction|If this is commitment-type holding, the transaction behind it |   ## Corporate Actions  Corporate actions are represented within LUSID in terms of a set of instrument-specific 'transitions'.  These transitions are used to specify the participants of the corporate action, and the effect that the corporate action will have on holdings in those participants.  *Corporate action*  | Field|Type|Description | | - --|- --|- -- | | SourceId|id|  | | CorporateActionCode|code|  | | AnnouncementDate|datetime|  | | ExDate|datetime|  | | RecordDate|datetime|  | | PaymentDate|datetime|  |    *Transition*  | Field|Type|Description | | - --|- --|- -- |   ## Property  Properties are key-value pairs that can be applied to any entity within a domain (where a domain is `trade`, `portfolio`, `security` etc).  Properties must be defined before use with a `PropertyDefinition` and can then subsequently be added to entities.  # Schemas  The following headers are returned on all responses from LUSID  | Name | Purpose | | - -- | - -- | | lusid-meta-duration | Duration of the request | | lusid-meta-success | Whether or not LUSID considered the request to be successful | | lusid-meta-requestId | The unique identifier for the request | | lusid-schema-url | Url of the schema for the data being returned | | lusid-property-schema-url | Url of the schema for any properties |   # Error Codes  | Code|Name|Description | | - --|- --|- -- | | <a name=\"100\">100</a>|Personalisations not found|The personalisation(s) identified by the pattern provided could not be found, either because it does not exist or it has been deleted. Please check the pattern your provided. | | <a name=\"101\">101</a>|NonRecursivePersonalisation|  | | <a name=\"102\">102</a>|VersionNotFound|  | | <a name=\"104\">104</a>|InstrumentNotFound|  | | <a name=\"105\">105</a>|PropertyNotFound|  | | <a name=\"106\">106</a>|PortfolioRecursionDepth|  | | <a name=\"108\">108</a>|GroupNotFound|  | | <a name=\"109\">109</a>|PortfolioNotFound|  | | <a name=\"110\">110</a>|PropertySchemaNotFound|  | | <a name=\"111\">111</a>|PortfolioAncestryNotFound|  | | <a name=\"112\">112</a>|PortfolioWithIdAlreadyExists|  | | <a name=\"113\">113</a>|OrphanedPortfolio|  | | <a name=\"119\">119</a>|MissingBaseClaims|  | | <a name=\"121\">121</a>|PropertyNotDefined|  | | <a name=\"122\">122</a>|CannotDeleteSystemProperty|  | | <a name=\"123\">123</a>|CannotModifyImmutablePropertyField|  | | <a name=\"124\">124</a>|PropertyAlreadyExists|  | | <a name=\"125\">125</a>|InvalidPropertyLifeTime|  | | <a name=\"127\">127</a>|CannotModifyDefaultDataType|  | | <a name=\"128\">128</a>|GroupAlreadyExists|  | | <a name=\"129\">129</a>|NoSuchDataType|  | | <a name=\"132\">132</a>|ValidationError|  | | <a name=\"133\">133</a>|LoopDetectedInGroupHierarchy|  | | <a name=\"135\">135</a>|SubGroupAlreadyExists|  | | <a name=\"138\">138</a>|PriceSourceNotFound|  | | <a name=\"139\">139</a>|AnalyticStoreNotFound|  | | <a name=\"141\">141</a>|AnalyticStoreAlreadyExists|  | | <a name=\"143\">143</a>|ClientInstrumentAlreadyExists|  | | <a name=\"144\">144</a>|DuplicateInParameterSet|  | | <a name=\"147\">147</a>|ResultsNotFound|  | | <a name=\"148\">148</a>|OrderFieldNotInResultSet|  | | <a name=\"149\">149</a>|OperationFailed|  | | <a name=\"150\">150</a>|ElasticSearchError|  | | <a name=\"151\">151</a>|InvalidParameterValue|  | | <a name=\"153\">153</a>|CommandProcessingFailure|  | | <a name=\"154\">154</a>|EntityStateConstructionFailure|  | | <a name=\"155\">155</a>|EntityTimelineDoesNotExist|  | | <a name=\"156\">156</a>|EventPublishFailure|  | | <a name=\"157\">157</a>|InvalidRequestFailure|  | | <a name=\"158\">158</a>|EventPublishUnknown|  | | <a name=\"159\">159</a>|EventQueryFailure|  | | <a name=\"160\">160</a>|BlobDidNotExistFailure|  | | <a name=\"162\">162</a>|SubSystemRequestFailure|  | | <a name=\"163\">163</a>|SubSystemConfigurationFailure|  | | <a name=\"165\">165</a>|FailedToDelete|  | | <a name=\"166\">166</a>|UpsertClientInstrumentFailure|  | | <a name=\"167\">167</a>|IllegalAsAtInterval|  | | <a name=\"168\">168</a>|IllegalBitemporalQuery|  | | <a name=\"169\">169</a>|InvalidAlternateId|  | | <a name=\"170\">170</a>|CannotAddSourcePortfolioPropertyExplicitly|  | | <a name=\"171\">171</a>|EntityAlreadyExistsInGroup|  | | <a name=\"173\">173</a>|EntityWithIdAlreadyExists|  | | <a name=\"174\">174</a>|PortfolioDetailsDoNotExist|  | | <a name=\"176\">176</a>|PortfolioWithNameAlreadyExists|  | | <a name=\"177\">177</a>|InvalidTransactions|  | | <a name=\"178\">178</a>|ReferencePortfolioNotFound|  | | <a name=\"179\">179</a>|DuplicateIdFailure|  | | <a name=\"180\">180</a>|CommandRetrievalFailure|  | | <a name=\"181\">181</a>|DataFilterApplicationFailure|  | | <a name=\"182\">182</a>|SearchFailed|  | | <a name=\"183\">183</a>|MovementsEngineConfigurationKeyFailure|  | | <a name=\"184\">184</a>|FxRateSourceNotFound|  | | <a name=\"185\">185</a>|AccrualSourceNotFound|  | | <a name=\"186\">186</a>|EntitlementsFailure|  | | <a name=\"187\">187</a>|InvalidIdentityToken|  | | <a name=\"188\">188</a>|InvalidRequestHeaders|  | | <a name=\"189\">189</a>|PriceNotFound|  | | <a name=\"190\">190</a>|InvalidSubHoldingKeysProvided|  | | <a name=\"191\">191</a>|DuplicateSubHoldingKeysProvided|  | | <a name=\"192\">192</a>|CutDefinitionNotFound|  | | <a name=\"193\">193</a>|CutDefinitionInvalid|  | | <a name=\"200\">200</a>|InvalidUnitForDataType|  | | <a name=\"201\">201</a>|InvalidTypeForDataType|  | | <a name=\"202\">202</a>|InvalidValueForDataType|  | | <a name=\"203\">203</a>|UnitNotDefinedForDataType|  | | <a name=\"204\">204</a>|UnitsNotSupportedOnDataType|  | | <a name=\"205\">205</a>|CannotSpecifyUnitsOnDataType|  | | <a name=\"206\">206</a>|UnitSchemaInconsistentWithDataType|  | | <a name=\"207\">207</a>|UnitDefinitionNotSpecified|  | | <a name=\"208\">208</a>|DuplicateUnitDefinitionsSpecified|  | | <a name=\"209\">209</a>|InvalidUnitsDefinition|  | | <a name=\"210\">210</a>|InvalidInstrumentIdentifierUnit|  | | <a name=\"211\">211</a>|HoldingsAdjustmentDoesNotExist|  | | <a name=\"212\">212</a>|CouldNotBuildExcelUrl|  | | <a name=\"213\">213</a>|CouldNotGetExcelVersion|  | | <a name=\"214\">214</a>|InstrumentByCodeNotFound|  | | <a name=\"215\">215</a>|EntitySchemaDoesNotExist|  | | <a name=\"216\">216</a>|FeatureNotSupportedOnPortfolioType|  | | <a name=\"217\">217</a>|QuoteNotFoundFailure|  | | <a name=\"219\">219</a>|InvalidInstrumentDefinition|  | | <a name=\"221\">221</a>|InstrumentUpsertFailure|  | | <a name=\"222\">222</a>|ReferencePortfolioRequestNotSupported|  | | <a name=\"223\">223</a>|TransactionPortfolioRequestNotSupported|  | | <a name=\"224\">224</a>|InvalidPropertyValueAssignment|  | | <a name=\"230\">230</a>|TransactionTypeNotFound|  | | <a name=\"231\">231</a>|TransactionTypeDuplication|  | | <a name=\"232\">232</a>|PortfolioDoesNotExistAtGivenDate|  | | <a name=\"-10\">-10</a>|ServerConfigurationError|  | | <a name=\"-1\">-1</a>|Unknown error|  | 
 *
 * OpenAPI spec version: 0.8.26
 * Contact: info@finbourne.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RestSharp.Portable;
using Lusid.Sdk.Client;
using Lusid.Sdk.Model;

namespace Lusid.Sdk.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IPersonalisationsApi : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Delete a personalisation
        /// </summary>
        /// <remarks>
        /// Delete a personalisation at a specific scope (or use scope ALL to purge the setting entirely)
        /// </remarks>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="key">The key of the setting to be deleted (optional)</param>
        /// <param name="scope">The scope to delete at (use ALL to purge the setting entirely) (optional)</param>
        /// <param name="group">Optional. If deleting a setting at group level, specify the group here (optional)</param>
        /// <returns>DeletedEntityResponse</returns>
        DeletedEntityResponse DeletePersonalisation (string key = null, string scope = null, string group = null);

        /// <summary>
        /// Delete a personalisation
        /// </summary>
        /// <remarks>
        /// Delete a personalisation at a specific scope (or use scope ALL to purge the setting entirely)
        /// </remarks>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="key">The key of the setting to be deleted (optional)</param>
        /// <param name="scope">The scope to delete at (use ALL to purge the setting entirely) (optional)</param>
        /// <param name="group">Optional. If deleting a setting at group level, specify the group here (optional)</param>
        /// <returns>ApiResponse of DeletedEntityResponse</returns>
        ApiResponse<DeletedEntityResponse> DeletePersonalisationWithHttpInfo (string key = null, string scope = null, string group = null);
        /// <summary>
        /// Get personalisation
        /// </summary>
        /// <remarks>
        /// Get a personalisation, recursing to get any referenced if required.
        /// </remarks>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="pattern">The search pattern or specific key (optional)</param>
        /// <param name="scope">Optional. The scope level to request for (optional)</param>
        /// <param name="recursive">Optional. Whether to recurse into dereference recursive settings (optional, default to false)</param>
        /// <param name="wildcards">Optional. Whether to apply wildcards to the provided pattern and pull back any matching (optional, default to false)</param>
        /// <param name="sortBy">Optional. Order the results by these fields. Use use the &#39;-&#39; sign to denote descending order e.g. -MyFieldName (optional)</param>
        /// <param name="start">Optional. When paginating, skip this number of results (optional)</param>
        /// <param name="limit">Optional. When paginating, limit the number of returned results to this many. (optional)</param>
        /// <returns>ResourceListOfPersonalisation</returns>
        ResourceListOfPersonalisation GetPersonalisations (string pattern = null, string scope = null, bool? recursive = null, bool? wildcards = null, List<string> sortBy = null, int? start = null, int? limit = null);

        /// <summary>
        /// Get personalisation
        /// </summary>
        /// <remarks>
        /// Get a personalisation, recursing to get any referenced if required.
        /// </remarks>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="pattern">The search pattern or specific key (optional)</param>
        /// <param name="scope">Optional. The scope level to request for (optional)</param>
        /// <param name="recursive">Optional. Whether to recurse into dereference recursive settings (optional, default to false)</param>
        /// <param name="wildcards">Optional. Whether to apply wildcards to the provided pattern and pull back any matching (optional, default to false)</param>
        /// <param name="sortBy">Optional. Order the results by these fields. Use use the &#39;-&#39; sign to denote descending order e.g. -MyFieldName (optional)</param>
        /// <param name="start">Optional. When paginating, skip this number of results (optional)</param>
        /// <param name="limit">Optional. When paginating, limit the number of returned results to this many. (optional)</param>
        /// <returns>ApiResponse of ResourceListOfPersonalisation</returns>
        ApiResponse<ResourceListOfPersonalisation> GetPersonalisationsWithHttpInfo (string pattern = null, string scope = null, bool? recursive = null, bool? wildcards = null, List<string> sortBy = null, int? start = null, int? limit = null);
        /// <summary>
        /// Upsert personalisations
        /// </summary>
        /// <remarks>
        /// Upsert one or more personalisations
        /// </remarks>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="personalisations">The set of personalisations to persist (optional)</param>
        /// <returns>UpsertPersonalisationResponse</returns>
        UpsertPersonalisationResponse UpsertPersonalisations (List<Personalisation> personalisations = null);

        /// <summary>
        /// Upsert personalisations
        /// </summary>
        /// <remarks>
        /// Upsert one or more personalisations
        /// </remarks>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="personalisations">The set of personalisations to persist (optional)</param>
        /// <returns>ApiResponse of UpsertPersonalisationResponse</returns>
        ApiResponse<UpsertPersonalisationResponse> UpsertPersonalisationsWithHttpInfo (List<Personalisation> personalisations = null);
        #endregion Synchronous Operations
        #region Asynchronous Operations
        /// <summary>
        /// Delete a personalisation
        /// </summary>
        /// <remarks>
        /// Delete a personalisation at a specific scope (or use scope ALL to purge the setting entirely)
        /// </remarks>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="key">The key of the setting to be deleted (optional)</param>
        /// <param name="scope">The scope to delete at (use ALL to purge the setting entirely) (optional)</param>
        /// <param name="group">Optional. If deleting a setting at group level, specify the group here (optional)</param>
        /// <returns>Task of DeletedEntityResponse</returns>
        System.Threading.Tasks.Task<DeletedEntityResponse> DeletePersonalisationAsync (string key = null, string scope = null, string group = null);

        /// <summary>
        /// Delete a personalisation
        /// </summary>
        /// <remarks>
        /// Delete a personalisation at a specific scope (or use scope ALL to purge the setting entirely)
        /// </remarks>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="key">The key of the setting to be deleted (optional)</param>
        /// <param name="scope">The scope to delete at (use ALL to purge the setting entirely) (optional)</param>
        /// <param name="group">Optional. If deleting a setting at group level, specify the group here (optional)</param>
        /// <returns>Task of ApiResponse (DeletedEntityResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<DeletedEntityResponse>> DeletePersonalisationAsyncWithHttpInfo (string key = null, string scope = null, string group = null);
        /// <summary>
        /// Get personalisation
        /// </summary>
        /// <remarks>
        /// Get a personalisation, recursing to get any referenced if required.
        /// </remarks>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="pattern">The search pattern or specific key (optional)</param>
        /// <param name="scope">Optional. The scope level to request for (optional)</param>
        /// <param name="recursive">Optional. Whether to recurse into dereference recursive settings (optional, default to false)</param>
        /// <param name="wildcards">Optional. Whether to apply wildcards to the provided pattern and pull back any matching (optional, default to false)</param>
        /// <param name="sortBy">Optional. Order the results by these fields. Use use the &#39;-&#39; sign to denote descending order e.g. -MyFieldName (optional)</param>
        /// <param name="start">Optional. When paginating, skip this number of results (optional)</param>
        /// <param name="limit">Optional. When paginating, limit the number of returned results to this many. (optional)</param>
        /// <returns>Task of ResourceListOfPersonalisation</returns>
        System.Threading.Tasks.Task<ResourceListOfPersonalisation> GetPersonalisationsAsync (string pattern = null, string scope = null, bool? recursive = null, bool? wildcards = null, List<string> sortBy = null, int? start = null, int? limit = null);

        /// <summary>
        /// Get personalisation
        /// </summary>
        /// <remarks>
        /// Get a personalisation, recursing to get any referenced if required.
        /// </remarks>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="pattern">The search pattern or specific key (optional)</param>
        /// <param name="scope">Optional. The scope level to request for (optional)</param>
        /// <param name="recursive">Optional. Whether to recurse into dereference recursive settings (optional, default to false)</param>
        /// <param name="wildcards">Optional. Whether to apply wildcards to the provided pattern and pull back any matching (optional, default to false)</param>
        /// <param name="sortBy">Optional. Order the results by these fields. Use use the &#39;-&#39; sign to denote descending order e.g. -MyFieldName (optional)</param>
        /// <param name="start">Optional. When paginating, skip this number of results (optional)</param>
        /// <param name="limit">Optional. When paginating, limit the number of returned results to this many. (optional)</param>
        /// <returns>Task of ApiResponse (ResourceListOfPersonalisation)</returns>
        System.Threading.Tasks.Task<ApiResponse<ResourceListOfPersonalisation>> GetPersonalisationsAsyncWithHttpInfo (string pattern = null, string scope = null, bool? recursive = null, bool? wildcards = null, List<string> sortBy = null, int? start = null, int? limit = null);
        /// <summary>
        /// Upsert personalisations
        /// </summary>
        /// <remarks>
        /// Upsert one or more personalisations
        /// </remarks>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="personalisations">The set of personalisations to persist (optional)</param>
        /// <returns>Task of UpsertPersonalisationResponse</returns>
        System.Threading.Tasks.Task<UpsertPersonalisationResponse> UpsertPersonalisationsAsync (List<Personalisation> personalisations = null);

        /// <summary>
        /// Upsert personalisations
        /// </summary>
        /// <remarks>
        /// Upsert one or more personalisations
        /// </remarks>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="personalisations">The set of personalisations to persist (optional)</param>
        /// <returns>Task of ApiResponse (UpsertPersonalisationResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<UpsertPersonalisationResponse>> UpsertPersonalisationsAsyncWithHttpInfo (List<Personalisation> personalisations = null);
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class PersonalisationsApi : IPersonalisationsApi
    {
        private Lusid.Sdk.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalisationsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public PersonalisationsApi(String basePath)
        {
            this.Configuration = new Lusid.Sdk.Client.Configuration { BasePath = basePath };

            ExceptionFactory = Lusid.Sdk.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalisationsApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public PersonalisationsApi(Lusid.Sdk.Client.Configuration configuration = null)
        {
            if (configuration == null) // use the default one in Configuration
                this.Configuration = Lusid.Sdk.Client.Configuration.Default;
            else
                this.Configuration = configuration;

            ExceptionFactory = Lusid.Sdk.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public String GetBasePath()
        {
            return this.Configuration.ApiClient.RestClient.BaseUrl.ToString();
        }

        /// <summary>
        /// Sets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        [Obsolete("SetBasePath is deprecated, please do 'Configuration.ApiClient = new ApiClient(\"http://new-path\")' instead.")]
        public void SetBasePath(String basePath)
        {
            // do nothing
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public Lusid.Sdk.Client.Configuration Configuration {get; set;}

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public Lusid.Sdk.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        /// Gets the default header.
        /// </summary>
        /// <returns>Dictionary of HTTP header</returns>
        [Obsolete("DefaultHeader is deprecated, please use Configuration.DefaultHeader instead.")]
        public IDictionary<String, String> DefaultHeader()
        {
            return new ReadOnlyDictionary<string, string>(this.Configuration.DefaultHeader);
        }

        /// <summary>
        /// Add default header.
        /// </summary>
        /// <param name="key">Header field name.</param>
        /// <param name="value">Header field value.</param>
        /// <returns></returns>
        [Obsolete("AddDefaultHeader is deprecated, please use Configuration.AddDefaultHeader instead.")]
        public void AddDefaultHeader(string key, string value)
        {
            this.Configuration.AddDefaultHeader(key, value);
        }

        /// <summary>
        /// Delete a personalisation Delete a personalisation at a specific scope (or use scope ALL to purge the setting entirely)
        /// </summary>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="key">The key of the setting to be deleted (optional)</param>
        /// <param name="scope">The scope to delete at (use ALL to purge the setting entirely) (optional)</param>
        /// <param name="group">Optional. If deleting a setting at group level, specify the group here (optional)</param>
        /// <returns>DeletedEntityResponse</returns>
        public DeletedEntityResponse DeletePersonalisation (string key = null, string scope = null, string group = null)
        {
             ApiResponse<DeletedEntityResponse> localVarResponse = DeletePersonalisationWithHttpInfo(key, scope, group);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Delete a personalisation Delete a personalisation at a specific scope (or use scope ALL to purge the setting entirely)
        /// </summary>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="key">The key of the setting to be deleted (optional)</param>
        /// <param name="scope">The scope to delete at (use ALL to purge the setting entirely) (optional)</param>
        /// <param name="group">Optional. If deleting a setting at group level, specify the group here (optional)</param>
        /// <returns>ApiResponse of DeletedEntityResponse</returns>
        public ApiResponse< DeletedEntityResponse > DeletePersonalisationWithHttpInfo (string key = null, string scope = null, string group = null)
        {

            var localVarPath = "./api/personalisations";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "text/plain",
                "application/json",
                "text/json"
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (key != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "key", key)); // query parameter
            if (scope != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "scope", scope)); // query parameter
            if (group != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "group", group)); // query parameter

            // authentication (oauth2) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + this.Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) this.Configuration.ApiClient.CallApi(localVarPath,
                Method.DELETE, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("DeletePersonalisation", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<DeletedEntityResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Key,  x => String.Join(",", x.Value.ToArray())),
                (DeletedEntityResponse) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(DeletedEntityResponse)));
        }

        /// <summary>
        /// Delete a personalisation Delete a personalisation at a specific scope (or use scope ALL to purge the setting entirely)
        /// </summary>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="key">The key of the setting to be deleted (optional)</param>
        /// <param name="scope">The scope to delete at (use ALL to purge the setting entirely) (optional)</param>
        /// <param name="group">Optional. If deleting a setting at group level, specify the group here (optional)</param>
        /// <returns>Task of DeletedEntityResponse</returns>
        public async System.Threading.Tasks.Task<DeletedEntityResponse> DeletePersonalisationAsync (string key = null, string scope = null, string group = null)
        {
             ApiResponse<DeletedEntityResponse> localVarResponse = await DeletePersonalisationAsyncWithHttpInfo(key, scope, group);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Delete a personalisation Delete a personalisation at a specific scope (or use scope ALL to purge the setting entirely)
        /// </summary>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="key">The key of the setting to be deleted (optional)</param>
        /// <param name="scope">The scope to delete at (use ALL to purge the setting entirely) (optional)</param>
        /// <param name="group">Optional. If deleting a setting at group level, specify the group here (optional)</param>
        /// <returns>Task of ApiResponse (DeletedEntityResponse)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<DeletedEntityResponse>> DeletePersonalisationAsyncWithHttpInfo (string key = null, string scope = null, string group = null)
        {

            var localVarPath = "./api/personalisations";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "text/plain",
                "application/json",
                "text/json"
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (key != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "key", key)); // query parameter
            if (scope != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "scope", scope)); // query parameter
            if (group != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "group", group)); // query parameter

            // authentication (oauth2) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + this.Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.DELETE, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("DeletePersonalisation", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<DeletedEntityResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Key,  x => String.Join(",", x.Value.ToArray())),
                (DeletedEntityResponse) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(DeletedEntityResponse)));
        }

        /// <summary>
        /// Get personalisation Get a personalisation, recursing to get any referenced if required.
        /// </summary>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="pattern">The search pattern or specific key (optional)</param>
        /// <param name="scope">Optional. The scope level to request for (optional)</param>
        /// <param name="recursive">Optional. Whether to recurse into dereference recursive settings (optional, default to false)</param>
        /// <param name="wildcards">Optional. Whether to apply wildcards to the provided pattern and pull back any matching (optional, default to false)</param>
        /// <param name="sortBy">Optional. Order the results by these fields. Use use the &#39;-&#39; sign to denote descending order e.g. -MyFieldName (optional)</param>
        /// <param name="start">Optional. When paginating, skip this number of results (optional)</param>
        /// <param name="limit">Optional. When paginating, limit the number of returned results to this many. (optional)</param>
        /// <returns>ResourceListOfPersonalisation</returns>
        public ResourceListOfPersonalisation GetPersonalisations (string pattern = null, string scope = null, bool? recursive = null, bool? wildcards = null, List<string> sortBy = null, int? start = null, int? limit = null)
        {
             ApiResponse<ResourceListOfPersonalisation> localVarResponse = GetPersonalisationsWithHttpInfo(pattern, scope, recursive, wildcards, sortBy, start, limit);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Get personalisation Get a personalisation, recursing to get any referenced if required.
        /// </summary>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="pattern">The search pattern or specific key (optional)</param>
        /// <param name="scope">Optional. The scope level to request for (optional)</param>
        /// <param name="recursive">Optional. Whether to recurse into dereference recursive settings (optional, default to false)</param>
        /// <param name="wildcards">Optional. Whether to apply wildcards to the provided pattern and pull back any matching (optional, default to false)</param>
        /// <param name="sortBy">Optional. Order the results by these fields. Use use the &#39;-&#39; sign to denote descending order e.g. -MyFieldName (optional)</param>
        /// <param name="start">Optional. When paginating, skip this number of results (optional)</param>
        /// <param name="limit">Optional. When paginating, limit the number of returned results to this many. (optional)</param>
        /// <returns>ApiResponse of ResourceListOfPersonalisation</returns>
        public ApiResponse< ResourceListOfPersonalisation > GetPersonalisationsWithHttpInfo (string pattern = null, string scope = null, bool? recursive = null, bool? wildcards = null, List<string> sortBy = null, int? start = null, int? limit = null)
        {

            var localVarPath = "./api/personalisations";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "text/plain",
                "application/json",
                "text/json"
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (pattern != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "pattern", pattern)); // query parameter
            if (scope != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "scope", scope)); // query parameter
            if (recursive != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "recursive", recursive)); // query parameter
            if (wildcards != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "wildcards", wildcards)); // query parameter
            if (sortBy != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("multi", "sortBy", sortBy)); // query parameter
            if (start != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "start", start)); // query parameter
            if (limit != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "limit", limit)); // query parameter

            // authentication (oauth2) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + this.Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) this.Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetPersonalisations", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<ResourceListOfPersonalisation>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Key,  x => String.Join(",", x.Value.ToArray())),
                (ResourceListOfPersonalisation) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(ResourceListOfPersonalisation)));
        }

        /// <summary>
        /// Get personalisation Get a personalisation, recursing to get any referenced if required.
        /// </summary>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="pattern">The search pattern or specific key (optional)</param>
        /// <param name="scope">Optional. The scope level to request for (optional)</param>
        /// <param name="recursive">Optional. Whether to recurse into dereference recursive settings (optional, default to false)</param>
        /// <param name="wildcards">Optional. Whether to apply wildcards to the provided pattern and pull back any matching (optional, default to false)</param>
        /// <param name="sortBy">Optional. Order the results by these fields. Use use the &#39;-&#39; sign to denote descending order e.g. -MyFieldName (optional)</param>
        /// <param name="start">Optional. When paginating, skip this number of results (optional)</param>
        /// <param name="limit">Optional. When paginating, limit the number of returned results to this many. (optional)</param>
        /// <returns>Task of ResourceListOfPersonalisation</returns>
        public async System.Threading.Tasks.Task<ResourceListOfPersonalisation> GetPersonalisationsAsync (string pattern = null, string scope = null, bool? recursive = null, bool? wildcards = null, List<string> sortBy = null, int? start = null, int? limit = null)
        {
             ApiResponse<ResourceListOfPersonalisation> localVarResponse = await GetPersonalisationsAsyncWithHttpInfo(pattern, scope, recursive, wildcards, sortBy, start, limit);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Get personalisation Get a personalisation, recursing to get any referenced if required.
        /// </summary>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="pattern">The search pattern or specific key (optional)</param>
        /// <param name="scope">Optional. The scope level to request for (optional)</param>
        /// <param name="recursive">Optional. Whether to recurse into dereference recursive settings (optional, default to false)</param>
        /// <param name="wildcards">Optional. Whether to apply wildcards to the provided pattern and pull back any matching (optional, default to false)</param>
        /// <param name="sortBy">Optional. Order the results by these fields. Use use the &#39;-&#39; sign to denote descending order e.g. -MyFieldName (optional)</param>
        /// <param name="start">Optional. When paginating, skip this number of results (optional)</param>
        /// <param name="limit">Optional. When paginating, limit the number of returned results to this many. (optional)</param>
        /// <returns>Task of ApiResponse (ResourceListOfPersonalisation)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<ResourceListOfPersonalisation>> GetPersonalisationsAsyncWithHttpInfo (string pattern = null, string scope = null, bool? recursive = null, bool? wildcards = null, List<string> sortBy = null, int? start = null, int? limit = null)
        {

            var localVarPath = "./api/personalisations";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "text/plain",
                "application/json",
                "text/json"
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (pattern != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "pattern", pattern)); // query parameter
            if (scope != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "scope", scope)); // query parameter
            if (recursive != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "recursive", recursive)); // query parameter
            if (wildcards != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "wildcards", wildcards)); // query parameter
            if (sortBy != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("multi", "sortBy", sortBy)); // query parameter
            if (start != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "start", start)); // query parameter
            if (limit != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "limit", limit)); // query parameter

            // authentication (oauth2) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + this.Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetPersonalisations", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<ResourceListOfPersonalisation>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Key,  x => String.Join(",", x.Value.ToArray())),
                (ResourceListOfPersonalisation) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(ResourceListOfPersonalisation)));
        }

        /// <summary>
        /// Upsert personalisations Upsert one or more personalisations
        /// </summary>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="personalisations">The set of personalisations to persist (optional)</param>
        /// <returns>UpsertPersonalisationResponse</returns>
        public UpsertPersonalisationResponse UpsertPersonalisations (List<Personalisation> personalisations = null)
        {
             ApiResponse<UpsertPersonalisationResponse> localVarResponse = UpsertPersonalisationsWithHttpInfo(personalisations);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Upsert personalisations Upsert one or more personalisations
        /// </summary>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="personalisations">The set of personalisations to persist (optional)</param>
        /// <returns>ApiResponse of UpsertPersonalisationResponse</returns>
        public ApiResponse< UpsertPersonalisationResponse > UpsertPersonalisationsWithHttpInfo (List<Personalisation> personalisations = null)
        {

            var localVarPath = "./api/personalisations";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
                "application/json-patch+json", 
                "application/json", 
                "text/json", 
                "application/_*+json"
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "text/plain",
                "application/json",
                "text/json"
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (personalisations != null && personalisations.GetType() != typeof(byte[]))
            {
                localVarPostBody = this.Configuration.ApiClient.Serialize(personalisations); // http body (model) parameter
            }
            else
            {
                localVarPostBody = personalisations; // byte array
            }

            // authentication (oauth2) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + this.Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) this.Configuration.ApiClient.CallApi(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("UpsertPersonalisations", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<UpsertPersonalisationResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Key,  x => String.Join(",", x.Value.ToArray())),
                (UpsertPersonalisationResponse) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(UpsertPersonalisationResponse)));
        }

        /// <summary>
        /// Upsert personalisations Upsert one or more personalisations
        /// </summary>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="personalisations">The set of personalisations to persist (optional)</param>
        /// <returns>Task of UpsertPersonalisationResponse</returns>
        public async System.Threading.Tasks.Task<UpsertPersonalisationResponse> UpsertPersonalisationsAsync (List<Personalisation> personalisations = null)
        {
             ApiResponse<UpsertPersonalisationResponse> localVarResponse = await UpsertPersonalisationsAsyncWithHttpInfo(personalisations);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Upsert personalisations Upsert one or more personalisations
        /// </summary>
        /// <exception cref="Lusid.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="personalisations">The set of personalisations to persist (optional)</param>
        /// <returns>Task of ApiResponse (UpsertPersonalisationResponse)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<UpsertPersonalisationResponse>> UpsertPersonalisationsAsyncWithHttpInfo (List<Personalisation> personalisations = null)
        {

            var localVarPath = "./api/personalisations";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
                "application/json-patch+json", 
                "application/json", 
                "text/json", 
                "application/_*+json"
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "text/plain",
                "application/json",
                "text/json"
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (personalisations != null && personalisations.GetType() != typeof(byte[]))
            {
                localVarPostBody = this.Configuration.ApiClient.Serialize(personalisations); // http body (model) parameter
            }
            else
            {
                localVarPostBody = personalisations; // byte array
            }

            // authentication (oauth2) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + this.Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("UpsertPersonalisations", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<UpsertPersonalisationResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Key,  x => String.Join(",", x.Value.ToArray())),
                (UpsertPersonalisationResponse) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(UpsertPersonalisationResponse)));
        }

    }
    
}
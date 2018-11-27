# Lusid.Sdk.Model.OutputTransaction
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**TransactionId** | **string** | Unique transaction identifier | [optional] 
**Type** | **string** | LUSID transaction type code - Buy, Sell, StockIn, StockOut, etc | [optional] 
**Description** | **string** | LUSID transaction description | [optional] 
**InstrumentUid** | **string** | Unique instrument identifier | [optional] 
**TransactionDate** | **DateTimeOffset?** | Transaction date | [optional] 
**SettlementDate** | **DateTimeOffset?** | Settlement date | [optional] 
**Units** | **double?** | Quantity of trade in units of the instrument | [optional] 
**TransactionPrice** | [**TransactionPrice**](TransactionPrice.md) | Execution price for the transaction | [optional] 
**TotalConsideration** | [**CurrencyAndAmount**](CurrencyAndAmount.md) | Total value of the transaction in settlement currency | [optional] 
**ExchangeRate** | **double?** | Rate between transaction and settlement currency | [optional] 
**TransactionToPortfolioRate** | **double?** | Rate between transaction and portfolio currency | [optional] 
**TransactionCurrency** | **string** | Transaction currency | [optional] 
**Properties** | [**List&lt;PerpetualProperty&gt;**](PerpetualProperty.md) |  | [optional] 
**CounterpartyId** | **string** | Counterparty identifier | [optional] 
**Source** | **string** | Where this transaction came from, either Client or System | [optional] 
**NettingSet** | **string** |  | [optional] 
**TransactionStatus** | **string** | Transaction status (active, amended or cancelled) | [optional] 
**EntryDateTime** | **DateTimeOffset?** | Date/Time the transaction was booked into LUSID | [optional] 
**CancelDateTime** | **DateTimeOffset?** | Date/Time the cancellation was booked into LUSID | [optional] 
**RealisedGainLoss** | [**List&lt;RealisedGainLoss&gt;**](RealisedGainLoss.md) | Collection of gains or losses | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

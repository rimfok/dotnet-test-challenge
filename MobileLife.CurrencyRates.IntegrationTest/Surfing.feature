Feature: Surfing
	In order to get rates
	As a user
	I want to fetch them from WS

@list
Scenario: List stored rates
	Given I have set initial id to 0
	When I invoke WS method ListStoredCurrencyRates
	Then I should get successful list response
	 And I should get collection of rates up to 10

@get
Scenario: Get concrete day and currency rate
	Given I have set initial day to "2017-01-01"
	 And Currency is to "GBP"
	When I invoke WS method GetEuroCurrencyRate
	Then I should get successful get response
	 And I should get rate equal to 0.856180

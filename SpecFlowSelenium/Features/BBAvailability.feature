Feature: Broadband Availability Map

As a user when I search for an address in the broadband availability map, then the netwokr capability at the given address is displayed.

@tag1
Scenario: Search for Broadband Availability
	Given I input the address '19A Woodland Road, Johnsonville, Wellington'
	When the listed address is searched
	Then the network capability result displays 'UFB fibre up to 1 Gbps'

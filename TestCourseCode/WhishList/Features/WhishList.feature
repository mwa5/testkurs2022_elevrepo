Feature: Spara önskningar i önskelistan

A short summary of the feature

@tag1
Scenario: Som pappa kan jag spara min sons önskningar
	Given Det finns ett barn som heter Folke och som önskar sig en kniv
	When Jag sparar önskan i önskelistan
	Then Så får jag tillbaka texten Önskan sparades

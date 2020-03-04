#Vending Machine 

A simple program that allows users to buy products using coins or credit cards.
Every product has its price and stock.
I used services to resolve dependencies and ConfigurationBuilder to load config from json file

## Classes and their use

* Program
	* Startup class that registers services, builds config and runs application
* ConsoleApplication
	* basic GUI for application
* VendinMachine
	* Business logic for interaction
* Product
	* Abstract class that defines basic business logic for derived models
* Snack, Drink
	* Basic models inheriting from Product class
* ABCCreditCard
	* Credit card provider with its own implementation of business logic
* BestBank, JavaFan
	* Credit cards with their own cosmetic changes

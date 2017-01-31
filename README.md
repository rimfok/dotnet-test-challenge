# MobileLife .NET test automation challenge

In this repository you'll find a simple .NET self-hosted WCF service application for retrieving Euro currency rates. There are two operations implemented:
* *GetEuroCurrencyRate* - used to retrieve EUR currency rate for exact day and currency.
* *ListStoredCurrencyRates* - used to export internal database of a service in batches of ten.

## Technical Stack 

This solution if built using more or less common technical stack used in MobileLife. We expect that you are at least a bit familiar with these technologies:
* .NET 4.6.1
* Entity Framework - ORM with Code First Migrations
* Castle Windsor -  Inversion of Control container 
* Topshelf - service hosting framework for building Windows services
* Polly - resilience and transient-fault-handling library

## Short Service Description

The service is responsible for providing Euro to other currency rates for particular day. 
It uses http://www.lb.lt/webservices/fxrates/FxRates.asmx currency rate provider to retrieve actual figures. 
Once it fetches currency rate for particular date it's stored in the database and all the consecutive requests for the same day and currency are returned directly from the database without hitting external currency rate provider.

There is also a possibility to retrieve whole internal database of this service using *ListStoredCurrencyRates* operation. It returns currency rate entities in batches of ten, so during first run you should provide 0 as a *start id* and afterwards call the same operations using last retrieved currency rate id to get next ten.

We have also seen some connection timeout issues while calling external currency rates provider, so it was decided to use Polly for retrying same call for three times before returning a failure to a service consumer.

## Your task

In the Tests folder you'll find two empty projects:
* MobileLife.CurrencyRates.IntegrationTest
* MobileLife.CurrencyRates.UnitTest

We don't expect them to be that way, so your tasks is to **create integration and unit tests for this application**.

What exact test cases to cover is for you to decide. Just remember that although test coverage is a nice metric it's not the main thing you should aim for. We strongly believe that covering edge cases and business requirements is the essential benefit of properly written tests.

We would expect tests to be documented either by using meaningful test case and method naming or explicit comments in the code.

Also if you find that some parts of the code are hard to test or see other ways to improve this application feel free to do that!

## Tooling

There are already some libraries added to the test projects: 
* *NUnit* - unit testing framework
* *Moq* - mocking library
* *SpecFlow* - Cucumber for .NET

We would prefer if you'd use these tools while writing test cases. However don't feel limited by them if you need something else just use it!

## Getting Started

1. Fork this repository.
2. Clone it to your PC.
3. Build the project.
4. Fire up the service. At first service call database will be created and seeded. Service will start listening on port 8888.
5. Create a branch dedicated to your task.
6. Once you are done commit and push the branch to remote.
7. Create a pull request for us.
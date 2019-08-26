# az-function-lab
This repo contains labs to demonstrate azure Function for AZ-203

## Topics to cover
  - Implement input and output bindings for a function
    [Trigger and Binding](https://docs.microsoft.com/en-us/azure/azure-functions/functions-triggers-bindings)
  
  - Implement function triggers by using data operations, timers, and webhooks
  
  - Implement Azure Durable Functions
  
  - Create Azure Function apps by using Visual Studio

### Versions
  There are 2 version of Azure Function, 1.x and 2.x. 
  1.x --> Runs on .NET 4.7 Framework
  2.x --> Self contained, dotnetcore based

### host.json
  This file sets global configuration of the functions that recide within the Azure Function App.

  For example, configuration of
    * application insights on frequency of telemetry data to collect
    * eventhub on maximum number of events to be recieved by the function
    * function timeout
    * logger settings
    * all supported bindings/triggers

  <i>Note: When running locally, host.json file can be configured to enable only certain functions</i>

  Based on Function app version, host.jsob file behaviour changes. 
  1.x: [Documentation](https://docs.microsoft.com/en-us/azure/azure-functions/functions-host-json-v1)  
  2.x: [Documentation](https://docs.microsoft.com/en-us/azure/azure-functions/functions-host-json) 

## Read before
  - [What is Serverless?](https://martinfowler.com/articles/serverless.html)
  - Azure Functions depend on the Azure Storage Account to store the context and settings with in built encryption, get basic understanding of [storage account](https://docs.microsoft.com/en-us/azure/storage/common/storage-account-overview)
  - Azure Functions's triggers and bindings are mostly Azure resources, get basic understanding of ServiceBus, StorageAccount, CosmosDb, NotificationHub, EventHub, KeyVault and ApplicationInsights

## What do we need to run the labs
  - [Azure Subscription](https://portal.azure.com)
  - Windows Laptop
  - [Install Dotnet core SDK](https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.401-windows-x64-installer)
  - [Install VSCode](https://code.visualstudio.com/download#)
    - Extension Azure functions
    - Azure Account
  - [Azure Storage Emulator](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-emulator)
    - Emulator requires [MS SQL](https://download.microsoft.com/download/5/E/9/5E9B18CC-8FD5-467E-B5BF-BADE39C51F73/SQLServer2017-SSEI-Expr.exe)
  - [Azure Storage Explorer](https://go.microsoft.com/fwlink/?LinkId=708343&clcid=0x409)
  - [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli-windows?view=azure-cli-latest)

## References
  - [Azure functions Reference](https://docs.microsoft.com/en-us/azure/azure-functions/functions-reference)
  - [Azure functions wiki](https://github.com/Azure/azure-functions-host/wiki/function.json)
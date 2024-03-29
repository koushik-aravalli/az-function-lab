## az-function-lab-objective
This lab will help us understand how a time trigger invokes an azure function. 
Checkout the presentation slides for lab01 scenario

#### Using [C#](https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-class-library#functions-class-library-project) : 

Lab01 is developed in C# (*.cs not *.csx), to help us understand how Azure functions are developed and developed

#### How to setup :
As mentioned earlier [readme/What do we need to run the labs](../../README.md), we will need Azure subscription, VSCode along with function app extension, dontnet core, functionapp, storage emulator along with explorer.

#### Quickly create an Azure FunctionApp
1. Open VS Code, click on the Azure ![](https://upload.wikimedia.org/wikipedia/commons/thumb/a/a8/Microsoft_Azure_Logo.svg/187px-Microsoft_Azure_Logo.svg.png) Icon on the left
1. Login to Azure Subscription
1. Right click on the Subscription, Select Create Function App (Advanced)
    - Enter name of the FunctionApp (globally unique)
    - Select Windows based OS
    - Select Consumption plan
    - Select .NET
    - Select Create a new Resource Group, Enter name of the ResoruceGroup
    - Select Create a new Storage Account, Enter name of the Storage Account
    - Skip Application Insights

![](../gifs/CreateAzurefunctionDirecltyFromVSCode.gif)


#### Create VSCode development workspace for Azure FunctionApp

#### Lab01_HttpTrigger
1. Open VS Code, click on the Azure [logo]:https://upload.wikimedia.org/wikipedia/commons/thumb/a/a8/Microsoft_Azure_Logo.svg/187px-Microsoft_Azure_Logo.svg.png Icon on the left
1. 
With installed Azure Function visual studio extension, follow the steps:
    - Select Create new project
    - Browse to the folder locaton where Function Project need to be created. Project is collection of functions
    - Proivde name of the function that will live within the Function Project
    - Specify namespace, for logical segregation
    - Select a trigger - HttpTrigger in this case 

![](../gifs/CreateAzureFunctionAppProject-Locally.gif)


After creation of a function within the FunctionApp Project locally, when more functions need to be added, use 'Create Function' within the same folder where the above created function exists. Follow steps as above to generate new function. The folder structure of a typical AzureFunctionApp project is as follows
![](../gifs/AzureFunctionFolderStructure.jpeg)

***Note : Azure Storage Explorer can create temporary local Storage Emulator, and using this, populate the local.settings.json file with the connection string of the local storage account***

#### Lab01_TimerTrigger
Actions are same as above except at the last step of Select a trigger, instead select TimeTrigger. 

After generation of the code, copy the code from [file](Lab01_Timertrigger.cs)

#### Lab01_ServicebusTrigger
Actions are same as above except at the last step of Select a trigger, instead select ServicebusQueueTrigger. 

After generation of the code, copy the code from [file](Lab01_ServicebusQueuetrigger.cs)


#### Complete setup with CLI and VSCode

  - Setup: Create RG, StorageAccount, SB
      - Login
        ```
        az login
        ```
      - Create Resource Group
        ```
        az group create --name az-function-lab --location northeurope
        ```
      - Create ServiceBus Namespace
        ```
        az servicebus namespace create --resource-group az-function-lab --name az-function-lab-sb21082019 --location northeurope
        ```
      - Create ServiceBus Queue
        ```
        az servicebus queue create --resource-group az-function-lab --namespace-name az-function-lab-sb21082019 --name SampleQueue
        ```
      - Get ConnectionKey
        ```
        az servicebus namespace authorization-rule keys list --resource-group az-function-lab --namespace-name az-function-lab-sb21082019 --name RootManageSharedAccessKey --query primaryConnectionString --output tsv
        ```
      - Deploy KeyVault
        ```
        az keyvault create -n azfunc203-20190825 -g az-function-lab
        ```
      - Save Servicebus connection string to KV secret
        ```
        az keyvault secret set --vault-name azfunc203-20190825 --name storageaccount-key --value "Endpoint=sb://xxxxxxxxxx.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=xxxxxxxxxxxxxx"
        ```

  - Start: Start functions in the lab
    Use following command to start the function, (make sure, the path is at *.csproj file)
    ```
    func host start
    ```

  - Stop: Stop functions in the lab
    Keyboard shortcut to stop the function
    ```
    Ctrl+C
    ```

  - Deploy FuntionApp to Azure RG:
    - [FunctionApp - CLI](https://docs.microsoft.com/en-us/cli/azure/functionapp?view=azure-cli-latest)

    - [With CLI](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local#project-file-deployment)
      * Create Storage Account
        (FunctionApp's cannot be created in Standard_RAGRS)
      ```
      az storage account create -n azfunc20325082019 -g az-function-lab -l northeurope --sku Standard_LRS
      ```
      * Create FunctionApp in ResourceGroup
      ```
      az functionapp create --name azfunc203-20190825 --resource-group az-function-lab --consumption-plan-location northeurope --storage-account azfunc20325082019
      ```
      * Deploy lab01 within FunctionApp
      ```
      func azure functionapp publish azfunc203-20190825
      ```
  

  - Managed Service Identity on Keyvault
    In order to communicate with the Keyvault, Azure Function should be enabled with system Identity. Follow below steps to enable identity 
    * Browse to the Azure function, on Platform features tab, select Identity
    * Set status to On and save the changes (notice that a guid is generated as ObjectID)
    * Browse back to the Keyvault in the ResourceGroup, Select Access policies
    * Add Access Policy, from Secret permissions, select Get/List/Set/Delete 
    * Select Service Principal, search for Azure Function, select and add, finally save the changes

  - Access Secrets
    - Add Package to project to support Keyvault access from application
      ```
      dotnet add package Microsoft.Azure.Services.AppAuthentication
      dotnet add package Microsoft.IdentityModel.Clients.ActiveDirectory
      dotnet add package Microsoft.Extensions.Configuration.AzureKeyVault
      ```
    - Add values to the local.settings.json

  - Binding : Twilio 
    - Before using Twilio binding, signup for [Twilio](https://www.twilio.com) to send/recieve sms/whatsapp/calls. Generate Account SID and Authentication Token
    - Add Package to project to support Keyvault access from application
      ```
      dotnet add package Microsoft.Azure.WebJobs.Extensions.Twilio
      ```

  - Setup: Destroy
    - Remove ResourceGroup
      ```
      az group delete --name az-function-lab
      ```

##### Dependencies
- **Adding binding dependencies**
    With VS or VSCode, binding need to be added to have a successful compilation. [Documentation](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-register) describes how to and also list of all supported bindings

    For example, within VSCode terminal (shortcut: Ctrl+Shift+`), add a new dependency to the project using following
    ```
    dotnet add package Microsoft.Azure.WebJobs.Extensions.ServiceBus
    ```

#### Resolve issues

**Nuget Package Error: Unable to load the service index for source**
    Find nuget.config at /users/{your-user-account}/AppData/Roaming/Nuget/nuget.config
    Check if the listed package sources are still accessile with the credentials
    Else, comment out the specific package sources

**Unresolved Azure Function class imports**
    When the created Azure Function have compilation errors, due to unresolved imports, browse to the FunctionApp project folder. Locate if *.csproj exists at this location. Run below to force all package are retrieved to support Azure Function
    ```
    dotnet restore
    ```
**Timer trigger function: Microsoft.WindowsAzure.Storage: No connection could be made because the target machine actively refused it**
    Download and install Storage Emulator
    Run the Emulator @ "C:\Program Files (x86)\Microsoft SDKs\Azure\Storage Emulator"
    ```
    AzureStorageEmulator.exe init
    AzureStorageEmulator.exe start
    ```

**Microsoft.WindowsAzure.Storage: Calculated MD5 does not match existing property**
    During local deployment of Functions, the is a dependency on Storage Emulator. Before execution of function app locally, emulator is started thereby a blob is created in local SQLdb. This error occurs when locks are blocking the next execution. Open Storage Explorer, browse to emulator and remove the blob and refresh.

**Signout of Azure VSCode**
    In case there is an another subscripton is being looged in, switch accounts in VSCode by signing out
    ```
    ctrl+shift+p
    ```

#### Using Javascript/Portal

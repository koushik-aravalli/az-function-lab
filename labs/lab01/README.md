## az-function-lab-objective
This lab will help us understand how a time trigger invokes an azure function. 
Checkout the presentation slides for lab01 scenario

#### Using C# : 

With installed Azure Function visual studio extension, follow the steps:
    - create new project
    - select browse to point at folder locaton where Function Project need to be created. Project is collection of functions
    - Proivde name of the function that will live within the Function Project
    - Specify namespace, for logical segregation
    - Select a trigger - HttpTrigger in this case 

After creation of a function within the FunctionApp Project locally, when more functions need to be added, use 'Create Function' within the same folder where the above created function exists. Follow steps as above to generate new function. 

Azure Storage Explorer can create temporary local Storage Emulator, and using this, populate the local.settings.json file with the connection string of the local storage account 

##### Dependencies
- **Adding binding dependencies**
    With VS or VSCode, binding need to be added to have a successful compilation. [Documentation](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-register) describes how to and also list of all supported bindings

    For VSCode
    ```
    dotnet add package Microsoft.Azure.WebJobs.Extensions.ServiceBus
    ```

#### Lab01_TimerTrigger
    - Setup: Create
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

    - Setup: Destroy
        - Remove ResourceGroup
          ```
          az group delete --name az-function-lab
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

#### Using Javascript/Portal

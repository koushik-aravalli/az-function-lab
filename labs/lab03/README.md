## az-function-lab-objective

This lab will help us understand how to create Durable functions. 

Checkout the presentation slides for lab03 scenario

## Durable functions

* Create a new FunctionApp project
    ```
    func init dotnet
    ```

* Install Durable functions extension to support
    ```
    func extensions install -p Microsoft.Azure.WebJobs.Extensions.DurableTask -v 2.0.0
    ```

### References
* [Durable Functions](https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-overview)
* [Serverless comparision](https://docs.microsoft.com/en-us/azure/azure-functions/functions-compare-logic-apps-ms-flow-webjobs?toc=%2fazure%2fazure-functions%2fdurable%2ftoc.json#compare-azure-functions-and-azure-logic-apps)
* []
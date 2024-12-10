## Solução Serverless com Azure Logic Apps e Azure Functions

### Visão Geral  
Nesta solução, usaremos diferentes serviços do Azure para criar uma arquitetura serverless que integra dados, processa eventos e armazena informações em um banco de dados. A solução é composta pelos seguintes componentes:  

- *Azure Logic Apps:* Orquestração e integração de fluxos de trabalho.  
- *Azure Functions:* Execução de pequenos trechos de código em resposta a eventos.  
- *Azure Database:* Armazenamento de dados estruturados.  
- *Azure Service Bus:* Mensageria para comunicação entre componentes.  
---

### Fluxo de Trabalho  

1. *Início do Processo:* O processo é iniciado por um evento, como a adição de um item a uma fila ou uma solicitação HTTP.
2. *Orquestração com Azure Logic Apps:* O Logic App orquestra a execução do fluxo de trabalho, acionando as Azure Functions conforme necessário.
3. *Processamento com Azure Functions:* As Azure Functions realizam operações específicas, como validação de dados ou transformação.
4. *Armazenamento de Dados:* Os dados processados são armazenados no Azure Database.
5. *Mensageria com Azure Service Bus:* O Service Bus facilita a comunicação entre os componentes, garantindo que as mensagens sejam entregues e processadas corretamente.
---

### Diagrama da Solução
Componentes:  

### 1. Azure Logic Apps
O Azure Logic Apps permite criar fluxos de trabalho automatizados que integram diferentes serviços e sistemas. Um Logic App pode ser usado para orquestrar a execução de Azure Functions e outras operações de forma sequencial ou paralela.

yaml
 ```
actions:
  - InitializeVariable:
      name: userID
      value: @{triggerBody().userID}
  - HttpRequest:
      method: POST
      uri: https://your-function-app.azurewebsites.net/api/process
      body: @{variables('userID')}
 ```
---  
### 2. Azure Functions  
As Azure Functions são usadas para executar pequenos trechos de código em resposta a eventos. Elas são ideais para processamento de dados, validação e outras operações que não requerem um servidor dedicado.

csharp
 ```
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("Processing request...");

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    // Process the request body and perform necessary operations
    return new OkObjectResult("Request processed successfully");
}
 ```
---
### 3. Azure Database
O Azure Database armazena os dados estruturados que são processados pelas Azure Functions. Pode ser um banco de dados SQL ou NoSQL, dependendo das necessidades da aplicação.

---
### 4. Azure Service Bus
O Azure Service Bus é um serviço de mensageria que permite a comunicação assíncrona entre os componentes. Ele garante a entrega confiável das mensagens e facilita o processamento paralelo.

json
 ```
{
  "serviceBusNamespace": "your-namespace",  
  "queueName": "your-queue",  
  "message": "your-message"  
}
 ```
---  

## Plano de hospedagem Azure Functions
Ao criar um aplicativo de funções no Azure, você precisa escolher uma opção de hospedagem para o aplicativo. O Azure oferece estas opções de hospedagem para seu código de função.  
Link para as características de cada plano: 
https://learn.microsoft.com/pt-br/azure/azure-functions/functions-scale#overview-of-plans

As opções de hospedagem do Azure Functions são facilitadas pela infraestrutura do Serviço de Aplicativo do Azure em máquinas virtuais do Linux e do Windows. A opção de 
hospedagem que você escolher determinará os seguintes comportamentos:  

- Como o aplicativo de funções é dimensionado.
- Os recursos disponíveis para cada instância do aplicativo de funções.
- Suporte para funcionalidades avançadas, como conectividade à Rede Virtual do Azure.
- Suporte para contêineres do Linux.

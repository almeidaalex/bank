### Banco Simples

Esse projeto tem como intenção simular algumas operações bancárias, de maneira simplificada como Depositar, Sacar e Pagar.

Foi construído usando .NET 5 e MySQL. 

Para testar a aplicação basta executar o seguinte comando na pasta raiz:

```bash
$ docker-compose build && docker-compose up -d
```

A aplicação é constituída de duas principais partes, frontend e api. Após a compilação, quando banco e aplicação estiverem no ar, poderá ser acessada pelo seguinte endereço:

`http://localhost:8080/index.html`


## API do Banco
Também é possível interagir com a aplicação vida API:

#### Depositar

> POST /api/account/deposit
> 
 Request body:
 ```json
 {
   "accountNo": 0,
   "amount": 0
 }
 ```

```bash
$ curl -X POST "http://localhost:8080/api/account/deposit" \
-H "accept: */*" -H  "Content-Type: application/json" -d "{\"accountNo\":1002,\"amount\":100}"
```

<br />

#### Saque

> POST /api/account/withdraw
 
 Request body:
 ```json
 {
   "accountNo": 0,
   "amount": 0
 }
 ```

```bash
$ curl -X POST "http://localhost:8080/api/account/withdraw" \
-H  "accept: */*" -H  "Content-Type: application/json" -d "{\"accountNo\":1002,\"amount\":100}"
```

<br />

#### Pagamento: 
> POST api/account/payment

Request body:
```json
{
  "accountNo": number,
  "invoice": {
    "number": 0,
    "amount": 0.00,
    "dueDate": "2021-03-23T23:14:52.526Z"
  }
}
```

```bash
$ curl -X POST "http://localhost:8080/api/account/payment" \
-H  "accept: */*" -H  "Content-Type: application/json" \
-d "{\"accountNo\":1002,\"invoice\":{\"number\":123,\"amount\":500,\"dueDate\":\"2021-03-23T23:14:52.526Z\"}}"
```

<br />

#### Extrato
> GET /api/account/{id}/statement

Response body
```json
{
  "accountNo": 0,
  "title": "string",
  "balance": 0,
  "statements": [
    {
      "accountNo": 0,
      "date": "2021-03-23T23:18:59.576Z",
      "operation": "string",
      "amount": 0,
      "description": "string"
    }
  ]
}
```

<br />

## Testes Unitários e Integração
O projeto também conta com cobertura de testes unitários e testes de integração. Para executar os testes, incluindo os resultados de cobertura, executar o comando:

```bash
$ dotnet test /p:CollectCoverage=true
```







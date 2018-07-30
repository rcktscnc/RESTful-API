# Case técnico

Esta é uma API Restful de consulta de transações. As instruções de uso estão nas seções abaixo.

## Técnicas

Nesse projeto foram utilizados os seguintes Design Patterns:

- Dependency Injection
- Inversion of Control
- Repository
- Singleton

Outras técnicas utilizadas:

- Proteção da API usando JWT para autenticação e autorização de acesso aos endpoints
- I/O assíncrono para aumentar a performance do servidor
- Versionamento dos endpoints (`/api/v1/`)
- Configuração de Cross-Origin Requests
- Testes Unitários
- Princípios SOLID

## Deployment

Para iniciar o servidor, basta clonar o repositório, e executar os comandos no terminal (no diretório raiz do projeto Case):
```
dotnet restore
dotnet run
```
O projeto deve funcionar sem necessidade de outras configurações, pois utiliza In Memory Database para facilitar a demonstração.

## Acessando Transações

Por padrão, o servidor ouve conexões HTTPS na porta 5001 (`https://localhost:5001`). No primeiro acesso a este endereço, seu browser indicará que esta conexão é insegura devido a falta de um certificado de segurança. Neste caso, basta escolher a opção de adicionar uma exceção ao endereço e avançar.

As transações podem ser acessadas por dois endpoints diferentes: `/api/v1/transactions` e `/api/v1/transactions/secure`. Ambos tem exatamente a mesma funcionalidade e respondem apenas a requests do tipo GET.

A versão "/transactions" não exige autenticação (para facilitar a demonstração), já a versão "/transactions/secure" precisa de um Bearer Token enviado no header `Authorization` da request HTTP. O header `Authorization` deve ter o seguinte formato: `Authorization: Bearer {Seu bearer token}`.

O Bearer Token pode ser adquirido fazendo uma request GET para o endpoint `/api/v1/auth`. Como este projeto é apenas uma demonstração, não é necessário fornecer nenhum tipo de credencial.

## Parâmetros

|Parâmetro | Repetível na query | Exemplo | Detalhe |
|----------|------|------|------|
| | Não |`/api/v1/transactions` | Retorna a primeira página de resultados (20 resultados). Equivalente a `/api/v1/transactions?page=1` |
| Page | Não |`/api/v1/transactions?page=2` | O valor padrão é de 20 resultados por página |
| PageSize | Não |`/api/v1/transactions?page=2&pagesize=6` | Altera o valor máximo de resultados por página |
| OrderBy | Não |`/api/v1/transactions?orderby=date_desc` | Possíveis valores para este parâmetro são `date_asc`, `date_desc`, `amount_asc` e `amount_desc` |
| CNPJ | Sim |`/api/v1/transactions?cnpj=28176030000172&cnpj=15593743000351` |  |
| Date | Sim |`/api/v1/transactions?date=2018-03-01&date=2018-03-27` | Ideal para pesquisar dias separados, por exemplo: dia 10, 15 e 30. Horário da autorização da transação |
| Brand | Sim |`/api/v1/transactions?brand=Visa&brand=Mastercard` |  |
| Acquirer | Sim |`/api/v1/transactions?acquirer=Stone&acquirer=Cielo` |  |
| Status | Sim |`/api/v1/transactions?status=Aprovada&status=Recusada` |  |
| DateMin | Não |`/api/v1/transactions?datemin=2018-03-01T00:55:36&datemax=2018-03-01T01:02:38` | Ideal para pesquisar período específico. Horário da autorização da transação |
| DateMax | Não |`/api/v1/transactions?datemin=2018-03-01T00:55:36&datemax=2018-03-01T01:02:38` | Ideal para pesquisar período específico. Horário da autorização da transação |
| AmountMin | Não |`/api/v1/transactions?amountmin=1&amountmax=1000` | Valor da transação em centavos |
| AmountMax | Não |`/api/v1/transactions?amountmin=1&amountmax=1000` | Valor da transação em centavos |
| Id | Não |`/api/v1/transactions?id=20` | Id da transação. Não pode ser usado em conjunto com nenhum outro parâmetro |

Todos os parâmetros podem ser usados na mesma query com exceção do parâmetro `Id`. Exemplo usando vários parâmetros:

`/api/v1/transactions?status=Aprovada&brand=Visa&date=2018-03-01&date=2018-03-27&cnpj=77404852000179&cnpj=30481457000126`

Os parâmetros `CNPJ`, `Date`, `Brand`, `Acquirer` e `Status`, podem ser usados múltiplas vezes na mesma query para criar um filtro mais abrangente. Exemplo:

`/api/v1/transactions?cnpj=28176030000172&cnpj=15593743000351&brand=Visa&brand=Mastercard`

## Dependências

A biblioteca `CsvHelper` é utilizada para importar o arquivo de transações para o banco de dados.

## Banco de dados

A Entity Framework Core é utilizada para acessar o banco. Para facilitar o deploy, essa demonstração utiliza In Memory Database fornecido pelo próprio ASP.NET Core.

## Ambiente

O projeto foi desenvolvido com a versão 2.1.302 do .NET Core

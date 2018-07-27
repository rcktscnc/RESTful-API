# Deploy

Para iniciar o servidor, basta clonar o repositório, e executar os comandos no terminal (no diretório do projeto):
```
dotnet restore
dotnet run
```
O projeto deve funcionar *out of the box*, pois utiliza In Memory Database para facilitar a demonstração.

# Acessando Transações

Por padrão, o servidor ouve conexões HTTPS na porta 5001 (`https://localhost:5001`).

As transações podem ser acessadas por dois endpoints diferentes: `/api/transactions` e `/api/transactions/secure`, ambas respondem apenas a requests GET.

A versão "/transactions" não exige autenticação (para facilitar a demonstração), já a versão "/transactions/secure" precisa de um Bearer Token enviado no header `Authorization` da request HTTP. O header `Authorization` deve ter o seguinte formato: `Authorization: Bearer {Seu bearer token}`.

O Bearer Token pode ser conseguido fazendo uma request GET para o endpoint `/api/auth`. Como este projeto é apenas uma demonstração, não é necessário fornecer nenhum tipo de credencial.

# Parâmetros

|Parâmetro |  Exemplo | Detalhe |
|----------|------|------|
| CNPJ | `/api/transactions?cnpj=28176030000172&cnpj=15593743000351` ||
| Date | `/api/transactions?date=2018-03-01&date=2018-03-27` |  |
| Brand |  `/api/transactions?brand=Visa&brand=Mastercard` ||
| Acquirer | `/api/transactions?acquirer=Stone&acquirer=Cielo` ||
| Status | `/api/transactions?status=Aprovada&status=Recusada` ||
| DateMin | `/api/transactions?datemin=2018-03-01T00:55:36&datemax=2018-03-01T01:02:38` | Obrigatório usar em conjunto com DateMax |
| DateMax | `/api/transactions?datemin=2018-03-01T00:55:36&datemax=2018-03-01T01:02:38` | Obrigatório usar em conjunto com DateMin |
| AmountMin | `/api/transactions?amountmin=1&amountmax=1000` | Obrigatório usar em conjunto com AmountMax |
| AmountMin | `/api/transactions?amountmin=1&amountmax=1000` | Obrigatório usar em conjunto com AmountMin |

Todos os parâmetros podem ser usados na mesma query. Exemplo usando vários parâmetros:

`/api/transactions?status=Aprovada&brand=Visa&date=2018-03-01&date=2018-03-27&cnpj=77404852000179&cnpj=30481457000126`

Com exceção dos parâmetros `DateMin`, `DateMax`, `AmountMin`, `AmountMax`, todos os pârametros podem ser usados múltiplas vezes na mesma query para criar um filtro mais abrangente: Exemplo:

`/api/transactions?cnpj=28176030000172&cnpj=15593743000351`

Não é recomendado usar os parâmetros `Date` e `DateMin`/`DateMax` na mesma query, já que `Date` filtra dias específicos, anulando o filtro do período entre `DateMin` e `DateMax`.

# Dependências

A única dependência externa é a biblioteca `CsvHelper`, que é utilizada para importar o arquivo de transações para o banco de dados.

# Banco de dados

Para facilitar o deploy na máquina de testes, essa demonstração utiliza o In Memory Database fornecido pelo próprio ASP.NET Core.

# Ambiente

O projeto foi desenvolvido com a versão 2.1.302 do .NET Core
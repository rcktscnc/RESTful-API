# Deploy

Para iniciar o servidor, basta clonar o repositório, e executar os comandos no terminal (no diretório raiz do projeto Case):
```
dotnet restore
dotnet run
```
O projeto deve funcionar *right out of the box*, pois utiliza In Memory Database para facilitar a demonstração.

# Acessando Transações

Por padrão, o servidor ouve conexões HTTPS na porta 5001 (`https://localhost:5001`).

As transações podem ser acessadas por dois endpoints diferentes: `/api/transactions` e `/api/transactions/secure`. Ambos tem exatamente a mesma funcionalidade e respondem apenas a requests do tipo GET.

A versão "/transactions" não exige autenticação (para facilitar a demonstração), já a versão "/transactions/secure" precisa de um Bearer Token enviado no header `Authorization` da request HTTP. O header `Authorization` deve ter o seguinte formato: `Authorization: Bearer {Seu bearer token}`.

O Bearer Token pode ser adquirido fazendo uma request GET para o endpoint `/api/auth`. Como este projeto é apenas uma demonstração, não é necessário fornecer nenhum tipo de credencial.

# Parâmetros

|Parâmetro |  Exemplo | Detalhe |
|----------|------|------|
| | `/api/transactions` | Retona a primeira página de resultados (20 resultados) |
| Page | `/api/transactions?page=2` | O valor padrão é de 20 resultados por página |
| PageSize | `/api/transactions?page=2&pagesize=6` | Altera o valor máximo de resultados por página |
| OrderBy | `/api/transactions?orderby=date_desc` | Possíveis valores para este parâmetro são `date_asc`, `date_desc`, `amount_asc` e `amount_desc` |
| CNPJ | `/api/transactions?cnpj=28176030000172&cnpj=15593743000351` | Pode ser repetido na query |
| Date | `/api/transactions?date=2018-03-01&date=2018-03-27` | Pode ser repetido na query |
| Brand |  `/api/transactions?brand=Visa&brand=Mastercard` | Pode ser repetido na query |
| Acquirer | `/api/transactions?acquirer=Stone&acquirer=Cielo` | Pode ser repetido na query |
| Status | `/api/transactions?status=Aprovada&status=Recusada` | Pode ser repetido na query |
| DateMin | `/api/transactions?datemin=2018-03-01T00:55:36&datemax=2018-03-01T01:02:38` | Obrigatório usar em conjunto com DateMax |
| DateMax | `/api/transactions?datemin=2018-03-01T00:55:36&datemax=2018-03-01T01:02:38` | Obrigatório usar em conjunto com DateMin |
| AmountMin | `/api/transactions?amountmin=1&amountmax=1000` | Obrigatório usar em conjunto com AmountMax |
| AmountMin | `/api/transactions?amountmin=1&amountmax=1000` | Obrigatório usar em conjunto com AmountMin |

Todos os parâmetros podem ser usados na mesma query. Exemplo usando vários parâmetros:

`/api/transactions?status=Aprovada&brand=Visa&date=2018-03-01&date=2018-03-27&cnpj=77404852000179&cnpj=30481457000126`

Os parâmetros `CNPJ`, `Date`, `Brand`, `Acquirer` e `Status`, podem ser usados múltiplas vezes na mesma query para criar um filtro mais abrangente. Exemplo:

`/api/transactions?cnpj=28176030000172&cnpj=15593743000351&brand=Visa&brand=Mastercard`

Não é recomendado usar os parâmetros `Date` e `DateMin`/`DateMax` na mesma query, já que um dos parâmetros anulará o outro.

# Dependências

A biblioteca `CsvHelper` é utilizada para importar o arquivo de transações para o banco de dados.

# Banco de dados

Para facilitar o deploy, essa demonstração utiliza In Memory Database fornecido pelo próprio ASP.NET Core. A Entitify Framework Core é utilizada para acessar o banco.

# Ambiente

O projeto foi desenvolvido com a versão 2.1.302 do .NET Core

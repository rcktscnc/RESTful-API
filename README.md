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

Qualquer parâmetro pode ser usado na mesma query. Exemplo:
```
/api/transactions?status=Aprovada&brand=Visa&date=2018-03-01&date=2018-03-27&cnpj=77404852000179&cnpj=30481457000126
```
Com exceção dos parâmetros `DateMin`, `DateMax`, `AmountMin`, `AmountMax`, todos os pârametros podem ser usados múltiplas vezes na mesma query para criar um filtro mais abrangente: Exemplo:

```
/api/transactions?cnpj=28176030000172&cnpj=15593743000351
```

Não é recomendado usar os parâmetros `Date` e `DateMin`/`DateMax` na mesma query, já que `Date` filtra dias específicos, anulando o filtro do período entre `DateMin` e `DateMax`.

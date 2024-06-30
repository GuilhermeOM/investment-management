## Execução
Configuração de execução do ambiente
```shell
  # clonar o projeto
  git clone https://github.com/GuilhermeOM/investment-management.git

  # entrar na raiz do projeto
  cd investment-management

  # buildar container
  docker-compose up --build

  # entrar no projeto de migration
  cd InvestmentManagement.SharedDataContext

  # installar ef
  dotnet tool install --global dotnet-ef

  # executar migration
  dotnet ef database update
```

Execução das aplicações
```shell
  # na raiz da solução
  cd InvestmentManagement.Client.API

  # executar client api
  dotnet run

  # na raiz da solução
  cd InvestmentManagement.Operator.API

  # executar client api
  dotnet run
```

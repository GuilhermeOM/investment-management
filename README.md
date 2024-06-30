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

## Utilização
#### A aplicação parte de 2 apis e um servidos websocker, onde ambas apis possuem o swagger configurado e o servidor websocket no momento ainda não possue integração.
    
* ClientAPI - responsável pelos processos voltados ao client: criação de usuario, buscar produtos comprados, verificar mercados, etc.
* OperatorAPI - responsável pelos processos voltado ao operador: cadastrar produto financeiro (no momento apenas ações com pagamentos de dividendos), verificar portifolio, cadastrar empresa.    
* WS - comunicar com uma interface para notificar as atualizações do mercado em tempo real.

## TODO
- [ ] Verificação de extrato das compras.
- [ ] Serviço para notificar aproximação de data de expiração.
- [ ] fluxo de compras no mercado secundario.

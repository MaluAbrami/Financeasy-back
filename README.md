# 📊 Financial Control Backend

## 📌 Descrição

Backend de um sistema de controle financeiro pessoal que permite ao usuário:

* Criar conta e autenticar-se
* Cadastrar contas bancárias e seus respectivos saldos
* Cadastrar cartões de crédito/débito
* Registrar transações financeiras
* Classificar gastos por categorias
* Visualizar gastos mensais
* Receber alertas de gastos por mês ou por categoria

O sistema expõe uma API REST para consumo por um frontend web (React).

### Entidades
* User - representa a conta de um usuário
    - Id
    - Email
    - Password
    - Profile_Photo
    - Alert_Limit
* Card - representa um cartão de crédito do usuário que tem seu
limite, transacoes e faturas
    - Id
    - User_Id
    - Bank_Account_Id
    - Category_Id
    - Name
    - Credit_Limit
    - Available_Limit
    - Closing_Day
    - Due_Day
    - Is_Active
    - Deleted_At
* Category - representa uma categoria que pode ser tanto de saídas
como de entradas
    - Id
    - User_Id
    - Name
    - Type
* Transaction - representa uma transação podendo ser uma entrada como
salário, como também um gasto dentro de alguma categoria, vai sempre
descontar do saldo da conta bancária
    - Id
    - Payment_Method
    - User_Id
    - Bank_Account_Id
    - Category_Id
    - Amount
    - Date
    - Description
* Bank account - representa uma conta bancária do usuário que tem seu
saldo próprio, cartões e transações somente daquele banco
    - Id
    - User_Id
    - Bank
    - Balance
    - Is_Active
    - Deleted_At
* Card purchase - representa a compra no cartão de crédito
    - Id
    - User_Id
    - Card_Id
    - Category_Id
    - Total_Amount
    - Installments
    - Purchase_Date
    - Description
* Card installment - representa uma parcela única referente a uma
compra feita no cartão de crédito
    - Id
    - Card_Purchase_Id
    - Card_Invoice_Id
    - Category_Name
    - Number
    - Total_Installments
    - Amount
    - Paid
* Card invoice - representa a fatura de um cartão de crédito que
engloba todas as parcelas de compras feitas que cairam no mesmo mês
    - Id
    - Card_Id
    - Closing_Date
    - Due_Date
    - Total_Amount
    - Is_Paid
* Alert - representa contas a pagar para alertar o usuário referente 
as contas previstas que ele mesmo cadastra para já definir gastos
fixos ao longo dos meses
    - Id
    - UserId
    - CategoryId
    - RecurrenceType
    - DueDate
    - NextDueDate
    - ExpectedAmount

## 🧱 Arquitetura
* Arquitetura em camadas
* CQRS (Command Query Responsibility Segregation)
* Backend desacoplado do frontend
* Comunicação via API REST
* Autenticação baseada em JWT

## 🛠️ Tecnologias Utilizadas

* Linguagem: `C#`
* Framework: `.NET`
* ORM: `Entity Framework`
* Banco de dados: `MySql`
* Autenticação: `JWT`
* Documentação: `Swagger / OpenAPI`
* Testes: `XUnit`

## 🔐 Autenticação

O sistema utiliza autenticação baseada em JWT (JSON Web Token).

* Login retorna um access_token
* Token deve ser enviado no header:

```
Authorization: Bearer {token}
```

## 📚 Funcionalidades

### Usuário
* Cadastro de usuário
* Login
* Atualização de dados
* Definição de um limite de gasto para alertas

### Contas Bancárias
* Criar conta bancária
* Consultar saldo
* Atualizar saldo
* Listar contas do usuário

### Cartões
* Cadastro de cartões
* Associação com contas bancárias
* Consulta de cartões

### Transações
* Registro de transações
* Pix, cartão de débito, transferência e dinheiro
* Associação com conta e categoria
* Filtro por período
* Listar todas as transações realizadas pelo usuário

### Categorias
* Cadastro de categorias
* Classificação de transações

### Relatórios

* Total gasto por mês
* Total gasto por categoria
* Comparativo mensal

### Alertas
* Alerta de gasto máximo mensal
* Alerta por categoria

## 🔌 Endpoints (Resumo)
Documentação completa disponível via Swagger.

## 🗃️ Modelo de Dados (Visão Geral)
* User
* BankAccount
* Card
* CardPurchase
* CardInstallment
* CardInvoice
* Transaction
* Category

Relacionamentos principais:

* User → BankAccount
* BankAccount → Card
* Category → Transaction

## ▶️ Executando o Projeto
```
# instalar dependências
dotnet restore

# rodar aplicação 
dotnet run
```

## 🧪 Testes
```
dotnet test
```

## 📖 Documentação da API
Após subir o projeto, a documentação estará disponível em:
`/swagger`

## 🚀 Próximos Passos (Roadmap)
* Autenticação com OAuth
* Dashboard financeiro
* Gráficos avançados
* Exportação de relatórios (PDF/CSV)
* Notificações em tempo real

## 👩‍💻 Autora

**Maria Luiza Abrami**

*Backend Developer*

*Graduada em Análise e Desenvolvimento de Sistemas*

## 📄 Licença

Este projeto é apenas para fins educacionais e uso pessoal para controle financeiro próprio.
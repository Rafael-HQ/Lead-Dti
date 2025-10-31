# 🧭 Lead-DTI — Guia de Configuração e Execução

Bem-vindo ao projeto Lead-DTI!
Este guia explica passo a passo como clonar o repositório, configurar o ambiente e rodar tanto o back-end (.NET) quanto o front-end (React + Vite).

## 🚀 Clonando o Repositório

Inicie o Git e clone o projeto com os comandos abaixo:

````csharp
git init
git clone https://github.com/Rafael-HQ/Lead-Dti.git
````

## ⚙️ Back-End (.NET)
### 📂 1. Acessando o Projeto

Entre na pasta **Lead-API** utilizando a IDE de sua preferência.
Dentro dela, você encontrará uma solução com dois projetos:

**Desafio-Dti-Lead → Projeto principal (API)**

**Desafio-Dti-Lead-Tests → Projeto de testes unitários**

Navegue até o projeto principal:
````csharp
cd Desafio-Dti-Lead
````

### 🧱 2. Criando e Atualizando o Banco de Dados

Crie as migrações e atualize seu banco local com:
````csharp
dotnet ef migrations add InitialMigrations --project Desafio-Dti-Lead
dotnet ef database update --project Desafio-Dti-Lead
````

### ▶️ 3. Executando a API

Rode o projeto principal e acesse o Swagger para testar os endpoints.

No endpoint POST **/api/Lead/create**, crie alguns Leads para realizar os testes futuros.
<img width="1419" height="593" alt="image" src="https://github.com/user-attachments/assets/2db83996-2a79-4e4c-9820-262d72d9eef3" />


### 🧪 Testes Unitários

Para executar os testes unitários saia da pasta do projeto principal:
````csharp
cd ..
````
Entre na pasta de testes:
````csharp
cd Desafio-Dti-Lead-Tests
````
Execute os testes:
````csharp
dotnet test
````

🔁 Mantenha o projeto principal **rodando** para que o front-end possa consumir a API.

## 💻 Front-End (React + Vite)
### 📂 1. Acessando o Projeto

Entre na pasta lead-front:
````csharp
cd lead-front
````
Verifique se o arquivo package.json está visível:
````csharp
ls
````
Se não estiver, verifique se você está no diretório correto.

### ▶️ 2. Executando o Front-End
Inicie o projeto com:
````csharp
npm run dev
````

Isso iniciará o servidor local, permitindo que o front consuma a API do back-end.

### ⚠️ Observação Importante
Neste projeto, todos os .gitignore foram removidos para facilitar os testes e a análise da aplicação.

API de Gerenciamento de Usuários

Esta API foi desenvolvida para gerenciar usuários, permitindo realizar operações de criação, leitura, atualização e exclusão (CRUD).
O objetivo do projeto é fornecer uma arquitetura organizada, aplicando boas práticas de desenvolvimento, como separação de camadas, padrões de projeto, validações e uso de ORM.

A aplicação foi construída utilizando a abordagem Minimal APIs do .NET, garantindo leveza, performance e simplicidade.
Ela também implementa comunicação estruturada entre as camadas Application, Infrastructure e Domain, tornando o código mais limpo, escalável e fácil de manter.

Tecnologias Utilizadas

.NET 8.0 / 9.0

Entity Framework Core

SQLite

FluentValidation

Swagger / Swashbuckle

Minimal APIs

C#

Padrões de Arquitetura em Camadas

Padrões de Projeto Implementados

Repository Pattern – abstrai o acesso ao banco de dados

Service Pattern – centraliza regras de negócio

DTO Pattern – controla o tráfego de dados entre camadas

Dependency Injection – gerencia dependências de forma automática

Como Executar o Projeto
Pré-requisitos

.NET SDK 8.0 ou superior

Postman ou Swagger (opcional, para testes)

Passos para executar
1. Clone o repositório
git clone https://github.com/Matheus-Leffa/api-usuarios-as-Matheus-Leffa
cd api-usuarios-as-Matheus-Leffa

2. Execute as migrations

Caso ainda não tenha o banco criado:

dotnet ef database update

3. Execute a aplicação
dotnet run


A API estará disponível em:

http://localhost:5075


Swagger estará em:

http://localhost:5075/swagger

Exemplos de Requisições
POST – Criar Usuário
{
  "nome": "Matheus Silva",
  "email": "matheus@email.com",
  "telefone": "47999123456",
  "senha": "SenhaSegura123",
  "dataNascimento": "1999-05-10"
}

PUT – Atualizar Usuário
{
  "nome": "Matheus Atualizado",
  "email": "matheus@novoemail.com",
  "telefone": "47999123456"
}

GET – Listar todos
GET /usuarios

GET – Buscar por ID
GET /usuarios/1

DELETE – Remover
DELETE /usuarios/1

Estrutura do Projeto
api-usuarios-as-Matheus-Leffa/
│
├── Application/
│   ├── DTOs/                Objetos de transferência de dados
│   ├── Interfaces/          Interfaces de serviços e repositórios
│   ├── Services/            Implementação das regras de negócio
│   └── Validators/          Validações com FluentValidation
│
├── Domain/
│   └── Entities/            Entidades do sistema (Usuario)
│
├── Infrastructure/
│   ├── Persistence/         DbContext e configuração do banco
│   └── Repositories/        Implementação do Repository Pattern
│
├── Properties/
│   └── launchSettings.json  Configurações de execução
│
├── appsettings.json         Configurações gerais
├── Program.cs               Configuração inicial da API
└── APIUsuarios.csproj       Arquivo de projeto .NET

Autor

Matheus Santos Leffa
Curso: Análise e Desenvolvimento de Sistemas

LINK PARA DRIVE COM VÍDEO DA APRESENTAÇÃO DO CÓDIGO:

https://drive.google.com/drive/folders/1C6UHBHNg41wWC1iOUwEFiAmc6pT9RbVr?usp=sharing

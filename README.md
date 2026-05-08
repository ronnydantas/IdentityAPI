🚀 Microservices Authentication & User Registration System

Projeto desenvolvido com foco em arquitetura de microsserviços utilizando .NET, RabbitMQ e comunicação assíncrona entre APIs.

A aplicação simula um fluxo real de autenticação e criação de usuários utilizando mensageria para desacoplar serviços.

📌 Arquitetura
Identity API → RabbitMQ → User Service
Fluxo da aplicação
Usuário realiza cadastro na Identity API
API cria usuário e gera autenticação JWT
Evento é publicado no RabbitMQ
UserService consome a mensagem
Pré-cadastro do cliente é realizado automaticamente
🛠 Tecnologias utilizadas
ASP.NET Core / .NET 9
C#
Entity Framework Core
PostgreSQL
RabbitMQ
Docker
JWT Authentication
Swagger
MediatR
📂 Estrutura do projeto
/IdentityAPI
    Responsável por:
    - Cadastro
    - Login
    - Geração de JWT
    - Publicação de eventos RabbitMQ

/UserService
    Responsável por:
    - Consumir mensagens do RabbitMQ
    - Processar eventos
    - Realizar pré-cadastro do cliente
🔐 Autenticação JWT

A autenticação foi implementada utilizando JWT Bearer Authentication.

Após o login, a API retorna um token JWT que pode ser utilizado para acessar rotas protegidas.

Exemplo:

Authorization: Bearer {token}
🐇 RabbitMQ

O RabbitMQ é utilizado para comunicação assíncrona entre os microsserviços.

Exchange utilizada
user.exchange
Queue utilizada
user.queue
Evento publicado
{
  "id": "guid",
  "name": "rafael",
  "fullName": "Rafael Oliveira",
  "email": "rafael@email.com"
}
🐳 Executando RabbitMQ com Docker
docker run -d --name rabbitmq \
-p 5672:5672 \
-p 15672:15672 \
rabbitmq:3-management
Painel do RabbitMQ
http://localhost:15672
Login padrão
user: guest
password: guest
⚙️ Configuração do appsettings.json
RabbitMQ
"RabbitMQ": {
  "Host": "localhost",
  "Port": 5672,
  "Username": "guest",
  "Password": "guest"
}
PostgreSQL
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=database_name;Username=postgres;Password=root"
}
▶️ Como executar o projeto
1. Clonar o repositório
git clone <url-do-repositorio>
2. Subir RabbitMQ
docker run -d --name rabbitmq \
-p 5672:5672 \
-p 15672:15672 \
rabbitmq:3-management
3. Configurar banco PostgreSQL

Atualize as connection strings dos projetos.

4. Executar migrations
dotnet ef database update
5. Executar os projetos
IdentityAPI
dotnet run
UserService
dotnet run
📘 Swagger

A documentação Swagger está habilitada nos projetos.

Exemplo:

https://localhost:{porta}/swagger
🔥 Funcionalidades implementadas

✅ Cadastro de usuário
✅ Login com JWT
✅ Rotas protegidas
✅ Swagger com Bearer Token
✅ Publicação de eventos RabbitMQ
✅ Consumo de mensagens
✅ Pré-cadastro automático
✅ Comunicação assíncrona entre APIs
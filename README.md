# MathBot
## _Simple Microservice Implementation_

## Tech Stack

- .NET Core 5
- React
- MongoDB
- RabbitMQ
- Docker

## Installation

MathBot requires [Docker](https://www.docker.com/) & [.NET](https://dotnet.microsoft.com/)

```sh
git clone https://github.com/UgurKacak/MathBot.git
cd MathBot
docker-compose up -d
```

## Services

| Service | Url | Swagger |
| ------ | ------ | ------ |
| ApiGateway | [http://localhost:1000] | - |
| AuthApi | [http://localhost:2000] | - |
| UserApi | [http://localhost:3000] | [http://localhost:3000/swagger/index.html] |
| QuestionApi | [http://localhost:4000] | [http://localhost:4000/swagger/index.html] |
| QuestionConsumer | - | - |

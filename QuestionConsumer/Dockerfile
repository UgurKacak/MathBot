#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["QuestionConsumer/QuestionConsumer.csproj", "QuestionConsumer/"]
RUN dotnet restore "QuestionConsumer/QuestionConsumer.csproj"
COPY . .
WORKDIR "/src/QuestionConsumer"
RUN dotnet build "QuestionConsumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuestionConsumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuestionConsumer.dll", "--server.urls", "http://+:80;https://+:443"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["QuestionAPI/QuestionAPI.csproj", "QuestionAPI/"]
RUN dotnet restore "QuestionAPI/QuestionAPI.csproj"
COPY . .
WORKDIR "/src/QuestionAPI"
RUN dotnet build "QuestionAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuestionAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuestionAPI.dll", "--server.urls", "http://+:80;https://+:443"]
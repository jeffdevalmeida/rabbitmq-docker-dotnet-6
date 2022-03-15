#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RabbitMQ.Sample/RabbitMQ.Sample.csproj", "RabbitMQ.Sample/"]
COPY ["RabbitMQ.Objects/RabbitMQ.Objects.csproj", "RabbitMQ.Objects/"]
RUN dotnet restore "RabbitMQ.Sample/RabbitMQ.Sample.csproj"
COPY . .
WORKDIR "/src/RabbitMQ.Sample"
RUN dotnet build "RabbitMQ.Sample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RabbitMQ.Sample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RabbitMQ.Sample.dll"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY teste-atak.Server/teste-atak.Server.csproj ./teste-atak.Server/
COPY teste-atak.Application/teste-atak.Application.csproj ./teste-atak.Application/
COPY teste-atak.Domain/teste-atak.Domain.csproj ./teste-atak.Domain/
COPY teste-atak.Infra.Data/teste-atak.Infra.Data.csproj ./teste-atak.Infra.Data/
COPY teste-atak.Infra.Ioc/teste-atak.Infra.Ioc.csproj ./teste-atak.Infra.Ioc/

RUN dotnet restore ./teste-atak.Server/teste-atak.Server.csproj

COPY teste-atak.Server/ ./teste-atak.Server/
COPY teste-atak.Application/ ./teste-atak.Application/
COPY teste-atak.Domain/ ./teste-atak.Domain/
COPY teste-atak.Infra.Data/ ./teste-atak.Infra.Data/
COPY teste-atak.Infra.Ioc/ ./teste-atak.Infra.Ioc/

RUN dotnet build ./teste-atak.Server/teste-atak.Server.csproj -c Release

RUN dotnet publish ./teste-atak.Server/teste-atak.Server.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "teste-atak.Server.dll"]
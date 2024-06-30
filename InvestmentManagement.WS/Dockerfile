FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./InvestmentManagement.WS/InvestmentManagement.WS.csproj" --disable-parallel
RUN dotnet publish "./InvestmentManagement.WS/InvestmentManagement.WS.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 8181

ENTRYPOINT ["dotnet", "InvestmentManagement.WS.dll"]

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /src
COPY *.sln .
COPY SageraLoans.Core.Tests/*.csproj SageraLoans.Core.Tests/
RUN dotnet restore SageraLoans.Core.Tests/*.csproj
COPY SageraLoans.UI/*.csproj SageraLoans.UI/
COPY SageraLoans.Models/*.csproj SageraLoans.Models/
COPY SageraLoans.Core/*.csproj SageraLoans.Core/
COPY SageraLoans.Data/*.csproj SageraLoans.Data/

RUN dotnet restore 

#Copy everything else
COPY . .

#Testing
FROM base AS testing
WORKDIR /src/SageraLoans.UI
WORKDIR /src/SageraLoans.Core
WORKDIR /src/SageraLoans.Models
WORKDIR /src/SageraLoans.Data

RUN dotnet build

WORKDIR /src/SageraLoans.Core.Tests
RUN dotnet test

#Publishing
FROM base AS publish
WORKDIR /src/SageraLoans.UI
RUN dotnet publish -c Release -o /src/publish

#Get the runtime into a folder called app
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS runtime
WORKDIR /app

#COPY SageraLoans.UI/*.db .
COPY --from=publish /src/SageraLoans.Data/Data/User.json .
COPY --from=publish /src/SageraLoans.Data/Data/Company.json .
COPY --from=publish /src/SageraLoans.Data/Data/LoanCategory.json .

COPY --from=publish /src/publish .

#ENTRYPOINT ["dotnet", "SageraLoans.UI.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet SageraLoans.UI.dll
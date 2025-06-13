FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["CoFloPeopleManagement.Api/CoFloPeopleManagement.Api.csproj", "CoFloPeopleManagement.Api/"]
COPY ["CoFloPeopleManagement.Application/CoFloPeopleManagement.Application.csproj", "CoFloPeopleManagement.Application/"]
COPY ["CoFloPeopleManagement.Infrastructure/CoFloPeopleManagement.Infrastructure.csproj", "CoFloPeopleManagement.Infrastructure/"]
COPY ["CoFloPeopleManagement.Application.Tests/CoFloPeopleManagement.Application.Tests.csproj", "CoFloPeopleManagement.Application.Tests/"]
COPY ["CoFloPeopleManagement.sln", "."]

RUN dotnet restore "CoFloPeopleManagement.sln"

COPY . .

WORKDIR "/src/CoFloPeopleManagement.Api"
RUN dotnet build "CoFloPeopleManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoFloPeopleManagement.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=publish /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "CoFloPeopleManagement.Api.dll"]
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["PatientsManagement/PatientsManagement.csproj", "PatientsManagement/"]
COPY ["PatientsManagement.Common/PatientsManagement.Common.csproj", "PatientsManagement.Common/"]
RUN dotnet restore "PatientsManagement/PatientsManagement.csproj"
COPY . .
WORKDIR "/src/PatientsManagement"
RUN dotnet build "PatientsManagement.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PatientsManagement.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PatientsManagement.dll"]

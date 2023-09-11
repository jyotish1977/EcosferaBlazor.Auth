#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Ecosfera.Blazor.UI/Ecosfera.Blazor.UI.csproj", "src/Ecosfera.Blazor.UI/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
RUN dotnet restore "src/Ecosfera.Blazor.UI/Ecosfera.Blazor.UI.csproj"
COPY . .
WORKDIR "/src/src/Ecosfera.Blazor.UI"
RUN dotnet build "Ecosfera.Blazor.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ecosfera.Blazor.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


ENTRYPOINT ["dotnet", "Ecosfera.Blazor.UI.dll"]

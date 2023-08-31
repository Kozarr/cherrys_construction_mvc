#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["cherrys_construction_mvc/cherrys_construction_mvc.csproj", "cherrys_construction_mvc/"]
RUN dotnet restore "cherrys_construction_mvc/cherrys_construction_mvc.csproj"
COPY . .
WORKDIR "/src/cherrys_construction_mvc"
RUN dotnet build "cherrys_construction_mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "cherrys_construction_mvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "cherrys_construction_mvc.dll"]
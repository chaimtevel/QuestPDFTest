#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-noble AS base
USER app
WORKDIR /app



FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["QuestPDF/QuestPDFTest.csproj", "QuestPDF/"]
RUN dotnet restore "./QuestPDF/QuestPDFTest.csproj"
COPY . .
WORKDIR "/src/QuestPDF"
RUN dotnet build "./QuestPDFTest.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./QuestPDFTest.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false



FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

USER root

RUN apt-get update && apt-get install -y qpdf && \
    apt-get clean && \ 
    rm -rf /var/lib/apt/lists/*


ENTRYPOINT ["dotnet", "QuestPDFTest.dll"]


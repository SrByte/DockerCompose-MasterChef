# Usa a imagem oficial do SDK do .NET Core como imagem base
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Define o diretório de trabalho dentro do contêiner para /app
WORKDIR /app

# Copia o arquivo do projeto (.csproj) para o contêiner e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia o resto do código da aplicação para o contêiner
COPY . .

# Compila a aplicação com a configuração de Release e publica os artefatos no diretório 'dist'
RUN dotnet publish -c Release -o dist

# Inicia a construção da imagem de tempo de execução utilizando a imagem do ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime

# Define o diretório de trabalho para /app na imagem de tempo de execução
WORKDIR /app

# Copia os artefatos publicados da etapa de construção para o diretório de trabalho na imagem de tempo de execução
COPY --from=build /app/dist ./

# Informa ao Docker que a aplicação dentro do contêiner estará escutando na porta 7107
EXPOSE 7107

# Define as variáveis de ambiente
ENV ASPNETCORE_URLS=http://+:7107
ENV ASPNETCORE_ENVIRONMENT=Development

# Define o comando que será executado quando o contêiner iniciar, que nesse caso, inicia a aplicação .NET
ENTRYPOINT ["dotnet", "Api.MasterChef.dll"]

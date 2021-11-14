#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ENV BuildingDocker true
WORKDIR /src
COPY ["Tasks/Tasks.csproj", "Tasks/"]
RUN dotnet restore "Tasks/Tasks.csproj"
COPY . .
WORKDIR "/src/Tasks"
RUN dotnet build "Tasks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tasks.csproj" -c Release -o /app/publish

FROM node:12 AS build-node
WORKDIR /ClientApp

COPY Tasks/ClientApp/package.json .
COPY Tasks/ClientApp/yarn.lock .
RUN yarn install
COPY Tasks/ClientApp/ .
ENV NODE_ENV production
RUN yarn run build

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build-node /ClientApp/build ./ClientApp/build
ENTRYPOINT ["dotnet", "Tasks.dll"]

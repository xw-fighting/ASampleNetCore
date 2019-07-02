#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-1803 AS build
WORKDIR /src
COPY ["src/Applications/ASample.NetCore.SignalRWeb/ASample.NetCore.SignalRWeb.csproj", "src/Applications/ASample.NetCore.SignalRWeb/"]
RUN dotnet restore "src/Applications/ASample.NetCore.SignalRWeb/ASample.NetCore.SignalRWeb.csproj"
COPY . .
WORKDIR "/src/src/Applications/ASample.NetCore.SignalRWeb"
RUN dotnet build "ASample.NetCore.SignalRWeb.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ASample.NetCore.SignalRWeb.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ASample.NetCore.SignalRWeb.dll"]
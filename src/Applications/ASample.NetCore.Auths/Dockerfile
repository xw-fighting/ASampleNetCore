#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-1803 AS build
WORKDIR /src
COPY ["src/Applications/ASample.NetCore.Auths/ASample.NetCore.Auths.csproj", "src/Applications/ASample.NetCore.Auths/"]
RUN dotnet restore "src/Applications/ASample.NetCore.Auths/ASample.NetCore.Auths.csproj"
COPY . .
WORKDIR "/src/src/Applications/ASample.NetCore.Auths"
RUN dotnet build "ASample.NetCore.Auths.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ASample.NetCore.Auths.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ASample.NetCore.Auths.dll"]
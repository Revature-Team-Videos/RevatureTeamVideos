FROM mcr.microsoft.com/dotnet/sdk as base

WORKDIR /workspace
COPY aspnet .
RUN dotnet build -c Release
RUN dotnet publish -c Release -o out VideoShare.Client/*.csproj

FROM mcr.microsoft.com/dotnet/aspnet

WORKDIR /workspace
COPY --from=base out .
CMD ["dotnet", "VideoShare.Client.dll"]
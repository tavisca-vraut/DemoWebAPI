FROM mcr.microsoft.com/dotnet/core/aspnet:2.2

WORKDIR /host

COPY . /host

EXPOSE 80

ARG LaunchFile="Default.dll"

# ENTRYPOINT ["dotnet", ${LaunchFile}]
ENTRYPOINT ["dotnet", "DemoWebApp.dll"]

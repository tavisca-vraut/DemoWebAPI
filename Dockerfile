FROM mcr.microsoft.com/dotnet/core/aspnet

WORKDIR /host

COPY . /host

EXPOSE 80

ARG APPLICATION_NAME_TO_BE_HOSTED=Default
ENV HOST_THIS_APP=${APPLICATION_NAME_TO_BE_HOSTED}

# ENTRYPOINT ["dotnet", ${LaunchFile}]
# ENTRYPOINT ["dotnet", "${HOST_THIS_APP}.dll"]
CMD dotnet ${HOST_THIS_APP}.dll

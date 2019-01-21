#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-sdk AS publish
WORKDIR "/src/CallsRegistry"
COPY [".", "/src/CallsRegistry/"]
RUN dotnet publish "CallsRegistry.csproj" -c Release -o /app

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CallsRegistry.dll" ]
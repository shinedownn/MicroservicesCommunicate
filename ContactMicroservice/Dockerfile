#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app/contact
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ContactMicroservice/ContactMicroservice.csproj", "ContactMicroservice/"]
RUN dotnet restore "ContactMicroservice/ContactMicroservice.csproj"
COPY . .
WORKDIR "/src/ContactMicroservice"
RUN dotnet build "ContactMicroservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactMicroservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app/contact
COPY --from=publish /app/contact/publish .
ENTRYPOINT ["dotnet", "ContactMicroservice.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base
WORKDIR /app
EXPOSE 8090
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
WORKDIR /src
COPY ["/source/org.ohdsi.cdm.presentation.builderwebapi/org.ohdsi.cdm.presentation.builderwebapi.csproj", "org.ohdsi.cdm.presentation.builderwebapi/"]
COPY ["/source/org.ohdsi.cdm.framework.common/org.ohdsi.cdm.framework.common.csproj", "org.ohdsi.cdm.framework.common/"]
COPY ["/source/org.ohdsi.cdm.framework.desktop/org.ohdsi.cdm.framework.desktop.csproj", "org.ohdsi.cdm.framework.desktop/"]
COPY ["/source/org.ohdsi.cdm.framework.etl/org.ohdsi.cdm.framework.etl.common/org.ohdsi.cdm.framework.etl.common.csproj", "org.ohdsi.cdm.framework.etl/org.ohdsi.cdm.framework.etl.common/"]
RUN dotnet restore "org.ohdsi.cdm.presentation.builderwebapi/org.ohdsi.cdm.presentation.builderwebapi.csproj"
COPY . .
WORKDIR "/src/source/org.ohdsi.cdm.presentation.builderwebapi"
RUN dotnet build "org.ohdsi.cdm.presentation.builderwebapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "org.ohdsi.cdm.presentation.builderwebapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

RUN apt update -y  &&  apt upgrade -y && apt-get update 
RUN apt install -y curl  unixodbc-dev

RUN apt-get update && apt-get install -y gnupg2

RUN curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
RUN curl https://packages.microsoft.com/config/ubuntu/18.04/prod.list > /etc/apt/sources.list.d/mssql-release.list
RUN apt-get update && ACCEPT_EULA=Y apt-get install -y msodbcsql17

RUN apt-get install -y odbc-postgresql
RUN apt-get update && apt-get install -y unzip libsasl2-modules-gssapi-mit

RUN curl -sL https://databricks-bi-artifacts.s3.us-east-2.amazonaws.com/simbaspark-drivers/odbc/2.6.29/SimbaSparkODBC-2.6.29.1049-Debian-64bit.zip -o databricksOdbc.zip && unzip databricksOdbc.zip
RUN dpkg -i simbaspark_2.6.29.1049-2_amd64.deb
RUN printf '%s\n' "[Simba Spark ODBC Driver]" "Description=Simba Apache Spark ODBC Connector (64-bit)" "Driver=/opt/simba/spark/lib/64/libsparkodbc_sb64.so" >>/etc/odbcinst.ini

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "org.ohdsi.cdm.presentation.builderwebapi.dll"]
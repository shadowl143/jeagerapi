FROM mcr.microsoft.com/mssql/server

EXPOSE 1433

# Copiar el script al contenedor
COPY init.sql /init.sql

# Crear un script de entrada para ejecutar SQL al iniciar
COPY entrypoint.sh /entrypoint.sh
# RUN chmod +x /entrypoint.sh

CMD ["/bin/bash", "/entrypoint.sh"]

#!/bin/bash
# Inicia SQL Server en segundo plano
/opt/mssql/bin/sqlservr &

# Espera a que SQL Server esté disponible
echo "Esperando a que SQL Server esté disponible..."
sleep 20

# Ejecuta el script SQL
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -i /init.sql

# Mantiene el contenedor vivo
wait
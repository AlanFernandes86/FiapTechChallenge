/opt/mssql-tools/bin/sqlcmd -S sql-server -U sa -P SqlServer2019! -d master -i /tmp/CreateDatabase.sql
/opt/mssql-tools/bin/sqlcmd -S sql-server -U sa -P SqlServer2019! -d master -i /tmp/CreateTables.sql
/opt/mssql-tools/bin/sqlcmd -S sql-server -U sa -P SqlServer2019! -d master -i /tmp/SetupDatabase.sql
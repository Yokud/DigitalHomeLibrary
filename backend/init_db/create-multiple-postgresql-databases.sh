#!/bin/bash
set -e
set -u

function create_user_and_database() {
    local database=$(echo $1 | cut -d ':' -f 1)
    local user=$(echo $1 | cut -d ':' -f 2)
    local password=$(echo $1 | cut -d ':' -f 3)

    echo "  Creating user '$user' and database '$database'"

    psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" <<-EOSQL
        CREATE USER $user WITH ENCRYPTED PASSWORD '$password';
        CREATE DATABASE $database;
        GRANT ALL PRIVILEGES ON DATABASE $database TO $user;
        -- В новых версиях Postgres (15+) нужно также дать права на схему public
        \c $database
        GRANT ALL ON SCHEMA public TO $user;
EOSQL
}

if [ -n "$POSTGRES_MULTIPLE_DATABASES" ]; then
    echo "Multiple database creation requested: $POSTGRES_MULTIPLE_DATABASES"
    for db_setup in $(echo $POSTGRES_MULTIPLE_DATABASES | tr ',' ' '); do
        create_user_and_database $db_setup
    done
    echo "All databases and users created"
fi

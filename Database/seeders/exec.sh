#!/bin/bash

DB=temuruang

echo 'Seeding workspaces table...';
cat Database/seeders/workspaces_seed.sql | docker exec -i temuruang-db psql -U postgres -d $DB

# echo 'Seeding articles table...';
# cat Database/seeders/articles_seed.sql | docker exec -i temuruang-db psql -U postgres -d $DB

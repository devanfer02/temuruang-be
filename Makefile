# Variables
MIGRATIONS_FOLDER = Migrations

.PHONY: help
help:
	@echo "Available commands:"
	@echo ""
	@echo "  run               - Run the application"
	@echo "  build             - Build the project"
	@echo "  migrate           - Apply the migrations to the database"
	@echo "  clean-migrations  - Clean up migration files"
	@echo "  fresh-migrate     - Drop the existing database, add initial migrations, and update the database"

.PHONY: run
run:
	dotnet run

.PHONY: dev
dev:
	dotnet watch run

.PHONY: build
build: restore
	dotnet build

.PHONY: migrate
migrate:
	dotnet ef database update

.PHONY: clean-migrations
clean-migrations:
	rm -rf $(MIGRATIONS_FOLDER)/*

.PHONY: migrate-fresh
migrate-fresh:
	@echo "Dropping existing database..."
	dotnet ef database drop --force
	@echo "Applying migrations..."
	dotnet ef migrations add InitialCreate
	dotnet ef database update

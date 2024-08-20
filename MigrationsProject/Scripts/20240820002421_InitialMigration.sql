CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

CREATE TABLE "Things" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Things" PRIMARY KEY AUTOINCREMENT,
    "Description" TEXT NOT NULL
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240820002421_InitialMigration', '8.0.8');


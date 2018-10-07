CREATE TABLE `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `Companies` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Name` longtext NOT NULL,
    `EstablishmentYear` int NOT NULL,
    CONSTRAINT `PK_Companies` PRIMARY KEY (`Id`)
);

CREATE TABLE `Employees` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `FirstName` longtext NOT NULL,
    `LastName` longtext NOT NULL,
    `DateOfBirth` datetime(6) NOT NULL,
    `JobTitle` int NOT NULL,
    `CompanyId` bigint NOT NULL,
    CONSTRAINT `PK_Employees` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Employees_Companies_CompanyId` FOREIGN KEY (`CompanyId`) REFERENCES `Companies` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_Employees_CompanyId` ON `Employees` (`CompanyId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20181007132501_Initial', '2.1.4-rtm-31024');


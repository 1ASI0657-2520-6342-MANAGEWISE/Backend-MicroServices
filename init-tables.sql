CREATE DATABASE IF NOT EXISTS managewise_iam;
CREATE DATABASE IF NOT EXISTS managewise_profiles;
CREATE DATABASE IF NOT EXISTS managewise_tasks;
CREATE DATABASE IF NOT EXISTS managewise_payments;

-- IAM Database (ya tiene tablas)
USE managewise_iam;

-- Profiles Database
USE managewise_profiles;

CREATE TABLE IF NOT EXISTS `Users` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `FirstName` longtext,
    `LastName` longtext,
    `Age` int NOT NULL,
    `Email` longtext,
    `Phone` longtext,
    `Password` longtext,
    `ProfileImg` longtext,
    `CompanyName` longtext,
    `CompanyEmail` longtext,
    `CompanyCountry` longtext,
    `CompanyId` int NOT NULL,
    `Role` int NOT NULL,
    `TeamRegisterCode` longtext,  
    PRIMARY KEY (`Id`)
    ) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS `DeletedUsers` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `DeletedAt` datetime(6) NOT NULL,
    `DeletedBy` longtext,
    `UserData` longtext,
    PRIMARY KEY (`Id`)
    ) ENGINE=InnoDB;

-- Tasks Database
USE managewise_tasks;

CREATE TABLE IF NOT EXISTS `Projects` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Rating` double NOT NULL,
  `CompanyId` int NOT NULL,
  `ProjectDate` date NOT NULL,
  `ProjectTime` time(6) NOT NULL,
  `ProjectLocation` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `AuditDate` date NOT NULL,
  `TeamMemberUserIds` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS `TaskItems` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `DueDate` date NOT NULL,
  `ProjectId` int NOT NULL,
  `State` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `AssigneeId` int NOT NULL,
  `CreatedAt` date NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_TaskItems_ProjectId` (`ProjectId`),
  CONSTRAINT `FK_TaskItems_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS `ProjectImages` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ProjectId` int NOT NULL,
  `Url` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_ProjectImages_ProjectId` (`ProjectId`),
  CONSTRAINT `FK_ProjectImages_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS `ProjectTeamMembers` (
  `UserId` int NOT NULL,
  `ProjectId` int NOT NULL,
  PRIMARY KEY (`UserId`, `ProjectId`),
  KEY `IX_ProjectTeamMembers_ProjectId` (`ProjectId`),
  CONSTRAINT `FK_ProjectTeamMembers_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS `FavoriteProjects` (
  `UserId` int NOT NULL,
  `ProjectId` int NOT NULL,
  PRIMARY KEY (`UserId`, `ProjectId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Payments Database
USE managewise_payments;

CREATE TABLE IF NOT EXISTS `PaymentDetails` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Amount` decimal(18,2) NOT NULL,
  `Currency` varchar(3) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `TransactionDate` datetime(6) NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

﻿CREATE TABLE [dbo].[UserProfiles]
(
    [UserId]                 INT           NOT NULL IDENTITY (1, 1),
    [FirstName]              NVARCHAR(50)  NOT NULL,
    [LastName]               NVARCHAR(50)  NOT NULL,
    [DirectionId]            INT           NOT NULL,
    [Education]              NVARCHAR(50)  NULL,
    [Address]                NVARCHAR(120) NULL,
    [BirthDate]              DATE          NULL,
    [StartDate]              DATE          NULL,
    [UniversityAverageScore] FLOAT         NOT NULL,
    [MathScore]              FLOAT         NOT NULL,
    [Sex]                    TINYINT       NOT NULL,
    [Skype]                  NVARCHAR(50)  NULL,
    [Email]                  NVARCHAR(50)  NOT NULL,
    [MobilePhone]            NVARCHAR(50)  NOT NULL,

    CONSTRAINT PK_UserProfiles_UserId PRIMARY KEY (UserId),
    CONSTRAINT FK_UserProfiles_Directions_DirectionId FOREIGN KEY (DirectionId) REFERENCES Directions (DirectionId) on delete cascade on update cascade
)

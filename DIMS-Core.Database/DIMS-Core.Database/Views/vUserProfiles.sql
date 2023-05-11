﻿CREATE or alter VIEW [dbo].[vUserProfiles]
AS
SELECT UserProfiles.UserId,
       (UserProfiles.FirstName + ' ' + UserProfiles.LastName) AS FullName,
       UserProfiles.Email,
       Directions.Name AS Direction,
       UserProfiles.Sex,
       UserProfiles.Education,
       (SELECT dbo.GetFullAge(UserProfiles.BirthDate)) AS Age,
       UserProfiles.UniversityAverageScore,
       UserProfiles.MathScore,
       UserProfiles.Address,
       UserProfiles.MobilePhone,
       UserProfiles.Skype,
       UserProfiles.StartDate
FROM [UserProfiles]
     INNER JOIN [Directions] ON UserProfiles.DirectionId = Directions.DirectionId
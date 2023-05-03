create or alter view vUserProgress
as
select UP.UserId, T.TaskId, TT.TaskTrackId, CONCAT(UP.FirstName, ' ', UP.LastName) as UserName, T.Name, TT.TrackNote, TT.TrackDate 
from UserTask as UT
join UserProfiles as UP on UP.UserId = UT.UserId
join Task as T on T.TaskId = UT.TaskId
join TaskTrack as TT on TT.UserTaskId = UT.UserTaskId
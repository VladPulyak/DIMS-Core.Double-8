create or alter view vUserTrack 
as
select UT.UserId, T.TaskId, TT.TaskTrackId, T.Name as TaskName, TT.TrackNote, TT.TrackDate
from UserTask as UT
join Task as T on T.TaskId = UT.TaskId
join TaskTrack as TT on TT.UserTaskId = UT.UserTaskId
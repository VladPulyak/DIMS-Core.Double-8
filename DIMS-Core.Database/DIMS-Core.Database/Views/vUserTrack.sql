create or alter view vUserTrack
as
select UserId, TaskId, TaskTrackId, UserName, Name as TaskName, TrackNote, TrackDate
from vUserProgress
create or alter view vUserTask
as
select UserProfiles.UserId, Task.TaskId, Task.Name as TaskName, Task.Description, Task.StartDate, Task.DeadlineDate, TaskState.StateName
from UserTask
join Task on Task.TaskId = UserTask.TaskId
join UserProfiles on UserProfiles.UserId = UserTask.UserId
join TaskState on TaskState.StateId = UserTask.StateId
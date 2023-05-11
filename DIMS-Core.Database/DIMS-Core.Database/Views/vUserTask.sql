create or alter view vUserTask
as
select UserTask.UserId, Task.TaskId, Task.Name as TaskName, Task.Description, Task.StartDate, Task.DeadlineDate, TaskState.StateName
from UserTask
join Task on Task.TaskId = UserTask.TaskId
join TaskState on TaskState.StateId = UserTask.StateId
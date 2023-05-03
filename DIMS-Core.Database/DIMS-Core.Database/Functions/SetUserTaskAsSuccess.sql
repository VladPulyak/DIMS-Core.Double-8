create or alter procedure SetUserTaskAsSuccess @UserId int, @TaskId int 
as
BEGIN
    update UserTask
    set StateId = 2
    where UserTaskId = 
    (select UserTaskId from UserTask
    where UserId = @UserId and TaskId = @TaskId)
END
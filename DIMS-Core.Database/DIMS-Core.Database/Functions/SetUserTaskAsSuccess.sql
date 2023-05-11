create or alter procedure SetUserTaskAsSuccess @UserId int, @TaskId int 
as
BEGIN
    update UserTask
    set StateId = 2
    where UserId = @UserId and TaskId = @TaskId
END
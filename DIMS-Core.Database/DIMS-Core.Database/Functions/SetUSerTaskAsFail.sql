create or alter procedure SetUserTaskAsFail @UserId int, @TaskId int 
as
BEGIN
    update UserTask
    set StateId = 3
    where UserId = @UserId and TaskId = @TaskId
END
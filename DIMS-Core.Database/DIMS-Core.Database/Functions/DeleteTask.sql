create or alter procedure DeleteTask @TaskId int 
as
delete from Task where TaskId = @TaskId;
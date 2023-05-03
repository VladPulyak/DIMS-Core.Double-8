create or alter view vUserTask
as
select UT.UserId, UT.TaskId, T.Name, T.Description, T.StartDate, T.DeadlineDate, TS.StateName
    from Task as T
    join UserTask as UT on T.TaskId = UT.TaskId
    join TaskState as TS on TS.StateId = UT.StateId
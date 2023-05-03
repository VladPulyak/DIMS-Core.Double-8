create table Task
(
    TaskId int identity(1,1) not null,
    Name nvarchar(50) not null,
    Description nvarchar(250) null,
    StartDate datetime not null,
    DeadlineDate datetime not null,
    CONSTRAINT PK_TaskId PRIMARY KEY (TaskId)
)
create table UserTask
(
    UserTaskId int identity(1,1) not null,
    TaskId int not null,
    UserId int not null,
    StateId int not null,
    constraint PK_UserTaskId primary key (UserTaskId),
    constraint FK_TaskId foreign key (TaskId) references Task(TaskId),
    constraint FK_UserId foreign key (UserId) references UserProfiles(UserId),
    constraint FK_StateId foreign key (StateId) references TaskState(StateId)
)
create table UserTask
(
    UserTaskId int identity(1,1) not null,
    TaskId int not null,
    UserId int not null,
    StateId int not null,
    constraint PK_UserTaskId primary key (UserTaskId),
    constraint FK_TaskId foreign key (TaskId) references Task(TaskId) on delete cascade on update cascade,
    constraint FK_UserId foreign key (UserId) references UserProfiles(UserId) on delete cascade on update cascade,
    constraint FK_StateId foreign key (StateId) references TaskState(StateId) on delete cascade on update cascade
)
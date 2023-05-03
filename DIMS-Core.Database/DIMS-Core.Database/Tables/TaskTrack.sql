create table TaskTrack
(
    TaskTrackId int identity(1,1) not null,
    UserTaskId int not null,
    TrackDate datetime not null,
    TrackNote nvarchar(250) not null,
    constraint PK_TaskTrackId primary key (TaskTrackId),
    constraint FK_UserTaskId foreign key (UserTaskId) references UserTask(UserTaskId)
)
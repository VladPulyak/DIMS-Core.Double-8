create table TaskState
(
    StateId int identity(1,1) not null,
    StateName nvarchar(50) unique not null,
    constraint PK_StateId primary key (StateId)
)
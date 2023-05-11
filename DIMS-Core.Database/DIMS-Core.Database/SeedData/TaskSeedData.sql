insert into Task (Name, Description, StartDate, DeadlineDate) values
(
    'Create table Task',
    'You need to create table Task.',
    GETDATE(),
    DATEADD(day,15,GETDATE())
),
    (
    'Create table Users',
    'You need to create table User.',
    GETDATE(),
    DATEADD(day,10,GETDATE())
),
    (
    'Create table Direction',
    'You need to create table Direction.',
    GETDATE(),
    DATEADD(day,20,GETDATE())
)
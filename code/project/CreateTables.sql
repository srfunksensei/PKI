CREATE DATABASE TEST
GO

USE TEST

CREATE TABLE [New_Users](
	[userID] INTEGER PRIMARY KEY IDENTITY NOT NULL,
	[userType] [nvarchar](30) NOT NULL,
	[name] [nvarchar](30) NULL,
	[surname] [nvarchar](30) NULL,
	[title] [nvarchar](30) NULL,
	[office] [nvarchar](30) NULL,
	[phone] [nvarchar](30) NULL,
	[mail] [nvarchar](30) NOT NULL
	) ON [PRIMARY]
GO

CREATE TABLE [All_Users](
	[userID] INTEGER PRIMARY KEY IDENTITY NOT NULL,
	[username] [nvarchar](30) UNIQUE NOT NULL,
	[pass] [nvarchar](30) NULL,
	[userType] [nvarchar](30) NOT NULL,
	[name] [nvarchar](30) NULL,
	[surname] [nvarchar](30) NULL,
	[title] [nvarchar](30) NULL,
	[office] [nvarchar](30) NULL,
	[phone] [nvarchar](30) NULL,
	[mail] [nvarchar](30) NULL,
	[isLogin] BIT NOT NULL,
	[isBlocked] BIT NOT NULL
	) ON [PRIMARY]
GO

CREATE TABLE [All_LabPersonnel](
	[labPersonnelID] INTEGER PRIMARY KEY IDENTITY NOT NULL,
	[labPersonnel_1] [nvarchar](30) NULL,
	[labPersonnel_2] [nvarchar](30) NULL,
	[labPersonnel_3] [nvarchar](30) NULL,
	[labPersonnel_4] [nvarchar](30) NULL,
	[labPersonnel_5] [nvarchar](30) NULL
	
	) ON [PRIMARY]
GO

CREATE TABLE [All_Classrooms](
	[classroomID] INTEGER PRIMARY KEY IDENTITY NOT NULL,
	[number] [nvarchar](10) UNIQUE NOT NULL,
	[blackboard] BIT NULL,
	[computers] BIT NULL,
	[projector] BIT NULL,
	[laboratory] BIT NULL,
	[capacity] INTEGER NOT NULL,
	[workingHours] [nvarchar](6) NOT NULL,
	[comment] [nvarchar](1000) NULL,
	[notice] [nvarchar](1000) NULL,
	[personelID] INT NOT NULL,
	FOREIGN KEY (personelID) REFERENCES All_LabPersonnel(labPersonnelID),
	[isVisible] BIT NOT NULL 
	) ON [PRIMARY]
GO

CREATE TABLE [All_Reservations](
	[reservationID] INTEGER PRIMARY KEY IDENTITY NOT NULL,
	[course] [nvarchar](10) NOT NULL,
	[ev_time] [nvarchar](6) NOT NULL,
	[ev_date] [nvarchar] (10) NOT NULL,
	[classroomNumberID] INT NOT NULL,
	[userID] INT NOT NULL,
	FOREIGN KEY (classroomNumberID) REFERENCES All_Classrooms(classroomID),
	FOREIGN KEY (userID) REFERENCES All_Users(userID)
	) ON [PRIMARY]
GO

INSERT INTO All_Users( username, pass, userType, isLogin, isBlocked)
 VALUES ( 'admin1', '123' , 'Administrator', 1, 0)
INSERT INTO All_Users( username, pass, userType, isLogin, isBlocked)
 VALUES ( 'lab1', '123', 'Laboratory personnel', 1, 0)
INSERT INTO All_Users( username, pass, userType, isLogin, isBlocked)
 VALUES ( 'nastavnik1', '123', 'Teacher', 1, 0)

use TEST
drop table New_Users
drop table All_Reservations
drop table All_Users
drop table All_Classrooms
drop table All_LabPersonnel


select * from All_LabPersonnel
select * from All_Classrooms
select * from all_users
select * from new_users
select * from All_Reservations
CREATE DATABASE FeedbackSystem
GO
USE FeedbackSystem
GO 
CREATE TABLE Trainee_Comment(
	ClassId INT ,
	ModuleId INT ,
	TraineeId NVARCHAR(50),
	Comment NVARCHAR(255)
	PRIMARY KEY(ClassId,ModuleId,TraineeId)
)
CREATE TABLE Trainee_Assignment(
	RegistrationCode NVARCHAR(50) ,
	TraineeId NVARCHAR(50) 
	PRIMARY KEY(RegistrationCode,TraineeId)
)
CREATE TABLE TypeFeedback(
	TypeId INT PRIMARY KEY IDENTITY,
	TypeName NVARCHAR(50) ,
	IsDeleted BIT
)

CREATE TABLE Feedback(
	FeedbackId INT PRIMARY KEY IDENTITY,
	Title NVARCHAR(255) ,
	AdminId nvarchar(450) REFERENCES dbo.Users(Id),
	IsDeleted BIT ,
	TypeFeedbackId INT REFERENCES dbo.TypeFeedback(TypeId) 
)
CREATE TABLE Topic(
	TopicId INT PRIMARY KEY IDENTITY,
	TopicName NVARCHAR(255) 
)
CREATE TABLE Question(
	QuestionId INT PRIMARY KEY IDENTITY,
	TopicId INT REFERENCES dbo.Topic(TopicId),
	QuestionContent NVARCHAR(255) ,
	IsDeleted BIT 
)
CREATE TABLE Feedback_Question(
	FeedbackId INT REFERENCES dbo.Feedback(FeedbackId),
	QuestionId INT REFERENCES dbo.Question(QuestionId),
	PRIMARY KEY(FeedbackId,QuestionId)
)
CREATE TABLE Module (
	ModuleId INT PRIMARY KEY IDENTITY,
	AdminId NVARCHAR(450) REFERENCES dbo.Users(Id),
	ModuleName NVARCHAR(50),
	StartTime DATE ,
	EndTime DATE  ,
	IsDeleted BIT ,
	FeedbackStartTime DATETIME ,
	FeedbackEndTime DATETIME ,
	FeedbackId INT REFERENCES dbo.Feedback(FeedbackId)
)

CREATE TABLE Class(
	ClassId INT PRIMARY KEY IDENTITY,
	ClassName NVARCHAR(255) ,
	Capacity INT ,
	StartTime DATE ,
	EndTime DATE,
	IsDeleted BIT 
)
CREATE TABLE Assignment(
	ClassId INT REFERENCES dbo.Class(ClassId),
	ModuleId INT REFERENCES dbo.Module(ModuleId),
	TrainerId nvarchar(450) REFERENCES dbo.Users(Id),
	RegistrationCode NVARCHAR(50)
	PRIMARY KEY(ClassId,ModuleId,TrainerId) 
)

CREATE TABLE Answer(
	ClassId INT  REFERENCES dbo.Class(ClassId),
	ModuleId INT REFERENCES dbo.Module(ModuleId),
	TraineeId nvarchar(450) REFERENCES dbo.Users(Id),
	QuestionId INT  REFERENCES dbo.Question(QuestionId),
	Value INT
	PRIMARY KEY(ClassId,ModuleId,TraineeId,QuestionId)
)
CREATE TABLE Enrollment(
	ClassId INT REFERENCES dbo.Class(ClassId),
	TraineeId nvarchar(450) REFERENCES dbo.Users(Id),
	PRIMARY KEY(ClassId,TraineeId)
)

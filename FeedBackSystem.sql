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
CREATE TABLE Admin(
	UserName NVARCHAR(50) PRIMARY KEY,
	Name NVARCHAR(255) ,
	Email NVARCHAR(50) ,
	Password NVARCHAR(255) 
)
CREATE TABLE Feedback(
	FeedbackId INT PRIMARY KEY IDENTITY,
	Title NVARCHAR(255) ,
	AdminId NVARCHAR(50) REFERENCES dbo.Admin(UserName),
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
	AdminId NVARCHAR(50) REFERENCES dbo.Admin(UserName),
	ModuleName NVARCHAR(50),
	StartTime DATE ,
	EndTime DATE  ,
	IsDeleted BIT ,
	FeedbackStartTime DATETIME ,
	FeedbackEndTime DATETIME ,
	FeedbackId INT REFERENCES dbo.Feedback(FeedbackId)
)
CREATE TABLE Trainer(
	Username NVARCHAR(50) PRIMARY KEY,
	Name NVARCHAR(50) ,
	Email NVARCHAR(50) ,
	Phone NVARCHAR(50) ,
	Address NVARCHAR(255) ,
	IsActive BIT ,
	Password NVARCHAR(255) ,
	IdSkill INT,
	ActivationCode NVARCHAR(50) ,
	ResetPasswordCode NVARCHAR(50),
	IsReceiveNotification BIT
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
	TrainerId NVARCHAR(50) REFERENCES dbo.Trainer(Username),
	RegistrationCode NVARCHAR(50)
	PRIMARY KEY(ClassId,ModuleId,TrainerId) 
)
CREATE TABLE Trainee(
	UserName NVARCHAR(50) PRIMARY KEY,
	Name NVARCHAR(50) ,
	Email NVARCHAR(50) ,
	Phone NVARCHAR(50) ,
	Address NVARCHAR(255),
	IsActive BIT ,
	Password NVARCHAR(255) ,
	ActivationCode NVARCHAR(50),
	ResetPasswordCode NVARCHAR(50)
)
CREATE TABLE Answer(
	ClassId INT  REFERENCES dbo.Class(ClassId),
	ModuleId INT REFERENCES dbo.Module(ModuleId),
	TraineeId NVARCHAR(50) REFERENCES dbo.Trainee(UserName),
	QuestionId INT  REFERENCES dbo.Question(QuestionId),
	Value INT
	PRIMARY KEY(ClassId,ModuleId,TraineeId,QuestionId)
)
CREATE TABLE Enrollment(
	ClassId INT REFERENCES dbo.Class(ClassId),
	TraineeId NVARCHAR(50) REFERENCES dbo.Trainee(UserName)
	PRIMARY KEY(ClassId,TraineeId)
)

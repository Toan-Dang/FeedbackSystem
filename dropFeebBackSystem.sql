USE FeedbackSystem
go
DROP TABLE dbo.Trainee_Assignment,dbo.Trainee_Comment
ALTER TABLE dbo.Answer DROP FK__Answer__ClassId__6D0D32F4,FK__Answer__ModuleId__6E01572D,FK__Answer__TraineeI__6EF57B66,FK__Answer__Question__6FE99F9F,PK__Answer__984515AD839909B0
DROP TABLE dbo.Answer
ALTER TABLE dbo.Question DROP FK__Question__TopicI__5812160E
DROP TABLE dbo.Topic
ALTER TABLE dbo.Feedback_Question DROP FK__Feedback___Quest__5BE2A6F2 
DROP TABLE dbo.Question
ALTER TABLE dbo.Feedback_Question DROP FK__Feedback___Feedb__5AEE82B9
DROP TABLE dbo.Feedback_Question
ALTER TABLE dbo.Feedback DROP FK__Feedback__TypeFe__534D60F1
DROP TABLE dbo.TypeFeedback
ALTER TABLE dbo.Feedback DROP FK__Feedback__AdminI__52593CB8
ALTER TABLE dbo.Module DROP FK__Module__Feedback__5FB337D6 
DROP TABLE dbo.Feedback
ALTER TABLE dbo.Module DROP FK__Module__AdminId__5EBF139D
DROP TABLE dbo.Admin
ALTER TABLE dbo.Assignment DROP FK__Assignmen__Class__66603565,FK__Assignmen__Modul__6754599E,FK__Assignmen__Train__68487DD7
DROP TABLE dbo.Assignment--,dbo.Trainer
ALTER TABLE dbo.Enrollment DROP FK__Enrollmen__Class__72C60C4A,FK__Enrollmen__Train__73BA3083
DROP TABLE dbo.Class,dbo.Enrollment,dbo.Module,dbo.Trainee

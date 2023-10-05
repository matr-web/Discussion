-- Transaction that is responsible for seeding default test data to the DB.
-- It removes all other data from the DB.
BEGIN TRY
	BEGIN TRANSACTION

		--Clear the DB.
		DELETE FROM Ratings;
		DELETE FROM Answers;
		DELETE FROM Questions;
		DELETE FROM Categories;
		DELETE FROM Users;

	    --Seed User's.
		SET IDENTITY_INSERT Users ON;

		INSERT INTO Users (Id, Username, Email, Role, PasswordHash) 
		VALUES (1, 'matr12', 'matr12@gmail.com', 'Administrator', '$2a$11$95RMcdSReN2vFOxtRm85cuOkmAVnwj0uwTrvRq7u/QF/8dMLpAMti'); --Password: MatrPassword

		INSERT INTO Users (Id, Username, Email, Role, PasswordHash) 
		VALUES (2, 'Veronica', 'veronica1@gmail.com', 'User', '$2a$11$QIyplKPbEGcZbqxhoLwz4uzPEKpEBS1xnvN6M3zdAoHu/p813TIvm'); --Password: VeroPassword

		SET IDENTITY_INSERT Users OFF;

		--Seed Categories.
		SET IDENTITY_INSERT Categories ON;

		INSERT INTO Categories(Id, Name) VALUES (1, 'IT');
		INSERT INTO Categories(Id, Name) VALUES (2, 'Health');
		INSERT INTO Categories(Id, Name) VALUES (3, 'Science');

		SET IDENTITY_INSERT Categories OFF;

		--Seed Question's.
		SET IDENTITY_INSERT Questions ON;

		INSERT INTO Questions(Id, CategoryId, Topic, Content, Date, UserId) 
		VALUES (1, 1, 'IT Question Topic #1', 'IT Question Content #1', CURRENT_TIMESTAMP, 1);

		INSERT INTO Questions(Id, CategoryId, Topic, Content, Date, UserId) 
		VALUES (2, 1, 'IT Question Topic #2', 'IT Question Content #2', CURRENT_TIMESTAMP, 2);

		INSERT INTO Questions(Id, CategoryId, Topic, Content, Date, UserId) 
		VALUES (3, 2, 'Health Question Topic #1', 'Health Question Content #1', CURRENT_TIMESTAMP, 1);

		INSERT INTO Questions(Id, CategoryId, Topic, Content, Date, UserId) 
		VALUES (4, 2, 'Health Question Topic #2', 'Health Question Content #2', CURRENT_TIMESTAMP, 2);

		INSERT INTO Questions(Id, CategoryId, Topic, Content, Date, UserId) 
		VALUES (5, 3, 'Science Question Topic #1', 'Science Question Content #1', CURRENT_TIMESTAMP, 1);

		INSERT INTO Questions(Id, CategoryId, Topic, Content, Date, UserId) 
		VALUES (6, 3, 'Science Question Topic #2', 'Science Question Content #2', CURRENT_TIMESTAMP, 2);

		SET IDENTITY_INSERT Questions OFF;

		--Seed Answer's.
		SET IDENTITY_INSERT Answers ON;

		INSERT INTO Answers(Id, QuestionId, Content, Date, UserId) 
		VALUES (1, 1, 'IT Question #1 Answer Content #1', CURRENT_TIMESTAMP, 2);
		INSERT INTO Answers(Id, QuestionId, Content, Date, UserId) 
		VALUES (2, 1, 'IT Question #1 Answer Content #2', CURRENT_TIMESTAMP, 1);
		INSERT INTO Answers(Id, QuestionId, Content, Date, UserId) 
		VALUES (3, 1, 'IT Question #1 Answer Content #3', CURRENT_TIMESTAMP, 2);


		INSERT INTO Answers(Id, QuestionId, Content, Date, UserId) 
		VALUES (4, 2, 'IT Question #2 Answer Content #1', CURRENT_TIMESTAMP, 2);
		INSERT INTO Answers(Id, QuestionId, Content, Date, UserId) 
		VALUES (5, 2, 'IT Question #2 Answer Content #2', CURRENT_TIMESTAMP, 1);


		INSERT INTO Answers(Id, QuestionId, Content, Date, UserId) 
		VALUES (6, 3, 'Health Question #1 Answer Content #1', CURRENT_TIMESTAMP, 1);
		INSERT INTO Answers(Id, QuestionId, Content, Date, UserId) 
		VALUES (7, 3, 'Health Question #1 Answer Content #2', CURRENT_TIMESTAMP, 2);


		INSERT INTO Answers(Id, QuestionId, Content, Date, UserId) 
		VALUES (8, 4, 'Health Question #2 Answer Content #1', CURRENT_TIMESTAMP, 1);
		INSERT INTO Answers(Id, QuestionId, Content, Date, UserId) 
		VALUES (9, 4, 'Health Question #2 Answer Content #2', CURRENT_TIMESTAMP, 1);
		INSERT INTO Answers(Id, QuestionId, Content, Date, UserId) 
		VALUES (10, 4, 'Health Question #2 Answer Content #3', CURRENT_TIMESTAMP, 2);
		INSERT INTO Answers(Id, QuestionId, Content, Date, UserId) 
		VALUES (11, 4, 'Health Question #2 Answer Content #4', CURRENT_TIMESTAMP, 2);

		INSERT INTO Answers(Id, QuestionId, Content, Date, UserId) 
		VALUES (12, 5, 'Science Question #1 Answer Content #1', CURRENT_TIMESTAMP, 2);
		INSERT INTO Answers(Id, QuestionId, Content, Date, UserId) 
		VALUES (13, 5, 'Science Question #1 Answer Content #2', CURRENT_TIMESTAMP, 1);
		INSERT INTO Answers(Id, QuestionId, Content, Date, UserId) 
		VALUES (14, 5, 'Science Question #1 Answer Content #3', CURRENT_TIMESTAMP, 2);


		INSERT INTO Answers(Id, QuestionId, Content, Date, UserId) 
		VALUES (15, 6, 'Science Question #2 Answer Content #1', CURRENT_TIMESTAMP, 2);
		INSERT INTO Answers(Id, QuestionId, Content, Date, UserId) 
		VALUES (16, 6, 'Science Question #2 Answer Content #2', CURRENT_TIMESTAMP, 1);

		SET IDENTITY_INSERT Answers OFF;

		--Seed Rating's.
		INSERT INTO Ratings(QuestionId, UserId, Value) VALUES (1, 1, 1);
		INSERT INTO Ratings(QuestionId, UserId, Value) VALUES (1, 2, 1);
		INSERT INTO Ratings(QuestionId, UserId, Value) VALUES (2, 1, -1);
		INSERT INTO Ratings(QuestionId, UserId, Value) VALUES (2, 2, -1);
		INSERT INTO Ratings(QuestionId, UserId, Value) VALUES (3, 1, -1);
		INSERT INTO Ratings(QuestionId, UserId, Value) VALUES (3, 2, 1);

		INSERT INTO Ratings(AnswerId, UserId, Value) VALUES (1, 1, -1);
		INSERT INTO Ratings(AnswerId, UserId, Value) VALUES (1, 2, 1);
		INSERT INTO Ratings(AnswerId, UserId, Value) VALUES (4, 1, 1);
		INSERT INTO Ratings(AnswerId, UserId, Value) VALUES (2, 2, 1);
		INSERT INTO Ratings(AnswerId, UserId, Value) VALUES (7, 1, -1);
		INSERT INTO Ratings(AnswerId, UserId, Value) VALUES (1, 2, 1);
		INSERT INTO Ratings(AnswerId, UserId, Value) VALUES (3, 1, -1);
		INSERT INTO Ratings(AnswerId, UserId, Value) VALUES (5, 2, 1);
		INSERT INTO Ratings(AnswerId, UserId, Value) VALUES (6, 1, 11);
		INSERT INTO Ratings(AnswerId, UserId, Value) VALUES (8, 2, 1);
		INSERT INTO Ratings(AnswerId, UserId, Value) VALUES (9, 1, 1);
		INSERT INTO Ratings(AnswerId, UserId, Value) VALUES (10, 2, -1);
		INSERT INTO Ratings(AnswerId, UserId, Value) VALUES (11, 1, -1);
		INSERT INTO Ratings(AnswerId, UserId, Value) VALUES (14, 2, 1);
		INSERT INTO Ratings(AnswerId, UserId, Value) VALUES (12, 1, -1);
		INSERT INTO Ratings(AnswerId, UserId, Value) VALUES (15, 2, -1);
		INSERT INTO Ratings(AnswerId, UserId, Value) VALUES (16, 1, 1);

	COMMIT TRANSACTION
	PRINT 'Transaction Commited'

END TRY

BEGIN CATCH

	ROLLBACK TRANSACTION
	PRINT 'Transaction Rollbacked'

END CATCH
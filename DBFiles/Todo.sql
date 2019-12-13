CREATE TABLE Todo(
	Id INT PRIMARY KEY IDENTITY(1,1),
	TodoName NVARCHAR(50),
	Done BIT,
	Content NVARCHAR(100)
)

INSERT INTO Todo(TodoName, Done, Content) VALUES ('Miam miam',0,'Faire à manger')
INSERT INTO Todo(TodoName, Done, Content) VALUES ('Duel',0,'It is time to du-du-du-duel !')
INSERT INTO Todo(TodoName, Done, Content) VALUES ('Sleep',1,'Dodo Maison')
INSERT INTO Todo(TodoName, Done, Content) VALUES ('Pas content',1,'Grèver')

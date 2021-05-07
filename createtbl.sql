create table User_Info
(
	User_ID varchar(60) primary key,
	Name varchar(60) ,
	Phone varchar(60) ,
	Email varchar(60) , 
	City varchar(60),
	Country varchar(60),
	Gender varchar(10),
	DOB date ,
	Bio varchar(1000)
)

create table Credential
(	
	User_ID varchar(60) foreign key references User_Info,
	Username varchar(60) primary key ,
	Password varchar(60)
)

create table Followers
(
	User_ID varchar(60) foreign key references User_Info,
	Follower_ID varchar(60) foreign key references User_Info,
	primary key (User_ID,Follower_ID)
)

create table Post
(
	Post_ID varchar(60) primary key,
	User_ID varchar(60) foreign key references User_Info,
	No_Likes int ,
	No_Comments int
)

Create table Likes
(
	Post_ID varchar(60) foreign key references Post,
	User_ID varchar(60) foreign key references User_Info
)

Create table Comments
(
	Post_ID varchar(60) foreign key references Post,
	User_ID varchar(60) foreign key references User_Info,
	comment varchar(1000)
)
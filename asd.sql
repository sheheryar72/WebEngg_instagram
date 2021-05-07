create table User_Info
(
	Username varchar(60) primary key ,
	Name varchar(60) Not Null,
	Phone varchar(60) ,
	Email varchar(60) , 
	City varchar(60),
	Country varchar(60),
	Gender varchar(10),
	DOB date Not null,
	Bio varchar(1000)
)

create table Credential
(	
	Username varchar(60) foreign key references User_Info unique,
	Password varchar(60) not null
)

create table Followers
(
	Username varchar(60) foreign key references User_Info,
	Follower_ID varchar(60) foreign key references User_Info,
	primary key (Username,Follower_ID)
)

create table Post
(
	Post_ID varchar(60) primary key,
	Username varchar(60) foreign key references User_Info,
	Source varchar(100),
	Source_Type varchar(100),
)

Create table Likes
(
	Post_ID varchar(60) foreign key references Post,
	Username varchar(60) foreign key references User_Info
)

Create table Comments
(
	Post_ID varchar(60) foreign key references Post,
	Username varchar(60) foreign key references User_Info,
	comment varchar(1000)
)


/*
Insert into User_Info values('fallen12','Fasih Hussain','0317-1067141','fasihhussain00@gmail.com','Karachi','Pakistan','Male','12-12-2002','hello brooo!!')
Insert into User_Info values('cavil12','Henery cavil','9844241445292','henerycavil00@gmail.com','Newyork','USA','Male','04-03-1987','Superman')

Insert into Credential values('fallen12','123')
Insert into Credential values('cavil12','123')

Insert into Followers values('fallen12','cavil12')
Insert into Followers values('cavil12','fallen12')

Insert into Post values('fallen12_ps1','fallen12','Images\fasih.jpg','Image',2,2)
Insert into Post values('cavil12_ps1','cavil12','Images\cavil.mp4','Video',2,2)
Insert into Post values('cavil12_ps1','123')

Insert into Likes values('fallen12_ps1','fallen12')
Insert into Likes values('cavil12_ps1','cavil12')

Insert into Comments values('fallen12_ps1','fallen12' , 'ohh nice')
Insert into Comments values('cavil12_ps1','cavil12' ,'nice broo')

select * from User_Info
select * from Credential
select * from Followers
select * from Post
select * from Comments
select * from Likes
*/
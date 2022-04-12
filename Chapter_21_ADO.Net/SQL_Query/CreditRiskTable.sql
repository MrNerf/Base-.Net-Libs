CREATE TABLE CriditRisk
(
	[CustId] [int] identity(1,1) NOT NULL,
	[FirstName] [nvarchar] (50) NULL,
	[SecondName] [nvarchar] (50) NULL,
	PRIMARY KEY CLUSTERED ([CustId] ASC)
)
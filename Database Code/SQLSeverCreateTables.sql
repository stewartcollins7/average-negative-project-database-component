DROP TABLE Comment;
DROP TABLE Media;
DROP TABLE ArtistExhibition;
DROP TABLE Exhibition;
DROP TABLE Artist;
DROP TABLE SiteUser;

CREATE TABLE "SiteUser" (
  UserID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  Username VARCHAR(max) NOT NULL,
  Email VARCHAR(max) NOT NULL,
  IsArtist BIT NOT NULL DEFAULT 0,
  UserPassword  VARCHAR(max) NOT NULL,
  Salt VARCHAR(max) NOT NULL,
  );


CREATE TABLE "Artist" (
 ArtistID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
 CreatedByUserID INT NOT NULL FOREIGN KEY REFERENCES SiteUser(UserID),
 Name VARCHAR(max) NOT NULL,
 Location VARCHAR(max) NULL,
 ArtistBio TEXT NULL,
 );

 CREATE TABLE "Exhibition" (
  ExhibitionID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  CuratedBy INT NOT NULL FOREIGN KEY REFERENCES Artist(ArtistID),
  ExhibitionType CHAR NOT NULL,
  ExhibitionName VARCHAR(max) NOT NULL,
  DateEntered DATE NOT NULL DEFAULT GETDATE(),
  ExhibitionDescription TEXT NULL,
  );

  CREATE TABLE "Media" (
  MediaID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  ExhibitionID INT NOT NULL FOREIGN KEY REFERENCES Exhibition(ExhibitionID),
  ArtistID INT NOT NULL FOREIGN KEY REFERENCES Artist(ArtistID),
  YoutubeURL VARCHAR(max) NOT NULL,
  MediaFilename VARCHAR(max) NOT NULL,
  Name VARCHAR(max) NOT NULL,
  ArtDescription TEXT NULL
  );

  CREATE TABLE "ArtistExhibition" (
  ArtistID INT NOT NULL FOREIGN KEY REFERENCES Artist(ArtistID),
  ExhibitionID INT NOT NULL FOREIGN KEY REFERENCES Exhibition(ExhibitionID),
  CONSTRAINT pk_ArtistExhibition PRIMARY KEY (ArtistID,ExhibitionID)
  );

  CREATE TABLE "Comment" (
  CommentID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  UserID INT NOT NULL FOREIGN KEY REFERENCES SiteUser(UserID),
  MediaID INT NOT NULL FOREIGN KEY REFERENCES Media(MediaID),
  Content TEXT NOT NULL,
  PlacedOn DATETIME NOT NULL DEFAULT GETDATE(),
  );

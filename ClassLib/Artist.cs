using System;
using System.Data;
using System.Collections.Generic;

namespace AverageNegative
{
	public class Artist
	{
		private int artistID, createdByUserID;
		private string name, location, bio;

		//Constructor for artist class
		private Artist(int artistID, int createdByUserID, string name, string location, string bio){
			this.artistID = artistID;
			this.createdByUserID = createdByUserID;
			this.name = name;
			this.location = location;
			this.bio = bio;
		}

		//Constructor for default item for testing
		public Artist(){
			this.artistID = 1;
			this.createdByUserID = 1;
			this.name = "dbErrorName";
			this.location = "dbErrorLocation";
			this.bio = "dbErrorBio";
		}


		//Deletes all media items by artist (database only), then deletes artist
		//Returns media list so media items can be deleted
		public List<Media> delete() {
			List<Media> media = getMediaArray ();
			foreach (Media item in media) {
				item.delete ();
			}string sql = "deleteArtist @artist=" + artistID;
			SqlComm.SqlExecute (sql);
			return media;
		}

		//Returns list of all media items in exhibition
		private List<Media> getMediaArray(){
			List<Media> mediaList = new List<Media>();
			string sql;
			DataTable mediaIDs;
			try{
				sql = "getArtistMediaIDs @artist=" + artistID;
				mediaIDs = SqlComm.SqlDataTable (sql);
				foreach (DataRow row in mediaIDs.Rows){
					mediaList.Add(Media.retrieve((int)row[0]));
				}
			}catch{
				mediaList.Add (new Media ());
				mediaList.Add (new Media ());
			}
			return mediaList;
		}

		//Inserts new artist into database with given details
		//Returns the ID of item inserted
		public static int insert(int userID,string name, string location,string bio){
			string sql;
			int returnID;
			//Insert artist into the database
			sql = "insertNewArtist @userID=" + userID + ",@name='" + name + "', @location =";
			sql = SqlComm.AddIfNotNull (sql, location);
			sql = sql + ",@bio=";
			sql = SqlComm.AddIfNotNull (sql, bio);
			//Don't know why I had to cast this as a decimal first but it was throwing an error when I tried casting to int directly
			Decimal returnValue = (Decimal)SqlComm.SqlReturn (sql);
			returnID = (int)returnValue;
			return returnID;
		}

        //Inserts new artist into database with given details
        //Returns the ID of item inserted
        public void update()
        {
            string sql;
            int returnID;
            //Insert artist into the database
            sql = "updateArtist @artistId=" + ArtistID + ",@name='" + name + "', @location =";
            sql = SqlComm.AddIfNotNull(sql, location);
            sql = sql + ",@bio=";
            sql = SqlComm.AddIfNotNull(sql, bio);
            SqlComm.SqlReturn(sql);
        }

		//Returns an artist object of the given artistID
		//Returns null if artist does not exist
		public static Artist retrieve(int artistID){
			int createdByUserID;
			string name, location, bio;
			try{
				DataTable artistTable = SqlComm.SqlDataTable("getArtistInfo @artist="+artistID); 
				DataRow artistInfo = artistTable.Select()[0];
				if (artistInfo.IsNull (0)) {
					return null;
				}

				createdByUserID = (int)artistInfo [0];
				name = (string)artistInfo [1];
				//Try and catch blocks catch exception thrown if values in DB are null, sets the values to empty string
				try{
					location = (string)artistInfo [2];
				}catch{
					location = "";
				}
				try{
					bio = (string)artistInfo [3];
				}catch{
					bio = "";
				}
				return new Artist (artistID, createdByUserID, name, location, bio);
			}catch{
				return new Artist ();
			}

		}

		//Getter methods
		public int ArtistID {
			get {
				return artistID;
			}
		}

		public int CreatedByUserID {
			get {
				return createdByUserID;
			}
		}

		public string Name {
			get {
				return name;
			}
		    set { name = value; }
		}

		public string Location {
			get {
				return location;
			}
            set { location = value; }
		}

		public string Bio {
			get {
				return bio;
			}
            set { bio = value; }
		}

	}
}


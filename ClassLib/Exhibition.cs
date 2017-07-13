using System;
using System.Data;
using System.Collections.Generic;

namespace AverageNegative
{
	public class Exhibition
	{
		private int exhibitionID, curatedBy;
		private string name, description, type;
		private DateTime dateEntered;

		//Constructor for exhibition class
		private Exhibition (int exhibitionID, int curatedBy, string name, string description, string type, DateTime dateEntered)
		{
			this.exhibitionID = exhibitionID;
			this.curatedBy = curatedBy;
			this.name = name;
			this.description = description;
			this.type = type;
			this.dateEntered = dateEntered;
		}

		//Updates the values of exhibition item with matching ID
		public static void update(int exhibitionID, string name, string description, string type){
			string sql;
			sql = "updateExhibition @exhibitionID =" + exhibitionID + ",@name='" + name + "',@type='" + type + "',@description=";
			sql = SqlComm.AddIfNotNull (sql, description);
			SqlComm.SqlExecute (sql);
		}
			

		//Deletes all media items in exhibition (database only), then deletes exhibition
		//Returns media list so media items can be deleted
		public List<Media> delete() {
			List<Media> media = getMediaArray ();
			foreach (Media item in media) {
				item.delete ();
			}string sql = "deleteExhibition @exhibition=" + exhibitionID;
			SqlComm.SqlExecute (sql);
			return media;
		}


		//Constructor for default item for testing
		public Exhibition ()
		{
			exhibitionID = 1;
			curatedBy = 1;
			name = "dbNotWorkingName";
			description = "dbNotWorkingDescrription";
			type = "G";
			dateEntered = DateTime.Now;
		}


		//Returns list of all media items in exhibition
		public List<Media> getMediaArray(){
			List<Media> mediaList = new List<Media>();
			string sql;
			DataTable mediaIDs;
			try{
				sql = "getMediaIDs @exhibition=" + exhibitionID;
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

		//Returns list of all artists involved in exhibition
		//The curator is the first artist in the array
		public List<Artist> getArtistArray() 
		{
			List<Artist> artistList = new List<Artist>();
			string sql;
			DataTable artistIDs;

			try{
				sql = "getExhibitionArtistIDs @exhibition=" + exhibitionID;
				artistIDs = SqlComm.SqlDataTable (sql);
				artistList.Add(Artist.retrieve(curatedBy));
				foreach (DataRow row in artistIDs.Rows){
					if ((int)row [0] != curatedBy) {
						artistList.Add (Artist.retrieve ((int)row [0]));
					}				
				}
			}catch{
				artistList.Add (new Artist ());
				artistList.Add (new Artist ());
			}
			return artistList;
		}

		//Returns info for all exhibitions (for Carousel)
		//Displays the top 30 most recently created if more than 30 exist
		public static List<Exhibition> getAllExhibitions() {
			List<Exhibition> exhibitionList = new List<Exhibition>();
			string sql;
			DataTable exhibitionIDs;

			try{
				sql = "getAllExhibitionIDs";
				exhibitionIDs = SqlComm.SqlDataTable (sql);
				foreach (DataRow row in exhibitionIDs.Rows){
					exhibitionList.Add(Exhibition.retrieve((int)row[0]));
				}
			}catch{
				exhibitionList.Add (new Exhibition ());
				exhibitionList.Add (new Exhibition ());
				exhibitionList.Add (new Exhibition ());
				exhibitionList.Add (new Exhibition ());
			}return exhibitionList;
		}

		//Returns info for all exhibitions (for Carousel)
		//Displays the top 30 most recently created if more than 30 exist
		public static List<Exhibition> getRecentExhibitions() {
			List<Exhibition> exhibitionList = new List<Exhibition>();
			string sql;
			DataTable exhibitionIDs;

			try{
				sql = "getRecentExhibitionIDs";
				exhibitionIDs = SqlComm.SqlDataTable (sql);
				foreach (DataRow row in exhibitionIDs.Rows){
					exhibitionList.Add(Exhibition.retrieve((int)row[0]));
				}
			}catch{
				exhibitionList.Add (new Exhibition ());
				exhibitionList.Add (new Exhibition ());
				exhibitionList.Add (new Exhibition ());
				exhibitionList.Add (new Exhibition ());
			}return exhibitionList;
		}

		//Inserts a new exhibition into the database with given details
		//Returns the exhibitionID of the item
		public static int insert(string name, string description, int artistID, string type){
			int returnID;
			string sql = "insertNewExhibition @name='" + name + "',@curatedBy = " + artistID + ",@description=";
			sql = SqlComm.AddIfNotNull (sql, description);
			sql = sql + ",@type = '" + type +"'";
			//Don't know why I had to cast this as a decimal first but it was throwing an error when I tried casting to int directly
			Decimal returnValue = (Decimal)SqlComm.SqlReturn (sql);
			returnID = (int)returnValue;
			return returnID;
		}

		//Returns an Exhibition object with the details of the given exhibition
		//Returns null if exhibition does not exist
		public static Exhibition retrieve(int exhibitionID){
			int curatedBy;
			string name, description, type;
			DateTime dateEntered;

			try{
				DataTable exhibitionTable = SqlComm.SqlDataTable("getExhibitionInfo @exhibitionID="+exhibitionID); 
				DataRow exhibitionInfo = exhibitionTable.Select()[0];
				if (exhibitionInfo.IsNull (0)) {
					return null;
				}
				curatedBy = (int)exhibitionInfo [0];
				name = (string)exhibitionInfo [1];
				//If description is null catch exception and set description to null
				try{
					description = (string)exhibitionInfo [2];
				}catch{
					description = "";
				}
				type = (string)exhibitionInfo[3];
				dateEntered = (DateTime)exhibitionInfo[4];
				return new Exhibition (exhibitionID,curatedBy,name,description,type,dateEntered);
			}catch{
				return new Exhibition ();
			}

		}

		//Getter methods
		public string Name {
			get {
				return name;		
			}
		}

		public int ExhibitionID {
			get {
				return exhibitionID;
			}
		}

		public int CuratedBy {
			get {
				return curatedBy;
			}
		}

		public string Description {
			get {
				return description;
			}
		}

		public string Type {
			get {
				return type;
			}
		}

		public DateTime DateEntered {
			get {
				return dateEntered;
			}
		}
	}
}


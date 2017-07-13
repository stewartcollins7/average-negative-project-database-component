using System;
using System.Data;
using System.Collections.Generic;

namespace AverageNegative
{
	public class Media
	{
		private int mediaID, exhibitionID, artistID;
		private string youtubeURL, filename, name, description;

		//Constructor for media class
		private Media (int mediaID, int exhibitionID, int artistID, string url, string filename, string name, string description)
		{
			this.mediaID = mediaID;
			this.exhibitionID = exhibitionID;
			this.artistID = artistID;
			this.youtubeURL = url;
			this.filename = filename;
			this.name = name;
			this.description = description;
		}

		//Constructor for default item for testing
		public Media ()
		{
			this.mediaID = 1;
			this.exhibitionID = 1;
			this.artistID = 1;
			this.youtubeURL = "dbErrorYoutubeURL";
			this.filename = "dbErrorFile";
			this.name = "dbErrorName";
			this.description = "dbErrorDescription";
		}

		//Deletes all the comment items for the media, then deletes the media item in database
		public void delete(){
			List<Comment> comments = this.getComments ();
			foreach (Comment item in comments) {
				item.delete ();
			}string sql = "deleteMedia @media=" + mediaID;
			SqlComm.SqlExecute (sql);
		}

		//Returns a list of all the comments the media item has
		public List<Comment> getComments(){
			List<Comment> commentList = new List<Comment>();
			string sql;
			DataTable comments;
			DateTime placedOn;
			int commentID, userID;
			string username;
			string content;

			try{
				sql = "getMediaComments @media=" + mediaID;
				comments = SqlComm.SqlDataTable (sql);
				foreach (DataRow row in comments.Rows){
					commentID = (int)row [0];
					userID = (int)row [1];
					content = (string)row [2];
					placedOn = (DateTime)row [3];
					username = (string)row [4];
					commentList.Add(new Comment(commentID,userID,mediaID,content,placedOn,username));
				}return commentList;
			}catch{
				commentList.Add (new Comment ());
				commentList.Add (new Comment ());
				return commentList;
			}
		}

		//Inserts media item into the database
		//Returns the id of item inserted
		public static int insert(int exhibitionID, int artistID, string youtubeURL, string filename, string name, string description){
			string sql;
			int returnID;

			sql = "insertNewMedia @exhibition=" + exhibitionID + ",@artist=" + artistID + ",@youtubeURL=";
			sql = SqlComm.AddIfNotNull (sql, youtubeURL);
			sql = sql + ",	@filename='" + filename + "',	@name='" + name + "',@description=";
			sql = SqlComm.AddIfNotNull (sql, description);
			//Don't know why I had to cast this as a decimal first but it was throwing an error when I tried casting to int directly
			Decimal returnValue = (Decimal)SqlComm.SqlReturn (sql);
			returnID = (int)returnValue;
			return returnID;
		}

		//Updates the values of media item with matching ID
		public static void update(int mediaID, int artistID, string youtubeURL, string name, string description){
			string sql;
			sql = "updateMedia @mediaID =" + mediaID + ",@artistID=" + artistID + ",@youtubeURL=";
			sql = SqlComm.AddIfNotNull (sql, youtubeURL);
			sql = sql + ",	@name='" + name + "',@description=";
			sql = SqlComm.AddIfNotNull (sql, description);
			SqlComm.SqlExecute (sql);
		}

		//Returns a Media object for the given media item
		//Returns null if item does not exist
		public static Media retrieve(int mediaID){
			int exhibitionID, artistID;
			string url, filename, name, description;
			try{
				DataTable mediaTable = SqlComm.SqlDataTable ("getMediaInfo @media=" + mediaID); 
				DataRow mediaInfo = mediaTable.Select () [0];
				if (mediaInfo.IsNull (0)) {
					return null;
				}

				exhibitionID = (int)mediaInfo [0];
				artistID = (int)mediaInfo [1];
				try{
					url = (string)mediaInfo [2];
				}catch{
					url = "";
				}
				filename = (string)mediaInfo [3];
				name = (string)mediaInfo [4];
				//If description is null catch exception and set description to null
				try{
					description = (string)mediaInfo [5];
				}catch{
					description = "";
				}
				return new Media (mediaID, exhibitionID, artistID, url, filename, name, description);
			}catch{
				return new Media ();
			}
			

		}

	
		//Getter methods
		public int MediaID {
			get{
				return mediaID;
			}
		}

		public int ArtistID {
			get {
				return artistID;
			}
		}
		public int ExhibitionID {
			get {
				return exhibitionID;
			}
		}

		public string Description {
			get {
				return description;
			}
		}

		public string Name {
			get {
				return name;
			}
		}

		public string Filename {
			get {
				return filename;
			}
		}

		public string YoutubeURL {
			get {
				return youtubeURL;
			}
		}
	}
}


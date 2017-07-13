using System;
using System.Data;
using System.Collections.Generic;

namespace AverageNegative
{
	public class User
	{
		public const String KUserSessionKey = "CurrentUserSessionKey";
		private string username;
		private int userID;
		private int isArtist;
		private string email;
		private int artistID=0;


		//Constructor for user class
		private User (String userName, int userID, String email, int isArtist)
		{
			this.username = userName;
			this.userID = userID;
			this.email = email;
			this.isArtist = isArtist;
			if (isArtist > 0) {
				string sql = "getArtistIDs @user=" + userID;
				this.artistID = (int)SqlComm.SqlReturn (sql);
			}
		}

		//Constructor for default item for testing
		private User(){
			username = "dbNotWorkingUsername";
			userID = 1;
			email = "dbNotWorkingEmail";
			isArtist = 1;
			artistID = 1;
		}

		//Constructor left in for testing, used as default authentication
		public User (String email){
			this.email = email;
		}

		//Method to update user details
		//Is an instance method as user info is stored in session data
		//Returns an error message if username or email already registered
		public string update(string username, string email){
			int count;
			string sql;

			if (!username.Equals(this.username)) {
				count = (int)SqlComm.SqlReturn ("usernameCount @username='" + username + "'");
				if (count > 0) {
					return "Username already registered";
				}
			}if(!email.Equals(this.email)){
				count = (int)SqlComm.SqlReturn ("emailCount @email='" + email+"'");
				if(count>0){
					return "Email already registered";
				}
			}
			sql = "updateUser @userID=" + userID + ",@username='" + username + "',@email ='" + email + "'";	
			SqlComm.SqlExecute (sql);
			this.email = email;
			this.username = username;
			return "Update successful";
		}

		//Updates the password of the given user
		private void updatePassword(int userID, string password){
			string salt;
			string sql;
			salt = (string)SqlComm.SqlReturn ("getSalt @username='" + username + "'");
			password = SqlComm.Enc (password + salt);
			sql = "updatePassword @userID =" + userID + ",@password ='" + password + "'";
			SqlComm.SqlExecute (sql);
		}



		//Deletes all items under user account, then deletes artist
		//Returns exhibition list so media items can be deleted
		public List<Exhibition> delete() {
			List<Comment> comments;
			List<Exhibition> exhibitions;
			List<Artist> artists;
			string sql;

			comments = getUserComments();
			foreach (Comment item in comments) {
				item.delete ();
			}
			if (isArtist < 1) {
				sql = "deleteUser @user=" + userID;
				SqlComm.SqlExecute (sql);
				return null;
			}
			exhibitions = getExhibitionArray();
			foreach (Exhibition item in exhibitions) {
				item.delete ();
			}artists = getArtistArray ();
			foreach (Artist item in artists) {
				item.delete ();
			}sql = "deleteUser @user=" + userID;
			SqlComm.SqlExecute (sql);
			return exhibitions;
		}

		//Returns a list of all comments made by the user
		private List<Comment> getUserComments(){
			List<Comment> commentList = new List<Comment>();
			string sql;
			DataTable comments;
			DateTime placedOn;
			int commentID, mediaID;
			string username;
			string content;
			sql = "getUserComments @user=" + userID;
			comments = SqlComm.SqlDataTable (sql);
			foreach (DataRow row in comments.Rows){
				commentID = (int)row [0];
				mediaID = (int)row [1];
				content = (string)row [2];
				placedOn = (DateTime)row [3];
				username = (string)row [4];
				commentList.Add(new Comment(commentID,userID,mediaID,content,placedOn,username));
			}return commentList;
		}

		//Returns a list of all artists created by given user
		public List<Artist> getArtistArray() 
		{
			List<Artist> artistList = new List<Artist>();
			string sql;
			DataTable artistIDs;

			try{
				sql = "getArtistIDs @user=" + userID;
				artistIDs = SqlComm.SqlDataTable (sql);
				foreach (DataRow row in artistIDs.Rows){
					artistList.Add(Artist.retrieve((int)row[0]));
				}
			}catch{
				artistList.Add (new Artist ());
				artistList.Add (new Artist ());
			}
			return artistList;

		}

		//Returns a list of all exhibitions created by given user
		public List<Exhibition> getExhibitionArray() 
		{
			if (artistID < 1) {
				return null;
			}
			List<Exhibition> exhibitionList = new List<Exhibition>();
			string sql;
			DataTable exhibitionIDs;

			try{
				sql = "getExhibitionIDs @artistID=" + artistID;
				exhibitionIDs = SqlComm.SqlDataTable (sql);
				foreach (DataRow row in exhibitionIDs.Rows){
					exhibitionList.Add(Exhibition.retrieve((int)row[0]));
				}
			}catch{
				exhibitionList.Add (new Exhibition ());
				exhibitionList.Add (new Exhibition ());
			}
			return exhibitionList;

		}


		//Checks the password is correct for given username and returns user object if correct
		//Returns null if login details incorrect
		public static User login(string username, string password){
			int count;
			string salt;
			string email;
			int userID, isArtist;

			try{
				//Check if username already registered
				count = (int)SqlComm.SqlReturn ("usernameCount @username='" + username + "'");
				if (count > 0) {
					salt = (string)SqlComm.SqlReturn ("getSalt @username='" + username + "'");
					password = SqlComm.Enc (password + salt);
					count = (int)SqlComm.SqlReturn ("checkPassword @username='" + username + "', @password='" + password + "'");
					if (count > 0) {
						userID = (int)SqlComm.SqlReturn ("getUserID @username='" + username + "'");
						email = (string)SqlComm.SqlReturn ("getEmail @userID=" + userID);
						isArtist = (int)SqlComm.SqlReturn ("getIsArtist @userID=" + userID);
						return new User (username, userID, email, isArtist);
					}

				}return null;
			}catch{
				return new User ();
			}

		}

		//Inserts the given details into the database if both email and username are not already registered
		//Returns a string with message indicated whether insert was succesful or reason why it wasn't
		public static string insert(string username, int isArtist, string email, string password){
			string sql, salt;
			int count;
			int userID;
			string dbMessage;

			//Get salt and hash password
			salt = SqlComm.CreateSalt();
			password = SqlComm.Enc (password + salt);

			//Check if email already registered
			count = (int)SqlComm.SqlReturn ("emailCount @email='" + email+"'");
			if(count<1){

				//Check if username already registered
				count = (int)SqlComm.SqlReturn ("usernameCount @username='" + username+"'");
				if(count<1){

					//If not already registered insert into database
					sql = "insertNewUser @userName='" + username + "' , @email='" + email + "', @isArtist ='" + isArtist + "',@userPassword = '" + password + "',@salt='" + salt + "'";
					SqlComm.SqlExecute (sql);

					//If is curator create default artist profile
					if(isArtist==1){
						userID = (int)SqlComm.SqlReturn("getUserID @username='"+username+"'");
						Artist.insert (userID, username, "", "");
					}dbMessage = "";
				}else{
					dbMessage= "Username already registered";
				}
			}else{
				dbMessage="Email already registered";
			}
			return dbMessage;
		}

		//Getter methods
		public string UserName {
			get {
				return username;		
			}
		}

		public int UserID {
			get {
				return userID;
			}
		}

		public int IsArtist {
			get {
				return isArtist;
			}
		}

		public string Email {
			get {
				return email;
			}
		}

		public int ArtistID {
			get {
				return artistID;
			}
		}



	}
}


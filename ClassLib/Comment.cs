using System;
using System.Data;

namespace AverageNegative
{
	public class Comment
	{
		private int commentID, userID, mediaID;
		private string username;
		private string content;
		private DateTime placedOn;


		//Constructor for comment class
		public Comment (int commentID, int userID, int mediaID, string content, DateTime placedOn, string username)
		{
			this.commentID = commentID;
			this.userID = userID;
			this.mediaID = mediaID;
			this.content = content;
			this.placedOn = placedOn;
			this.username = username;
		}
		//Constructor for default item for testing
		public Comment(){
			this.commentID = 1;
			this.userID = 1;
			this.mediaID = 1;
			this.content = "dbNotWorkingContent";
			this.placedOn = DateTime.Now;
			this.username = "dbNotWorkingUsername";
		}

		//Inserts a new comment into the database with given details
		//Returns the commentID of the item
		public static int insert(int userID, int mediaID, string content){
			int returnID;
			string sql = "insertNewComment @user=" + userID + ",@media = " + mediaID + ",@content='" + content + "'";
			//Don't know why I had to cast this as a decimal first but it was throwing an error when I tried casting to int directly
			Decimal returnValue = (Decimal)SqlComm.SqlReturn (sql);
			returnID = (int)returnValue;
			return returnID;
		}

		//Deletes comment from database
		public void delete(){
			string sql = "deleteComment @comment=" + commentID;
			SqlComm.SqlExecute (sql);
		}
			
		//Getter methods
		public int UserID {
			get {
				return userID;
			}
		}

		public int MediaID {
			get {
				return mediaID;
			}
		}
		public DateTime PlacedOn {
			get {
				return placedOn;
			}
		}

		public string Content {
			get {
				return content;
			}
		}

		public string Username {
			get {
				return username;
			}
		}

		public int CommentID {
			get {
				return commentID;
			}
		}
	}
}


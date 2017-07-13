using System;
using System.Collections.Generic;

namespace AverageNegative
{
	public class CarouselItem
	{
		private string name, description, coverImage, hyperlink;
		private int exhibitionID;
			
		//Constructor for CarouselItem
		private CarouselItem (string name, string description, string coverImage, int exhibitionID, string link)
		{
			this.name = name;
			this.description = description;
			this.coverImage = coverImage;
			this.exhibitionID = exhibitionID;
			this.hyperlink = link;
		}

		//Returns info for 9 most recent exhibitions (for Carousel)
		public static List<CarouselItem> getCarouselItems() {
			List<Exhibition> exhibitionList = Exhibition.getRecentExhibitions();
			string coverImage;
			string link;
			string sql;
			int i=0;
			List<CarouselItem> carouselItems = new List<CarouselItem>();
			while (i < exhibitionList.Count) {
				sql = "getCoverImage @exhibition=" + exhibitionList [i].ExhibitionID;
				if(exhibitionList[i].Type.Equals("G")){
					link = "http://averagenegative.azurewebsites.net/StreetViewExhibit/Gallery.aspx?GalleryId=" + exhibitionList[i].ExhibitionID;
				}else{
					link = "http://averagenegative.azurewebsites.net/Portraits-Exhibit/Portraits.aspx?GalleryId=" + exhibitionList[i].ExhibitionID;
				}
				coverImage = (string)SqlComm.SqlReturn (sql);
				carouselItems.Add(new CarouselItem(exhibitionList [i].Name,exhibitionList [i].Description,coverImage,exhibitionList[i].ExhibitionID, link));
				i++;
			}
			return carouselItems;
		}

		public string Name {
			get {
				return name;
			}
		}

		public string Description {
			get {
				return description;
			}
		}

		public string CoverImage {
			get {
				return coverImage;
			}
		}

		public int ExhibitionID {
			get {
				return exhibitionID;
			}
		}

		public string Hyperlink {
			get {
				return hyperlink;
			}
		}
	}
}


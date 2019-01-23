using SQLite;

namespace GitHubSearch
{
	[Table("Favorites")]
	class FavoriteUser
	{
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int Id { get; set; }

		public FavoriteUser()
		{

		}

		public FavoriteUser(string name, string url)
		{
			userName = name;
			htmlUrl = url;
		}

		public string userName { get; set; }

		public string htmlUrl { get; set; }


		public override string ToString()
		{
			return "  ";
		}
	}
}
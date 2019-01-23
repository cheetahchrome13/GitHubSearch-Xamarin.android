using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using GitHubSearch.Model;
using SQLite;

namespace GitHubSearch
{
	class CustomAdapter : ArrayAdapter
	{
		private Context context;
		public List<User> users;
		public List<FavoriteUser> favorites = new List<FavoriteUser>();
		//IList users;
		private LayoutInflater inflater;
		private int resource;
		bool clue;
		Container container;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="resource"></param>
		/// <param name="users"></param>
		/// <param name="clue"></param>
		public CustomAdapter(Context context, int resource, List<User> users, bool clue) : base(context, resource, users)
		{
			this.context = context;
			this.resource = resource;
			this.users = users;
			this.clue = clue;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="resource"></param>
		/// <param name="favorites"></param>
		/// <param name="clue"></param>
		/// <param name="shim"></param>
		public CustomAdapter(Context context, int resource, List<FavoriteUser> favorites, bool clue, int shim) : base(context, resource, favorites)
		{
			this.context = context;
			this.resource = resource;
			this.favorites = favorites;
			this.clue = clue;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="position"></param>
		/// <param name="convertView"></param>
		/// <param name="parent"></param>
		/// <returns></returns>
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			if (inflater == null)
			{
				inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
			}

			if (convertView == null)
			{
				convertView = inflater.Inflate(resource, parent, false);
			}

			if (clue)
			{
				Container frame = new Container(convertView)
				{
					UserName = { Text = users[position].userName }
				};

				container = frame;
			}
			else
			{
				Container frame = new Container(convertView)
				{
					UserName = { Text = favorites[position].userName }
				};

				container = frame;
			}
			//convertView.SetBackgroundColor(position % 2 == 0 ? Color.DarkGray : Color.Black);
			convertView.SetBackgroundColor(Color.Black);
			string testFave = clue ? users[position].userName : favorites[position].userName;
			container.FavoriteButton.SetImageResource(IsFavorite(testFave) ? Resource.Drawable.favorite : Resource.Drawable.not_favorite);
			container.UserName.Click += UserNameClick;
			container.FavoriteButton.Click += FavoriteButtonClick;

			
			void UserNameClick(object sender, EventArgs e)
			{
				string url = clue ? users[position].htmlUrl : favorites[position].htmlUrl;
				var uri = Android.Net.Uri.Parse(url);
				var intent = new Intent(Intent.ActionView, uri);
				context.StartActivity(intent);
			}
			
			void FavoriteButtonClick(object sender, EventArgs e)
			{

				ImageButton favorite = sender as ImageButton;

				string testName = clue ? users[position].userName : favorites[position].userName;

				if (IsFavorite(testName))
				{
					favorite.SetImageResource(Resource.Drawable.not_favorite);
					RemoveFavorite(testName);
				}

				else
				{
					favorite.SetImageResource(Resource.Drawable.favorite);
					string name = clue ? users[position].userName : favorites[position].userName;
					string url = clue ? users[position].htmlUrl : favorites[position].htmlUrl;
					AddFavorite(name, url);
				}
			}

			void AddFavorite(string name, string url)
			{
				try
				{
					string dbpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

					//setup a new blank database if one doesn't already exist
					var db = new SQLiteConnection(System.IO.Path.Combine(dbpath, "savedFavorites.db"));

					//create a new table if it doesn't already exist
					db.CreateTable<FavoriteUser>();
					FavoriteUser newFavorite = new FavoriteUser(name, url);
					db.Insert(newFavorite);
					db.Close();
				}
				catch
				{

				}
			}

			return convertView;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="testName"></param>
		void RemoveFavorite(string testName)
		{
			string dbpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var db = new SQLiteConnection(System.IO.Path.Combine(dbpath, "savedFavorites.db"));
			db.CreateTable<FavoriteUser>();
			FavoriteUser user = new FavoriteUser();
			foreach (FavoriteUser f in favorites)
			{
				if (f.userName == testName)
				{
					user = f;
				}
			}

			db.Delete<FavoriteUser>(user.Id); // Id is the primary key//var rowcount =
			db.Close();
			if (!clue)
			{
				MainActivity.favoritesButton.PerformClick();// trigger a button 'remote'
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="testName"></param>
		/// <returns></returns>
		bool IsFavorite(string testName)
		{
			//users.Clear();
			favorites.Clear();
			string dbpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var db = new SQLiteConnection(System.IO.Path.Combine(dbpath, "savedFavorites.db"));
			db.CreateTable<FavoriteUser>();

			var table = db.Table<FavoriteUser>();
			foreach (var fave in table)
			{
				favorites.Add(fave);
			}
			db.Close();
			List<FavoriteUser> tempList = new List<FavoriteUser>();
			foreach (FavoriteUser f in favorites)
			{
				if (f.userName == testName)
				{
					tempList.Add(f);
				}
			}
			return tempList.Count > 0;
		}
	}
}
using Android.App;
using Android.OS;
using Android.Widget;
using Refit;
using GitHubSearch.Interface;
using System.Collections.Generic;
using GitHubSearch.Model;
using Android.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System;
using Android.Support.V7.App;
using SQLite;

namespace GitHubSearch
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity//Activity//ListActivity
	{
		IGitHubApi gitHubApi;
		List<User> users = new List<User>();
		List<FavoriteUser> favorites = new List<FavoriteUser>();
		EditText searchBox;
		Button gitUsers;
		//Button deleteButton;
		public static ImageButton favoritesButton;
		CustomAdapter adapter;
		//IListAdapter ListAdapter;
		ListView listView;
		protected override void OnCreate(Bundle savedInstanceState)
        {
			try
			{
				base.OnCreate(savedInstanceState);

				SetContentView(Resource.Layout.activity_main);
				searchBox = FindViewById<EditText>(Resource.Id.searchBox);
				gitUsers = FindViewById<Button>(Resource.Id.searchButton);
				favoritesButton = FindViewById<ImageButton>(Resource.Id.favoritesButton);
				//deleteButton = FindViewById<Button>(Resource.Id.deleteButton);//for testing
				listView = FindViewById<ListView>(Resource.Id.userListView);
				gitUsers.Click += GitUsers_Click;
				favoritesButton.Click += FavoritesButton_Click;
				//deleteButton.Click += DeleteButton_Click;//for testing

				JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
				{
					ContractResolver = new CamelCasePropertyNamesContractResolver(),
					//ContractResolver = new SnakeCasePropertyNamesContractResolver(),
					Converters = { new StringEnumConverter() }
				};

				gitHubApi = RestService.For<IGitHubApi>("https://api.github.com");

			}
			catch (Exception ex)
			{
				Log.Error("Lookie Justin", ex.Message);
			}
		}

		/// <summary>
		/// ONLY USED IN TESTING
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeleteButton_Click(object sender, EventArgs e)
		{
			string dbpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

			//setup a new blank database if one doesn't already exist
			var db = new SQLiteConnection(System.IO.Path.Combine(dbpath, "savedFavorites.db"));

			//drop table to delete faves
			db.DropTable<FavoriteUser>();

			//create a new table FavoriteUser if it doesn't already exist
			db.CreateTable<FavoriteUser>();

			favorites.Clear();
			var table = db.Table<FavoriteUser>();
			foreach (var fave in table)
			{
				favorites.Add(fave);
			}
			db.Close();
			adapter = new CustomAdapter(this, Resource.Layout.row_template, favorites, false, 1);
			listView.Adapter = adapter;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void FavoritesButton_Click(object sender, EventArgs e)
		{
			users.Clear();
			favorites.Clear();
			string dbpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

			//setup a new blank database if one doesn't already exist
			var db = new SQLiteConnection(System.IO.Path.Combine(dbpath, "savedFavorites.db"));

			//create a new table FavoriteUser if it doesn't already exist
			db.CreateTable<FavoriteUser>();

			//read each FavoriteUser that is in the table
			var table = db.Table<FavoriteUser>();
			foreach (var fave in table)
			{
				favorites.Add(fave);
			}
			db.Close();
			adapter = new CustomAdapter(this, Resource.Layout.row_template, favorites, false, 1);
			listView.Adapter = adapter;
		}


		private void GitUsers_Click(object sender, EventArgs e)
		{
			users.Clear();
			//favorites.Clear();
			GetUsers();
		}

		private async void GetUsers()
		{
			try
			{
				ApiResponse response = await gitHubApi.GetUser(searchBox.Text);
				users = response.items;
				adapter = new CustomAdapter(this, Resource.Layout.row_template, users, true);
				listView.Adapter = adapter;
			}
			catch (Exception ex)
			{
				Toast.MakeText(this, ex.StackTrace, ToastLength.Long).Show();

			}
		}
	}
}
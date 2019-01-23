using Android.Views;
using Android.Widget;

namespace GitHubSearch
{
	class Container
	{
		public TextView UserName;
		public ImageButton FavoriteButton;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="v"></param>
		public Container(View v)
		{
			this.UserName = v.FindViewById<TextView>(Resource.Id.userNameView);
			this.FavoriteButton = v.FindViewById<ImageButton>(Resource.Id.favoriteUserButton);
		}
	}
}
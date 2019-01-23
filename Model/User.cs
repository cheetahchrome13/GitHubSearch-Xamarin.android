
using Newtonsoft.Json;

namespace GitHubSearch.Model
{
	public class User
	{
		[JsonProperty(PropertyName = "login")]
		public string userName { get; set; }

		[JsonProperty(PropertyName = "id")]
		public int id { get; set; }

		[JsonProperty(PropertyName = "node_id")]
		public string nodeId { get; set; }

		[JsonProperty(PropertyName = "avatar_url")]
		public string avatarUrl { get; set; }

		[JsonProperty(PropertyName = "gravatar_id")]
		public string gravatarId { get; set; }

		[JsonProperty(PropertyName = "url")]
		public string url { get; set; }

		[JsonProperty(PropertyName = "html_url")]
		public string htmlUrl { get; set; }

		[JsonProperty(PropertyName = "followers_url")]
		public string followersUrl { get; set; }

		[JsonProperty(PropertyName = "following_url")]
		public string followingUrl { get; set; }

		[JsonProperty(PropertyName = "gists_url")]
		public string gistsUrl { get; set; }

		[JsonProperty(PropertyName = "starred_url")]
		public string starredUrl { get; set; }

		[JsonProperty(PropertyName = "subscriptions_url")]
		public string subscriptionsUrl { get; set; }

		[JsonProperty(PropertyName = "organizations_url")]
		public string organizationsUrl { get; set; }

		[JsonProperty(PropertyName = "repos_url")]
		public string reposUrl { get; set; }

		[JsonProperty(PropertyName = "events_url")]
		public string eventsUrl { get; set; }

		[JsonProperty(PropertyName = "received_events_url")]
		public string receivedEventsUrl { get; set; }

		[JsonProperty(PropertyName = "type")]
		public string type { get; set; }

		[JsonProperty(PropertyName = "site_admin")]
		public bool siteAdmin { get; set; }

		[JsonProperty(PropertyName = "score")]
		public double score { get; set; }

		public override string ToString()
		{
			return userName;
		}
	}
}
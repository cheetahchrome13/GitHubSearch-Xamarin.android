
using System.Threading.Tasks;
using Refit;
using GitHubSearch.Model;

namespace GitHubSearch.Interface
{
	[Headers("User-Agent: :request:")]
	interface IGitHubApi
	{
		[Get("/search/users?q=location:{search}")]
		Task<ApiResponse> GetUser(string search);
	}
}
#r "nuget: TweetinviAPI, 5.0.3"
#r "nuget: DotNetEnv, 2.0.0"

using Tweetinvi;
using DotNetEnv;

DotNetEnv.Env.TraversePath().Load();
readonly string TWITTER_USER_TOKEN = Environment.GetEnvironmentVariable("TWITTER_USER_TOKEN");
readonly string TWITTER_USER_SECRET = Environment.GetEnvironmentVariable("TWITTER_USER_SECRET");
readonly string TWITTER_CONSUMER_TOKEN = Environment.GetEnvironmentVariable("TWITTER_CONSUMER_TOKEN");
readonly string TWITTER_CONSUMER_SECRET = Environment.GetEnvironmentVariable("TWITTER_CONSUMER_SECRET");

// Check that secrets were set correctly.
if (string.IsNullOrEmpty(TWITTER_USER_TOKEN)) { throw new Exception($"Missing secret {nameof(TWITTER_USER_TOKEN)}"); }
if (string.IsNullOrEmpty(TWITTER_USER_SECRET)) { throw new Exception($"Missing secret {nameof(TWITTER_USER_SECRET)}"); }
if (string.IsNullOrEmpty(TWITTER_CONSUMER_TOKEN)) { throw new Exception($"Missing secret {nameof(TWITTER_CONSUMER_TOKEN)}"); }
if (string.IsNullOrEmpty(TWITTER_CONSUMER_SECRET)) { throw new Exception($"Missing secret {nameof(TWITTER_CONSUMER_SECRET)}"); }
var client = new TwitterClient(TWITTER_CONSUMER_TOKEN, TWITTER_CONSUMER_SECRET, TWITTER_USER_TOKEN, TWITTER_USER_SECRET);
var user = await client.Users.GetAuthenticatedUserAsync();
Console.WriteLine($"Authenticated as user {user}.");

// Find any tweets from the current user marked as "#selfdestruct."
var searchResponse = await client.Search.SearchTweetsAsync($"from:{user.ScreenName} #selfdestruct");
foreach (var tweet in searchResponse)
{
    Console.WriteLine($"Destroying {tweet.Url}.");
    await tweet.DestroyAsync();
}

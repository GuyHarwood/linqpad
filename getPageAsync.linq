<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var t = GetPage(new Uri("http://www.stackoverflow.com"));
	t.Result.Dump();
}

private static async Task<string> GetPage(Uri uri)
{
	var httpClient = new System.Net.Http.HttpClient();
	var downloadContentTask = httpClient.GetAsync(uri);
	await downloadContentTask;
	return await downloadContentTask.Result.Content.ReadAsStringAsync();
}
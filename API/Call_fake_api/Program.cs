// See https://aka.ms/new-console-template for more information
using System.Text;
using System.Text.Json;

//Create Fake DATA to post
var postData = new PostData()
{
    Id = 1,
    Firstname = "TestName",
    LastName = "TestLastName",
    Email = "TestEmail"
};


//Create base class for the endpoint
var client = new HttpClient();
client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");

//Transform client api object in json
var json = JsonSerializer.Serialize(postData);
var content = new StringContent(json, Encoding.UTF8,"application/json");

var response = client.PostAsync("posts", content).Result;

//See the response in string if success
if (response.IsSuccessStatusCode)
{
    var responseContent = response.Content.ReadAsStringAsync().Result;

    var postContent = JsonSerializer.Deserialize<PostData>(responseContent);

    Console.WriteLine($"Json");
    Console.WriteLine($"{responseContent}\n");

    Console.WriteLine($"Object c#:");
    Console.WriteLine($"id: {postContent.Id.ToString().ToLower()}\n {postContent.Firstname}\n  {postContent.LastName}\n  {postContent.Email}\n ");

}
else
{
    Console.WriteLine($"Error {response.StatusCode}");
}




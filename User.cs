namespace Friend_face;

public class User
{
    public string Name = string.Empty; // samme som ""
    public int Age;
    public string City = string.Empty;
    public string Country = string.Empty;
    public string Bio = string.Empty;
    public string Password = string.Empty;
    public List<User> Friends;  // Her er lista over brukerens venner


    public User(string name, int age, string city, string country, string bio, string password)
    {
        Name = name;
        Age = age;
        City = city;
        Country = country;
        Bio = bio;
        Password = password;
        Friends = new List<User>();
    }
}
namespace UserProfileApi.Models;

public class UserProfile
{
    public int Id { get; set; }
    public string Nickname { get; set; } = string.Empty;
    public string? AvatarUrl{ get; set; }
    public List<string> ExpertiseAreas { get; set; } = new();
    public string Gender { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public TimeSpan BirthTime { get; set; }
    public string BirthPlace { get; set; } = string.Empty;
    public string CurrentLocation { get; set; } = string.Empty; 
    public Dictionary<string, string> CustomFields { get; set; } = new();
}
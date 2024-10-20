namespace Social_Media.Models;

public class User
{
    public int UserID { get; set; }
    public string UserName { get; set; }
    public int Posts {get; set;}
    public int Likes{get; set;}
    public int comments{get;set;}
    public int CalculateEngagementScore(){
        return (Posts*5) + (Likes*2) + (Comments*3);
    }
    public bool IsContentFlagged(string content){
        var bannedWords = new HashSet<string> {"monolith", "spaghettiCode", "goto", "hack", "architrixs", "quickAndDirty", "cowboy", "yo", "globalVariable", "recursiveHell", "backdoor", "hotfix", "leakyAbstraction", "mockup", "singleton", "silverBullet", "technicalDebt" };
        var words = content.Split(' ');
            foreach (var word in words)
            {
                if (bannedWords.Contains(word.ToLower()))
                {
                    return true;
                }
            }
        return false;     
    }
}
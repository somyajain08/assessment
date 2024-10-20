using System.Collections.Generic;
using System.Linq;
using Social_Media.Models;

namespace Social_Media.Services
{
    public class ContentModerationService
    {
        // List of banned words
        private static readonly List<string> BannedWords = new List<string>
        {
            "monolith", "spaghettiCode", "goto", "hack", "architrixs",
            "quickAndDirty", "cowboy", "yo", "globalVariable", 
            "recursiveHell", "backdoor", "hotfix", "leakyAbstraction", 
            "mockup", "singleton", "silverBullet", "technicalDebt"
        };

        // Method to check if content contains any banned words
        public bool IsContentValid(string content, out string feedbackMessage)
        {
            var foundBannedWords = BannedWords.Where(word => content.Contains(word, System.StringComparison.OrdinalIgnoreCase)).ToList();

            if (foundBannedWords.Any())
            {
                feedbackMessage = $"Your content contains banned words: {string.Join(", ", foundBannedWords)}.";
                return false;
            }

            feedbackMessage = string.Empty;
            return true;
        }
    }
}

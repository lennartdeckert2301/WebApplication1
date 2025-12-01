using System;
using System.Collections.Generic;

namespace LernmoduleApp.Models
{
    public class QuizRun
    {
        public int Id { get; set; }
        public int ModuleIndex { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectCount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<QuizAnswer> Answers { get; set; } = new();
    }
}

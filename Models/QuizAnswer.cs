namespace LernmoduleApp.Models
{
    public class QuizAnswer
    {
        public int Id { get; set; }
        public int RunId { get; set; }
        public QuizRun? Run { get; set; }

        public string Question { get; set; } = string.Empty;
        public int? SelectedIndex { get; set; }
        public int CorrectIndex { get; set; }

        public bool IsCorrect => SelectedIndex.HasValue && SelectedIndex.Value == CorrectIndex;
    }
}

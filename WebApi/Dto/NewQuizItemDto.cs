namespace WebApi.Dto;

public class NewQuizItemDto
{
    public string question { get; set; }
    public IEnumerable<string> options { get; set; }
    public int correctOptionIndex { get; set; }
}

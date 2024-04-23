using ApplicationCore.Models.QuizAggregate;
using FluentValidation;

namespace WebApi.Validators;

public class QuizItemValidator : AbstractValidator<QuizItem>
{
    public QuizItemValidator()
    {
        RuleFor(q => q.Question)
            .MaximumLength(200).WithMessage("Pytanie nie może być dłuższe niż 200 znaków.")
            .MinimumLength(3).WithMessage("Pytanie nie może być krótsze od 3 znaków!");

        RuleForEach(q => q.IncorrectAnswers)
            .MaximumLength(200).WithMessage("Odpowiedź nie może być dłuższa niż 200 znaków.")
            .MinimumLength(1).WithMessage("Odpowiedź nie może być krótsza niż 200 znaków.");

        RuleFor(q => q.CorrectAnswer)
            .MaximumLength(200).WithMessage("Odpowiedź nie może być dłuższa niż 200 znaków.")
            .MinimumLength(1).WithMessage("Odpowiedź nie może być krótsza niż 200 znaków.");

        RuleFor(q => new { q.CorrectAnswer, q.IncorrectAnswers })
            .Must(t => !t.IncorrectAnswers.Contains(t.CorrectAnswer))
            .WithMessage("Poprawna odpowiedź nie powinna występować w liście niepoprawnych odpowiedzi!");

        RuleFor(q => q.IncorrectAnswers)
                .Must(i => i.Count > 0).WithMessage("Lista niepoprawnych odpowiedzi nie może być pusta.");
    }
}

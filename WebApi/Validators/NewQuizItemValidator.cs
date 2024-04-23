using ApplicationCore.Models.QuizAggregate;
using FluentValidation;
using WebApi.Dto;

namespace WebApi.Validators;

public class NewQuizItemValidator : AbstractValidator<NewQuizItemDto>
{
    public NewQuizItemValidator()
    {
        RuleFor(q => q.question)
                .NotEmpty().WithMessage("Pytanie nie może być puste.")
                .MaximumLength(200).WithMessage("Pytanie nie może być dłuższe niż 200 znaków.")
                .MinimumLength(3).WithMessage("Pytanie nie może być krótsze od 3 znaków!");

        RuleFor(q => q.options)
            .NotEmpty().WithMessage("Opcje odpowiedzi nie mogą być puste.")
            .Must(options => options != null && options.Any()).WithMessage("Opcje odpowiedzi nie mogą być puste.")
            .When(q => q.options != null);

        RuleFor(q => q.correctOptionIndex)
            .Must((dto, correctIndex) => dto.options != null && dto.options.Count() > correctIndex)
            .WithMessage("Indeks poprawnej odpowiedzi wykracza poza zakres dostępnych opcji.");

        RuleFor(q => q.options)
            .Must((dto, options) => dto.correctOptionIndex >= 0 && dto.correctOptionIndex < options.Count())
            .WithMessage("Indeks poprawnej odpowiedzi wykracza poza zakres dostępnych opcji.");
    }
}

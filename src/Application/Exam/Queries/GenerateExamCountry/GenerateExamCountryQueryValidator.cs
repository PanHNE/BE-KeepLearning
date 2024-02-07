namespace Application.Exam.Queries.GenerateExamCountry;

public class GenerateExamCountryQueryValidator : AbstractValidator<GenerateExamCountryQuery>
{
    public GenerateExamCountryQueryValidator()
    {
        RuleFor(q => q.Continents).NotEmpty().NotNull();
    }
}

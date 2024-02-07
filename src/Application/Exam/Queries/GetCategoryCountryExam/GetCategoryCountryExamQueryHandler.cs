using Domain.Models.Enums;

namespace Application.Exam.Queries.GetCategoryCountryExam;

public class GetCategoryCountryExamQueryHandler : IRequestHandler<GetCategoryCountryExamQuery, IEnumerable<string>>
{
    public GetCategoryCountryExamQueryHandler()
    {
    }

    public async Task<IEnumerable<string>> Handle(GetCategoryCountryExamQuery request, CancellationToken cancellationToken)
    {
        return await Task.Run(() => GuessType.GetAllLikeStrings());
    }
}

namespace jobs_application_api.Models
{
    public record AddJobVacancyInputModel(
        string Title,
        string Description,
        string Company,
        bool IsRemote,
        string SalaryRange)
    {
    }
}
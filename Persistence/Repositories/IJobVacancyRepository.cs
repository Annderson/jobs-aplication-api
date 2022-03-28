using jobs_application_api.Entities;

namespace jobs_application_api.Persistence.Repositories
{
    public interface IJobVacancyRepository
    {
        List<JobVacancy> GetAll();
        JobVacancy GetById(int id);
        void Add(JobVacancy jobVacancy);
        void Update(JobVacancy jobVacancy);
        void AddApplication(JobApplication jobApplication);
    }
}
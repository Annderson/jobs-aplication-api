using Microsoft.AspNetCore.Mvc;
using jobs_application_api.Models;
using jobs_application_api.Entities;
using jobs_application_api.Persistence.Repositories;

namespace jobs_application_api.Controllers
{

    [Route("api/job-vacancies/{id}/applications")]
    [ApiController]
    public class JobApplicationController : ControllerBase
    {
        private readonly IJobVacancyRepository _repository;

        public JobApplicationController(IJobVacancyRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Post(int id, AddJobApplicationInputModel model)
        {
            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            var application = new JobApplication(
                model.ApplicantName,
                model.ApplicantEmail,
                id
            );

            _repository.AddApplication(application);

            return NoContent();
        }
    }
}
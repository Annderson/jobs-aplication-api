using Microsoft.AspNetCore.Mvc;
using jobs_application_api.Models;
using jobs_application_api.Entities;
using jobs_application_api.Persistence.Repositories;
using Serilog;

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

        /// <summary>
        ///   Register application in a job vacancy 
        /// </summary>
        /// <remarks>
        /// {
        ///   "applicantName": "Candidate's name",
        ///   "applicantEmail": "exemplo@mail.com",
        ///   "idJobVacancy": "Job id"
        /// }
        /// </remarks>
        /// <param name="id">Id job vacancy</param>
        /// <param name="model">Candidate data</param>
        /// <returns></returns>
        /// <response code="201">Success</response>
        /// <response code="400">Invalid data</response>
        [HttpPost]
        public IActionResult Post(int id, AddJobApplicationInputModel model)
        {
            Log.Information("Register job application");
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
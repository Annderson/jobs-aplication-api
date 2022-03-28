using Microsoft.AspNetCore.Mvc;
using jobs_application_api.Models;
using jobs_application_api.Entities;
using jobs_application_api.Persistence.Repositories;

namespace jobs_application_api.Controllers
{

    [Route("api/job-vacancies")]
    [ApiController]
    public class JobVacanciesController : ControllerBase
    {
        private readonly IJobVacancyRepository _repository;

        public JobVacanciesController(IJobVacancyRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var jobVacancies = _repository.GetAll();
                
            return Ok(jobVacancies);
        }

        [HttpGet("{id}")]
        public  IActionResult GetById(int id)
        {
            var jobVacancy = _repository.GetById(id);
            if (jobVacancy == null)
                return NotFound();

            return Ok(jobVacancy);
        }

        [HttpPost]
        public IActionResult Post(AddJobVacancyInputModel model)
        {
            var jobVacancy = new JobVacancy(
                model.Title,
                model.Description,
                model.Company,
                model.IsRemote,
                model.SalaryRange
            );
            _repository.Add(jobVacancy);
            return CreatedAtAction("GetById", new { id = jobVacancy.Id }, jobVacancy);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateJobVacancyInputModel model)
        {
            var jobVacancy = _repository.GetById(id);
            
            if (jobVacancy == null)
                return NotFound();
            
            jobVacancy.Update(model.Title, model.Description);
            _repository.Update(jobVacancy);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
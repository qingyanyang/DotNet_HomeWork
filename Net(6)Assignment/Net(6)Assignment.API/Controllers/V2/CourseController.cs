#pragma warning restore CS1591
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Net_6_Assignment.Common.Enums;
using Net_6_Assignment.Common.Exceptions;
using Net_6_Assignment.DTOs;
using Net_6_Assignment.Models;
using Net_6_Assignment.Services;

namespace Net_6_Assignment.Controllers.V2
{
    [ApiExplorerSettings(GroupName = nameof(APIVersion.V2))]
    [ApiController]
    [Route("api/v2/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly IService<Course> _CourseService;
        private readonly IMapper _mapper;
        private readonly ILogger<CourseController> _logger;

        public CourseController(IService<Course> CourseService, IMapper mapper, ILogger<CourseController> logger)
        {
            _CourseService = CourseService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all Course items.
        /// </summary>
        /// <returns>A list of all Course items.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseResponseDTO>>> GetAllCourses()
        {
            _logger.LogInformation("Retrieving all courses.");
            IEnumerable<Course> CourseList = await _CourseService.GetAllAsync();
            IEnumerable<CourseResponseDTO> res = _mapper.Map<IEnumerable<CourseResponseDTO>>(CourseList);
            _logger.LogInformation("Successfully retrieved all courses. Total count: {Count}", CourseList.Count());
            return Ok(res);
        }

        /// <summary>
        /// Retrieves a Course item by its specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Course item.</param>
        /// <returns>The Course item with the specified ID.</returns>
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CourseResponseDTO>> GetCourseById(Guid id)
        {
            _logger.LogInformation("Retrieving course with ID: {Id}", id);
            Course? Course = await _CourseService.GetByConditionAsync(Course => Course.Id == id);
            if (Course == null)
            {
                _logger.LogWarning("Course with ID: {Id} does not exist.", id);
                throw new A6NotFoundException("Course does not exist!");
            }
            CourseResponseDTO res = _mapper.Map<CourseResponseDTO>(Course);
            _logger.LogInformation("Successfully retrieved course with ID: {Id}", id);
            return Ok(res);
        }

        /// <summary>
        /// Creates a new Course item.
        /// </summary>
        /// <param name="CourseInfo">The request DTO containing Course item information.</param>
        /// <returns>The newly created Course item, along with the URI to access it.</returns>
        [HttpPost]
        public async Task<ActionResult<CourseResponseDTO>> CreateCourse([FromBody] CourseRequestDTO CourseInfo)
        {
            _logger.LogInformation("Creating a new course.");
            Course Course = _mapper.Map<Course>(CourseInfo);
            Course newCourse = await _CourseService.CreateAsync(Course);
            CourseResponseDTO createdCourse = _mapper.Map<CourseResponseDTO>(newCourse);
            _logger.LogInformation("Successfully created course with ID: {Id}", createdCourse.Id);
            return CreatedAtAction(nameof(GetCourseById), new { id = createdCourse.Id }, createdCourse);
        }

        /// <summary>
        /// Updates an existing Course item.
        /// </summary>
        /// <param name="updatedCourse">The DTO containing the updated Course item information.</param>
        /// <returns>The updated Course item information.</returns>
        [HttpPut]
        public async Task<ActionResult<CourseResponseDTO>> UpdateCourse([FromBody] CourseUpdateRequestDTO updatedCourse)
        {
            Guid CourseId = updatedCourse.Id;
            _logger.LogInformation("Updating course with ID: {Id}", CourseId);

            Course? existingCourse = await _CourseService.GetByConditionAsync(Course => Course.Id == CourseId);
            if (existingCourse == null)
            {
                _logger.LogWarning("Course with ID: {Id} does not exist. Cannot update.", CourseId);
                throw new A6NotFoundException("Course does not exist!");
            }

            Course mappedUpdatedCourse = _mapper.Map<Course>(updatedCourse);
            Course updatedDbCourse = await _CourseService.UpdateAsync(existingCourse, mappedUpdatedCourse);
            CourseResponseDTO updatedCourseRes = _mapper.Map<CourseResponseDTO>(updatedDbCourse);
            _logger.LogInformation("Successfully updated course with ID: {Id}", CourseId);
            return Ok(updatedCourseRes);
        }

        /// <summary>
        /// Deletes a Course item by its specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Course item.</param>
        /// <returns>A no-content response.</returns>
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteCourse(Guid id)
        {
            _logger.LogInformation("Attempting to delete course with ID: {Id}", id);

            Course? existingCourse = await _CourseService.GetByConditionAsync(Course => Course.Id == id);
            if (existingCourse == null)
            {
                _logger.LogWarning("Course with ID: {Id} does not exist. Cannot delete.", id);
                throw new A6NotFoundException("Course does not exist!");
            }

            await _CourseService.DeleteAsync(existingCourse);
            _logger.LogInformation("Successfully deleted course with ID: {Id}", id);
            return NoContent();
        }
    }
}
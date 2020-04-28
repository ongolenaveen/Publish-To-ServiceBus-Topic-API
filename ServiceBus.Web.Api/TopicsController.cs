using Microsoft.AspNetCore.Mvc;
using ServiceBus.Web.App.Contracts;
using ServiceBus.Web.App.Contracts.Models;
using System.Threading.Tasks;

namespace ServiceBus.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController: ControllerBase
    {
        private readonly IMessageService<Employee> _messageService;

        /// <summary>
        /// Topics Contoller Constructor
        /// </summary>
        /// <param name="messageService">Message Service</param>
        public TopicsController(IMessageService<Employee> messageService)
        {
            _messageService = messageService;
        }

        /// <summary>
        /// Publish Employee Details to Service Bus Topic 
        /// </summary>
        /// <param name="employee">Employee Details</param>
        /// <returns>Http Status Code</returns>
        [HttpPost("publish")]
        public async Task<IActionResult> Publish(Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _messageService.Publish(employee);

            return Ok();
        }
    }
}

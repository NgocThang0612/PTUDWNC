using Microsoft.AspNetCore.Mvc;
using TatBlog.Core.Constants;
using TatBlog.Services.Subscribers;

namespace TatBlog.WebApp.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly ISubscriberRepository _subscriberRepository;
        public NewsletterController(ISubscriberRepository subscriberRepository)
        {
            _subscriberRepository = subscriberRepository;
        }

        public async Task<IActionResult> Subscriber(
            string Email)
            
        {
            var sub = await _subscriberRepository.SubscribeAsync(Email);
            return View(sub);
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}

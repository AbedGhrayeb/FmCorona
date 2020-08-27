using Application.Topics;
using Application.Topics.Commands;
using Application.Topics.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class CommonController : BaseMvcController
    {
        public async Task<IActionResult> Contacts()
        {
            return View(await Mediator.Send(new ContactsList.ContactsListQuery()));
        }
        public async Task<IActionResult> Topics()
        {
            var topics = await Mediator.Send(new TopicsList.TopicsListQuery());
            var topicsVm = topics.TopicsDtos;
            return View(topicsVm);
        }
        public async Task<IActionResult> Topic(int id)
        {
            var topic = await Mediator.Send(new TopicDetails.TopicDeatailsQuery{Id=id});
            var vm = topic.TopicDto;
            return View(vm);
        }

        public async Task<IActionResult> EditTopic(int id)
        {
            var topicEnvelope= await Mediator.Send(new TopicDetails.TopicDeatailsQuery{Id=id});
            var topic=topicEnvelope.TopicDto;
            var model=new EditTopicVm{
                Id=topic.Id,Title=topic.Title,Body=topic.Body
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTopic(int id,EditTopicVm model)
        {
            try
            {
                model.Id=id;
                await Mediator.Send(new EditTopic.EditTopicCommand(model));
                return View(nameof(Topics));
            }
            catch (System.Exception)
            {
                
                return View(model);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using simpleProject.Parameter;

namespace simpleProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        private static List<Card> _cards = new List<Card>();


        [HttpGet]
        public List<Card> GetList()
        {
            return _cards;
        }
        //搜尋卡片列表

        [HttpGet]
        [Route("{id}")]
        public Card Get([FromRoute] int id)
        {
            return _cards.FirstOrDefault(card => card.Id == id);
        }
        //搜尋單一卡片

        [HttpPost]
        public IActionResult Insert([FromBody] CardParameter parameter)
        {
            _cards.Add(new Card
            {
                Id = _cards.Any()
            ? _cards.Max(card => card.Id) + 1
            : 0,
                Name = parameter.Name,
                Description = parameter.Description
            });
            return Ok();
        }
        //新增卡片
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] CardParameter parameter)
        {
            var targetCard = _cards.FirstOrDefault(card => card.Id == id);
            if (targetCard is null) {
                return NotFound();
            }
            targetCard.Name = parameter.Name;
            targetCard.Description = parameter.Description;

            return Ok();
        }
        //搜尋卡片

        
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _cards.RemoveAll(card => card.Id == id);
            return Ok();
        }
        //刪除卡片
     }
}


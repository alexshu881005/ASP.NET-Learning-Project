using Microsoft.AspNetCore.Mvc;
using simpleProject.Parameter;
using TestProject.Repository;

namespace simpleProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        private static List<Card> _cards = new List<Card>();
        string _connectionString = @"Server=(LocalDB)\MSSQLLocalDB;Database=Localcard;Trusted_Connection=True;";
        CardRepository cardRepository = new CardRepository();
        [HttpGet]
        public List<Card> GetList()
        {
            var GetCard=cardRepository.GetList();
            return GetCard.ToList();

        }
        //搜尋卡片列表

        [HttpGet]
        [Route("{id}")]
        public Card Get([FromRoute] int id)
        {
            var result = cardRepository.Get(id);
            if (result is null) {
                Response.StatusCode = 404;
                return null;
            }
            return result;
        }
        //搜尋單一卡片

        [HttpPost]
        public IActionResult Insert([FromBody] CardParameter parameter)
        {
            var AddCard = cardRepository.Create(parameter);
                        
            return Ok();
        }//新增卡片
        
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(
        [FromRoute] int id,
        [FromBody] CardParameter parameter)
        {
            var targetCard = this.cardRepository.Get(id);
            if (targetCard is null)
            {
                return NotFound();
            }

            var isUpdateSuccess = this.cardRepository.Update(id, parameter);
            if (isUpdateSuccess)
            {
                return Ok();
            }
            return StatusCode(500);
        }
        //修改卡片


        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            this.cardRepository.Delete(id);
            return Ok();
        }
        //刪除卡片
     }
}


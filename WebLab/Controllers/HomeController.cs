using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;  // Не забудьте импортировать пространство имен

namespace WebLab.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Пример данных для SelectList
            var options = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Text = "Option 1", Value = "1" },
                    new SelectListItem { Text = "Option 2", Value = "2" },
                    new SelectListItem { Text = "Option 3", Value = "3" }
                },
                "Value", // Название свойства, которое будет значением для select
                "Text"   // Название свойства, которое будет отображаться в списке
            );

            ViewBag.Text = "Добро пожаловать!";
            return View(options);
        }

        [HttpGet]
        public IActionResult FormSubmit(string login, string password, string radioGroup, bool checkbox1, bool checkbox2, string selDemo)
        {
            // Обработать данные формы
            // Например, вывести их в консоль
            Console.WriteLine($"Login: {login}, Password: {password}, Selected Radio: {radioGroup}, Checkbox1: {checkbox1}, Checkbox2: {checkbox2}, Selected Option: {selDemo}");
            return View("Index");
        }
    }
}
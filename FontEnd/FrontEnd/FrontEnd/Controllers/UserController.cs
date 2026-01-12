using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FrontEnd.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("UserApi");
            var users = await client.GetFromJsonAsync<List<UserViewModel>>("Users");
            return View(users ?? new List<UserViewModel>());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel user)
        {
            if (!ModelState.IsValid) return View(user);

            var client = _httpClientFactory.CreateClient("UserApi");
            var response = await client.PostAsJsonAsync("Users", user);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            var errorContent = await response.Content.ReadAsStringAsync();

            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var errorResponse = JsonSerializer.Deserialize<ApiErrorResponse>(errorContent, options);

                string errorMessage = errorResponse?.Message ?? "Erorr from server";

                ModelState.AddModelError(string.Empty, errorMessage);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Unknow erorr from server");
            }

            return View(user);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("UserApi");
            var user = await client.GetFromJsonAsync<UserViewModel>($"Users/{id}");
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UserViewModel user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var client = _httpClientFactory.CreateClient("UserApi");

            var response = await client.PutAsJsonAsync($"Users/{id}", user);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            var errorContent = await response.Content.ReadAsStringAsync();

            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var errorResponse = JsonSerializer.Deserialize<ApiErrorResponse>(errorContent, options);

                string errorMessage = errorResponse?.Message ?? "Erorr while update";

                ModelState.AddModelError(string.Empty, errorMessage);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Unknow erorr from server");
            }

            return View(user);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("UserApi");
            var user = await client.GetFromJsonAsync<UserViewModel>($"Users/{id}");
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _httpClientFactory.CreateClient("UserApi");
            await client.DeleteAsync($"Users/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
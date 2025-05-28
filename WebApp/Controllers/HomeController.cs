using dotnet_note_webapp_example.Data;
using dotnet_note_webapp_example.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_note_webapp_example.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly NoteClient _client;
        public HomeController(NoteClient client)
        {
            _client = client;
        }

        [HttpGet("")]
        public IActionResult Index() => View();

        [HttpPost("CreateNote")]
        public IActionResult CreateNote(Note note)
        {
            _client.AddNote(note);
            return RedirectToPage("/Index");
        }

        [HttpPost("UpdateNote")]
        public IActionResult UpdateNote(Note note)
        {
            _client.UpdateNote(note);
            return RedirectToPage("/Index");
        }

        // Use POST for delete to support form submissions
        [HttpPost("DeleteNote/{id}")]
        public IActionResult DeleteNote(int id)
        {
            var note = _client.GetNote(id.ToString()).FirstOrDefault();
            if (note != null)
            {
                _client.DeleteNote(id.ToString());
            }
            return RedirectToPage("/Index");
        }

        [HttpPost("DownloadNote/{id}")]
        public IActionResult DownloadNote(int id)
        {
            var note = _client.GetNote(id.ToString()).FirstOrDefault();
            if (note == null)
                return NotFound();
            var content = System.Text.Encoding.UTF8.GetBytes(note.Content);
            var stream = new MemoryStream(content);
            var fileName = $"{note.Title}.txt";
            return File(stream, "text/plain", fileName);
        }
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using dotnet_note_webapp_example.Data;
using dotnet_note_webapp_example.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_note_webapp_example.Pages
{
    public class IndexModel : PageModel
    {
        private readonly NoteClient _client;

        public IndexModel(NoteClient client)
        {
            _client = client;
        }

        public IList<Note> Notes { get; set; }

        public async Task OnGetAsync()
        {
            Notes = _client.GetNote(null); //Check for concurrency issues later
        }
    }
}
namespace Campoverde.QMS.Controllers
{
    public class QuoteNoteController(CampoverdeDbContext dbContext) : Controller
    {
        private readonly CampoverdeDbContext _dbContext = dbContext;
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateNote(int quoteId, string quoteNote)
        {
            if (quoteId > 0 && !string.IsNullOrWhiteSpace(quoteNote))
            {
                QuoteNote note = new QuoteNote
                {
                    QuoteId = quoteId,
                    Notes = quoteNote,
                    IsActive = true,
                    IsDeleted = false
                };
                _dbContext.QuoteNote.Add(note);
                _dbContext.SaveChanges();

                return Json(true);
            }
            return Json(false);
        }

        [HttpGet]
        public JsonResult GetQuoteNotes(int quoteId)
        {
            List<QuoteNote> quoteNote = [.. _dbContext.QuoteNote.Where(x => x.QuoteId == quoteId)];
            return Json(quoteNote);
        }
    }
}

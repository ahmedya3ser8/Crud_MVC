
namespace GameZone.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly AppDbContext _Context;

        public CategoriesService(AppDbContext dbContext)
        {
            _Context = dbContext;
        }
        public IEnumerable<SelectListItem> GetSelectedList()
        {
            return _Context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name 
                }).OrderBy(c => c.Text).AsNoTracking().ToList();
        }
    }
}

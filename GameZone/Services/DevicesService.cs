namespace GameZone.Services
{
    public class DevicesService : IDevicesService
    {
        private readonly AppDbContext _Context;
        public DevicesService(AppDbContext dbContext)
        {
            _Context = dbContext;
        }
        public IEnumerable<SelectListItem> GetDeviceList()
        {
            return _Context.Devices
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                }).OrderBy(d => d.Text).AsNoTracking().ToList();
        }
    }
}

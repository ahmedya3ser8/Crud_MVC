namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
		private readonly ICategoriesService _categoriesService;
		private readonly IDevicesService _devicesService;
		private readonly IGameService _gameService;

		public GamesController(ICategoriesService categoriesService, IDevicesService devicesService, IGameService gameService)
        {
			_categoriesService = categoriesService;
			_devicesService = devicesService;
			_gameService = gameService;
		}
        public IActionResult Index()
        {
            var games = _gameService.GetAll();
            return View(games);
        }

        public IActionResult Details(int id)
        {
            var game = _gameService.GetById(id);

            if (game is null)
                return NotFound();

            return View(game);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new()
            {
                Categories = _categoriesService.GetSelectedList(),
                Devices = _devicesService.GetDeviceList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
				model.Categories = _categoriesService.GetSelectedList();
                model.Devices = _devicesService.GetDeviceList();
				return View(model);
            }

            await _gameService.Create(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _gameService.GetById(id);
            if (game is null)
                return NotFound();
            EditGameFormViewModel viewModel = new()
            {
                Id = id,
                Name = game.Name,
                CategoryId = game.CategoryId,
                Description = game.Description,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = _categoriesService.GetSelectedList(),
                Devices = _devicesService.GetDeviceList(),
                CurrentCover = game.Cover
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetSelectedList();
                model.Devices = _devicesService.GetDeviceList();
                return View(model);
            }

            var game = await _gameService.Update(model);

            if (game is null)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _gameService.Delete(id);
            return isDeleted ? Ok() : BadRequest();
        }
    }
}

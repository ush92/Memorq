using Memorq.Infrastructure;
using Memorq.Services;

namespace Memorq.ViewModels
{
    public class ImportExportViewModel : BaseViewModel
    {
        private readonly ICategoryService _categoryService;
        private readonly IItemService _itemService;
        private readonly IWindowFactory _windowFactory;
        private readonly IStringResourcesDictionary _stringResourcesDictionary;

        public ImportExportViewModel(ICategoryService categoryService, IItemService itemService,
                                     IWindowFactory windowFactory, IStringResourcesDictionary stringResourcesDictionary)
        {
            _categoryService = categoryService;
            _itemService = itemService;
            _windowFactory = windowFactory;
            _stringResourcesDictionary = stringResourcesDictionary;
        }
    }
}
using Memorq.Infrastructure;
using Memorq.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorq.ViewModels
{
    public class ImportExportViewModel : BaseViewModel
    {
        private readonly ICategoryService _categoryProvider;
        private readonly IItemService _itemProvider;
        private readonly IWindowFactory _windowFactory;

        public ImportExportViewModel(ICategoryService categoryProvider, IItemService itemProvider, IWindowFactory windowFactory)
        {
            _categoryProvider = categoryProvider;
            _itemProvider = itemProvider;
            _windowFactory = windowFactory;
        }
    }
}
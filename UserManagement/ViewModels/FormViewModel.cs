using System.Collections.Generic;

namespace UserManagement.ViewModels
{
    public class FormViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<CheckBoxViewModel> Items { get; set; }
    }
}

using System.Collections.Generic;

namespace FootballManager.Core {
    public class ApplicationViewModel : BaseViewModel 
    {

        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Home;

        public List<BaseViewModel> ActiveViewModels { get; set; } = new List<BaseViewModel>();

        public bool SideMenuVisible { get; set; } = false;

        public void GoToPage(ApplicationPage page) {

            CurrentPage = page;
            SideMenuVisible = page != ApplicationPage.Home;
        }
    }
}

using FootballManager.Core;
using System.Linq;
using System.Windows.Controls;

namespace FootballManager {


    public class BasePage<VM> : Page 
        where VM: BaseViewModel, new() {

        private VM mViewModel;

        public VM ViewModel {
            get { return mViewModel; }
            set {
                if (mViewModel == value)
                    return;

                mViewModel = value;

                this.DataContext = mViewModel;
            }
        }

        public BasePage() {

            ApplicationViewModel temp = IoC.Get<ApplicationViewModel>();

            VM viewModel = temp.ActiveViewModels.Where(x => x.GetType() == typeof(VM)).FirstOrDefault() as VM;

            if (viewModel != null)
                this.ViewModel = viewModel;

            else {
                this.ViewModel = new VM();
                temp.ActiveViewModels.Add(this.ViewModel);
            }
            
            
        }


    }
}

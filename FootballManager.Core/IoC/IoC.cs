using Ninject;

namespace FootballManager.Core {
    public static class IoC {

        public static IKernel Kernel { get; private set; } = new StandardKernel();

        public static void Setup() {

            BindViewModels();
        }

        public static T Get<T>() {

            return Kernel.Get<T>();
            
        }

        private static void BindViewModels() {

            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());
        }
    }
}

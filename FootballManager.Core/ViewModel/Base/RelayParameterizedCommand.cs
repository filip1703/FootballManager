using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FootballManager.Core {
    public class RelayParameterizedCommand : ICommand {

        private Action<object> mAction;

        public event EventHandler CanExecuteChanged = (sender,e) => { };

        public RelayParameterizedCommand(Action<object> action) {

            mAction = action;
        }

        public bool CanExecute(object parameter) {

            return true;
        }

        public void Execute(object parameter) {

            mAction(parameter);
        }
    }
}

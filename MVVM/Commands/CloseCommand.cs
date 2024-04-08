using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MovieApp.MVVM.Commands
{
    class CloseCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            Application.Current.Shutdown();
        }
    }
}

using System.Diagnostics;
using System.Security.Policy;
using System.Windows;

namespace MovieApp.MVVM.Commands
{
    internal class GetProviderPageCommand : CommandBase
    {
        // Em teste...
        public override void Execute(object? parameter)
        {
            string target;
            string movieProviderName = (string)parameter;

            if (movieProviderName.Contains("Netflix")) 
            {
                target = "https://www.netflix.com";
            }
            else
            {
                target = "https://www.google.com";
            }

            try
            {
                Process.Start(new ProcessStartInfo(target) { UseShellExecute = true });

            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }
    }
}

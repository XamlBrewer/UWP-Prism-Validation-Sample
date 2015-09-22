using Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace XamlBrewer.Uwp.PrismValidationSample.ViewModels
{
    internal class CivilizationViewModel : ValidatableBindableBase
    {
        private string name;
        private string quadrant;
        private string estimatedPopulation;
        private DateTime membershipDate;
        private DateTime discoveryDate;

        private DelegateCommand validateCommand;

        public CivilizationViewModel()
        {
            validateCommand = new DelegateCommand(Validate_Executed);
        }

        [Required(ErrorMessage = "Name is required.")]
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public string Quadrant
        {
            get { return quadrant; }
            set { SetProperty(ref quadrant, value); }
        }

        [Numeric(ErrorMessage = "Population should be numeric.")]
        public string EstimatedPopulation
        {
            get { return estimatedPopulation; }
            set { SetProperty(ref estimatedPopulation, value); }
        }

        public DateTime DiscoveryDate
        {
            get { return discoveryDate; }
            set { SetProperty(ref discoveryDate, value); }
        }

        [GreaterThanProperty("DiscoveryDate", ErrorMessage="Membership should be after Discovery date.")]
        public DateTime MembershipDate
        {
            get { return membershipDate; }
            set { SetProperty( ref membershipDate, value); }
        }

        public ICommand ValidateCommand
        {
            get { return validateCommand; }
        }

        private void Validate_Executed()
        {
            ValidateProperties();
        }
    }
}

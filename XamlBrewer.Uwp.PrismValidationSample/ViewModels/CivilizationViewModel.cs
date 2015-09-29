using Mvvm;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace XamlBrewer.Uwp.PrismValidationSample.ViewModels
{
    internal class CivilizationViewModel : ValidatableBindableBase
    {
        private string name;
        private string location;
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

        [RegularExpression(@"[ABCD]\d{2,5}", ErrorMessage = "Location is Quadrant (A -> D) and Sector (2 -> 5 digits)")]
        public string Location
        {
            get { return location; }
            set { SetProperty(ref location, value); }
        }

        [Numeric(ErrorMessage = "Population should be numeric.")]
        public string EstimatedPopulation
        {
            get { return estimatedPopulation; }
            set { SetProperty(ref estimatedPopulation, value); }
        }

        // Should have a NotLaterThanProperty attribute.
        public DateTime DiscoveryDate
        {
            get { return discoveryDate; }
            set
            {
                SetProperty(ref discoveryDate, value);

                // Doesn't trigger the validation.
                // this.OnPropertyChanged("MembershipDate"); 

                // Triggers the validation twice. Silly overkill.
                // MembershipDate = new DateTime(membershipDate.Ticks+1);
                // MembershipDate = new DateTime(membershipDate.Ticks-1);
            }
        }

        [LaterThanPropertyAttribute("DiscoveryDate", ErrorMessage = "Affiliation date should come after date of first contact.")]
        public DateTime MembershipDate
        {
            get { return membershipDate; }
            set { SetProperty(ref membershipDate, value); }
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

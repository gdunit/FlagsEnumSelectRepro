namespace FlagsEnumSelectRepro.Pages
{
    public partial class Index
    {

        protected WorkingDays WorkingDaysValue { get; set; }

        protected IReadOnlyList<WorkingDays> WorkingDaysValueList 
        {
            get
            {
                return GetWorkingDaysList();
            }
            set
            {
                SetWorkingDaysFromList(value);
            }
        }

        public IReadOnlyList<WorkingDays> GetWorkingDaysList()
        {
            List<WorkingDays> result = new List<WorkingDays>();
            // Iterate over the flags
            foreach (WorkingDays wd in Enum.GetValues(typeof(WorkingDays)))
            {
                if (WorkingDaysValue.HasFlag(wd) && wd != WorkingDays.None)
                {
                    result.Add(wd);
                }
            }
            if (result.Count == 0)
            {
                result.Add(WorkingDays.None);
            }
            return result;
        }

        public void SetWorkingDaysFromList(IReadOnlyList<WorkingDays> input)
        {
            // Iterate over the flags
            foreach (WorkingDays wd in Enum.GetValues(typeof(WorkingDays)))
            {
                if (input.Contains(wd))
                {
                    if (!WorkingDaysValue.HasFlag(wd))
                    {
                        WorkingDaysValue |= wd;
                    }
                    else
                    {
                        WorkingDaysValue &= ~wd;
                    }
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            // Simulate loading data from db
            await Task.Delay(1000);
            await Task.Run(() => { WorkingDaysValue = WorkingDays.Monday | WorkingDays.Tuesday; });
            await base.OnInitializedAsync();
        }

        [Flags]
        public enum WorkingDays
        {
            None = 0,
            Sunday = 1,
            Monday = 2,
            Tuesday = 4,
            Wednesday = 8,
            Thursday = 16,
            Friday = 32,
            Saturday = 64
        }
    }
}

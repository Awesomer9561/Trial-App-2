using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Trial_App_2.ViewModel;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Models;

namespace Trial_App_2.Pages.BasicFeatures.Pages
{
    public class CalenderViewModel : BaseViewModel
    {
        public EventCollection Events { get; set; }
        public string note;
        public string Note
        {
            get { return note; }
            set
            {
                note = value;
                OnPropertyChange("Note");
            }
        }
        public ICommand AddEvent { get; }
        public CalenderViewModel()
        {
            AddEvent = new Command(setEvent);
        }
        public void setEvent()
        {
            Events = new EventCollection
            {
                [DateTime.Now] = new List<Pages.Calender.EventModel>
    {
        new Pages.Calender.EventModel { Name = "Cool event1", Description = "This is Cool event1's description!" },
        new Pages.Calender.EventModel { Name = "Cool event2", Description = "This is Cool event2's description!" }
    },
                // 5 days from today
                [DateTime.Now.AddDays(5)] = new List<Pages.Calender.EventModel>
    {
        new Pages.Calender.EventModel  { Name = "Cool event3", Description = "This is Cool event3's description!" },
        new Pages.Calender.EventModel  { Name = "Cool event4", Description = "This is Cool event4's description!" }
    },
                // 3 days ago
                [DateTime.Now.AddDays(-3)] = new List<Pages.Calender.EventModel>
    {
        new Pages.Calender.EventModel  { Name = "Cool event5", Description = "This is Cool event5's description!" }
    },
                // custom date
                [new DateTime(2020, 3, 16)] = new List<Pages.Calender.EventModel>
    {
        new Pages.Calender.EventModel  { Name = "Cool event6", Description = "This is Cool event6's description!" }
    }
            };
        }


    }
}

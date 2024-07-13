using Microsoft.AspNetCore.Mvc.Rendering;

namespace Onlinehelpdesk.Models.ViewModel
{

    public class TicketViewModel
    {


        public Ticket Ticket { get; set; }

        public string Id { get; set; }
       

        // Holds the SelectList for categories
        public SelectList Category { get; set; }
        public int CategoryId { get; set; } // Holds the selected category ID

        // Holds the SelectList for statuses
        public SelectList Status { get; set; }
       public int StatusId { get; set; }
        // Holds the SelectList for periods
        public SelectList Period { get; set; }
        public int PeriodId { get; set; } // Holds the selected period ID


        public int SelectedCategoryId { get; set; }  // Holds the selected role ID
    }
}

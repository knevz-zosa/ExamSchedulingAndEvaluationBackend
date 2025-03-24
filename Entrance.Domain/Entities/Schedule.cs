using Entrance.Domain.Contracts;

namespace Entrance.Domain.Entities;
public class Schedule : BaseEntity<int>
{
    public DateTime ScheduleDate { get; set; }
    public string SchoolYear { get; set; }
    public string Venue { get; set; }
    public string Time { get; set; }
    public int Slot { get; set; }
    public int CampusId { get; set; }
    public Campus Campus { get; set; }
    public ICollection<Applicant> Applicants { get; set; }
    public DateTime DateCreated { get; set; }
    public int CreatedById { get; set; }

    public int Available => AvailableSlots();

    private int AvailableSlots()
    {
        if (Applicants == null || Applicants.Count == 0)
        {
            return Slot;
        }
        else
        {
            int completedApplicantsCount = Applicants.Count(x => x.Registered == true);

            int availableSlots = Slot - completedApplicantsCount;

            return availableSlots;
        }
    }
}

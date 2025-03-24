namespace Entrance.Shared.Models;

#region REQUESTS
public class PersonalityProfileRequest
{
    public int ApplicantId { get; set; }
    public bool WellGroomed { get; set; } = false;
    public bool Friendly { get; set; } = false;
    public bool Active { get; set; } = false;
    public bool Confident { get; set; } = false;
    public bool Polite { get; set; } = false;
    public bool SelfControl { get; set; } = false;
    public bool WorksPromptly { get; set; } = false;
    public bool Adaptable { get; set; } = false;
    public bool Outgoing { get; set; } = false;
    public bool Organized { get; set; } = false;
    public bool Creative { get; set; } = false;
    public bool Truthful { get; set; } = false;
    public bool HabituallySilent { get; set; } = false;
    public bool Generous { get; set; } = false;
    public bool Conforming { get; set; } = false;
    public bool Resourceful { get; set; } = false;
    public bool Cautious { get; set; } = false;
    public bool Conscientious { get; set; } = false;
    public bool GoodNatured { get; set; } = false;
    public bool Industrious { get; set; } = false;
    public bool EmotionallyStable { get; set; } = false;
    public bool WorksWillWithOthers { get; set; } = false;
    public bool VolunteersToLead { get; set; } = false;
    public bool PreferredByGroups { get; set; } = false;
    public bool TakesChargeWhenAssigned { get; set; } = false;
    public string? Problems { get; set; }
    public string? ComfortableDiscussing { get; set; }
    public bool Studies { get; set; } = false;
    public bool Family { get; set; } = false;
    public bool Friend { get; set; } = false;
    public bool Self { get; set; } = false;
    public string? Specify { get; set; }
}
#endregion


#region RESPONSES
public class PersonalityProfileResponse
{
    public int Id { get; set; }
    public bool WellGroomed { get; set; }
    public bool Friendly { get; set; }
    public bool Active { get; set; }
    public bool Confident { get; set; }
    public bool Polite { get; set; }
    public bool SelfControl { get; set; }
    public bool WorksPromptly { get; set; }
    public bool Adaptable { get; set; }
    public bool Outgoing { get; set; }
    public bool Organized { get; set; }
    public bool Creative { get; set; }
    public bool Truthful { get; set; }
    public bool HabituallySilent { get; set; }
    public bool Generous { get; set; }
    public bool Conforming { get; set; }
    public bool Resourceful { get; set; }
    public bool Cautious { get; set; }
    public bool Conscientious { get; set; }
    public bool GoodNatured { get; set; }
    public bool Industrious { get; set; }
    public bool EmotionallyStable { get; set; }
    public bool WorksWillWithOthers { get; set; }
    public bool VolunteersToLead { get; set; }
    public bool PreferredByGroups { get; set; }
    public bool TakesChargeWhenAssigned { get; set; }
    public string? Problems { get; set; }
    public string? ComfortableDiscussing { get; set; }
    public bool Studies { get; set; }
    public bool Family { get; set; }
    public bool Friend { get; set; }
    public bool Self { get; set; }
    public string? Specify { get; set; }
}
#endregion

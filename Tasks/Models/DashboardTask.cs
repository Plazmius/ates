namespace Tasks.Models
{
    public class DashboardTask
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public DashboardTaskAssignee Assignee { get; set; }
    }
}
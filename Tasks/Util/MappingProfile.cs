using System.Threading.Tasks;
using AutoMapper;
using Tasks.Models;
using Tasks.Persistence;

namespace Tasks.Util
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<PopugTask, DashboardTask>();
            CreateMap<Popug, DashboardTaskAssignee>();
        }
    }
}
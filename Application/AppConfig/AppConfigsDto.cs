
using Domain;

namespace Application.AppConfigs
{
    public class AppConfigDto
    {
         public int Id { get; set; }
        public AppConfigType ConfigType { get; set; }

        public string Title { get; set; }
		public int Order { get; set; }							
		public string Det1 { get; set; }
		public string Det2 { get; set; }
		public string Det3 { get; set; }
		public string Det4 { get; set; }
		public string Det5 { get; set; }
        
    }
}
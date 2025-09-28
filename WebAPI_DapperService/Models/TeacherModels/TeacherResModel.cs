namespace WebAPI_DapperService.Models.TeacherModels
{
    public class TeacherResModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public bool DeleteFlag { get; set; }
    }
}
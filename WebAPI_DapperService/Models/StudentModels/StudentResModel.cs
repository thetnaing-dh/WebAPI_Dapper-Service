namespace WebAPI_DapperService.Models.StudentModels
{
    public class StudentResModel
    {
        public int Id { get; set; }
        public string RollNo { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool DeleteFlag { get; set; }
    }
}
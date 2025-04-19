namespace Shared.ErrorModels
{
    public class ValidationsError
    {

        public string Field { get; set; } = default;


        public IEnumerable<string> Errors { get; set; }
    }
}
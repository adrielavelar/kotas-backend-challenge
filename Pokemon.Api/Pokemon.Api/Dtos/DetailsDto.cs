namespace Pokemon.Api.Dtos
{
    public class DetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Base64Sprite { get; set; } = string.Empty;
        public List<string> Evolutions { get; set; } = new();
    }
}

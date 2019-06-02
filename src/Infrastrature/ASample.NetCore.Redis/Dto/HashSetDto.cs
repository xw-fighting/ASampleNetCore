
namespace ASample.NetCore.Redis.Dto
{
    public class HashSetDto
    {
        public HashSetDto(string name,string value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}

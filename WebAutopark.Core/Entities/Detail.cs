using WebAutopark.Core.Entities.Base;

namespace WebAutopark.Core.Entities
{
    public class Detail : Product
    {
        public string Name { get; set; }

        public override string Type => "Detail";
    }
}
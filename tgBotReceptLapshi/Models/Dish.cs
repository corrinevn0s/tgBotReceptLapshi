using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace tgBotReceptLapshi.Models
{
    public record Dish
    {
        public Guid ID { get; } = Guid.NewGuid();
        public string Name { get; init; }
        public string Look {  get; init; }
        public string Recipe { get; init; }
        public string Video { get; init; }
    }
}

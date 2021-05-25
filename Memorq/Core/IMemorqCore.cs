using Memorq.Models;

namespace Memorq.Core
{
    public interface IMemorqCore
    {
        Item UpdateItemStats(Item item, int grade);
    }
}

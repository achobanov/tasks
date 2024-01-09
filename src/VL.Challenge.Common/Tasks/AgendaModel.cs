using VL.Challenge.Domain.Entities;

namespace VL.Challenge.Common.Tasks;

public class AgendaModel : List<List<VLTask>>
{
    public void Remove(int taskId)
    {
        foreach (var group in this)
        {
            var match = group.FirstOrDefault(x => x.Id == taskId);
            if (match != null)
            {
                group.Remove(match);
                if (!group.Any())
                {
                    this.Remove(group);
                }
                break;
            }
        }
    }
}

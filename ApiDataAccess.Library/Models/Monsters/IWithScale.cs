using Newtonsoft.Json;

namespace ApiDataAccess.Library.Models.Monsters
{
    public interface IWithScale
    {
        byte Scale { get; set; }
    }
}
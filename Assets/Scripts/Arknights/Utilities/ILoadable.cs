using System.Collections.Generic;

namespace Arknights
{
    public interface ILoadable
    {
        public void Load(List<GridType> mapDataList, int width, int height);
    }
}
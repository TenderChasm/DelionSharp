using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delion.AssetManagement
{
    //really no more data because FreeType loads a font from the HDD itself
    //So we only need to provide it with a path to the font

    public class TTFontAsset : Asset
    {
        //shortcut for retrieving the full path to this asset
        public string FullPath => Game.AssetManager.FullPath(this);
        public TTFontAsset(DatabaseAssetRecord assetInfo) : base(assetInfo)
        {

        }
    }
}

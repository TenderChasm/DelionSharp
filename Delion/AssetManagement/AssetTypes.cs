using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delion.AssetManagement

{
    //equals to number of Asset children classes
    //IMPORTANT AssetTypes serialization to DB depends on ther integer value,so you should explicitly state underlying number
    public enum AssetTypes
    {
        Texture = 0,
        Audio = 1,
        VoxelModel = 2,
        TTFont = 3,
    }
    
    //the fabric class which correlates given AssetType and return appropriate new asset object
    //Object is obtained via an appropriate delegate using constructor in the private dictionary
    //Delegate accepts DatabaseAssetRecord and returns Asset since all Asset constructors accept DatabaseAssetRecord
    //Don't forget the dictionary is filled up manually!!!
    public static class AssetFabric
    {
        static private Dictionary<AssetTypes, Func<DatabaseAssetRecord,Asset>> assetConstructor;
        static AssetFabric()
        {
            assetConstructor = new Dictionary<AssetTypes, Func<DatabaseAssetRecord, Asset>>();

            assetConstructor.Add(AssetTypes.Texture, (DatabaseAssetRecord r) => new TextureAsset(r));
            assetConstructor.Add(AssetTypes.VoxelModel, (DatabaseAssetRecord r) => new VoxelModelAsset(r));
            assetConstructor.Add(AssetTypes.TTFont, (DatabaseAssetRecord r) => new TTFontAsset(r));
        }

        public static Asset ConstructAsset(DatabaseAssetRecord record)
        {
            return assetConstructor[record.Type](record);
        }
    }

    //the class of the usable asset
    /*Assets can be tracked by AssetManager and be manual - in the first case they are created 
     * by it and it has records in its register about it
     * In the second case they are not loaded but created in runtime and AssetManager doesn't know about them
     * They must be freed manually and theit aliases are NULL. An example of this type is atlas of symbols
     */
    public abstract class Asset
    {
        public string Alias { get; protected set; }
        public AssetTypes AssetType { get; protected set; }

        //common operations for all assets - initialize name and type and launch the loading entrypoint(Load())
        public Asset(DatabaseAssetRecord assetInfo)
        {
            Alias = assetInfo.Alias;
            AssetType = assetInfo.Type;
        }

        public Asset(string alias, AssetTypes type)
        {
            Alias = alias;
            AssetType = type;
        }

        //Function that is called by AssetManager when it is to free the Asset
        public virtual void FreeForAssetManager()
        {

        }
    }
}

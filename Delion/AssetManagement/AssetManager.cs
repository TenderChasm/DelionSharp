using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Delion.AssetManagement
{
    //provides basic AssetRecord class, a list of which is serialized into JSON and shared with the DBControllerApplication
    public class DatabaseAssetRecord
    {
        public string Alias { get; set; }
        public string Path { get; set; }
        public AssetTypes Type { get; set; }
        //The assetType loader determines how to load an asset depending on a data in Settins string
        public string Data { get; set; }
    }
    public class AssetManager
    {
        public string Folder { get; }

        //it is supposed that DBFile is located in the root folder
        public string ResourseDbFileName { get; }
        
        //contains AssetRecords for all existing files

        //acts like a register of all obtainable by their aliases assets
        //the first value contains a database record associating with an asset
        //the second value contains an appropriate loaded asset(If it had been loaded before)
        private Dictionary<string, (DatabaseAssetRecord DatabaseAssetInfo, Asset LoadedAsset)> register;

        public string FullPath(string localPath)
        {
            return Game.AssetManager.Folder + '\\' + localPath;
        }

        public string FullPath(Asset asset)
        {
            string localPath = register[asset.Alias].DatabaseAssetInfo.Path;
            return FullPath(localPath);
        }

        public AssetManager(string folder, string resourseDbFileName)
        {
            Folder = folder;
            ResourseDbFileName = resourseDbFileName;
            register = new Dictionary<string, (DatabaseAssetRecord DatabaseAssetInfo, Asset LoadedAsset)>();
            try
            {
                LoadDb();
            }
            catch
            {
                throw new FileLoadException("Error during loading the resourse database");
            }
        }

        private void LoadDb()
        {
            FileStream dBfile = new FileStream(ResourseDbFileName, FileMode.Open);
            using (StreamReader reader = new StreamReader(dBfile))
            {
                string rawJson = reader.ReadToEnd();
                 DatabaseAssetRecord[] rawDatabase = JsonSerializer.Deserialize<DatabaseAssetRecord[]>(rawJson);

                foreach (DatabaseAssetRecord record in rawDatabase)
                    register.Add(record.Alias, new (record, null));
            }
        }
        /// <summary>
        /// Just meow. What else can I say?
        /// </summary>
        public Asset Get(string alias)
        {
            bool res = register.TryGetValue(alias, out (DatabaseAssetRecord databaseAssetInfo, Asset LoadedAsset) tuple);
            if (res == false)
                throw new KeyNotFoundException($"{alias} doesn't exist in the DB");

            if (tuple.LoadedAsset != null)
            {
                return tuple.LoadedAsset;
            }
            else
            {
                Asset loadedAsset;
                try
                {
                    loadedAsset = AssetFabric.ConstructAsset(tuple.databaseAssetInfo);
                }
                catch
                {
                    throw new FileLoadException($"{alias} asset can't be loaded");
                }
                register[alias] = new (tuple.databaseAssetInfo, loadedAsset);
                return loadedAsset;
            }
                
        }

    }
}

using Delion.Utilities;
using System.IO;
using System.Text.Json;

namespace Delion.AssetManagement
{
    //voxel model representation
    /*Current VoxelModel additional data structure:
     * 1csv - Format name
     *      (exist one format  - AMA)*/
    public class VoxelModelAsset : Asset
    {
        public struct Cell
        {
            public Vector4b Color;
            public ushort MaterialID;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }

        public Cell[,,] Data { get; private set; }

        public VoxelModelAsset(DatabaseAssetRecord assetInfo) : base(assetInfo)
        {
            ChooseLoaderAndLoad(Game.AssetManager.FullPath(assetInfo.Path), assetInfo.Data);
        }

        //chooses a loader depending on the settings enum value
        public void ChooseLoaderAndLoad(string path, string settings)
        {
            string[] loaderSetting = settings.Split(';');
            switch(loaderSetting[0])
            {
                case "AMA":
                    LoadFromAma(path);
                    break;
            }
        }

        //Current version of AMA format contains:
        //X,Y,Z Int32 numbers at the beginning
        //X,Y,Z sized array of Cells,where Cell is 4 byte color and 2 byte materialID
        private unsafe void LoadFromAma(string path)
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            using (BinaryReader reader = new BinaryReader(stream))
            {
                reader.ReadBytes(3);

                X = reader.ReadInt32();
                Y = reader.ReadInt32();
                Z = reader.ReadInt32();

                Data = new Cell[X, Y, Z];

                for (int k = 0; k < Z; k++)
                {
                    for (int j = 0; j < Y; j++)
                    {
                        for (int i = 0; i < X; i++)
                        {
                            byte[] buff = reader.ReadBytes(sizeof(Vector4b));
                            Data[i, j, k].Color.R = buff[0];
                            Data[i, j, k].Color.G = buff[1];
                            Data[i, j, k].Color.B = buff[2];
                            Data[i, j, k].Color.A = buff[3];

                            Data[i, j, k].MaterialID = reader.ReadUInt16();
                        }
                    }
                }
            }
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;


public static class MapSerializer
{
    [System.Serializable]
    class SerBlock
    {
        public NotVector3 pos;
        public NotVector3 rot;
        public BlockInfo info;
        public float[] Blendshapes;
    }

    [System.Serializable]
    struct NotVector3
    {
        public float x, y, z;

        public static explicit operator Vector3(NotVector3 nv)
        {
            return new Vector3(nv.x, nv.y, nv.z);
        }
        public static explicit operator NotVector3(Vector3 v)
        {
            return new NotVector3() { x = v.x, y = v.y, z = v.z };
        }

    }

    public static void SaveMap(string path)
    {
        var infos = MonoBehaviour.FindObjectsOfType<InfoOnBlock>();
        var blocks = new SerBlock[infos.Length];
        for (int i = 0; i < infos.Length; i++)
        {
            blocks[i] = new SerBlock() { info = infos[i].Info, pos = (NotVector3)infos[i].transform.position, rot = (NotVector3)infos[i].transform.rotation.eulerAngles };
            if (infos[i].Blendshapes != null)
                blocks[i].Blendshapes = infos[i].Blendshapes;
        }

        var bf = new BinaryFormatter();
        if (!Directory.GetParent(path).Exists)
            Directory.CreateDirectory(Directory.GetParent(path).FullName);

        var file = File.Create(path);
        bf.Serialize(file, blocks);
    }

    public static void LoadMap(string path)
    {
        if (File.Exists(path))
        {
            try
            {
                var bf = new BinaryFormatter();
                var file = File.Open(path, FileMode.Open);
                var blocks = (SerBlock[])bf.Deserialize(file);
                file.Close();

                //SceneManager.sceneLoaded += (x, y) => { FinishLoad(x, y, blocks); };
                //SceneManager.LoadScene("levelEditor", LoadSceneMode.Single);

                BlockManipulator.DestroyAll();

                for (int i = 0; i < blocks.Length; i++)
                {
                    TileSpawner.SpawnFull(blocks[i].info, (Vector3)blocks[i].pos, blocks[i].Blendshapes, (Vector3)blocks[i].rot, false);
                }
            }
            catch
            {
                Debug.Log("Uh oh error while loading.");
            }

        }
    }

}

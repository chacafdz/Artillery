using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public abstract class PuntajePersistente : ScriptableObject
{
    public void Guardar(string nombreArchivo = null)
    {
        var bf = new BinaryFormatter();
        var file = File.Create(ObtenerRuta(nombreArchivo));
        var json = JsonUtility.ToJson(this);

        bf.Serialize(file, json);
        file.Close();
    }

    public virtual void Cargar(string nombreArchivo = null)
    {
        if (File.Exists(ObtenerRuta(nombreArchivo)))
        {
            var bf = new BinaryFormatter();
            var archivo = File.Open(ObtenerRuta(nombreArchivo), FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(archivo), this);
            archivo.Close();
        }
    }

    public string ObtenerRuta(string nombreArchivo = null)
    {
        var nombreArchivoCompleto = string.IsNullOrEmpty(nombreArchivo) ? name : nombreArchivo;
        return string.Format("{0}/{1}.ebac", Application.persistentDataPath, nombreArchivoCompleto);
    }
}
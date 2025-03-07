using System.Collections.Generic; 
using UnityEngine; 


public class ObjectPool : MonoBehaviour
{
    public GameObject prefab; 
    public int initialSize = 10; 

    
    private List<GameObject> pool = new List<GameObject>();

    private void Start()
    {
        // Se crean y se agregan al pool tantos objetos como se haya especificado en initialSize
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(prefab); 
            obj.SetActive(false); 
            pool.Add(obj); 
        }
    }

    // Método para obtener un objeto del pool
    public GameObject GetObject()
    {
        // Recorre todos los objetos del pool
        foreach (GameObject obj in pool)
        {
            
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true); 
                return obj; 
            }
        }

        // Si no hay objetos inactivos disponibles, se instancia uno nuevo
        GameObject newObj = Instantiate(prefab);
        pool.Add(newObj); 
        return newObj; 
    }

    // Método para devolver un objeto al pool
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false); // Desactiva el objeto para que se pueda reutilizar
    }
}

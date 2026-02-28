// Dosya: GlobalStateManager.cs
// Konum: Assets/_Project/Scripts/Layer0_Bedrock/State/GlobalStateManager.cs

using System.Collections.Generic;
using UnityEngine;

// YENİ: Projenin tek gerçeklik kaynağı. Hiçbir dış sisteme veya Unity UI objesine bağımlı değildir (Layer 0).
public class GlobalStateManager : MonoBehaviour
{
    public static GlobalStateManager Instance { get; private set; }

    // Tek ve yegane durum deposu (DR Emir 11)
    private Dictionary<string, int> stateFlags = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region Contract 1: Flag Operations

    // YENİ: Bayrak değerini doğrudan atar.
    public void SetFlag(string key, int value)
    {
        stateFlags[key] = value;
    }

    // YENİ: Mevcut bayrak değerine ekleme/çıkarma yapar (örneğin eksi ceza veya artı puan).
    public void ModifyFlag(string key, int delta)
    {
        if (!stateFlags.ContainsKey(key))
        {
            stateFlags[key] = 0;
        }
        stateFlags[key] += delta;
    }

    // YENİ: Bayrak değerini okur. Bulunamazsa MVP Test 1.1 uyarınca hata fırlatmaz, 0 döner.
    public int GetFlag(string key)
    {
        return stateFlags.ContainsKey(key) ? stateFlags[key] : 0;
    }

    // YENİ: Save System için tüm sözlüğü dışa aktarır. Bellek referansı kopması için yeni bir kopya döner.
    public Dictionary<string, int> GetAllFlags()
    {
        return new Dictionary<string, int>(stateFlags);
    }

    // YENİ: Load System için dışarıdan gelen sözlüğü içeri aktarır.
    public void LoadAllFlags(Dictionary<string, int> flags)
    {
        if (flags != null)
        {
            stateFlags = new Dictionary<string, int>(flags);
        }
    }

    #endregion
}
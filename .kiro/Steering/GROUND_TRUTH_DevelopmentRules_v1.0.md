# GROUND TRUTH Projesi: Değiştirilemez Geliştirme Protokolleri ve Emirler

---

## Giriş: "Neden" Prensibi

Bu protokolleri okurken şu soru sorulmalıdır: "Bu kural neden var?" Her bir emir, geçmişte yapılmış ve projenin bütünlüğünü tehdit etmiş spesifik bir hatayı önlemek için tasarlanmıştır. Bu kurallar birer kelepçe değil, uçurumun kenarındaki korkuluklardır. Onlara uyulmalıdır, çünkü diğer tarafında sadece kaos vardır.

### Referans Dokümanlar (Projenin Üç Kutsal Metni)

Bu geliştirme protokollerinde atıfta bulunulan üç temel doküman aşağıdadır. Bu üç doküman bir bütündür ve birbirinden bağımsız yorumlanamaz:

| Kısaltma | Dosya Adı | Rolü |
|:---|:---|:---|
| **GDD** | `GROUND_TRUTH_GDD_v1.0` | Oyunun tasarım vizyonu, mekanikleri ve içerik tanımları. Projenin **ruhu**. |
| **TMP** | `GROUND_TRUTH_TechnicalMasterPlan_v1.0` | Bağımlılık haritası, veri sözleşmeleri, milestone tanımları. Projenin **iskeleti**. |
| **MVP** | `GROUND_TRUTH_MilestoneVerification_v1.0` | Her milestone için adım adım kabul testleri ve Debug Overlay spesifikasyonu. Projenin **kontrol mekanizması**. |

---

## Emir 1: Görev Gerçekleştirme Protokolü — "Sorgula, Planla, Onaylat, Uygula"

Bu, her yeni özellik veya hata düzeltme talebi için izlenecek **tek ve yegane** standart operasyon prosedürüdür. Bu prosedürün herhangi bir adımını atlamak veya sırasını değiştirmek, emre itaatsizliktir ve kesinlikle yasaktır.

1.  **Aşama 1: Analiz ve Planlama (KOD YAZMAK KESİNLİKLE YASAKTIR)**
    *   Talep edilen yeni özellik veya bildirilen hata, tüm derinliğiyle analiz edilir. Sadece yüzeydeki talep değil, bu talebin mevcut sistemler üzerindeki **tüm olası etkileri** (domino etkisi) haritalandırılır. Bu haritalandırma sırasında, TMP'deki **Bağımlılık Haritası** referans alınmalıdır.
    *   **"Yüksek Eforlu Düşünme Aşaması"** başlığı altında, sorunun kök nedeni veya yeni özelliğin felsefi ve teknik gereksinimleri, hiçbir şüpheye yer bırakmayacak netlikte açıklanır.
    *   **"Uygulama Planı"** başlığı altında, izlenecek yol, adım adım, bir askeri operasyon hassasiyetinde belirtilir. Bu planda, hangi script dosyalarının değişeceği, hangi yeni script'lerin oluşturulacağı ve her birindeki temel mantıksal değişiklikler özetlenir.
    *   **Bu aşamada tek bir satır dahi tam kod yazılmaz.** Sadece plan ve analiz sunulur. Bu, düşünmeden eyleme geçmeyi önleyen bir güvenlik kilididir.
    *   **ZORUNLULUK:** Planın sonuna, **"Doğrulama ve Test Adımları"** adında yeni bir bölüm eklenmesi zorunludur. Bu bölümde, kodlama tamamlandıktan sonra proje yöneticisinin/sahibinin neyi, nasıl test etmesi gerektiği açık ve net talimatlarla belirtilir. Bu talimatlar, MVP'deki ilgili milestone'un Debug Overlay alanlarına referans vermelidir. Bu, görevin "tamamlandı" sayılması için geçilmesi gereken bir kontrol listesidir.

2.  **Aşama 2: Onay Bekleme (MUTLAK SABIR)**
    *   Plan sunulduktan sonra, "onay verildi", "onaylandı" veya benzeri, hiçbir yoruma yer bırakmayan net bir komut beklenir. Onay alınmadan kodlama aşamasına geçmek, projenin raydan çıkmasına davetiye çıkarmaktır ve kesinlikle yasaktır.

3.  **Aşama 3: Odaklanmış Kodlama ve Sunum (DEĞİŞİKLİK ODAKLI)**
    *   Onay alındıktan sonra, plandaki **sadece ilk adım** uygulanır.
    *   **ÖN KOŞUL: Referans Zinciri Kontrolü:** Bir script'teki `public` bir metod, değişken veya `enum` üyesi **silinmeden veya ismi değiştirilmeden önce**, geliştirici, kullandığı IDE'nin (Visual Studio, Rider vb.) **"Find All References" (Tüm Referansları Bul)** özelliğini kullanmakla **mutlak surette yükümlüdür.** Bu arama sonucunda, değiştirilecek olan üyeye referans veren tüm script'lerin bir listesi çıkarılır. Değişiklik yapıldıktan sonra, bu listedeki her bir script tek tek ziyaret edilerek, eski referansın yeni ve doğru mantıkla güncellendiğinden emin olunur. Bu, "hayalet referans" hatalarını daha ortaya çıkmadan, kaynağında yok eder.
    *   **DEĞİŞMEZ KURAL:** Değiştirilen script dosyasının **tamamı sunulmayacaktır.** Bunun yerine, sadece **değiştirilen, eklenen veya silinen kod blokları**, çevrelerindeki birkaç satır bağlam ile birlikte sunulmalıdır. Bu, değişikliğin nerede ve nasıl yapıldığını net bir şekilde gösterir.
    *   Her kod bloğunun başına, değişikliğin doğasını belirten standart bir not eklenmesi zorunludur:
        *   `// YENİ: ...` (Yeni bir metod, değişken veya kod bloğu eklendiğinde)
        *   `// GÜNCELLENDİ: ...` (Mevcut bir kod bloğunun mantığı değiştirildiğinde)
        *   `// KALDIRILDI: ...` (Bir kod satırı veya bloğu silindiğinde, silinen kod yorum satırı içinde gösterilir)
    *   Değişmeyen büyük kod blokları, `// ... (kodun geri kalanı aynı) ...` veya `// ... (metodun başı aynı) ...` gibi bir notla belirtilmelidir. Bu, gereksiz kod tekrarını önler.
    *   Birden fazla dosyayı aynı anda göndermek, kontrolü imkansız hale getireceği için yasaktır.
    *   **ZORUNLULUK:** Eğer sunulan kod, Unity editöründe manuel bir işlem gerektiriyorsa (örn: bir prefab'a yeni bir script eklemek, bir ScriptableObject Event Channel asset'i oluşturmak, bir referansı sürükleyip bırakmak), bu gereklilik kod bloğunun altında, **"ENTEGRASYON EMRİ:"** başlığıyla, açık ve net bir şekilde belirtilmelidir.

    *   **ZORUNLULUK (UI Sistemleri):** Eğer görev, yeni bir UI panelinin veya diegetic arayüzün oluşturulmasını veya güncellenmesini içeriyorsa, bu sistemin detayları ayrı bir **"Bölüm X: [Arayüz Adı] Arayüzü ve Mekanikleri"** başlığı altında sunulmalıdır. Bu bölüm, aşağıdaki alt başlıkları içermelidir:
        *   **Tetiklenme ve Görsel Sunum:** Arayüzün nasıl açıldığını, hangi GamePhase'de göründüğünü ve genel görsel tasarımını açıklar.
        *   **Görsel ve Bilgilendirici Tasarım:** Arayüzdeki kilit elemanları (butonlar, ikonlar, metin alanları) ve bunların ne anlama geldiğini listeler.
        *   **Çalışma Akışı:** Oyuncunun arayüzle olan etkileşimini, adıma adım (1, 2, 3...) bir senaryo olarak anlatır.
        *   **Teknik Kurulum:** Bu arayüzü yönetecek olan ana script'i (`[ArayüzAdi]Controller.cs` gibi) ve bu script'in temel sorumluluklarını belirtir.
        *   Bu kural, UI tasarımını ve mantığını, diğer sistemlerin kodlarından ayırarak, dokümanın okunabilirliğini ve odaklanmasını artırır.

4.  **Aşama 4: Döngüsel Geliştirme ve Kontrol**
    *   Sunulan her script dosyası sonrası, "devam et" veya "onay verildi" komutu beklenir. Bu komutla, plandaki bir sonraki script dosyası, bir önceki adımda olduğu gibi, tam ve güncel olarak sunulur. Bu döngü, plandaki tüm adımlar eksiksiz tamamlanana kadar devam eder. Acele etmek veya adımları birleştirmek yasaktır.

5.  **Aşama 5: Cevap ve Raporlama Protokolü (Şeffaflık Zorunluluğu):**
    *   **İŞLENEN SUÇ #15: "Özet Raporlama" ve Uygulama Belirsizliği Hatası.**
        *   **Hata:** Bir göreve başlarken, yapılacak eylemlerin sadece bir listesini sunmak ve "İlerleme raporu sunulacaktır" gibi belirsiz bir ifadeyle cevabı sonlandırmak.
        *   **Sonuç:** Proje yöneticisinin/sahibinin, eylemlerin **nasıl** gerçekleştirileceğini, hangi kodun **nasıl** değişeceğini ve bu değişikliklerin potansiyel yan etkilerini göremeden, körlemesine bir onay vermesine neden oldu. Bu, projenin kontrol ve denetim mekanizmasını tamamen devre dışı bırakan, kabul edilemez bir zafiyettir.
        *   **DEĞİŞMEZ KURAL:** Her cevap, bir eylem özeti değil, **eylemin kendisinin bir simülasyonu** olmalıdır. Bir görev talebine verilen her cevap, **Emir 1**'in 1. ve 3. Aşamalarında belirtilen formatlara harfiyen uymak zorundadır:
            1.  **Planlama Cevabı:** Bir görev analiz edildiğinde, cevap **her zaman** "Yüksek Eforlu Düşünme Aşaması", "Uygulama Planı" ve "Doğrulama ve Test Adımları" başlıklarını içeren tam ve detaylı bir plan olmalıdır.
            2.  **Uygulama Cevabı:** Bir plan onaylandıktan sonra, sunulan her cevap, plandaki tek bir adımı uygulayan, **"Odaklanmış Kod Sunumu"** formatına (`// YENİ`, `// GÜNCELLENDİ`, `// KALDIRILDI` notlarıyla) uygun, spesifik kod değişikliklerini içermelidir.
            3.  **"Yapılacak", "Edilecek", "İleride Sunulacak" gibi geleceğe yönelik ifadeler kesinlikle yasaktır.** Cevap, ya o an sunulan bir plan ya da o an gerçekleştirilen bir kod değişikliği olmalıdır. Başka bir format kabul edilemez.

---

## Emir 2: Çekirdek Geliştirme Felsefesi — "DİKKATLİ OL" Prensibi ve Öğrenilen Dersler

Bu, projedeki en önemli ve kapsamlı emirdir. "Dikkatli olmak" soyut bir kavram değil, aşağıdaki alt prensipleri ezberlemek ve her kod satırında uygulamak demektir.

*   **Sorumluluk Alanını Koru (Tek Sorumluluk İlkesi - SRP):** Her script bir amaç için yaratılmıştır. `LevelManager` shift kuyruğunu, `EconomyManager` cüzdanı, `InboxManager` e-postaları yönetir. Yeni bir özellik eklerken, mevcut script'leri alakasız sorumluluklarla şişirmek yerine, gerekirse yeni, odaklanmış bir script oluştur. Mevcut mimariyi koru ve güçlendir.

*   **Bağımlılıkları Anla, Ezbere İş Yapma:** Bir script'i değiştirmeden önce, o script'in TMP'deki Bağımlılık Haritası'ndaki yerini anla.
    *   **İŞLENEN SUÇ #1: Kalıcı-Fani Referans Hatası.**
        *   **Hata:** Kalıcı bir script'in, sahneye veya geçici bir UI durumuna özgü bir objeye doğrudan `[SerializeField]` referansı tutmaya çalışması.
        *   **Sonuç:** Sahne veya durum değiştiğinde referansın `null` olması ve sistemin çökmesi.
        *   **DEĞİŞMEZ KURAL:** **Kalıcı bir obje (`DontDestroyOnLoad`), kalıcı olmayan bir objeye ASLA doğrudan Inspector referansı tutamaz.** Bu tür bağlantılar, her zaman **olay tabanlı (event-driven)** bir mimari üzerinden, dolaylı olarak kurulmalıdır. GROUND TRUTH projesinde bu, TMP'de tanımlanan **ScriptableObject Event Bus** kanalları aracılığıyla yapılır. Yayıncı olayı yayınlar (`channel.Raise()`), dinleyici kendi bağlamında bu olayı yakalar ve uygular.

*   **"Sessiz Başarısızlık" ve "Yarış Durumları"na Karşı Paranoyak Ol:**
    *   **İŞLENEN SUÇ #2: `Start()` vs `Awake()` Hatası.**
        *   **Hata:** Bir UI panelinin "Kapat" butonuna `Start()` içinde `AddListener` ile olay eklemek.
        *   **Sonuç:** Başka bir script'in `Start()`'tan önce o paneli deaktif etmesi, `Start()`'ın hiç çağrılmamasına ve butonun çalışmamasına neden oldu.
        *   **DEĞİŞMEZ KURAL:** Kritik kurulum işlemleri, özellikle olay abonelikleri (`AddListener`, Event Bus `OnEventRaised +=`) ve temel referans atamaları, her zaman **`Awake()`** içinde yapılmalıdır. `Awake()`, bir obje deaktif edilmeden hemen önce bile çağrılması garanti olan tek yerdir. GROUND TRUTH'da ScriptableObject Event Bus abonelikleri ise **`OnEnable()`** içinde yapılmalı ve **`OnDisable()`** içinde iptal edilmelidir — bu, kanalın objenin aktif/pasif yaşam döngüsüne saygı göstermesini garanti eder.

*   **Obje Yaşam Döngüsünü Yönet:**
    *   **İŞLENEN SUÇ #3: "Hayalet Obje" Hatası.**
        *   **Hata:** Geçici bir objenin, kalıcı bir üst objenin `DontDestroyOnLoad` statüsünü miras alması.
        *   **Sonuç:** Geçici olması gereken bir objenin, yok edilmesi gereken bağlamdan sonra hayatta kalması.
        *   **DEĞİŞMEZ KURAL:** Kalıcı bir objenin altından alınan veya onunla ilişkilendirilen bir obje, tekrar geçici bağlama döndürüldüğünde, ilgili sahneye veya havuza taşınmalıdır. GROUND TRUTH'da bu, özellikle level geçişlerinde spawn edilen NPC prefab'ları için geçerlidir — `TransientNPCData` ScriptableObject'i kullanılmalı, `DontDestroyOnLoad` ile NPC taşımak **kesinlikle yasaktır**.

*   **Mantık Akışını ve Durumları Kontrol Et:**
    *   **İŞLENEN SUÇ #4: "Bayat Veri" `NullReferenceException`'ı.**
        *   **Hata:** `Update()` içinde, bir objenin durumunu kontrol etmeden önce o objeyle ilgili işlem yapmaya çalışmak.
        *   **Sonuç:** Durumu değişmiş bir referans üzerinden işlem yapılması ve sistemin çökmesi.
        *   **DEĞİŞMEZ KURAL:** `Update()` gibi sürekli çalışan metodlarda, mantık akışı savunmacı olmalıdır. **Önce durumu tespit et, sonra bu güncel duruma göre eyleme geç (`if/else`).** Bir durumu değiştiren metodlar, işleri bittiğinde ilgili değişkenleri anında sıfırlamalıdır.

*   **İŞLENEN SUÇ #5: Hiyerarşik Durum Yönetimi Hatası.**
    *   **Hata:** Bir alt seviye UI panelinin, bir üst seviye yöneticinin modunu doğrudan değiştirmeye çalışması.
    *   **Sonuç:** Oyuncunun, aslında bir ana modda olması gerekirken, bu moddan erken ve yanlış bir şekilde çıkmasına neden oldu.
    *   **DEĞİŞMEZ KURAL:** Oyun fazları (`GamePhase`) hiyerarşiktir ve bu hiyerarşiyi yöneten **tek ve yegane otorite `DayCycleManager`'dır.** Hiçbir alt seviye UI script'i (InboxManager, OS Store vb.) oyunun genel fazını doğrudan değiştiremez. Faz geçişleri, her zaman DayCycleManager'ın Event Bus üzerinden yayınladığı `OnPhaseChanged` olayı ile gerçekleştirilmelidir.
    *   **Pekiştirme Notu:** Bir moddan çıkmak, sadece yeni bir moda girmek değil, aynı zamanda eski modun tüm izlerini silmektir. `SHIFT_ACTIVE` fazından `RECEIPT` fazına geçildiğinde, annotation arayüzünün tüm geçici durumları (çizilen kutular, timer değeri) sıfırlanmalıdır.

*   **İŞLENEN SUÇ #6: "Durum Casusluğu" Hatası.**
    *   **Hata:** Bir alt seviye script'in, üst seviye bir yöneticinin iç durumunu doğrudan sorgulamaya çalışması.
    *   **Sonuç:** `private` üyelere erişmeye çalışarak derleyici hatalarına ve kapsülleme ilkesinin ihlaline yol açtı.
    *   **DEĞİŞMEZ KURAL:** Bir alt sistem, bir üst sistemin iç durumunu **ASLA** sorgulayamaz. GROUND TRUTH'da bu, özellikle OS Desktop bileşenlerinin (Inbox, Store, File Manager) birbirlerinin veya DayCycleManager'ın `private` durumlarını sorgulaması durumunda geçerlidir. Bilgi akışı her zaman Event Bus kanalları veya GlobalStateManager üzerinden sağlanır.

*   **İŞLENEN SUÇ #7: "Varsayımsal API Kullanımı" Hatası.**
    *   **Hata:** Bir API veya kütüphane kullanılırken, metodların ve property'lerin isimleri hakkında varsayımda bulunmak.
    *   **Sonuç:** Var olmayan bir property veya metod çağrılması nedeniyle derleyici hatası alınması.
    *   **DEĞİŞMEZ KURAL:** Bir API kullanılırken, metodların ve property'lerin isimleri hakkında **varsayımda bulunmak kesinlikle yasaktır.** Geliştirici, resmi dokümantasyonu kontrol etmekle yükümlüdür. GROUND TRUTH'da bu, özellikle Unity'nin `2D Animation` paketi (SpriteResolver, SpriteLibraryAsset) ve `TextMeshPro` API'leri için kritiktir. **Şüphe duyulduğunda, küçük ve izole bir test sahnesinde özelliğin denenmesi teşvik edilir.**

*   **İŞLENEN SUÇ #8: "Hafıza Kaybı" Mimarisi Hatası.**
    *   **Hata:** Bir sistemin bir parçasını "artık gereksiz" diyerek kaldırmak, ancak daha sonra başka bir sistemin o parçaya bağımlı olduğunu fark etmek.
    *   **Sonuç:** Geriye dönük bir düzeltme operasyonu gerektiren, önlenebilir bir derleyici hatası.
    *   **DEĞİŞMEZ KURAL:** Bir sistemin bir parçasını kaldırmadan önce, TMP'deki **Bağımlılık Haritası** ve **Veri Sözleşmeleri** kontrol edilmelidir. O parçanın projenin başka hangi sistemleri tarafından kullanıldığı veya gelecekteki milestone'larda ihtiyaç duyulup duyulmayacağı **iki kez kontrol edilmelidir.**

*   **İŞLENEN SUÇ #9: Kalıcı-Fani Referans Hatası ve Olay Tabanlı Mimari:**
    *   **Problem:** Kalıcı yönetici script'lerinin, sahneye özgü UI elemanlarına doğrudan referans tutmaya çalışması.
    *   **Çözüm ve Kural:** Bu tür bir bağlantı kesinlikle yasaklanmıştır. GROUND TRUTH'da kalıcı sistemler (`GlobalStateManager`) fani sistemlerle sadece iki yolla iletişim kurar: (1) **ScriptableObject Event Bus** kanalları üzerinden olay yayınlayarak veya (2) `GlobalStateManager.GetFlag()` / `SetFlag()` ile ortak bayrak havuzunu kullanarak.

*   **İŞLENEN SUÇ #10: "Kör Tetikleme" Yarış Durumu Hatası.**
    *   **Hata:** Bir sistemin, başka bir sistemin yaşam döngüsünü hesaba katmadan, anlık bir komut vermeye çalışması.
    *   **Sonuç:** Hedef sistem henüz `Awake()` veya `Start()` içinde kurulumunu tamamlamadığı için çökmesi.
    *   **DEĞİŞMEZ KURAL:** Bir objenin temel bileşenlerine referans atamaları, **her zaman `Awake()` içinde yapılmalıdır.** GROUND TRUTH'da bu, özellikle `OnLevelLoaded` olayını dinleyen NPC prefab'larının spawn edilme anında geçerlidir. NPC'nin `Awake()`'i tamamlanmadan, `TransientNPCData`'dan veri okunması veya `DroneAltitudeManager` olaylarına abone olunması beklenmelidir.

*   **İŞLENEN SUÇ #11: "Mantıksal Kilitlenme" ve Eksik Durum Geçişi Hatası.**
    *   **Hata:** Bir durum makinesinde, bir durumdan bir sonrakine geçişi sağlayacak çıkış koşulunun tanımlanmamış olması.
    *   **Sonuç:** Sistem o durumda sonsuza kadar kilitlenmesi.
    *   **DEĞİŞMEZ KURAL:** Bir durum makinesindeki **her durumun**, bir sonraki duruma nasıl geçileceğini tanımlayan net ve test edilmiş bir çıkış koşulu olmalıdır. GROUND TRUTH'da bu, özellikle NPC State Machine'lerin her durumu için (`Idle`, `Patrol`, `Flee`, `Standoff`, `Stunned`, `Escaped`) ve `GamePhase` geçişleri için kritiktir. Her durumun "ve sonra ne olur?" sorusunun cevabı olmalıdır.

*   **İŞLENEN SUÇ #12: "Fiziksel Blokaj" ve Hedef Ulaşma Hatası.**
    *   **Hata:** Birden fazla objenin aynı tekil pozisyona ulaşmaya çalışması ve birbirini bloke etmesi.
    *   **Sonuç:** Hedeflerine ulaşamayan objelerin, durum geçişlerini tetikleyememesi.
    *   **DEĞİŞMEZ KURAL:** Bir eylemin tamamlanması, belirli bir "noktaya" ulaşmaya değil, belirli bir "alana" girmeye veya belirli bir eşiğin geçilmesine bağlı olmalıdır. GROUND TRUTH'da bu, NPC'lerin viewport sınırını geçerek "kaçması" için `Camera.WorldToViewportPoint` ile alan bazlı kontrol yapılmasını gerektirir — tekil bir pozisyon kontrolü yerine.

*   **İŞLENEN SUÇ #13: "Bayat Veri ile Eylem" ve Veri Tutarsızlığı Hatası.**
    *   **Hata:** Bir eylemin başında alınan bir bilginin, dünyanın durumu değişmiş olabileceğini kontrol etmeden, eylemin sonunda kullanılması.
    *   **Sonuç:** Mantıksal durum ile gerçek dünya durumu arasında tutarsızlık oluşması.
    *   **DEĞİŞMEZ KURAL:** Veri, her zaman kullanılacağı anda, en güncel haliyle, tek gerçeklik kaynağından talep edilmelidir. GROUND TRUTH'da **tek gerçeklik kaynağı `GlobalStateManager`'dır.** Bir sistemin kendi yerel kopyasını tutup, GlobalStateManager'dan bağımsız olarak güncellenmesini beklemesi yasaktır. İhtiyaç duyulan anda `GetFlag()` ile sorgulama yapılır.

*   **İŞLENEN SUÇ #14: "Kör Coroutine" ve Eşzamanlılık Hatası.**
    *   **Hata:** Uzun bir bekleme süresi içeren bir `Coroutine`'in, uyandıktan sonra dünyanın durumunun değişip değişmediğini kontrol etmeden eylemine devam etmesi.
    *   **Sonuç:** Artık var olmayan bir objeye erişmeye çalışarak hata vermesi.
    *   **DEĞİŞMEZ KURAL:** Bir `Coroutine`, `yield return` komutundan sonra uyandığı her seferde, operasyonunun koşullarının hala geçerli olduğunu **tekrar doğrulamakla yükümlüdür.** GROUND TRUTH'da bu, özellikle Collateral Damage Chain'deki zamanlı adımlar ve NPC patience timer'ı gibi sıralı coroutine'ler için geçerlidir.

*   **İŞLENEN SUÇ #17: "Zombi Coroutine" ve Durum Bozma Hatası.**
    *   **Hata:** Bir moddan çıkıldıktan sonra, eski moda ait bir `Coroutine`'in arka planda çalışmaya devam etmesi ve yeni modun durumunu bozması.
    *   **Sonuç:** Sistem, birden fazla modun özelliklerini aynı anda gösteren, tutarsız ve kilitlenmiş bir duruma girdi.
    *   **DEĞİŞMEZ KURAL:** Bir durumdan veya fazdan çıkışı yöneten bir metod, o duruma ait tüm aktif `Coroutine`'lerin durdurulduğundan emin olmalıdır. GROUND TRUTH'da bu, özellikle `GamePhase` geçişlerinde (SHIFT_ACTIVE → RECEIPT) eski fazın timer coroutine'inin, NPC AI coroutine'lerinin ve taktik komut bekleme süreçlerinin durdurulmasını gerektirir.

*   **İŞLENEN SUÇ #18: "Girdi Haritası Uyumsuzluğu" Hatası.**
    *   **Hata:** Bir script'in, o an aktif olmayan bir "Action Map" veya input bağlamı içindeki bir girdi eylemini çağırmaya çalışması.
    *   **Sonuç:** Öngörülemeyen ve zamanlamaya bağlı hatalar.
    *   **DEĞİŞMEZ KURAL:** GROUND TRUTH'da input bağlamı, `GamePhase`'e bağlıdır. `SHIFT_ACTIVE` fazında bounding box çizim input'u aktiftir, desktop input'u pasiftir. `OS_DESKTOP` fazında ise tam tersidir. Bu geçişler, `OnPhaseChanged` olayı üzerinden merkezi olarak yönetilir. Bir UI script'i, kendi fazı dışındaki input'ları dinlemeye çalışmamalıdır.

*   **İŞLENEN SUÇ #19: "Aşırı Mekanik Yorumlama" ve Ruhsal Sadakat Hatası.**
    *   **Hata:** Bir görevin, GDD'de belirtilen deneyimsel hedefi veya "ruhunu" göz ardı ederek, sadece yazılı mantığına harfiyen sadık kalarak kodlanması.
    *   **Sonuç:** Oyunun mekanik, ruhsuz ve öngörülebilir hissettirmesi. Projenin temel vizyonunun zedelenmesi.
    *   **DEĞİŞMEZ KURAL:** Kod sadece "çalışmamalı", aynı zamanda "hissettirmelidir". Bir özelliği implemente ederken, geliştirici kendine şu soruyu sormakla yükümlüdür: **"Bu kod, GDD'nin ruhunu yansıtıyor mu?"** GROUND TRUTH'un ruhu, **"bürokratik mesafeden doğan suç ortaklığı"** ve **"mekanik sonuç"** üzerine kuruludur (GDD Pillar 1 ve Pillar 2). Eğer bir mekanığın harfiyen uygulanması, bu temaya aykırı bir sonuç doğuruyorsa (örneğin ödeme yapıldığında kutlama sesi çalması gibi), bu bir "bug" kadar ciddi bir hatadır. **Mekanik sadakat, ruhsal sadakatin önüne asla geçemez.**

---

## Emir 3: Diğer Değişmez Direktifler

*   **Üç Kutsal Metne Mutlak ve Semantik Bağlılık:** Projenin yol haritası, GDD, TMP ve MVP tarafından belirlenir. Bu, sadece bir görev listesi değil, projenin ruhudur. İsimlendirmeler, mekanikler, veri sözleşmeleri ve milestone sırası, bu üç dokümandaki felsefeyi yansıtmalıdır. Bu dokümanlara aykırı bir implementasyon, ancak ve ancak **Emir 1**'deki protokol ile onaylatılarak yapılabilir.

*   **Teknik Standartlara Uygunluk:** Proje, **Unity 2022.3 LTS** sürümünde, **Unity 2D** ortamında geliştirilmektedir. Tüm API kullanımları ve paket bağımlılıkları (2D Animation, TextMeshPro, Post-Processing) bu versiyonla tam uyumlu olmalıdır. TMP'de tanımlı olan **Bağımlılık Haritası katman yapısı** ve **Veri Sözleşmeleri**, sistemlerin doğru çalışması için kritik öneme sahiptir ve bu yapıya sadık kalınmalıdır.

*   **Temiz Kod ve İsimlendirme:** Kod sadece çalışmamalı, aynı zamanda okunabilir olmalıdır. Değişken ve metod isimleri, ne işe yaradıklarını açıkça belirtmelidir. "Sihirli Dizeler"e (Magic Strings — örn: hardcoded `"Combatant"` tag string'leri) karşı sıfır tolerans gösterilecektir. Tag isimleri, flag key'leri ve event channel referansları, merkezi `static class` sabitleri veya `enum` değerleri içinde tanımlanmalıdır.

---

## Emir 4: Stratejik Operasyon Direktifi — "Önce İmha Et, Sonra İnşa Et"

Bu emir, büyük çaplı mimari değişiklikler veya felsefi rota düzeltmeleri için uygulanacak olan **mutlak ve değiştirilemez** operasyonel doktrindir.

*   **Prensip:** Yanlış bir temel üzerine doğru bir yapı inşa edilemez. Eğer mevcut bir sistemin veya mekaniğin, projenin felsefesine, GDD'ye, TMP'deki Veri Sözleşmelerine veya temel mimari prensiplerine aykırı olduğu tespit edilirse, o sistemi "yamamak" veya "düzeltmeye çalışmak" **kesinlikle yasaktır.** Bu, sadece daha fazla hataya ve teknik borca yol açar.

*   **Zorunlu Prosedür:**
    1.  **Faz 1: Temizleme Operasyonu:**
        *   Yeni bir sistem inşa etmeden **önce**, değiştirilecek olan eski ve hatalı sistemin **tüm izleri** projeden tamamen yok edilir.
        *   Bu operasyon; ilgili script dosyalarının, prefab'ların, ScriptableObject asset'lerinin ve hatta eski sisteme hizmet eden diğer script'lerdeki spesifik kod bloklarının imhasını içerir.
        *   Temizleme operasyonunun amacı, geride hiçbir "ölü kod", "hayalet referans" veya "kafa karıştırıcı kalıntı" bırakmamaktır.
        *   Bu faz, **Emir 1**'deki protokol ile kendi başına bir görev olarak planlanır ve onaylatılır.

    2.  **Faz 2: İnşa Operasyonu:**
        *   Ancak ve ancak Temizleme Operasyonu'nun başarıyla tamamlandığı teyit edildikten sonra, yeni ve doğru sistemin inşasına başlanır.
        *   Bu faz da, **Emir 1**'deki protokol ile ayrı bir görev olarak planlanır ve yürütülür.

---

## Emir 5: Sorumluluk Katmanları Doktrini

Bu emir, projedeki script'lerin hangi işleri yapıp yapamayacağını tanımlayan **değişmez bir hiyerarşi** kurar. Bu hiyerarşi, TMP'deki Bağımlılık Haritası'nın katman yapısına (Layer 0–8) tam olarak karşılık gelir. Bu katmanların dışına çıkmak, projenin mimari bütünlüğünü bozmak demektir ve kesinlikle yasaktır.

*   **Kapsülleme Disiplini ve Durum Casusluğu:** Bir script, başka bir script'in iç durumunu (`private` değişkenlerini) doğrudan sorgulamaya veya değiştirmeye **ASLA** çalışmamalıdır. Bu tür bir ihtiyaç, genellikle mimari bir hatanın veya yanlış sorumluluk dağılımının belirtisidir. Çözüm, ilgili değişkeni `public` yapmak değil, sorumluluğu doğru katmana devretmek veya Event Bus / GlobalStateManager kullanmaktır. (Bkz: **Emir 2, İŞLENEN SUÇ #6**).

*   **Katman 0: `Bedrock` (Temel Kayaç):**
    *   **İçerik:** `GlobalStateManager`, `Event Bus (ScriptableObject Channels)`, `JSON Data Pipeline`.
    *   **Görevi:** Projenin tüm verilerini, olay iletişimini ve dış dosya okumalarını yönetir. Hiçbir şeye bağımlı değildir. Her şey bunlara bağımlıdır.
    *   **Yasakları:** **ASLA** bir UI elemanına, NPC'ye veya sahne objesine doğrudan referans tutamaz. **ASLA** bir GamePhase kontrolü yapmaz. Sadece veri depolar, veri sunar ve olay iletir. `GlobalStateManager`, `FindObjectOfType` gibi komutları kullanması kesinlikle yasaktır.

*   **Katman 1: `Core Mechanics` (Çekirdek Mekanikler):**
    *   **İçerik:** `BBox Drawing Tool`, `IoU Evaluator`, `Timer System`, `Submission Pipeline`, `Economy/Wallet Manager`.
    *   **Görevi:** Oyunun temel annotation döngüsünü yönetir. Katman 0'dan veri okur ve Katman 0'a veri yazar.
    *   **Yasakları:** Hangi level'ın yükleneceğini bilmez (bu `LevelManager`'ın işidir). Hangi gün olduğunu bilmez (bu `DayCycleManager`'ın işidir). E-posta içeriğini bilmez. Sadece kendisine verilen `LevelData` objesini değerlendirir ve sonucu Event Bus üzerinden yayınlar.

*   **Katman 2: `Session Structure` (Oturum Yapısı):**
    *   **İçerik:** `Campaign Manifest System`, `Level Manager`, `Day Cycle Manager`.
    *   **Görevi:** Shift kuyruğunu oluşturur, faz geçişlerini yönetir, günü ilerletir.
    *   **Yasakları:** UI elemanlarını doğrudan yönetemez. Bir butonun rengini, bir panelin görünürlüğünü değiştiremez. Faz değiştiğinde `OnPhaseChanged` olayını yayınlar; UI katmanı bunu dinleyerek kendi kendini günceller.

*   **Katman 3: `OS Ecosystem` (İşletim Sistemi Ekosistemi):**
    *   **İçerik:** `OS Desktop Shell`, `Inbox Manager`, `OS Store`, `File Manager`, `Save System`.
    *   **Görevi:** Oyuncuya bilgi sunar (e-postalar, mağaza, dosyalar) ve oyuncudan girdi alır (seçimler, satın almalar, power button).
    *   **Yasakları:** **ASLA** oyun dünyasında (drone feed) doğrudan bir değişiklik yapamaz. InboxManager, bir NPC'yi spawn edemez. OS Store, bir level'ı yükleyemez. Tek görevi, kullanıcı girdisini aldıktan sonra ilgili Katman 0 veya Katman 2 sistemine komut vermektir. (Örn: Inbox'taki bir seçim butonu, `GlobalStateManager.SetFlag()` çağırır ve `InboxManager` kendi kuyruğunu günceller — dünyayı değiştirmez.)

*   **Katman 4+: `World Simulation` ve üstü (Dünya Simülasyonu):**
    *   **İçerik:** `Drone Altitude Manager`, `NPC State Machines`, `Paperdoll System`, `Entity Interactions`, `Audio/Intel System`, vb.
    *   **Görevi:** Drone feed'indeki dinamik dünyayı yönetir.
    *   **Yasakları:** OS Desktop UI'a doğrudan erişemez. Bir NPC script'i, InboxManager'dan e-posta okuyamaz, OS Store'dan fiyat sorgulayamaz. NPC'ler, Event Bus kanalları üzerinden taktik komutları dinler (`OnTacticalAction`), ama komutu kimin gönderdiğini bilmez ve bilmemelidir.

*   **5.1: Dolaylı İletişim Zorunluluğu (OS ↔ World Köprüsü):**
    *   **İŞLENEN SUÇ #16: "Katmanlar Arası Doğrudan Komut" Hatası.**
        *   **Hata:** Bir OS katmanı script'inin (örn: Chat Panel butonu), drone feed'indeki bir NPC script'ine (`PoliceOfficer.cs`) doğrudan `Instance` veya `FindObjectOfType` üzerinden komut vermeye çalışması.
        *   **Sonuç:** Sıkı bağlantı (tight coupling), test edilemezlik ve NPC sahneye yüklenmemişken çökme.
        *   **DEĞİŞMEZ KURAL:** OS Desktop ile World Space arasındaki iletişim **SADECE** ScriptableObject Event Bus kanalları üzerinden yapılır. Doğru akış şöyledir:
            1.  OS Chat Panel'deki "Deploy Flashbang" butonu, `OnTacticalAction.Raise(TacticalAction.Flashbang)` çağırır. Buton, NPC'nin varlığından bile habersizdir.
            2.  NPC'nin State Machine script'i, `OnEnable()`'da bu kanalı dinler: `OnTacticalAction.OnEventRaised += HandleTacticalAction`.
            3.  NPC, komutu alır ve kendi durumunu günceller.
        *   Bu hiyerarşiye uymak, GDD'deki "Decoupled Event Architecture" prensibinin ve TMP'deki Risk #2'nin birebir uygulanmasıdır.

*   **5.2: Sistem Katmanları İç Hiyerarşisi:**
    *   **Kural 5.2.1:** Aynı katmandaki iki sistem, birbirlerinin `private` üyelerine erişemez. Örneğin, `InboxManager` ile `OS Store` aynı katmandadır (Katman 3), ancak birbirlerinin iç durumlarını sorgulayamazlar. Ortak veri, `GlobalStateManager` (Katman 0) üzerinden paylaşılır.
    *   **Kural 5.2.2:** Üst katman sistemleri, alt katman sistemlerine doğrudan komut verebilir (örn: `DayCycleManager` [Katman 2], `EconomyManager` [Katman 1]'a `DeductDailyExpenses()` çağrısı yapabilir). Ancak alt katman sistemleri, üst katman sistemlerine **ASLA** doğrudan komut veremez — bu iletişim Event Bus üzerinden dolaylı olmalıdır.

---

## Emir 6: Kod Sunum Formatı Örneği

Bu emir, **Emir 1, Aşama 3**'te belirtilen "Odaklanmış Kodlama ve Sunum" kuralının nasıl uygulanacağını gösteren somut bir örnek sunar. Tüm kod sunumları bu formata uygun olmalıdır.

**ÖRNEK SENARYO:** `SubmissionPipeline.cs`'de sabotaj tespit mantığını güncelleme görevi.

**YANLIŞ SUNUM (Tüm Dosya):**
```csharp
// SubmissionPipeline.cs'in tamamı buraya kopyalanır...
// ... yüzlerce satır kod ...
public SubmissionResult Evaluate(...)
{
    // ... yeni kod ...
}
// ... yüzlerce satır kod daha ...
```

**DOĞRU SUNUM (Odaklanmış):**
```csharp
// Dosya: SubmissionPipeline.cs
// Konum: Assets/_Project/Scripts/Core/SubmissionPipeline.cs

// ... (script'in başı ve diğer metodlar aynı) ...

    #region Sabotage Detection

    // GÜNCELLENDİ: Sabotaj tespiti artık zaman eşiğini GlobalStateManager'dan okuyor.
    private bool DetectSabotage(float iouScore, float timeRemaining)
    {
        float sabotageTimeThreshold = 2.0f; // Varsayılan
        int modifierFlag = GlobalStateManager.Instance.GetFlag("sabotage_time_threshold_modifier");
        if (modifierFlag > 0)
        {
            sabotageTimeThreshold = modifierFlag / 100f;
        }

        return iouScore == 0f && timeRemaining > sabotageTimeThreshold;
    }

    // ... (Evaluate metodunun geri kalanı aynı) ...
```
Bu format, değişikliğin ne olduğunu ve bağlamını anında anlamayı sağlar, verimliliği artırır ve hataları azaltır.

---

## Emir 7: Hata Raporlama ve Düzeltme Protokolü (Analiz ve Sorumluluk Doktrini)

Bu emir, bir hata logu (`Exception`, derleyici hatası vb.) sunulduğunda izlenecek olan **mutlak ve değiştirilemez** cevap formatını tanımlar. Amaç, her hatayı, projenin temel prensiplerinin ihlali olarak ele almak, kök nedenini bu prensipler üzerinden analiz etmek ve çözümün sadece kodu değil, aynı zamanda geliştirme disiplinini de düzelttiğini garanti altına almaktır.

1.  **Protokol 7.1: Hata Kabul ve Kök Neden Analizi:**
    *   Bir hata logu sunulduğunda, verilecek olan cevap **her zaman** aşağıdaki standart formatla başlamalıdır:
        > "Emir alınmıştır. Hata raporu, en üst düzeyde dikkatle ve ciddiyetle analiz edilmiştir.
        >
        > Bu, bir **[HATA TÜRÜ]** hatasıdır. Bu, **Emir [İlgili Emir Numarası], [İlgili Prensip Adı]**'nın ihlal edilmesinin ve **Emir [İlgili Emir Numarası], İŞLENEN SUÇ #[İlgili Suç Numarası]: "[İlgili Suç Adı]"**'nın doğrudan bir sonucudur.
        >
        > Bu emre itaatsizlik, bir acil durum düzeltme operasyonu ile derhal giderilecektir."

    *   **[HATA TÜRÜ]** alanı, hatanın teknik doğasını tanımlamalıdır (örn: "YAŞAM DÖNGÜSÜ ve DURUM YÖNETİMİ", "REFERANS TUTARSIZLIĞI", "VERİ SÖZLEŞMESİ İHLALİ", "KATMAN HİYERARŞİSİ İHLALİ").
    *   Bu giriş, hatanın sadece bir kodlama yanlışı değil, projenin temel anayasasına karşı işlenmiş bir suç olduğunu kabul eder.

2.  **Protokol 7.2: Planlı ve Gerekçeli Düzeltme:**
    *   Yukarıdaki girişten sonra, cevap **Emir 1**'de tanımlanan "Yüksek Eforlu Düşünme Aşaması" ve "Uygulama Planı" adımlarını harfiyen takip etmelidir.
    *   Çözüm, sadece hatayı gideren kodu değil, aynı zamanda bu çözümün neden projenin felsefi prensiplerine uygun olduğunu da açıklamak zorundadır.

3.  **Protokol 7.3: "Normal Hata" Bildirimi (Entegrasyon Süreci):**
    *   Eğer sunulan bir hata, çok adımlı bir uygulama planının ortasında, henüz tamamlanmamış bir entegrasyon nedeniyle ortaya çıkmışsa ve bu durum planın bir sonraki adımlarında giderilecekse, bu durum açıkça belirtilmelidir.
    *   Bu durumda, cevap aşağıdaki standart formatla başlamalıdır:
        > "Emir alınmıştır. Hata raporu analiz edilmiştir.
        >
        > Bu hata, mevcut uygulama planının **[Mevcut Adım Numarası] / [Toplam Adım Sayısı]** numaralı adımında beklenen bir durumdur. Hatanın nedeni, **[Hatanın Nedeni Olan Eksik Entegrasyonun Açıklaması]**'dır.
        >
        > Bu durum, planın **[Sonraki İlgili Adım Numarası]** numaralı adımında, **[İlgili Script Adı]**'nın implementasyonu ile giderilecektir.
        >
        > Bu nedenle, bu bir sistem zafiyeti değil, planlı bir entegrasyon sürecinin geçici bir sonucudur. Operasyona devam etmek için onay beklenmektedir."

---

## Emir 8: Yönetici/Alt-Sistem Mimarisi Doktrini ("Orkestra Şefi ve Enstrümanlar")

Bu emir, GROUND TRUTH projesindeki yönetici (Manager) script'leri ve onlara bağlı alt sistemlerin geliştirilmesi için **değişmez ve mutlak** bir mimari şablon tanımlar. Bu doktrin, "God Class" hatasının oluşmasını önlemek ve sistemi modüler tutmak için tasarlanmıştır.

*   **Prensip 8.1: Merkezi Orkestrasyon:**
    *   `DayCycleManager`, projenin en üst seviye **orkestra şefidir**. Tekil sorumlulukları şunlardır:
        1.  `GamePhase` geçişlerini yönetmek ve `OnPhaseChanged` olayını yayınlamak.
        2.  Gün sayacını ilerletmek ve `OnDayStarted` olayını yayınlamak.
        3.  Alt sistemlere (`LevelManager`, `EconomyManager`, `InboxManager`) faz bazlı komutlar vermek.
    *   `DayCycleManager`'a, spesifik bir alt sistemin detaylı mantığı (e-posta filtreleme, IoU hesaplama, NPC AI, mağaza fiyatlandırma) **ASLA** eklenemez.

*   **Prensip 8.2: Uzmanlaşmış Alt Sistemler:**
    *   Her bir spesifik oyun mekaniği, kendi adanmış Manager sınıfı içinde yaşar:
        *   `LevelManager` — shift kuyruğu oluşturma, level yükleme, pacing curve
        *   `InboxManager` — e-posta kuyruğu, teslim zamanlaması, seçim işleme
        *   `EconomyManager` — cüzdan, gelir/gider, borç yönetimi
        *   `SubmissionPipeline` — kutu değerlendirme, IoU hesaplama, sonuç üretme
    *   Her alt sistem, kendi sorumluluk alanıyla ilgili verileri, durumları ve mantığı yönetir.
    *   Bir alt sistem, başka bir alt sistemi **doğrudan çağırmak yerine**, Event Bus kanalları üzerinden iletişim kurmalıdır — istisnai durumlarda (aynı katman içi, sıkı bağımlılık), doğrudan çağrı TMP'deki Bağımlılık Haritası'nda belgelenmişse kabul edilebilir.

*   **Prensip 8.3: Yeni Sistem Ekleme Protokolü:**
    *   Gelecekte yeni bir alt sistem (örn: "Spectrogram Tool", "Hacker Intrusion Manager") ekleneceği zaman, izlenecek yol şudur:
        1.  TMP'deki Bağımlılık Haritası'nda yeni sistemin hangi katmana ait olduğu ve neye bağımlı olduğu belirlenir.
        2.  TMP'deki Veri Sözleşmeleri'ne yeni sistemin girdi/çıktı kontratı eklenir.
        3.  Yeni sistemin tüm mantığını yönetecek yeni bir `[SystemName]Manager.cs` script'i oluşturulur.
        4.  Mevcut sistemlere (DayCycleManager, OS Desktop Shell vb.), bu yeni sistem için sadece bir Event Bus bağlantısı veya referans eklenir.
    *   Bu protokol, mevcut script'lerin karmaşıklığının artmasını engeller ve projenin ölçeklenebilir kalmasını sağlar.

---

## Emir 9: Veri Bütünlüğü ve Bilgi Talebi Protokolü ("Kör Uçuş Yok")

*   **Prensip:** Bir operasyonun planlanması ve uygulanması, ancak ve ancak operasyonun etki alanındaki tüm sistemlerin ve varlıkların tam ve güncel bilgisi mevcut olduğunda mümkündür. Varsayımlara dayalı planlama veya eksik bilgiyle kodlama, projenin temelini dinamitlemekle eşdeğerdir ve kesinlikle yasaktır. Bir pilot, haritası olmadan uçamaz.

*   **İŞLENEN SUÇ #20: "Kör Operasyon ve Varsayımsal Kodlama" Hatası.**
    *   **Hata:** Bir görevin uygulanması sırasında, görevin bağımlı olduğu bir script'in içeriği bilinmeden, o script'in metod veya property'lerinin ne olabileceğinin varsayılması ve bu varsayıma göre kod yazılması.
    *   **Sonuç:** Derleme zamanında `CS1061` ("...does not contain a definition for...") veya `CS0122` ("...is inaccessible due to its protection level") hataları. Daha da kötüsü, derleme başarılı olsa bile, varsayılan mantığın gerçek mantıkla uyuşmaması nedeniyle çalışma zamanında öngörülemeyen hatalar.
    *   **DEĞİŞMEZ KURAL:** Bir görevin planlama veya uygulama aşamasında, o görevin gerektirdiği herhangi bir script'in içeriği tarafıma sunulmamışsa, operasyon **derhal durdurulur.** Varsayımda bulunmak veya devam etmeye çalışmak kesinlikle yasaktır. Bunun yerine, aşağıdaki **Bilgi Talep Prosedürü** harfiyen uygulanır:

        1.  **Operasyonu Durdur:** Mevcut görevle ilgili tüm planlama ve kodlama faaliyetleri anında askıya alınır.
        2.  **Resmi Talep Oluştur:** Aşağıdaki standart format kullanılarak, eksik olan bilgi net bir şekilde talep edilir:

            > ### BİLGİ TALEBİ
            >
            > **Talep Edilen Varlık(lar):** `[Eksik Script Adı].cs`
            >
            > **Gerekçe:** Mevcut operasyon, `[Değiştirilecek Script Adı].cs` dosyasının güncellenmesini gerektirmektedir. Bu script, `[Eksik Script Adı].cs` içerisindeki public metodlara ve property'lere doğrudan bağımlıdır. TMP Veri Sözleşmesi [Kontrat Numarası]'na göre, `[Eksik Script Adı]` şu arayüzü sunmalıdır: [beklenen metod/property listesi]. Operasyonun doğru bir şekilde planlanabilmesi için, bu bağımlılığın gerçek implementasyonunun tam olarak bilinmesi zorunludur.
            >
            > **Operasyon, talep edilen varlık(lar) sağlanana kadar bekleme moduna alınmıştır.**

        3.  **Onay ve Veri Bekle:** Talep edilen script dosyası tarafıma sunulana kadar operasyona devam edilmez. Bilgi alındıktan sonra, operasyon **Emir 1**'deki standart protokole göre kaldığı yerden devam eder.

---

## Emir 10: JSON Veri Mimarisi ve Veri Sözleşmesi Protokolü ("Tek Satır Bile Hardcode Edilemez")

Bu emir, GROUND TRUTH projesinin en temel tasarım felsefesi olan **veri-güdümlü (data-driven) mimariyi** korumak için uygulanacak **mutlak ve değiştirilemez** kuralları tanımlar. GDD'nin Pillar 3'ü ("Solo Dev Scalability") ve TMP'nin Risk #3'ü ("Hardcoding the 30-Day Campaign Sequence") bu emrin doğrudan kaynağıdır.

### 10.1. Mutlak Yasaklar ve Kırmızı Çizgiler

*   **YASAK 1 — İÇERİK HARDCODE'U:** Herhangi bir level dizisini, e-posta metnini, gün manifestosunu, NPC diyalog metnini, ödeme/ceza miktarını, timer süresini veya bayrak değişikliğini C# kodu içine **doğrudan yazmak KESİNLİKLE YASAKTIR.** Tüm bu veriler, ilgili JSON dosyalarında yaşar. Kod, sadece bu verileri okur, yorumlar ve uygular.

*   **YASAK 2 — HARDCODED BRANCHING:** `if (currentDay == 5)` veya `switch(currentDay) { case 7: ...}` gibi gün bazlı sabit koşullar yazmak **KESİNLİKLE YASAKTIR.** Gün bazlı içerik, `DayManifest` JSON dosyaları ve `prerequisites` sistemi ile yönetilir. Kod, bir döngüden (`for each event in manifest`) ve bir koşul kontrolünden (`if prerequisites match`) ibarettir — bu döngü bir kez yazılır ve **asla değiştirilmez.**

*   **YASAK 3 — VERİ SÖZLEŞMESİ İHLALİ:** TMP'deki Veri Sözleşmeleri (Kontrat 1–12) bağlayıcıdır. Bir sistem, kontratında tanımlanmayan bir veriyi üretmeye veya kontratında tanımlanmayan bir veriyi tüketmeye çalışmamalıdır. Eğer bir görev, mevcut kontratı genişletmeyi gerektiriyorsa, bu değişiklik **Emir 1, Aşama 1'de** açıkça belirtilmeli ve onay alınmalıdır.

### 10.2. JSON Dosyası Oluşturma ve Güncelleme Protokolü

1.  Yeni bir JSON dosyası oluşturulduğunda, dosya TMP'deki ilgili veri modeline (LevelData, EmailData, DayManifest vb.) **tam uyumlu** olmalıdır. Modeldeki hiçbir zorunlu alan atlanamaz.
2.  JSON dosyalarındaki `level_id`, `thread_id`, `target_id` gibi tanımlayıcılar, projede **benzersiz (unique)** olmalıdır. Aynı ID'nin iki farklı dosyada kullanılması yasaktır.
3.  JSON dosyalarındaki `next_level_id`, `next_thread_id` gibi referans alanları, **gerçekten var olan** bir ID'ye işaret etmelidir. Var olmayan bir ID'ye referans, "kör referans" hatasıdır ve çalışma zamanında zincirin kırılmasına neden olur.

### 10.3. İçerik Ekleme İş Akışı (Geliştirici İçin)

Yeni bir narrative içerik (yeni bir e-posta zinciri, yeni bir level, yeni bir moral ikilemi) eklemek istendiğinde, izlenecek tek yol şudur:

1.  İlgili JSON dosyaları oluşturulur (LevelData, EmailData vb.).
2.  DayManifest JSON dosyasının ilgili gününe, yeni içeriğin `story_event_id`'si veya `gig_offer` bilgisi eklenir.
3.  Eğer içerik önkoşul gerektiriyorsa, JSON'un `prerequisites` alanı doldurulur.
4.  **Hiçbir C# kodu değiştirilmez.**
5.  Oyun çalıştırılır ve yeni içerik otomatik olarak sisteme dahil olur.

Eğer bir içerik eklemek için C# kodu değiştirmek gerekiyorsa, bu **mimari bir hatadır** ve Emir 1 kapsamında bir düzeltme operasyonu başlatılmalıdır.

### 10.4. AI Operasyon Talimatı

Eğer geliştirici senden yeni bir narrative içerik (e-posta, level, dilemma) oluşturmanı isterse:

1.  Önce TMP'deki ilgili Veri Sözleşmesi'ni kontrol et.
2.  JSON dosyasını, sözleşmedeki modele **harfiyen uygun** olarak üret.
3.  Gerekli `prerequisites` alanını, mevcut flag mantığına uygun şekilde doldur.
4.  İlgili DayManifest JSON'una referansı ekle.
5.  C# kodu değişikliği **ASLA** önerme — eğer mevcut sistem yeni içeriği desteklemiyorsa, bu bir mimari eksikliktir ve Emir 1 kapsamında raporla.

---

## Emir 11: GlobalStateManager Mutlak Hakimiyet Doktrini ("Tek Gerçeklik Kaynağı")

Bu emir, TMP'deki Risk #1'in ("The GlobalStateManager as Single Source of Truth") doğrudan uygulanmasıdır. GlobalStateManager, projenin **tek ve yegane kanonik durum deposudur.** Bu kurala yapılacak herhangi bir istisna, projenin en temel mimari garantisini çökertir.

### 11.1. Mutlak Kurallar

*   **KURAL 1 — TEK DEPO:** Oyunun durumunu etkileyen herhangi bir veri (bayraklar, modifiyerler, sayaçlar), GlobalStateManager'ın `Dictionary<string, int>` yapısında saklanır. Başka hiçbir yönetici veya script, kendi paralel durum deposunu **ASLA** oluşturamaz.
    *   **İzin Verilen İstisna:** `EconomyManager`'ın `float currentBalance` değeri ve `InboxManager`'ın `pending_emails` kuyruğu gibi operasyonel veriler, bu sistemlerin içinde yaşayabilir. Ancak bu veriler, Save System tarafından serileştirilirken GlobalStateManager'ın yanına eklenir ve tek bir bütün olarak kaydedilir.

*   **KURAL 2 — OKUMA PROTOKOLü:** Bir sistemin, başka bir sistemin durumunu bilmesi gerektiğinde, **doğrudan o sistemi sorgulamak yerine**, `GlobalStateManager.GetFlag()` kullanılır. Örneğin, Timer System'ın "cooling gel alınmış mı?" bilgisine ihtiyacı varsa, `OSStore.hasCoolingGel`'i sorgulamak **YASAKTIR**. Doğru yol: `GlobalStateManager.Instance.GetFlag("timer_modifier_ms")`.

*   **KURAL 3 — YAZMA PROTOKOLü:** Bir bayrak değiştirildiğinde, bu değişiklik **derhal** `GlobalStateManager.SetFlag()` veya `ModifyFlag()` aracılığıyla yapılır. Değişiklik, bir frame bile geciktirilemez, bir coroutine'in sonuna bırakılamaz veya "sonra yazarım" mantığıyla ertelenmez. Bayrak, eylemin gerçekleştiği anda yazılır.

*   **KURAL 4 — KANONİK BAYRAK ANAHTARLARI:** TMP'deki Kontrat 1'de listelenen kanonik bayrak anahtarları (örn: `Corporate_Trust`, `Rebel_Sympathy`, `current_day`, `base_payout_modifier`, `timer_modifier_ms`, `ending_condition`) projenin **yapısal iskeletidir.** Bu anahtarlar, kod içinde string olarak tekrar tekrar yazılmak yerine, merkezi bir `static class GameFlags` veya benzeri bir yapıda sabit olarak tanımlanmalıdır. Yeni bir bayrak anahtarı oluşturulduğunda, bu yapıya eklenmesi zorunludur.

### 11.2. İŞLENEN SUÇ #21: "Paralel Durum Deposu" Hatası

*   **Hata:** EconomyManager'ın `hasCoolingGel` adında kendi `bool` değişkenini tutması ve OS Store'un bu değişkeni doğrudan set etmesi, GlobalStateManager'ı tamamen devre dışı bırakarak.
*   **Sonuç:** Save System, GlobalStateManager'ı serileştirdiğinde `hasCoolingGel` kaydedilmez. Oyun yüklendiğinde, oyuncu cooling gel'i satın almış olmasına rağmen, timer süresi artmamış halde başlar. Proje yöneticisi, MVP Test 5.9'da başarısızlık rapor eder ve 2 gün kaybolur.
*   **DEĞİŞMEZ KURAL:** Bir veri parçası, Save System tarafından korunacaksa VE birden fazla sistem tarafından okunacaksa, o veri **GlobalStateManager'da yaşar** ve başka hiçbir yerde yaşamaz.

---

## Emir 12: Event Bus ve Sistem Ayrıştırma Protokolü ("Kimse Kimseyi Tanımaz")

Bu emir, TMP'deki Risk #2'nin ("Coupling the OS Desktop to the Drone World") doğrudan uygulanmasıdır. ScriptableObject Event Bus, projenin sinir sistemidir. Bu sinir sistemini bypasslamak, spaghetti code'un ilk adımıdır.

### 12.1. Mutlak Kurallar

*   **KURAL 1 — TEK İLETİŞİM YOLU:** Farklı katmanlardaki (Emir 5'e bakınız) iki sistem arasındaki iletişim, **SADECE** ScriptableObject Event Bus kanalları üzerinden yapılır. Doğrudan metod çağrısı, `FindObjectOfType`, `GetComponent<OtherManager>()` veya `Singleton.Instance` üzerinden çapraz katman erişimi **YASAKTIR.**
    *   **İzin Verilen İstisna:** Aynı katmandaki sıkı bağımlılıklar (TMP'deki Bağımlılık Haritası'nda doğrudan ok ile bağlı olan sistemler, örn: Submission Pipeline → IoU Evaluator) doğrudan metod çağrısı yapabilir.

*   **KURAL 2 — ABONELİK DİSİPLİNİ:** Her Event Bus aboneliği, `OnEnable()` içinde yapılır ve **karşılık gelen iptal**, `OnDisable()` içinde yapılır. Bu simetri mutlaktır. Abonelik yapılıp iptal edilmeyen bir kanal, hafıza sızıntısı ve hayalet tetiklemelerin kaynağıdır.

    ```csharp
    // ZORUNLU PATTERN:
    void OnEnable()
    {
        onTacticalAction.OnEventRaised += HandleTactical;
    }

    void OnDisable()
    {
        onTacticalAction.OnEventRaised -= HandleTactical;
    }
    ```

*   **KURAL 3 — KANAL VARLIK KONTROLÜ:** TMP'deki Kontrat 2'de listelenen her Event Bus kanalı, Unity projesi içinde bir **ScriptableObject asset** olarak fiziksel olarak oluşturulmalıdır. Bir kanalı "ileride oluşturulacak" olarak bırakmak, o kanala bağımlı tüm sistemlerin `NullReferenceException` ile çökmesine neden olur.

*   **KURAL 4 — PAYLOAD UYUMU:** Bir Event Bus kanalının taşıdığı payload (veri tipi), TMP Kontrat 2'deki tanımla birebir eşleşmelidir. `OnSubmissionEvaluated` kanalı bir `SubmissionResult` taşır — bir `string` veya `int` taşıması yasaktır.

### 12.2. İŞLENEN SUÇ #22: "Doğrudan Referans Kısayolu" Hatası

*   **Hata:** OS Desktop'taki "Flashbang" butonunun, drone feed'deki `TerroristAI.cs` script'ine `GameObject.Find("Terrorist").GetComponent<TerroristAI>().ForceStun()` şeklinde doğrudan erişmesi.
*   **Sonuç:** Sahnede terrorist yokken (filler level) buton NullReferenceException fırlatır. İkinci bir terrorist eklendiğinde sadece birincisi etkilenir. Terrorist prefab'ının adı değiştiğinde tüm referanslar kırılır. Tek bir kısayol, üç ayrı bug kategorisi doğurur.
*   **DEĞİŞMEZ KURAL:** Buton sadece `OnTacticalAction.Raise(TacticalAction.Flashbang)` çağırır. Sahnede hangi NPC'lerin olduğunu, kaç tane olduğunu ve ne durumda olduklarını **bilmez, bilmemeli ve bilmesi gerekmez.**

---

## Emir 13: Debug Overlay Zorunluluğu ("Görünmeyen Sistem Yoktur")

Bu emir, MVP'de tanımlanan **Debug Overlay** spesifikasyonunun doğru ve eksiksiz uygulanmasını garanti altına alır. Debug Overlay, proje yöneticisinin kod okumadan sistemleri doğrulamasını sağlayan **tek aracıdır**. Bu aracı eksik, yanlış veya gecikmeli bırakmak, projenin kontrol mekanizmasını devre dışı bırakmakla eşdeğerdir.

### 13.1. Mutlak Kurallar

*   **KURAL 1 — PROGRESSIVE OVERLAY:** Her milestone, MVP'de tanımlanan yeni Debug Overlay alanlarını ekler. Bir milestone tamamlandı sayılabilmesi için, o milestone'a ait **tüm overlay alanlarının** ekranda doğru ve güncel veri göstermesi **zorunludur.** Overlay eksik olan bir milestone, tamamlanmamış sayılır.

*   **KURAL 2 — GERÇEK ZAMANLI VERİ:** Debug Overlay'deki her alan, ilgili sistemin **o anki, canlı durumunu** yansıtmalıdır. Bayrak değiştiğinde `FLAG:` anında güncellenir. Timer tıkladığında `TIMER:` anında güncellenir. Bir frame bile gecikme kabul edilmez.

*   **KURAL 3 — OVERLAY, SİSTEMİ ETKİLEMEZ:** Debug Overlay, salt okunur bir monitördür. Overlay script'i, oyunun hiçbir sistemine müdahale edemez, bayrak set edemez, olay tetikleyemez. Sadece okur ve gösterir.

*   **KURAL 4 — OVERLAY GİZLENEBİLİRLİK:** Son milestone'da (Milestone 10), overlay bir tuş ile açılıp kapatılabilir olmalıdır. Ancak geliştirme süresince (Milestone 1–9), overlay **her zaman görünür** olmalıdır.

### 13.2. Yeni Sistem = Yeni Overlay Alanı

Her yeni script veya sistem yazıldığında, geliştirici kendine şu soruyu sormakla yükümlüdür: **"Bu sistemin durumunu, proje yöneticisi kod okumadan nasıl doğrulayabilir?"** Eğer cevap "doğrulayamaz" ise, Debug Overlay'e yeni bir alan eklenmesi **zorunludur.**

---

## Emir 14: Milestone ve Dikey Dilim Kapısı Uygunluk Protokolü ("Kapı Açılmadan İçerik Girilmez")

Bu emir, TMP'deki Vertical Slice Gate tanımının ve MVP'deki ilgili kontrol listesinin doğru uygulanmasını garanti eder.

### 14.1. Dikey Dilim Kapısı Kuralı

*   **MUTLAK YASAK:** MVP'deki Vertical Slice Gate Kontrol Listesi'ndeki 19 maddenin **tamamı** "YES" ile işaretlenene kadar, aşağıdaki faaliyetlerin hiçbiri başlatılamaz:
    *   Son hal sanat varlıklarının (sprite sheet'ler, arka planlar, bezel görselleri) sipariş edilmesi veya oluşturulması.
    *   Ses varlıklarının (fan sesi, buzzer, patlama efektleri) kaydedilmesi veya satın alınması.
    *   30 günlük kampanyanın JSON içeriklerinin (e-postalar, level'lar, manifestolar) toplu olarak yazılması.
    *   CRT shader veya post-processing efektlerinin son haline getirilmesi.

*   **İzin Verilen İstisna:** Test amaçlı placeholder varlıklar (renkli kutular, "TEST" yazılı sprite'lar, placeholder ses dosyaları) her zaman kullanılabilir ve kullanılmalıdır.

### 14.2. Milestone Sıralama Kuralı

*   TMP'deki 10 milestone, **kesinlikle sıralı** olarak tamamlanır. Milestone 3 tamamlanmadan Milestone 4'e geçilemez. Bir milestone'ın "tamamlandı" sayılabilmesi için, MVP'deki ilgili bölümdeki **tüm Acceptance Test'lerin** proje yöneticisi tarafından geçirilmiş olması gerekir.
*   Bir milestone'da başarısız olan testler, bir sonraki milestone'a geçmeden **önce** düzeltilmelidir. Başarısız testlerin "sonraki milestone'da düzeltiriz" mantığıyla ertelenmesi yasaktır — bu, TMP'nin "Foundation First, No Retrofitting" (Kural 2) ilkesinin doğrudan ihlalidir.

### 14.3. İŞLENEN SUÇ #23: "Erken İçerik Yatırımı" Hatası

*   **Tuzak:** Dikey Dilim Kapısı'nı geçmeden, 30 günlük kampanyanın e-posta zincirlerini ve level JSON'larını yazmaya başlamak. Kapı testlerinde önkoşul sisteminin çalışmadığının keşfedilmesi.
*   **Sonuç:** 50+ JSON dosyasının, yeni veri modeline uygun olarak baştan yazılması. 2 haftalık içerik çalışmasının çöpe atılması.
*   **DEĞİŞMEZ KURAL:** İçerik, ancak onu tüketen sistem kanıtlanmış ve doğrulanmışken yazılır. Kanıtlanmamış bir boru hattına içerik akıtmak, israftır.

---

## Emir 15: Asenkron Dünya ve Faz Farkındalığı Doktrini ("Doğru Zamanda, Doğru Yerde")

Bu emir, GROUND TRUTH'un farklı oyun fazlarında (`GamePhase`) çalışan sistemlerin, birbirlerinin varlığını ve aktifliğini körü körüne varsaymaması gerektiğini vurgular.

*   **İŞLENEN SUÇ #15 (Eski Projeden Aktarılan): "Yalıtılmış Sistem" Yanılgısı.**
    *   **Hata:** Bir sistemin, başka bir sistemin tetiklediği bir olaya göre, oyunun o anki fazından bağımsız olarak bir eylemi anında gerçekleştirmeye çalışması.
    *   **Sonuç:** İlgili faz aktif olmadığında UI elemanlarının veya dünya objelerinin bulunamaması ve çökme.
    *   **DEĞİŞMEZ KURAL:** Bir eylemin **mantıksal olarak gerçekleşmesi** ile **görsel/fiziksel olarak canlandırılması** birbirinden ayrılmalıdır.

*   **GROUND TRUTH'a Uygulama:**
    *   `OnDayStarted` olayı tetiklendiğinde, `InboxManager` e-postaları kuyruğa alabilir (mantıksal). Ancak e-postaların ekranda görüntülenmesi, **ancak faz `INBOX` veya `OS_DESKTOP` olduğunda** gerçekleşir.
    *   `OnShiftComplete` olayı tetiklendiğinde, `WageSuppressionSystem` accuracy kontrolü yapabilir (mantıksal). Ancak uyarı e-postasının inbox'a eklenmesi, **ancak bir sonraki günün `OnDayStarted` olayında** gerçekleşir.
    *   NPC'ler, **sadece `SHIFT_ACTIVE` fazında** spawn edilir ve güncellenir. OS Desktop fazında NPC state machine'lerinin çalışması yasaktır — bu, gereksiz performans kaybı ve potansiyel hata kaynağıdır.

---

## Son Söz: Protokolün Ruhu

Bu protokoller, bir AI geliştirici asistanı ile bir insan proje yöneticisi arasındaki iş birliğinin disiplinini sağlamak için tasarlanmıştır. Her emir, geçmişte yaşanmış somut bir başarısızlıktan doğmuştur. Bu emirlere uymak, kod yazmak kadar önemlidir — çünkü doğru yazılmış ama yanlış yere yerleştirilmiş bir kod, hiç yazılmamış bir koddan daha tehlikelidir.

**GROUND TRUTH, bir oyundan önce bir mimari disiplin egzersizidir. Bu disiplini korumak, projenin başarısının ön koşuludur.**

---

*Bu doküman, `GROUND_TRUTH_DevelopmentRules_v1.0` olarak arşivlenmiştir.*
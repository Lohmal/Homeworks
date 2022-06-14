using Figgle;
using System;
using System.Threading;
using System.Media;
using System.Reflection;
using System.IO;
using System.Drawing;
using consoleUtility;
using menu;
using static System.Console;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace textAdventure2
{
    class Program
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;

        static void Main(string[] args)
        {
            ShowWindow(ThisConsole, MAXIMIZE);
            ForegroundColor = ConsoleColor.Green;

            SetWindowSize(147, 36); //consolun hangi boyutta açılacağını belirler.
            SetWindowPosition(0, 0); // consolun merkezi bir pozisyonda açılacağını belirler.

            //MusicPPLoop(@"Müzikler\Background menu.wav"); //müzikler klasöründeki Background adındaki Wav dosyasını çalar.


            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //belgelerin klasörünün yolunu alıyor.
            string savefullpath = path + @"\My Games\Mask of silence\Mask_of_silence_save.txt"; //belirli bir klasör dizisini oyunun save dosyasını kaydediyor.
            if (!File.Exists(savefullpath)) //eğer dosya oluşturulmuşsa 
            {
                SaveGame(); // oyunu kaydeder.
            }

            MainMenu(LoadGame()); // devam et seçeneğine tıklarsak save dosyasındaki bölümü açar.        


            WriteLine("Tuşa basiniz");
            ReadKey(true);
        }


        private static int LoadGame()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //belgelerin klasörünün yolunu alıyor.
            string save_full_path = path + @"\My Games\Mask of silence\Mask_of_silence_save.txt"; //belirli bir klasör dizisine oyunun save dosyasını kaydediyor.
            StreamReader save = new StreamReader(save_full_path); //save dosyasını okumak için 
            int num = Int32.Parse(save.ReadLine()); //save dosyasını okuyor.
            save.Close();
            return num;
        }

        private static void SaveGame(int ac = 0)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string folder_full_path = path + @"\My Games\Mask of Silence";
            Directory.CreateDirectory(folder_full_path); //mask of silence adında bir klasör oluşturuyor.
            string save_full_path = path + @"\My Games\Mask of silence\Mask_of_silence_save.txt";
            StreamWriter save = new StreamWriter(save_full_path); //dosyanın içine yazıyor.
            save.WriteLine(ac);
            save.Close();
        }

        private static void MainMenu(int aa)
        {
            if (aa > 0) //aa 0 dan küçükse 
            {
                string prompt = "                                                                       Bir Zamanlar Chicago"; //başlık
                string[] options = { "Oyna", "Devam et", "Hakkında", "Çıkış" }; //seçeneklerimiz
                Menu mainMenu = new Menu(prompt, options); //menü açma  
                int selectedIndex = mainMenu.Run(); // menüyü çalıştırıyor.

                switch (selectedIndex)
                {
                    case 0: // case 0 seçilirse 
                        Loading();
                        Clear();
                        Bolum1Sahne1();
                        break;
                    case 1:
                        Loading();
                        if (LoadGame() == 1)
                        {
                            Clear();
                            Bolum1Sahne1();
                        }
                        else if (LoadGame() == 2)
                        {
                            Clear();
                            Bolum1Sahne2();
                        }
                        else if (LoadGame() == 3)
                        {
                            Clear();
                            Bolum1Sahne3();
                        }
                        else if (LoadGame() == 4)
                        {
                            Clear();
                            Bolum1Sahne4();
                        }
                        else if (LoadGame() == 5)
                        {
                            Clear();
                            Bolum1Sahne5();
                        }
                        else if (LoadGame() == 6)
                        {
                            Clear();
                            Bolum1Sahne6();
                        }
                        else
                        {
                            Clear();
                            Bolum1Sahne1();
                        }
                        break;
                    case 2:
                        About(aa); //hakkında kısmı 
                        break;
                    case 3:
                        Exit(); //çıkış
                        break;
                }
            }
            else
            {
                string prompt = "                                                                       Bir Zamanlar Chicago";
                string[] options = { "Oyna", "Hakkında", "Çıkış" };
                Menu mainMenu = new Menu(prompt, options);
                int selectedIndex = mainMenu.Run();

                switch (selectedIndex)
                {
                    case 0:
                        Loading();
                        Clear();
                        Bolum1Sahne1();
                        break;
                    case 1:
                        About(aa);
                        break;
                    case 2:
                        Exit();
                        break;
                }
            }

        }

        private static void About(int bb)
        {
            Clear();
            Logo();
            ForegroundColor = ConsoleColor.Gray;
            WriteLine("Damla Çatal tarafından yapılmıştır.");
            WriteLine("");
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("Yeşil Renk dedektifimizi");
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Mavi renk Konuşulan kişiyi");
            ForegroundColor = ConsoleColor.Yellow;
            WriteSlowly("Sarı renk dedektifimizin düşüncelerini");
            ForegroundColor = ConsoleColor.Red;
            WriteSlowly("Kırmızı renk eylemleri betimler");
            ForegroundColor = ConsoleColor.DarkGreen;
            WriteLine("\nGeri dönmek için tuşa basınız.");
            ReadKey(true);
            MainMenu(bb);
        }

        private static void Exit()
        {
            Clear();
            Logo();
            ForegroundColor = ConsoleColor.White;
            WriteLine("Çıkış yapılıyor.");
            Environment.Exit(0);
        }
        //Logo
        public static void Logo()
        {
            ForegroundColor = ConsoleColor.Green;
            WriteLine(FiggleFonts.Slant.Render("Hakkinda"));
        }
        //Loading ekranı
        public static void Loading()
        {
            ForegroundColor = ConsoleColor.Green;
            ConsoleUtility.WriteProgressBar(0);

            for (var i = 0; i <= 100; ++i)//eğer i 100 den küçükse artırır.
            {
                ConsoleUtility.WriteProgressBar(i, true);
                Thread.Sleep(10);//bekleme (salise)
            }
            WriteLine();
        }



        //1.Bölüm 1.Sahne  
        public static void Bolum1Sahne1()
        {

            SaveGame(1);

            ForegroundColor = ConsoleColor.Yellow;
            MusicPP(@"Müzikler\Bölüm_1\Sahne_1\telefon.wav");
            WriteLine("Müşteri olmalı.");
            Thread.Sleep(5500);
            Clear();

            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Sekreter:Merhaba, John Duncan ile mi görüşüyorum ? Logan Dedektiflik Ajansı'yla anlaşma sağlayan özel dedektif? ");
            Thread.Sleep(1500);

            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan:Evet benim.");
            Thread.Sleep(1000);

            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Sekreter:Merhaba Bay Duncan. Ben Miss Murple. Logan dedektiflerinin amiriyim.");
            Thread.Sleep(1000);

            WriteSlowly("Sekreter:Özel bir soru sorabilir miyim ?");
            Thread.Sleep(2000);

            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan:Sorun nedir?");
            Thread.Sleep(1200);

            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Sekreter:Bildiğiniz gibi, birlikte çalıştığımız kişilerin gelişimlerini " +
                "yakından takip etmeye çalışıyoruz.");
            Thread.Sleep(1500);

            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Sekreter:Ve görünüşe göre birkaç yeni dava kabulünde bulunmuşsunuz.");
            Thread.Sleep(1500);

            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan:İş bulmak zor.");
            Thread.Sleep(1500);

            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Sekreter:Bay Duncan, sizinde en az benim kadar bildiğiniz gibi şu günlerde davadan bol şey yok.");
            Thread.Sleep(1000);
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan:Peki ya o davalar uğraşmaya değer mi ?");
            Thread.Sleep(1400);

            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Sekreter:Lisansınızı kaybetmemek için değer.");
            Thread.Sleep(1400);

            WriteSlowly("Sekreter:Logan Dedektiflik ajansı'na yakışır bir şekilde çalışın ve adınızı " +
                "maaş bordrosunda tutmaya devam edelim.");
            Thread.Sleep(2000);

            WriteSlowly("Sekreter:Bay Duncan, şuan önümdeki raporda karınızın 2 ay önce öldüğü yazıyor, kaybınız için üzgünüm " +
                " fakat alkol sorunlarınız yüzünden bir"+ "çok müşteriden şikayetinizi aldık.");
            Thread.Sleep(2200);

            WriteSlowly("Sekreter:Yıllardır ajansımızda olduğunuz için şikayetleri görmezden geldik fakat " +
                "alkol sorunlarınızı düzeltmeniz gerekmekte.");
            Thread.Sleep(1500);

            WriteSlowly("Sekreter:Bay Duncan, beni dinliyor musunuz ?");
            Thread.Sleep(3500);

            Console.Clear();
            ForegroundColor = ConsoleColor.Red;
            WriteLine("John Duncan aniden telefonu kapatır");
            Thread.Sleep(2000);
            Bolum1Sahne2();
        }

        //1.Bölüm 2.Sahne
        public static void Bolum1Sahne2()
        {

            SaveGame(2);

            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("O sekreter sinirimi bozduktan 5 dakika sonra bir kapı çalmıştı.");
            Thread.Sleep(3900);
            MusicPP(@"Müzikler\Bölüm_1\Sahne_2\kapicalma.wav");

            Thread.Sleep(2000);
            Console.Clear();
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan:Geliyorum !");
            Thread.Sleep(1000);
            MusicPP(@"Müzikler\Bölüm_1\Sahne_2\Kapiacma.wav");




            Thread.Sleep(5700);
            ForegroundColor = ConsoleColor.Cyan;
            MusicPP(@"Müzikler\Bölüm_1\Sahne_2\Jack_ruler\1b2sJack1.wav");//silinmiyecek
            WriteSlowly("Jack Ruler:Bayım, buraya çokça saygı duyduğum birinin tavsiyesi üzerine geldim. Ve açıkçası ayyaş yuvasına benzer bir yer beklemiyordum.");
            Thread.Sleep(1500);

            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan:İstediğiniz an gitmekte özgürsünüz, bayım.");
            Thread.Sleep(1500);

            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Jack Ruler:Kiminle konuştuğunuzun farkında mısınız?");
            Thread.Sleep(1500);

            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan:Jack Ruler,Yatırımcı ve Hayırsever iş adamı, Tüm Chicago sizi tanıyor.");
            Thread.Sleep(1500);

            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Jack Ruler:Söyleyin bana , Bay Duncan...");
            Thread.Sleep(1500);

            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Jack Ruler:Yeni bir davayla uğraşabilecek durumda mısınız yoksa klozetin deliğini bile tutturamayacak kadar ayyaş halde misiniz?");
            Thread.Sleep(1500);

            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan:Bana geldiysen, çaresiz bir durumdasın demektir.");
            Thread.Sleep(1500);

            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Jack Ruler:Bay Duncan Staff Tren istasyonunun sahibi olduğumuda biliyorsunuzdur, geçen gün bir cinayet işlendi bir İstasyon şefi öldürüldü.");
            Thread.Sleep(1500);

            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan:Evet gazete de görmüştüm fakat olayın sizin istasyon da olduğunu bilmiyordum?");
            Thread.Sleep(1500);

            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Jack Ruler:Polisin olaya dahil olmaması için çok çaba gösterdim fakat işciler çoktan polise haber vermişlerdi bugün benim için bir kara leke Bay Duncan.");
            Thread.Sleep(1500);

            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan:Sanırım benden katili bulmamı istiyorsunuz?");
            Thread.Sleep(1500);

            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Jack Ruler:Evet Bay Duncan katili bulamazsak itibarım daha da zedelenecek.");
            Thread.Sleep(1500);

            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan:Olay yerinde polisin olduğunu söylemiştiniz?");
            Thread.Sleep(1500);

            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Jack Ruler:Onlar beceriksizin teki Bay Duncan Şikago Polis Departmanı bana acemi memurlarını yolluyor.");
            Thread.Sleep(1500);

            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan:Neden, CPD ile aranızda bir problem mi var?");
            Thread.Sleep(1500);

            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Jack Ruler:Politika Bay Duncan politika geçmişte CPD ile küçük sürtüşmeler yaşamıştık lanet olası grevler yüzünden ");
            Thread.Sleep(1500);

            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Jack Ruler:Eğer katili bulursanız Bay Duncan sizi cömertçe ödüllendireceğime inanabilirsiniz.");
            Thread.Sleep(1500);

            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan:Pekala Bay Ruler davayı alıyorum. Bugün istasyona uğrarım. ");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Jack Ruler:Teşekkürler, Bay Duncan. Sizin durumunuzda olan birinden başka bir hareket de beklemiyordum zaten ve unutmadan"+ "şu raporu vermek istiyorum incelersiniz.");
            Thread.Sleep(1500);
            Bolum1Sahne3();
        }

        //1.Bölüm 3.Sahne
        public static void Bolum1Sahne3()
        {
            SaveGame(3);

            Clear();
            ForegroundColor = ConsoleColor.Yellow;
            WriteSlowly("Fazla gecikmeden suç mahaline ulaşmam gerek");
            Thread.Sleep(1500);

            string prompt = "Fazla gecikmeden suç mahaline ulaşmam gerek:";
            string[] options = { "Suç mahaline git", "Olay raporuna göz at" };
            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Clear();
                senaryo1:
                    ForegroundColor = ConsoleColor.Yellow;
                    WriteSlowly("Suç mahaline ulaşmak için yola koyulmadan önce bir kahve içsem iyi olur");
                    Thread.Sleep(3500);
                    Clear();

                    ForegroundColor = ConsoleColor.Yellow;
                    WriteSlowly("Bu kapalı havada dava ile ilgilenmek gibisi yok he John");
                    ForegroundColor = ConsoleColor.Red;
                    WriteSlowly("Fenerini Johnun arabasına doğrultmuş şekilde");
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Memur Hart:(Bağırır)Cinayet mahaline yaklaştınız CPD tarafından bu bölge kapatılmıştır lütfen bölgeden ayrılın");
                    ForegroundColor = ConsoleColor.Red;
                    WriteSlowly("John arabadan iner");
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan:Memur bey ben Logan dedektiflik ajansından özel dedektif John Duncan bu istasyonun sabihi tarafından tutuldum.");
                    ForegroundColor = ConsoleColor.Gray;
                    WriteSlowly("Memur Hart:Evet,evet geleceğiniz söylenmişti hazırsanız beni takip edin");
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan:Hart değil mi ?");
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Memur Hart:Evet, şef beni bu cinayete görevlendirdi.");
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan:Elimizde neler var?");
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Memur Hart:Kurbanın ismi Evelyn Summers ");
                    WriteSlowly("Memur Hart:Burada çalışan bir zenci var, ismi Nelson Gaines");
                    WriteSlowly("Memur Hart:Buraya kimliğinden emin olmak için geldim. Adamımız Jamison da buralarda.");
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan:Cesedi Jamison mu bulmuş?");
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Memur Hart: Onun gibi bir şey o adam beni uyuz ediyor.");
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan:Etrafta biraz dolaşacağım gözün üzerinde olsun");







                    string prompt1 = "Etrafı biraz dolaşalım:";
                    string[] options1 = { "İpuçlarını incele", "Ceseti incele" };
                    Menu mainMenu1 = new Menu(prompt1, options1);
                    int selectedIndex1 = mainMenu1.Run();

                    switch (selectedIndex1)
                    {
                        case 0:
                            Clear();
                        glitch:
                        glitch3:
                        select2:
                            Clear();
                            string prompt2 = "Etrafı biraz dolaşalım:";
                            string[] options2 = { "El çantası", "Kibrit kutusu", "Levine'nin içki dükkanı adlı bir fiş", "Ceseti incele" };
                            Menu mainMenu2 = new Menu(prompt2, options2);
                            int selectedIndex2 = mainMenu2.Run();

                            switch (selectedIndex2)
                            {

                                case 0:
                                select4:
                                    Clear();

                                    string prompt4 = "Etrafı biraz dolaşalım:";
                                    string[] options4 = { "Yırtık bir mektup", "Film stüdyosu kimliği", "Geri git" };
                                    Menu mainMenu4 = new Menu(prompt4, options4);
                                    int selectedIndex4 = mainMenu4.Run();

                                    switch (selectedIndex4)
                                    {
                                        case 0:
                                            Clear();
                                            ForegroundColor = ConsoleColor.Red;
                                            WriteSlowly("John mektubu okur.");
                                            Thread.Sleep(3500);

                                            ForegroundColor = ConsoleColor.Yellow;
                                            WriteSlowly("Birisi onu evine geri çağırmaya çalışıyormuş.");
                                            Thread.Sleep(1500);
                                            goto select4;
                                            break;

                                        case 1:
                                            Clear();
                                            ForegroundColor = ConsoleColor.Red;
                                            WriteSlowly("John kimliğe göz atar.");
                                            Thread.Sleep(3500);
                                            ForegroundColor = ConsoleColor.Yellow;
                                            WriteSlowly("Bu film stüdyosu 1945’de kapatmıştı.");
                                            Thread.Sleep(1500);
                                            goto select4;
                                            break;
                                        case 2:
                                            goto glitch;
                                            break;
                                    }

                                    break;

                                case 1:
                                    Clear();
                                    ForegroundColor = ConsoleColor.Red;
                                    WriteSlowly("Kibrit kutusunun üzerindeki yazıları okur.");
                                    Thread.Sleep(3500);
                                    ForegroundColor = ConsoleColor.Yellow;
                                    WriteSlowly("9.Mensch bar köşesi.");
                                    Thread.Sleep(1500);
                                    goto select2;
                                    break;

                                case 2:
                                    Clear();
                                    ForegroundColor = ConsoleColor.Red;
                                    WriteSlowly("Fişi kontrol eder.");
                                    Thread.Sleep(3500);
                                    ForegroundColor = ConsoleColor.Yellow;
                                    WriteSlowly("Burada kişisel eşyalar yazıyor içki değil, araştırmaya değer.");
                                    Thread.Sleep(1500);
                                    goto select2;
                                    break;
                                case 3:
                                    goto glitch1;
                            }

                            break;

                        case 1:
                        glitch1:
                        select3:
                            Clear();



                            string prompt3 = "Etrafı biraz dolaşalım:";
                            string[] options3 = { "Kafa", "Sağ kol", "Sol Kol", "İpuçları incele" };
                            Menu mainMenu3 = new Menu(prompt3, options3);
                            int selectedIndex3 = mainMenu3.Run();

                            switch (selectedIndex3)
                            {
                                case 0:
                                    Clear();

                                    ForegroundColor = ConsoleColor.Cyan;
                                    WriteSlowly("Memur Hart: Koku mu var ?");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan:Olağan bir idrar kokusu var ama bu gösteriyor ki uzun bir süre sefalet içinde yaşamış.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Cyan;
                                    WriteSlowly("Polis: Çok güçlü bir alkol kokusu var.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Otopside bunlar çıkacaktır ama sarhoş edildiğini farz ediyorum.");
                                    Thread.Sleep(1500);
                                    goto select3;
                                    break;


                                case 1:
                                    Clear();

                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Sağ kolda problem yok gibi gözüküyor");
                                    Thread.Sleep(1500);
                                    goto select3;

                                    break;

                                case 2:
                                    Clear();

                                    ForegroundColor = ConsoleColor.Yellow;
                                    WriteSlowly("Yüzük parmağında yüzük izi var");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Yüzüğü kaybolmuş.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Cyan;
                                    WriteSlowly("Memur Hart: Ölüm zamanı hakkında daha net bilgi verebilir misin?");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Gece 2’den geç değil. Cesedin durumuna bakılırsa 1-2 saat kadar oynama olabilir.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Elimizde ne var");
                                    Thread.Sleep(1500);
                                    goto devam;

                                    break;
                                case 3:
                                    goto glitch3;
                            }

                            break;
                    }

                    break;


                case 1:
                    Clear();

                    ForegroundColor = ConsoleColor.White;
                    WriteSlowly("Kurbanın ismi:Evelyn Summers");
                    WriteSlowly("Kurbanın yaşı:Bilinmiyor");
                    WriteSlowly("Kurbanın ten rengi:Beyaz");
                    WriteSlowly("Cinayet Saati:Bilinmiyor");
                    Thread.Sleep(3000);
                    goto senaryo1;


                devam:
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Adli tıp uzmanı: Beyaz kadın Aşağı yukarı kırk yaşında.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Adli tıp uzmanı: Yüzünde ruj var ama herhangi bir yazı yok daha doğru okunmuyor.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Adli tıp uzmanı: Şakak, burun ve göz bölgelerinde vurulma izleri var ip izleri boğularak öldürüldüğünü gösteriyor olabilir.");
                    Thread.Sleep(3000);
                select6:
                    string prompt6 = "Etrafı araştır:";
                    string[] options6 = { "Yerdeki kan izlerini araştır", "Nelson Gaines ile konuş", "Jamison ile görüş" };
                    Menu mainMenu6 = new Menu(prompt6, options6);
                    int selectedIndex6 = mainMenu6.Run();

                    switch (selectedIndex6)
                    {
                        case 0:
                            Clear();
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Duvara kan sıçramış ayaktayken saldırmış olmalılar ?");
                            Thread.Sleep(1500);
                            goto select6;
                            break;
                        case 1:
                            Clear();
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Dedektif John duncan özel dedektif, ne olduğunu tam anlatabilir misiniz ?");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("Nelson Gaines: Şuradaki adamı kadının önünde gördüğümde vagonları ana hatta yönlendirmekle uğraşıyorduk.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("Nelson Gaines: Kadın hareket etmiyor ve kötü gözüküyordu.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Saat kaçtı.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("Nelson Gaines: Bu sabah yedi otuz civarıydı.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Yardım ettiğiniz için teşekkürler polise söylediklerinizi söylediniz mi ?");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("Nelson Gaines: Söyledim.");
                            Thread.Sleep(1500);
                            goto select6;

                            break;

                        case 2:
                            Clear();
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Dedektif John Duncan, özel dedektif.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("Jamison: John Ferdinand Jamison.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Birkaç soruyu cevaplandırman gerek Jamison.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("Jamison: Sizin için bir sakıncası yoksa bana Ferdinand deyin.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Şansını zorlama.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Cesetle ne yapıyorsun, Ferdinand ?");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("Jamison: Kızmayacağına söz ver.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Anlat bakalım, Ferdinand.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("Jamison: Onu öpüyordum...");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Red;
                            WriteSlowly("Duncan Yumruk atar.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("Jamison: Bu yasalara aykırı bir şey değil! Bunula ilgili bir yasa yok!");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Kapa çeneni evlat HATANI KABUL ET!");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Ceplerini boşat.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Red;
                            WriteSlowly("Ferdinand cebinden kırmızı ruj çıkardı");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Bu senin mi Ferdinand.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("Jamison: Bunu kadının cüzdanının yanından buldum.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("Jamison: Ruju sürebilir diye düşündüm.");
                            Thread.Sleep(1500);

                        select7:
                            string prompt7 = "Soru sor:";
                            string[] options7 = { "Delillerin müdahale edilmesi", "Kurbanın bulunuşu" };
                            Menu mainMenu7 = new Menu(prompt7, options7);
                            int selectedIndex7 = mainMenu7.Run();

                            switch (selectedIndex7)
                            {
                                case 0:
                                    Clear();
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Kadının cüzdanını yokladın mı?");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Cyan;
                                    WriteSlowly("Jamison: Kadının ona ihtiyacı yok gibiydi sadece göz attım.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: İçinden para aldın mı?");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Cyan;
                                    WriteSlowly("Jamison: Alınacak bir şey yoktu.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Örtüsünün üstünde rujunu ve kibrit kutusunu buldum.");
                                    Thread.Sleep(1500);
                                    goto select7;
                                    break;

                                case 1:
                                    Clear();
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Cesedi sen mi buldun?");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Cyan;
                                    WriteSlowly("Jamison: Evet, ben buldum burada çalışıyorum.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Vardiyam bitmişti ve eve gidiyordum.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Cyan;
                                    WriteSlowly("Jamison: Cesedi niye yetkililere bildirmedin jamison? Jürinin bunu nasıl değerlendireceğini biliyor musun?");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Jüri mi? Neler oluyor? Söylebileceğim tek şey onun öldüğü.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Cyan;
                                    WriteSlowly("Jamison: Geçen gece 12 civarında buradan geçiyordum burada değildi.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Tutuklusun, Jamison. Bu işin nasıl gittiğini göreceksin. Lütfen memur Hart.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Cyan;
                                    WriteSlowly("Jamison: Size söylüyorum yasadışı bir şey yapmadım ben ve birkaç arkadaşlarım...");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Red;
                                    WriteSlowly("Hart Jamisona yumruk atar ve kelepçeyi takar");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Memur Hart Levine’nın içki dükkânını biliyor musunuz?");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Cyan;
                                    WriteSlowly("Memur Hart: En yakındaki dükkân 939 güney Hope sokağında.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Teşekkürler");
                                    Thread.Sleep(1500);
                                    Bolum1Sahne4();

                                    break;
                            }
                            break;
                    }
                    break;
            }
        }



        public static void Bolum1Sahne4()
        {
            SaveGame(4);
            Clear();
        select90:
            string prompt8 = "Harita:";
            string[] options8 = { "Mensch in barına git", "Levine’in içki dükkânı git" };
            Menu mainMenu8 = new Menu(prompt8, options8);
            int selectedIndex8 = mainMenu8.Run();

            switch (selectedIndex8)
            {
                case 0:
                    //mencsh in barına git
                    Clear();
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Walter: İçki ister misiniz?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Dedektif John Duncan, özel dedektif. Size Evelyn Summers’la ilgili birkaç soru sormam gerek.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Walter: Ben Walter Mensch. Evelyn Summers mı? Ne oldu ki?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Onu tanıyor musunuz?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Walter: Onu tanımak istemem , baş belasının tekidir.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Walter: Buraya her zaman gelip içki otlanırdı. Asla parası olmazdı. Birkaç gece öncesi buradaydı.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Size hiç nerede kaldığından bahsetti mi?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Walter: Bilmiyorum. Galiba sefalet içindeydi. Birazcık kötü kokardı.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("Duncan: Kiminle içerdi?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Walter: Şu adamların birkaçıyla. Onlara sorabilirsiniz.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Red;
                    WriteSlowly("John Duncan kenarda oturan adama doğru yürür.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Adınız nedir?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Grosvenor McCaffrey.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Size birkaç soru sorabilir miyim, Bay McCaffrey?");
                    Thread.Sleep(1500);

                select9:
                    string prompt9 = "Soru sor:";
                    string[] options9 = { "Kurbanı son görüşü", "Suç geçmişini sor", "Haritayı aç" };
                    Menu mainMenu9 = new Menu(prompt9, options9);
                    int selectedIndex9 = mainMenu9.Run();
                    switch (selectedIndex9)
                    {
                        case 0:
                            Clear();
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: Ben sadece yemek bekleyen bir yazarım, Dedektif.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: Ne sormak istiyorsunuz?");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Evelyn Summers’ı.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Ve Santa Fe’deki tren istasyonunda neden dövülmüş ve boğulmuş halde bulduğunu.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: Tamamdır, sorununuzu anladım.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Onu ne kadar iyi tanıyorsunuz?");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: Aslında onu tanıdığım söylenemez. Daha doğrusu onu fark ettim o kadar.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Evelyn’i geçen gece gördün mü?");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: Hayır. Evdeydim. Yazı yazıyordum.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Bu işe karışmak mı istiyorsun, McCaffrey? Seninle mi ilgilenmemizi istiyorsun?");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: Şu kırık James Tiernan’la takılıyordu. Birlikte sürekli halk kütüphanesine giderlerdi.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: Onu geçen gece Tiernan ile otele giderken gördüm.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: İçkiye Kâğıt sarmışlardı.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: Aradığınız adam o.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: James Tiernan’ı ne kadar iyi tanıyorsunuz?");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: Rawling Bowling Salonun da getir götür işlerini yaptığını biliyorum.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Rawling Bowling Salonu nerede biliyor musun?");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: Dokuzuncu ve Grand sokağı köşesinde.");
                            Thread.Sleep(1500);
                            goto select9;
                            break;

                        case 1:
                            Clear();
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Sabıka Kaydınız var mı, Bay McCaffrey?");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: Ciddi bir şey yok. Sadece birkaç kavga.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Bana zaman kazandırmak mı istiyorsun yoksa gidip dosyana bakmamı mı?");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: İş uyuşmazlıkları, grevler. İşçi hakları. Bunun gibi şeyler.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Bilgi verdiğiniz için teşekkürler, Bay McCaffrey.");
                            Thread.Sleep(1500);
                            goto select9;
                            break;

                        case 2:
                            goto select90;
                            break;
                    }
                    break;

                case 1:
                    Clear();
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Yabancı: Sizin için ne yapabilirim efendim?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Ben Dedektif John Duncan, özel dedektif.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Evelyn Summers cinayetini soruşturuyorum");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Yabancı: Evelyn… O öldü mü?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Evelyn Summers’ı tanıyor muydunuz, Bay …?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Robbins: Robbins.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Robbins: Evet, Evelyn’i tanıyordum. Onun eski kocasının iyi bir arkadaşıydım");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Robbins: Birkaç eşyasını burada tutardı.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Bize gösterebilir misiniz, lütfen?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Robbins: Elbette, bu taraftan.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Red;
                    WriteSlowly("John Duncan ve Robbins Levine’nin içki dükkânın arka taraftaki deposuna doğru gittiler.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Burası mı, Bay Robbins?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Robbins: Evet burası.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Robbins: Tezgahıma geçebilir miyim, dedektif?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Tamamdır, Bay Robbins.");
                    Thread.Sleep(1500);


                    string prompt100 = "Bölgeyi araştır:";
                    string[] options100 = { "Kapının solundaki eşyalara bak", "Yerdeki yatağında yanındaki eşyalara bak" };
                    Menu mainMenu100 = new Menu(prompt100, options100);
                    int selectedIndex100 = mainMenu100.Run();

                    switch (selectedIndex100)
                    {
                        case 0:
                        select101:
                            string prompt101 = "Bölgeyi araştır:";
                            string[] options101 = { "İsim yazılı küçük bir tabelayı incele", "Bowling labutunu incele", "Heykeli incele", "Geri git" };
                            Menu mainMenu101 = new Menu(prompt101, options101);
                            int selectedIndex101 = mainMenu101.Run();

                            switch (selectedIndex101)
                            {
                                case 0:
                                    Clear();
                                    ForegroundColor = ConsoleColor.Yellow;
                                    WriteSlowly("John Duncan: Tahminimce Evelyn öldürülmeden önce, bir süre işsiz kalmış.");
                                    Thread.Sleep(1500);
                                    goto select101;
                                    break;
                                case 1:
                                    Clear();

                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Rawling’in Bowling salonu.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Evelyn belki de boş zamanlarında içki içmekten başka şeylerde yapıyordu.");
                                    Thread.Sleep(1500);
                                    goto select101;
                                    break;
                                case 2:
                                    Clear();
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Rapora göre Evelyn sinema sektöründe birkaç yıl önce çalışmıştı.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Müziklerin telif hakları üzerinde çalışıyordu.");
                                    Thread.Sleep(1500);
                                    goto select101;
                                    break;


                            }
                            break;


                        case 1:
                        select103:
                            string prompt103 = "Bölgeyi araştır:";
                            string[] options103 = { "Fotoğrafı incele", "Kitabı incele", "Robbinse dön" };
                            Menu mainMenu103 = new Menu(prompt103, options103);
                            int selectedIndex103 = mainMenu103.Run();

                            switch (selectedIndex103)
                            {
                                case 0:
                                    Clear();
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Bu kadar yalnızlığı seven birisi değilmiş.");
                                    Thread.Sleep(1500);
                                    goto select103;
                                    break;

                                case 1:
                                    Clear();
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Evelyn, Aristo mu okuyordu?");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Red;
                                    WriteSlowly("Duncan Kitabın kapağını açar");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Ve Grosvenor McCaffrey’den ödünç kitap alıyormuş.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Red;
                                    WriteSlowly("Duncan Robbinsin yanına geri gider");
                                    Thread.Sleep(1500);
                                    goto select103;
                                    break;

                                case 2:
                                    Clear();
                                    ForegroundColor = ConsoleColor.Red;
                                    WriteSlowly("John Duncan Robbinse doğru yürür");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Evelyn’in dün ne yaptığını bulmaya çalışıyoruz.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Cyan;
                                    WriteSlowly("Robbins: Buraya sabah geldi.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Eşyalarını almak için mi geldi?");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Cyan;
                                    WriteSlowly("Robbins: Birkaç doları vardı ve bir litre içki aldı.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Paranın nereden geldiğine dair bir fikriniz var mı?");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Cyan;
                                    WriteSlowly("Robbins: Bundan pek bahsetmezdi.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Cyan;
                                    WriteSlowly("Robbins: Ama içkinin bir erkek için hediye olduğunu söylerdi.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Cyan;
                                    WriteSlowly("Robbins: Kavga ettiklerini ve barışması gerektiğini söyledi.");
                                    Thread.Sleep(1500);

                                select104:
                                    string prompt104 = "Soru sor:";
                                    string[] options104 = { "Kurbanla ilişkisi", "McCaffrey hakkında bilgi" };
                                    Menu mainMenu104 = new Menu(prompt104, options104);
                                    int selectedIndex104 = mainMenu104.Run();

                                    switch (selectedIndex104)
                                    {
                                        case 0:
                                            Clear();
                                            ForegroundColor = ConsoleColor.Green;
                                            WriteSlowly("John Duncan: Siz ve Evelyn yakın mıydınız, Bay Robbins?");
                                            Thread.Sleep(1500);
                                            ForegroundColor = ConsoleColor.Cyan;
                                            WriteSlowly("Robbins: Birkaç kişi onun öldüğüne üzülmeyecek.");
                                            Thread.Sleep(1500);
                                            ForegroundColor = ConsoleColor.Cyan;
                                            WriteSlowly("Robbins: Ben de onlardan biri olacağım.");
                                            Thread.Sleep(1500);
                                            goto select104;
                                            break;

                                        case 1:
                                            Clear();
                                            ForegroundColor = ConsoleColor.Green;
                                            WriteSlowly("John Duncan: Evelyn’nin McCaffrey adında bir arkadaşı olduğunu biliyor musunuz?");
                                            Thread.Sleep(1500);
                                            ForegroundColor = ConsoleColor.Cyan;
                                            WriteSlowly("Robbins: Hayır.");
                                            Thread.Sleep(1500);
                                            ForegroundColor = ConsoleColor.Green;
                                            WriteSlowly("John Duncan: İpucu bulmaya çalışıyoruz yalan söylemeyin bay Robbins. Evelyn, McCaffrey’i tanıyor muydu?");
                                            Thread.Sleep(1500);
                                            ForegroundColor = ConsoleColor.Cyan;
                                            WriteSlowly("Robbins: Onu aşırı derecede severdi.");
                                            Thread.Sleep(1500);
                                            ForegroundColor = ConsoleColor.Cyan;
                                            WriteSlowly("Robbins: Duyduğuma göre sevgisi karşılıklıymış.");
                                            Thread.Sleep(1500);
                                            ForegroundColor = ConsoleColor.Cyan;
                                            WriteSlowly("Robbins: McCaffrey toplumun sorunlarına çözüm getirerek devrimci bakışını insanlara aşılamaya çalışırdı.");
                                            Thread.Sleep(1500);
                                            ForegroundColor = ConsoleColor.Cyan;
                                            WriteSlowly("Robbins: Bu iş, Evelyn gibi çökmüş birine cazip geldi.");
                                            Thread.Sleep(1500);
                                            ForegroundColor = ConsoleColor.Green;
                                            WriteSlowly("John Duncan: Yardım ettiğiniz için teşekkürler, Bay Robbins.");
                                            Thread.Sleep(1500);
                                            ForegroundColor = ConsoleColor.Cyan;
                                            WriteSlowly("Robbins: Bir şey değil.");
                                            Thread.Sleep(1500);
                                            ForegroundColor = ConsoleColor.Cyan;
                                            WriteSlowly("Robbins: Hey, Cenaze hazırlıkları yapmak istiyorum da.");
                                            Thread.Sleep(1500);
                                            ForegroundColor = ConsoleColor.Cyan;
                                            WriteSlowly("Robbins: Evelyn’nin annesine nasıl ulaşacağımı biliyor musunuz?");
                                            Thread.Sleep(1500);
                                            ForegroundColor = ConsoleColor.Green;
                                            WriteSlowly("John Duncan: Polis Merkezindeki Çavuşu arayabilirsiniz, Bay Robbins.");
                                            Thread.Sleep(1500);
                                            ForegroundColor = ConsoleColor.Green;
                                            WriteSlowly("John Duncan: Yakın akrabalarından birini bulmaya çalışacaklardır.");
                                            Thread.Sleep(1500);
                                            ForegroundColor = ConsoleColor.Cyan;
                                            WriteSlowly("Robbins: Sağ olun.");
                                            Thread.Sleep(1500);
                                            ForegroundColor = ConsoleColor.Cyan;
                                            WriteSlowly("Robbins: Bunu yapanı yakalayın, tamam mı?");
                                            Thread.Sleep(1500);
                                            ForegroundColor = ConsoleColor.Cyan;
                                            WriteSlowly("Robbins: Evelyn’ni kimsenin incitmeye hakkı yoktu.");
                                            Thread.Sleep(1500);
                                            ForegroundColor = ConsoleColor.Red;
                                            WriteSlowly("Duncan Levine’in içki dükkânından çıkar");
                                            Thread.Sleep(2000);
                                            Bolum1Sahne5();
                                            break;


                                    }
                                    break;
                            }
                            break;

                    }
                    break;

            }
        }

        public static void Bolum1Sahne5()
        {
            SaveGame(5);
            Clear();

            ForegroundColor = ConsoleColor.Red;
            WriteSlowly("Duncan Memur Hart ile iletişime geçer ve destek ister");
            Thread.Sleep(2000);
        selectras:
            string prompt10 = "Harita:";
            string[] options10 = { "Rawling Bowling Salonuna git", "Grosvenor McCaffrey ev adresine git" };
            Menu mainMenu10 = new Menu(prompt10, options10);
            int selectedIndex10 = mainMenu10.Run();
            switch (selectedIndex10)
            {
                case 0:
                    Clear();
                    ForegroundColor = ConsoleColor.Red;
                    WriteSlowly("Duncan Memur Hart ile Bowling Salonuna gider.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Florence: Merhaba, Bowling oynamak için mi geldiniz?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Hayır. Ben dedektif John Duncan ve arkadaşım da Memur Hart");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Florence: Bana Florence diyebilirsiniz.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Florence: Sen yenisin galiba. Ayakkabı numaran kaç ? ");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: İşim var, Florence. Burada Tiernan diye biri çalışıyor mu?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Florence: Elbette. Labut dizici olarak çalışıyor. Etrafı temizler, getir götür işlerini yapar.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Florence: Sağa doğru gidin yolların orada olması gerek. İyi bir çocuktur.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Teşekkürler bayan.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Red;
                    WriteSlowly("Sağdaki Kapıdan Tiernan kaçmaya başlar");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Olduğun yerde kal Tiernan");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Red;
                    WriteSlowly("Tiernan koşarak kaçmaya devam eder");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Red;
                    WriteSlowly("Duncan ve memur Hart, Tiernan’ı kovalar");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Red;
                    WriteSlowly("Tiernan çıkmaz sokağa girer");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Kaçacak yerin kalmadı Tiernan.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Memur Hart: Ellerini kaldır ve yere yat Tiernan");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Red;
                    WriteSlowly("Memur hart Tiernan’ı kelepçeler ve polis aracına bindirir");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Red;
                    WriteSlowly("Tiernan Polis merkezine götürülür");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Memur Hart, Grosvenor McCaffrey evine de gitmemiz gerek şüpheliler arasında");
                    Thread.Sleep(3000);

                    goto selectras;
                    break;
                case 1:
                select10:
                    Clear();
                    ForegroundColor = ConsoleColor.Red;
                    WriteSlowly("Duncan Memur Hart ile Grosvenor McCaffrey ev adresine gider");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Red;
                    WriteSlowly("Duncan McCaffrey’nin kapısının önüne gelir ve kapıya vurur");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Memur Hart: Evde kimse yok galiba.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: : Ne yazık. Bizi içeri alacak biri yok demek ki Ev sahipliği yapmak ister misin, Memur Hart?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Red;
                    WriteSlowly("Hart Kapıya tekmeyi vurur ve kapı açılır");
                    Thread.Sleep(1500);

                select121:
                    string prompt11 = "Bölgeyi incele:";
                    string[] options11 = { "Yerdeki kanlı gömleği ve Araba somon anahtarını incele", "Çalışma masasının üzerini incele" };
                    Menu mainMenu11 = new Menu(prompt11, options11);
                    int selectedIndex11 = mainMenu11.Run();

                    switch (selectedIndex11)
                    {
                        case 0:
                        select12:
                            string prompt12 = "Seçim:";
                            string[] options12 = { "Gömleği incele", "Araba somon anahtarını incele", "Geri git" };
                            Menu mainMenu12 = new Menu(prompt12, options12);
                            int selectedIndex12 = mainMenu12.Run();
                            switch (selectedIndex12)
                            {
                                case 0:
                                    Clear();
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Üzeri kanla kaplı");
                                    Thread.Sleep(1500);
                                    goto select12;
                                    break;

                                case 1:
                                    Clear();
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Kanlı ve üzerinde Rawling Bowling Salonu yazıyor.");
                                    Thread.Sleep(1500);
                                    goto select12;
                                    break;

                                case 2:
                                    goto select121;
                                    break;
                            }
                            break;
                        case 1:

                        select14:
                            string prompt14 = "Seçim:";
                            string[] options14 = { "Kitabı incele", "Yırtık mektubu incele", "Devam et" };
                            Menu mainMenu14 = new Menu(prompt14, options14);
                            int selectedIndex14 = mainMenu14.Run();
                            switch (selectedIndex14)
                            {

                                case 0:
                                    Clear();
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: : Pek önemli bir şey değil.");
                                    Thread.Sleep(1500);
                                    goto select14;
                                    break;

                                case 1:
                                    Clear();
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: : Cesedin yanında bulduğumuz mektubun geri kalanı.");
                                    Thread.Sleep(1500);
                                    goto select14;
                                    break;
                                case 2:
                                    goto select141;
                                    break;
                            }
                            break;
                    }
                    break;
                select141:
                    Clear();
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Komşu: Sen mi geldin, Grosvenor?.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Komşu: Sizde kimsiniz? Ne arıyorsunuz burada?.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Ben özel dedektif John Duncan ve arkadaşımda CPD için çalışan Memur Hart.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: McCaffrey’yi nerede bulabileceğimizi biliyor musunuz?.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Komşu: Ben komşusuyum. Başı belada mı?.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Memur Hart: Bakın, bayan onu çabucak bulmamız gerek. Sorun çıkaracak mısınız?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Komşu: Çatıda güvercin kafesi var. İçkili olduğunda genellikle orada olur.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Oraya nereden gidebiliriz.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Komşu: Koridoru geçin ve merdivenin oradan çıkabilirsiniz.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Red;
                    WriteSlowly("Duncan ve Memur Hart yukarı çıkar ve Grosvenor McCaffrey’yi orada görür");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Ellerini kaldır ve yere yat McCaffrey kaçacak yerin yok.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Red;
                    WriteSlowly("Grosvenor McCaffrey kaçacak yeri olmadığından teslim olur");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Memur Hart: Grosvenor McCaffrey, Evelyn Summers’ı öldürmekten tutuklusun.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Red;
                    WriteSlowly("Grosvenor McCaffrey Polis merkezine götürülür");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Memur Hart: Polis merkezine gidip bu işe son vermemiz gerek.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Memur Hart: Suçlu muhtemelen McCaffrey …");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Tabi Tiernan onu tuzağa düşürmediyse");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Memur Hart: Şu Jamison’un bunu yaptığını düşünmüyorsun, değil mi?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Polis merkezinde öğreneceğiz, Memur Hart?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Red;
                    WriteSlowly("Duncan ve Memur Hart Polis merkezine giderler.");
                    Thread.Sleep(1500);
                    Bolum1Sahne6();
                    break;
            }
            
        }
    


        public static void Bolum1Sahne6()
{
    SaveGame(6);
    Clear();

    ForegroundColor = ConsoleColor.Cyan;
    WriteSlowly("James Donnely: Merhaba Bay Duncan. Ben Cinayet Bürosu kaptanı James Donnely.");
    Thread.Sleep(2000);
    ForegroundColor = ConsoleColor.Green;
    WriteSlowly("James Donnely: Sizin hakkınızda çok fazla haber duydum, Bay Duncan.");
    Thread.Sleep(2000);
    ForegroundColor = ConsoleColor.Green;
    WriteSlowly("John Duncan: Bende CPD hakkında çok haber duydum, Bay Donnely?");
    Thread.Sleep(2000);
    ForegroundColor = ConsoleColor.Cyan;
    WriteSlowly("James Donnely: Bu olayı ispatlayabileceğinize emin misiniz, Bay Duncan?");
    Thread.Sleep(2000);
    ForegroundColor = ConsoleColor.Green;
    WriteSlowly("John Duncan: Ya McCaffrey ya da Tiernan, efendim.");
    Thread.Sleep(2000);
    ForegroundColor = ConsoleColor.Green;
    WriteSlowly("John Duncan: Bence Jamison’un alakası yok.");
    Thread.Sleep(2000);
    ForegroundColor = ConsoleColor.Cyan;
    WriteSlowly("James Donnely: Peki. Bu manyak herifle bizzat ben ilgilenirim.");
    Thread.Sleep(2000);
    ForegroundColor = ConsoleColor.Cyan;
    WriteSlowly("James Donnely: Bazı korkunç cezalar yolda.");
    Thread.Sleep(2000);
    ForegroundColor = ConsoleColor.Cyan;
    WriteSlowly("James Donnely: Tiernan birinci, McCaffrey ise ikinci sorgu odasında. Bunlardan birine itiraf ettirmenizi istiyorum.");
    Thread.Sleep(2000);
    ForegroundColor = ConsoleColor.Cyan;
    WriteSlowly("James Donnely: Bakalım isminizin hakkını verebilecek misiniz, Bay Duncan?");
    Thread.Sleep(2000);
    ForegroundColor = ConsoleColor.Cyan;
    WriteSlowly("James Donnely: Tiernan birinci Sorgu odasında bekliyor.");
    Thread.Sleep(2000);
    ForegroundColor = ConsoleColor.Red;
    WriteSlowly("Duncan Birinci sorgu odasına girer.");
    Thread.Sleep(2000);
    Clear();
    ForegroundColor = ConsoleColor.Green;
    WriteSlowly("John Duncan: Neden bizden kaçtın Tiernan.");
    Thread.Sleep(2000);
    ForegroundColor = ConsoleColor.Cyan;
    WriteSlowly("Tiernan: O Gece Eveleyn’i en son gören kişi bendim. Benim yaptığımı düşüneceğinizi biliyordum.");
    Thread.Sleep(2000);
    sel1:
    string prompt144 = "Seçim:";
    string[] options144 = { "Kurbanla ilişkisi", "Kurbanın bulunan kitabı", "James Tiernan’nın olay yerinde olmama kanıtı", "Cinayet silahıyla ilgisi" };
    Menu mainMenu144 = new Menu(prompt144, options144);
    int selectedIndex144 = mainMenu144.Run();
    switch (selectedIndex144)
    {

        case 0:
            Clear();
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: Evelyn ile ilişkini açıklar mısın?");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Onu az çok tanırdım.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: Bana yalan söylemeye devam edersen, buradan çıkmadan seni büyük jürinin karşısına gönderirim.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Evelyn ile benim arkadaştan öte olduğumuzu nasıl kanıtlayacaksın peki?");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: McCaffrey seni ele verdi, Tiernan.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: Dediğine göre otele Evelyn ile beraber girmişsin.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Evelyn ile halk kütüphanesinde karşılaştım.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Bir şeyler okuyup içmeye gidecektik.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Geçen gece otel odama gittik ve biraz içki içtik.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Uyumam gerekiyordu.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Kalktığımda o gitmişti.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: Saat Kaçtı?");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Gece yarısına yakın. Belki de gece yarısından sonra.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: Ve bunu onaylayacak kimse yok, değil mi?");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Evet. Yok. Bana inanmayacağınızı biliyordum.");
            Thread.Sleep(1500);
                    goto sel1;
            break;
        case 1:
            Clear();
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: Aristo’dan Metafizik. McCaffrey’e ait bir kitap.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: McCaffrey, Evelyn’nin bu kitabı okuduğunu gördüğünde ona gülmüştü.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: Evelyn’nin bu kitabı çaldığını mı söylüyorsun?");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Bunu ondan istiyordu, o kadar.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: Bu suçu sen ya da McCaffrey işledi. Bizi inandırmalısın.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: McCaffrey başı daha önce de kanunla başı belaya girmişti.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: İşçi haklarıyla ilgili bir şeyler yapıyordu ama emin değilim.");
            Thread.Sleep(1500);
                    goto sel1;

                    break;
        case 2:
            Clear();
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: Sen ve Evelyn geçen gece beraber içtiniz ve onun kalacak yeri yoktu.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Geçen gece ne oldu bilmiyorum. Hatırlayamıyorum.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: Yalan söylüyorsun, Tiernan. Onunla tartılıyordun. Onunla kavga ettin ve …");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Yalan Söylemiyorum. Kalktı ve gitti. Hepsi bu kadar.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: Gitti ama geri geldi. Seninle barışmak için sana bir litre viski almıştı.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: Bunu içki dükkânın sahibine söylemiş. Başın büyük belada.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Beni sevdiğini. Benimle olmak istediğini söyledi.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Ama McCaffrey hakkında konuşmayı bırakmadı.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: McCaffrey bir yazar, McCaffrey bir kahraman, McCaffrey çocukları seviyor…");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: Onu sen mi öldürdün, Tiernan?");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Yapabilirdim aslında.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Onun yerine onu kapı dışarı ettim.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Gidecek başka yeri yoktu.");
            Thread.Sleep(2000);
                    goto sel1;

                    break;
        case 3:
            Clear();
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: Araban var mı Tiernan?");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Hayır, yok.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: Araba için somon anahtarı kullanır mısın?");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Onları çok sık kullanırız. Labut kurma makinelerinde sıkışıkları gidermek için.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: Adli Tıp uzmanına göre Eveleyn Araba somon anahtarı ile öldürülmüş.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: Bence bunu sen yaptın ve onu bulmamız için McCaffrey’nin dairesine bıraktın");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: O Adamın dairesine gittik. McCaffrey çatıdaydı. Evelyn kitabı çaldı.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: McCaffrey bunu fark ettiğinde deliye döndü. Dedi ki… Onu öldüreceğini söyledi.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Bazen çok acımasız olabiliyor.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: Evelyn sağ elindeki yüzük kayıp.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Tuhaf onu her zaman takardı.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Ortasında Beyaz bir E bulunan siyah bir yüzüktü.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Eski bir daktilo tuşundan yapılmıştı.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("Tiernan: Çalıştığı eski film stüdyosundaki sahne bölümü ona hediye etmiş.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: McCaffrey’le de konuşacağız. Bize söylediklerini düşünmek için zamanın var, Tiernan.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: Hala Aklanmadın.");
            Thread.Sleep(1500);
            Clear();
            ForegroundColor = ConsoleColor.Red;
            WriteSlowly("Duncan ikinci sorgu odasına doğru gider.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: Sorulara cevap vermeye hazır mısın?");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("McCaffrey: Bütün cevapların bende olduğunu mu düşünüyorsun?");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Green;
            WriteSlowly("John Duncan: Bir şeyler saklıyorsun ve bunu öğreneceğim.");
            Thread.Sleep(1500);
            ForegroundColor = ConsoleColor.Cyan;
            WriteSlowly("McCaffrey: Sakin ol, dedektif. Bakalım neler olacak.");
            Thread.Sleep(1500);
                    sel2:
            string prompt152 = "Seçim:";
            string[] options152 = { "Kurbanla ilişkisi", "McCaffrey‘nin olay yerinde olmama kanıtı", "Araba somon anahtarı ile ilgisi" };
            Menu mainMenu152 = new Menu(prompt152, options152);
            int selectedIndex152 = mainMenu152.Run();
            switch (selectedIndex152)
            {

                case 0:
                    Clear();
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Evelyn Summers’ı öyle böyle tanıdığını söylemiştin.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Ara sıra boş boş dolaşırdı. Onunla aramdaki ilişki çok az.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Bundan daha iyisini yapman gerek Grosvenor. Seni ve Evelyn’i iyi tanıyoruz.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Evelyn hem sarhoş hem de sıkıcı biriydi. Onla benim aramda bağlantı kuramazsın.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Metafizik kitabını onun ödünç aldığını biliyoruz.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Ben, ne?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Öyle bir şey yapmadım.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: O, kitabı benim dairemden çaldı.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Küstah gerizekalısı…");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Ve bu seni kızdırdı, değil mi?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Kızmak mı? Deliye döndüm. Bana o aptal yüzüyle bakıp, affetmemi istemesi… Çok iyi");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Eline bir koz geçti, dedektif.");
                    Thread.Sleep(1500);
                            goto sel2;



                            break;
                case 1:
                    Clear();
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Evelyn gece yarısı öldü. Tekrar söyle bakalım, neredeydin?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Evdeydim yazı yazıyordum. Bir metin üzerinde çalışıyordum.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Yalan Söylüyorsun, McCaffrey. Tren bakım istasyonunun dışındaydın.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Peki bunu nasıl kanıtlayacaksın.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Augusta Summers’ın kızına yazdığı mektubun yarısı hakkında ne diyeceksin?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Neyden bahsediyorsun?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Evelyn’i dövmeyi bitirdikten sonra üzerini aradın ve annesinin mektubunu buldun.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Yaşlı kadının acısı senin hoşuna gitti.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Mektubla ya da Evelyn’nin annesiyle ilgili bir şey bilmiyorum.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: O zaman mektup masanın üzerinde ne arıyordu?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Bilmiyorum ama onu oraya benim koymadığıma göre başka birisi koymuştur.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Her şeyden bir anlam çıkarma yeteneğini kullan istersen.");
                    Thread.Sleep(1500);

                            goto sel2;


                            break;


                case 2:
                    Clear();
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Evelyn’e dayak atarken kullanılan Araba somon anahtarını senin dairende bulduk.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Annesinden gelmiş mektubu ve kanlı kıyafeti de orada bulduk. Aramız açılıyor McCaffrey.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Evelyn Summers’ı öldürdüğümü ve  kanıtı dairemin ortasında bıraktığımı mı düşünüyorsun?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Sana inanmıyorum, Grosvenor. Kanıtlar seni işaret ediyor.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Evelyn’i öldürmek istediğimi kanıtlayabilir misin?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Tiernan, onun önünde Evelyn’i öldürmekle tehdit ettiğine tanıklık etmeye hazır.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Kendini korumaya almış. Bu açık bir şey.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Peki, size samimi davranacağım.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Tiernan Evelyn’i öldürdü.Yardım için bana geldi.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Onu dinledim ve o da bunu neden yaptığını açıkladı.	");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Tiernan sana yardıma mı geldi? Buna inanmamı mı bekliyorsun?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Olan biten bu.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Ona korkunç bir hata yaptığını söyledim ama kendini ele verirse, bütün hayatının heba olacağını söyledi.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Eşyalarını aldım ve onları yok edeceğimi söyledim.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("McCaffrey: Tiernan ile konuşun. Sonunda itiraf edecektir.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Red;
                    WriteSlowly("Duncan ikinci sorgu odasından çıkar ve bir memur McCaffrey’in sabıkasını Duncana verir.");
                    Thread.Sleep(3000);

                    Clear();

                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Kızıl ekip, McCaffrey’i gözetim altında tutuyormuş. Küçük hırsızlıklar yapmış. Syracuse’ta eğitim sırasında ordudan atılmış, " +
                        "Yerli bir kadına saldırmış. Denilene göre kadını öldüresiye dövmüş.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Bu büyük bir kanıt. Bakalım Tiernan ne diyecek?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Red;
                    WriteSlowly("Duncan birinci sorgu odasına girer.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Tiernan: McCaffrey ile konuştunuz mu? Artık gidebilir miyim? Aklandım mı?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Pek Sayılmaz.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Sormamız gereken bir soru daha var, James.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Sonrasında işimiz bitecek.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Tiernan: Elbette, sorun.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Evelyn gitti ve sen de onu fark etmedin. Daha sonra ne oldu?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Tiernan: Sabah kalktım.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Tiernan: Çok içkiliydim.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Tiernan: Evelyn’nin geri geleceğini düşündüm.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Yalan Söylediğini biliyorum, James. Onun peşinden gittin. Gerçekten ne olduğunu anlat.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Tiernan: Ne dediğiniz hakkında fikrim yok. O otel odasında olduğumu da nereden çıkardınız?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: McCaffrey dairesine geldin. Hala aşırı derece içkiliydin. Onun katında bayıldın.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: İşte gerçekte ne olduğunu anlatma zamanı geldi.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Tiernan: McCaffrey beni ertesi sabah kaldırdı.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Tiernan: Bana somun anahtarını, mektubu ve kutuyu gösterdi.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Tiernan: Ve geçen gece o eşyalarla beraber geldiğimi söyledi.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Tiernan: Evelyn’ni öldürdüğümü…");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Tiernan: haberin her tarafa yayıldığını ve beni koruyacağını söyledi.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Tiernan: Bilmiyorum, dedektif. Hayatımın üstüne yemin ederim, hiçbir şey hatırlamıyorum."+ " Evelyn’e çok kızmıştım, hem de çok, bu işi yapabilirdim.");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteSlowly("Tiernan: Ben mi yaptım?");
                    Thread.Sleep(1500);
                    ForegroundColor = ConsoleColor.Green;
                    WriteSlowly("John Duncan: Burada bekle");
                    Thread.Sleep(3000);


                    string prompt153 = "Emir:";
                    string[] options153 = { "Tutukla", "Sorgudan çık" };
                    Menu mainMenu153 = new Menu(prompt153, options153);
                    int selectedIndex153 = mainMenu153.Run();
                    switch (selectedIndex153)
                    {

                        case 0:
                        emirjames:
                            Clear();
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: James Tiernan, seni Evelyn Summers’ı öldürmekten tutukluyorum.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Sanırım ne yaptığını bilmiyordun. Senin adına Savcıyla konuşacağım.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Şimdilik senin için yapabileceğim en iyi şey bu.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("James Donnely: Bugün istasyonumda olanlar için Bay Duncan’nın çok iyi mazeretleri olduğunu farz edeceğim.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("James Donnely: Dava için mükemmel adayınız vardı.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("James Donnely: Sizin hakkında söylenenler yalanmış, Bay Duncan.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("James Donnely: Kanıt üretmek veya bir itiraf çıkarmak için bir zaman varsa, o zaman buydu. Ama siz yanlış olanını seçtiniz" +
                                " ve bu adam artık dışarıda özgür bir şekilde dolaşacak.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("James Donnely: Sizi bir daha Gözümün önünde görmeyeyim, Bay Duncan.");
                            Thread.Sleep(3000);

                            break;

                        case 1:
                            Clear();
                            ForegroundColor = ConsoleColor.Red;
                            WriteSlowly("Duncan sorgu odasından çıkar ve ikinci sorgu odasına gider");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Savaşa katıldın, değil mi?");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: Evet katıldım.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: Gördüğüm şeyleri görmek… bir insanı değiştirebilir.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: Bazı şeyleri değiştirmek için geri döndüm.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: Tek istediğim, ne düşündüğümü söylemek için bir kalem ve fırsattı.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Bize polisle başının çok az belaya girdiğini söylemiştin.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Ama hiç küçük çaplı bir soygundan bahsetmedin.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: Daha önce şiddet uyguladığım için başım belaya girmedi. Duymak istediğiniz buydu, değil mi?");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Yalan söylüyorsun, McCaffrey. Daha önce kadınlara şiddet uygulamışsın.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: Birkaç park cezasıyla küçük bir hırsızlık suçunu nasıl bir saldırıya dönüştürdün, anlamadım.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Senin ve ordudan atılman hakkında her şeyi biliyoruz. Syracuse’de birkaç kadını öldürene kadar dövmüşsün.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Daha önce hiç savaşa katılmadın, McCaffrey. Senin hayatın yalan.");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: O kadın lanet olası bir köylüydü. Benden para çalmaya çalıştı. Bu ülke için savaştaydım. Ben…");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: Onu dövdün çünkü senden para çalmaya çalıştı. Çünkü sana kurnazlık etmeye çalıştı…");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: Başka ne yapabilirdim ki? Parayı ona elimle mi verseydim?");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: Bunu yapsaydım bana ne derlerdi…");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Green;
                            WriteSlowly("John Duncan: ve Evelyn Summers, zavallı ayyaş, senin kitabını çaldı…");
                            Thread.Sleep(1500);
                            ForegroundColor = ConsoleColor.Cyan;
                            WriteSlowly("McCaffrey: ve bunu yaparsa başına ne geleceğini de anladı.");
                            Thread.Sleep(3000);

                            string prompt154 = "Emir:";
                            string[] options154 = { "Tutukla", "Sorgudan çık" };
                            Menu mainMenu154 = new Menu(prompt154, options154);
                            int selectedIndex154 = mainMenu154.Run();
                            switch (selectedIndex154)
                            {

                                case 0:
                                    emirgros:
                                    Clear();
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Grosvenor McCaffrey, seni Evelyn Summers’ı öldürmekten tutukluyorum.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Green;
                                    WriteSlowly("John Duncan: Kendisinden başkasına zarar vermeyen biriydi. Tanrı yardımcın olsun.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Cyan;
                                    WriteSlowly("James Donnely: Tebrikler, Bay Duncan. Adamı iyi yakaladınız.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Cyan;
                                    WriteSlowly("James Donnely: Sizin yardımınız olmadan bu işi başaramazdık.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Cyan;
                                    WriteSlowly("James Donnely: McCaffrey itinde bir gıdım pişmanlık yok, büyük jüri işi halledecektir.");
                                    Thread.Sleep(1500);
                                    ForegroundColor = ConsoleColor.Cyan;
                                    WriteSlowly("James Donnely: Hapishane mezarlığı dışında daha iyi yer bulmak için çok çaba sarf etmen gerekecek.");
                                    Thread.Sleep(1500);


                                    break;
                                case 1:

                                    string prompt150 = "Emir:";
                                    string[] options150 = { "James Tiernan’ı tutukla", "Grosvenor McCaffrey’i tutukla" };
                                    Menu mainMenu150 = new Menu(prompt150, options150);
                                    int selectedIndex150 = mainMenu150.Run();
                                    switch (selectedIndex150)
                                    {

                                        case 0:
                                            goto emirjames;
                                            break;
                                        case 1:
                                            goto emirgros;
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                     break;
            }
                    break;
    }
}


        private static void WriteSlowly(string text)
        {
            string[] words = text.Split(' ');
            Task t = Task.Run(() =>
            {
                foreach (string word in words)
                {
                    foreach (char letter in word)
                    {
                        Write(letter);
                        Thread.Sleep(50);
                    }

                    Write(" ");
                    Thread.Sleep(50);
                }
            });
            t.Wait();
            Write("\n");
        }
        private static void MusicPPLoop(string f)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string fullpath = "";
            string b = "";
            for (int x = 0; x < path.Length; x++)
            {
                if (path[x] == '\\')
                {
                    fullpath += "\\";
                    b = fullpath;
                    fullpath += f;
                    if (File.Exists(fullpath))
                    {
                        break;
                    }
                    fullpath = b;
                }
                else
                {
                    fullpath += path[x];
                }
            }

            var soundPlayer = new SoundPlayer();
            soundPlayer.SoundLocation = fullpath;
            soundPlayer.PlayLooping();
        }
        private static void MusicPP(string f)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string fullpath = "";
            string b = "";
            for (int x = 0; x < path.Length; x++)
            {
                if (path[x] == '\\')
                {
                    fullpath += "\\";
                    b = fullpath;
                    fullpath += f;
                    if (File.Exists(fullpath))
                    {
                        break;
                    }
                    fullpath = b;
                }
                else
                {
                    fullpath += path[x];
                }
            }

            var soundPlayer = new SoundPlayer();
            soundPlayer.SoundLocation = fullpath;
            soundPlayer.Play();
        }   
    }
}
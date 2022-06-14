using System;


namespace consoleUtility
{
    class ConsoleUtility
    {
        const char _block = '■'; //bu kare yükleme ekranının temasını belirtiyor.
        const string _back = "\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b"; //yükleme ekranının uzunluğunu belirtiyor.
        public static void WriteProgressBar(int percent, bool update = false) //WriteProgressBar adında fonksiyon tanımlandı.
        {
            if (update)
                Console.Write(_back); //yükleme ekranının uzunluğunun güncel halini belirtiyor.
            Console.Write("["); 
            var p = (int)((percent / 10f) + .5f); //yüklemenin tamamlanmasını gösteriyor.
            for (var i = 0; i < 10; ++i) // i nin 0 dan 10 a kadar artırılmasını gösteriyor.
            {
                if (i >= p)  //yükleme ekranının çalışma sistemi.
                    Console.Write(' ');
                else
                    Console.Write(_block);
            }
            Console.Write("] {0,3:##0}%", percent); //yükleme ekranının son kısmı ve yükleme oranını gösteriyor.
        }
    }
}

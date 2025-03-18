using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnhancedMusicPlayerTask
{
    public class Music
    {
        public Music()
        {
            music.Add("kol wahed", @"C:\Users\bassi\Desktop\learn CS\EnhancedMusicPlayerTask\Music\Adam - Kol Wahed _ ادم - كل واحد(M4A_128K).m4a");
            music.Add("baad el kalam", @"C:\Users\bassi\Desktop\learn CS\EnhancedMusicPlayerTask\Music\Ahmed Kamel - Baad El Kalam _ Official Lyrics Video - 2023 _ احمد كامل - بعد الكلام(M4A_128K).m4a");
            music.Add("kan fe tefl", @"C:\Users\bassi\Desktop\learn CS\EnhancedMusicPlayerTask\Music\Ahmed Kamel - Kan Fe Tefl أحمد كامل - كان فى طفل Feat Fady Haroun(M4A_128K).m4a");
            music.Add("nuggets", @"C:\Users\bassi\Desktop\learn CS\EnhancedMusicPlayerTask\Music\3ab3az x _BegadOsama - NUGGETS _ عبعظ و بيجاد - ناجتس (انا المشطشط)(M4A_128K).m4a");
        }
        public Dictionary<string,string> music { get; private set; }=new Dictionary<string,string>();
    }
}

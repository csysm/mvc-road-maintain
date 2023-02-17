using System.Linq;

namespace RoadMaintainSys.UI.Utility
{
    public class LengthLimit//限制页面显示的字符长度
    {
        public static string ContentLenLimit(string content,int displayLength)
        {
            if (content.Length>displayLength)
            {
                return content.Substring(0, displayLength)+"...";
            }
            return content;
        }
    }
}

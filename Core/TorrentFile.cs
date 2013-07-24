using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace H31DHTMgr
{
    /// <summary>
    /// Torrent文件 
    /// 2013-06-28
    /// </summary>
    public class TorrentFile
    {

        #region 私有字段
        private string _OpenError = "";
        private bool _OpenFile = false;

        private string _TorrentAnnounce = "";
        private IList<string> _TorrentAnnounceList = new List<string>();
        private DateTime _TorrentCreateTime = new DateTime(1970,1,1,0,0,0);
        private long _TorrentCodePage = 0;
        private string _TorrentComment = "";
        private string _TorrentCreatedBy = "";
        private string _TorrentEncoding = "";
        private string _TorrentCommentUTF8 = "";
        private IList<TorrentFileInfoClass> _TorrentFileInfo = new List<TorrentFileInfoClass>();
        private string _TorrentName = "";
        private string _TorrentNameUTF8 = "";
        private long _TorrentPieceLength = 0;
        private byte[] _TorrentPieces;
        private string _TorrentPublisher = "";
        private string _TorrentPublisherUTF8 = "";
        private string _TorrentPublisherUrl = "";
        private string _TorrentPublisherUrlUTF8 = "";
        private IList<string> _TorrentNotes = new List<string>();
        #endregion

        #region 属性
        /// <summary>
        /// 错误信息
        /// </summary>
        public string OpenError { set { _OpenError = value; } get { return _OpenError; } }
        /// <summary>
        /// 是否正常打开文件
        /// </summary>
        public bool OpenFile { set { _OpenFile = value; } get { return _OpenFile; } }
        /// <summary>
        /// 服务器的URL(字符串)
        /// </summary>
        public string TorrentAnnounce { set { _TorrentAnnounce = value; } get { return _TorrentAnnounce; } }
        /// <summary>
        /// 备用tracker服务器列表(列表)
        /// </summary>
        public IList<string> TorrentAnnounceList { set { _TorrentAnnounceList = value; } get { return _TorrentAnnounceList; } }
        /// <summary>
        /// 种子创建的时间，Unix标准时间格式，从1970 1月1日 00:00:00到创建时间的秒数(整数)
        /// </summary>
        public DateTime TorrentCreateTime { set { _TorrentCreateTime = value; } get { return _TorrentCreateTime; } }
        /// <summary>
        /// 未知数字CodePage
        /// </summary>
        public long TorrentCodePage { set { _TorrentCodePage = value; } get { return _TorrentCodePage; } }
        /// <summary>
        /// 种子描述
        /// </summary>
        public string TorrentComment { set { _TorrentComment = value; } get { return _TorrentComment; } }
        /// <summary>
        /// 编码方式
        /// </summary>
        public string TorrentCommentUTF8 { set { _TorrentCommentUTF8 = value; } get { return _TorrentCommentUTF8; } }
        /// <summary>
        /// 创建人
        /// </summary>
        public string TorrentCreatedBy { set { _TorrentCreatedBy = value; } get { return _TorrentCreatedBy; } }
        /// <summary>
        /// 编码方式
        /// </summary>
        public string TorrentEncoding { set { _TorrentEncoding = value; } get { return _TorrentEncoding; } }
        /// <summary>
        /// 文件信息
        /// </summary>
        public IList<TorrentFileInfoClass> TorrentFileInfo { set { _TorrentFileInfo = value; } get { return _TorrentFileInfo; } }
        /// <summary>
        /// 种子名
        /// </summary>
        public string TorrentName { set { _TorrentName = value; } get { return _TorrentName; } }
        /// <summary>
        /// 种子名UTF8
        /// </summary>
        public string TorrentNameUTF8 { set { _TorrentNameUTF8 = value; } get { return _TorrentNameUTF8; } }
        /// <summary>
        /// 每个块的大小，单位字节(整数)
        /// </summary>
        public long TorrentPieceLength { set { _TorrentPieceLength = value; } get { return _TorrentPieceLength; } }
        /// <summary>
        /// 每个块的20个字节的SHA1 Hash的值(二进制格式)
        /// </summary>
        private byte[] TorrentPieces { set { _TorrentPieces = value; } get { return _TorrentPieces; } }
        /// <summary>
        /// 出版
        /// </summary>
        public string TorrentPublisher { set { _TorrentPublisher = value; } get { return _TorrentPublisher; } }
        /// <summary>
        /// 出版UTF8
        /// </summary>
        public string TorrentPublisherUTF8 { set { _TorrentPublisherUTF8 = value; } get { return _TorrentPublisherUTF8; } }
        /// <summary>
        /// 出版地址
        /// </summary>
        public string TorrentPublisherUrl { set { _TorrentPublisherUrl = value; } get { return _TorrentPublisherUrl; } }
        /// <summary>
        /// 出版地址
        /// </summary>
        public string TorrentPublisherUrlUTF8 { set { _TorrentPublisherUrlUTF8 = value; } get { return _TorrentPublisherUrlUTF8; } }
        /// <summary>
        /// NODES
        /// </summary>
        public IList<string> TorrentNotes { set { _TorrentNotes = value; } get { return _TorrentNotes; } }
        #endregion
        

       

 

        public TorrentFile(string FileName)
        {
            try
            {
                System.IO.FileStream TorrentFile = new System.IO.FileStream(FileName, System.IO.FileMode.Open);
                if (TorrentFile.Length == 0)
                    return;
                byte[] TorrentBytes = new byte[TorrentFile.Length];
                TorrentFile.Read(TorrentBytes, 0, TorrentBytes.Length);
                TorrentFile.Close();

                if ((char)TorrentBytes[0] != 'd')
                {

                    if (OpenError.Length == 0) OpenError = "错误的Torrent文件，开头第1字节不是100";
                    return;
                }
                GetTorrentData(TorrentBytes);
                if (TorrentName.Length == 0 && TorrentFileInfo.Count > 0)
                    TorrentName = TorrentFileInfo[0].Path;
            }
            catch (System.Exception ex)
            {
                //错误日志记录 
                //H31Debug.PrintLn("TorrentFile:" + ex.StackTrace);
            }
        }

        public TorrentFile(byte[] content)
        {
            if(content!=null)
                GetTorrentData(content);
        }


        #region 开始读数据
        /// <summary>
        /// 开始读取
        /// </summary>
        /// <param name="TorrentBytes"></param>
        private void GetTorrentData(byte[] TorrentBytes)
        {
            try
            {
                int StarIndex = 1;         
                while (true)
                {
                    string test = Encoding.UTF8.GetString(TorrentBytes, StarIndex, TorrentBytes.Length - StarIndex > 500 ? 500 : TorrentBytes.Length - StarIndex);
                    object Keys = GetKeyText(TorrentBytes, ref StarIndex);
                    if (Keys == null)
                    {
                        if (StarIndex >= TorrentBytes.Length) 
                            OpenFile = true;
                        break;
                    }

                    if (GetValueText(TorrentBytes, ref StarIndex, Keys.ToString().ToUpper()) == false) 
                        break;
                }
            }
            catch (System.Exception ex)
            {
                //错误日志记录 
                H31Debug.PrintLn("GetTorrentData:" + ex.StackTrace);
            }
        }
        #endregion


        /// <summary>
        /// 读取结构
        /// </summary>
        /// <param name="TorrentBytes"></param>
        /// <param name="StarIndex"></param>
        /// <param name="Keys"></param>
        /// <returns></returns>
        private bool GetValueText(byte[] TorrentBytes,ref int StarIndex, string Keys)
        {
            switch (Keys)
            {
                case "ANNOUNCE":
                    TorrentAnnounce = GetKeyText(TorrentBytes, ref StarIndex).ToString();
                    break;
                case "ANNOUNCE-LIST":
                case "ANNOUNCE_LIST":
                    int ListCount = 0;
                    ArrayList _TempList=GetKeyData(TorrentBytes, ref StarIndex, ref ListCount);
                    for (int i = 0; i != _TempList.Count; i++)
                    {
                        TorrentAnnounceList.Add(_TempList[i].ToString());
                    }                    
                    break;
                case "CREATION DATE":
                    object Date = GetKeyNumb(TorrentBytes, ref StarIndex);
                    if (Date == null)
                    {
                        if (OpenError.Length == 0) OpenError = "CREATION DATE 返回不是数字类型";
                        //return false;
                    }  
                    else
                        TorrentCreateTime = TorrentCreateTime.AddTicks(long.Parse(Date.ToString()));
                    break;
                case "CODEPAGE":
                    object CodePageNumb = GetKeyNumb(TorrentBytes, ref StarIndex);
                    if (CodePageNumb == null)
                    {
                        if (OpenError.Length == 0) OpenError = "CODEPAGE 返回不是数字类型";
                        return false;
                    }
                    TorrentCodePage = long.Parse(CodePageNumb.ToString());
                    break;
                case "ENCODING":
                    TorrentEncoding = GetKeyText(TorrentBytes, ref StarIndex).ToString();
                    break;
                case "CREATED BY":
                    TorrentCreatedBy = GetKeyText(TorrentBytes, ref StarIndex).ToString();
                    break;
                case "COMMENT":
                    TorrentComment = GetKeyText(TorrentBytes, ref StarIndex).ToString();
                    break;
                case "COMMENT.UTF-8":
                    TorrentCommentUTF8 = GetKeyText(TorrentBytes, ref StarIndex).ToString();
                    break;
                case "INFO":
                    int FileListCount = 0;
                    GetFileInfo(TorrentBytes, ref StarIndex, ref FileListCount);
                    break;
                case "NAME":
                    TorrentName = GetKeyText(TorrentBytes, ref StarIndex).ToString();
                    break;
                case "NAME.UTF-8":
                    TorrentNameUTF8 = GetKeyText(TorrentBytes, ref StarIndex).ToString();
                    break;
                case "PIECE LENGTH":
                    object PieceLengthNumb = GetKeyNumb(TorrentBytes, ref StarIndex);
                    if (PieceLengthNumb == null)
                    {
                        if (OpenError.Length == 0) OpenError = "PIECE LENGTH 返回不是数字类型";
                        return false;
                    }
                    TorrentPieceLength = long.Parse(PieceLengthNumb.ToString());
                    break;
                case "PIECES":
                    TorrentPieces = GetKeyByte(TorrentBytes, ref StarIndex);                  
                    break;
                case "PUBLISHER":
                    TorrentPublisher = GetKeyText(TorrentBytes, ref StarIndex).ToString();
                    break;
                case "PUBLISHER.UTF-8":
                    TorrentPublisherUTF8 = GetKeyText(TorrentBytes, ref StarIndex).ToString();
                    break;
                case "PUBLISHER-URL":
                    TorrentPublisherUrl = GetKeyText(TorrentBytes, ref StarIndex).ToString();
                    break;
                case "PUBLISHER-URL.UTF-8":
                    TorrentPublisherUrlUTF8 = GetKeyText(TorrentBytes, ref StarIndex).ToString();
                    break;
                case "NODES":                   
                    int NodesCount = 0;
                    ArrayList _NodesList = GetKeyData(TorrentBytes, ref StarIndex, ref NodesCount);
                    int IPCount= _NodesList.Count/2;
                    for (int i = 0; i != IPCount; i++)
                    {
                        TorrentNotes.Add(_NodesList[i * 2] + ":" + _NodesList[(i * 2) + 1]);
                    }
                    break;
                case "DURATION":
                    object duration1 = GetKeyNumb(TorrentBytes, ref StarIndex);
                    break;
                case "ENCODED RATE":
                    object end1 = GetKeyNumb(TorrentBytes, ref StarIndex);
                    break;
                case "HEIGHT":
                    object temp1 = GetKeyNumb(TorrentBytes, ref StarIndex);
                    break;
                case "WIDTH":
                    object temp2 = GetKeyNumb(TorrentBytes, ref StarIndex);
                    break;
                default:
                    //return false;
                    object temp3= GetKeyNumb(TorrentBytes, ref StarIndex);
                    break;
            }
            return true;
        }


        #region 获取数据
        /// <summary>
        /// 获取列表方式 "I1:Xe"="X" 会调用GetKeyText
        /// </summary>
        /// <param name="TorrentBytes"></param>
        /// <param name="StarIndex"></param>
        /// <param name="ListCount"></param>
        private ArrayList GetKeyData(byte[] TorrentBytes, ref int StarIndex, ref int ListCount)
        {
            ArrayList _TempList = new ArrayList();
            while (true)
            {
                string TextStar = Encoding.UTF8.GetString(TorrentBytes, StarIndex, 1);
                switch (TextStar)
                {
                    case "l":
                        StarIndex++;
                        ListCount++;
                        break;
                    case "e":
                        ListCount--;
                        StarIndex++;
                        if (ListCount <= 0) return _TempList;
                        break;
                    case "i":
                        _TempList.Add(GetKeyNumb(TorrentBytes,ref StarIndex).ToString());
                        break;
                    default:
                        object ListText = GetKeyText(TorrentBytes, ref StarIndex);
                        if (ListText != null)
                        {
                            _TempList.Add(ListText.ToString());
                        }
                        else
                        {
                            if (OpenError.Length == 0)
                            {
                                OpenError = "错误的Torrent文件，ANNOUNCE-LIST错误";
                                return _TempList;
                            }
                        }
                        break;
                }
            }
        }
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="TorrentBytes"></param>
        /// <param name="StarIndex"></param>
        /// <returns></returns>
        private object GetKeyText(byte[] TorrentBytes, ref int StarIndex)
        {
            int Numb = 0;
            int LeftNumb = 0;
            for (int i = StarIndex; i != TorrentBytes.Length; i++)
            {
               char byte1=(char)TorrentBytes[i];
               if (byte1 == ':') 
                    break;
                if (byte1 == 'e')
                {                    
                    LeftNumb++;
                    continue;
                }

                Numb++;
            }

            StarIndex += LeftNumb;
            string TextNumb = Encoding.UTF8.GetString(TorrentBytes, StarIndex, Numb);
            try
            {
                char byte2 = (char)TorrentBytes[StarIndex];
                if (byte2 == 'l' || byte2=='d')
                {
                    for (int i = StarIndex; i != TorrentBytes.Length; i++)
                    {
                        char byte1 = (char)TorrentBytes[i];
                        if (byte1 == ':')
                            break;
                        if (byte1 == 'l' || byte1 == 'd')
                        {
                            LeftNumb++;
                            continue;
                        }
                    }
                    TextNumb = Encoding.UTF8.GetString(TorrentBytes, StarIndex + LeftNumb, Numb - LeftNumb);
                }
                int ReadNumb = Int32.Parse(TextNumb);
                StarIndex = StarIndex + Numb + 1;
                object KeyText = Encoding.UTF8.GetString(TorrentBytes, StarIndex, ReadNumb);
                StarIndex += ReadNumb;
                return KeyText;
            }
            catch
            {                
                return null;
            }

        }
        /// <summary>
        /// 获取数字
        /// </summary>
        /// <param name="TorrentBytes"></param>
        /// <param name="StarIndex"></param>
        private object GetKeyNumb(byte[] TorrentBytes, ref int StarIndex)
        {
            char byte2 = (char)TorrentBytes[StarIndex];
            while (byte2 == 'l' || byte2 == 'd')
            {
                StarIndex++;
                byte2 = (char)TorrentBytes[StarIndex];
            }
            if (Encoding.UTF8.GetString(TorrentBytes, StarIndex, 1) == "i")
            {
               
                int Numb = 0;
                for (int i = StarIndex; i != TorrentBytes.Length; i++)
                {
                    if ((char)TorrentBytes[i] == 'e') break;
                    Numb++;
                }                
                StarIndex++;
                long RetNumb = 0;
                try
                {                   
                   RetNumb=long.Parse(Encoding.UTF8.GetString(TorrentBytes, StarIndex, Numb-1));
                   StarIndex += Numb;
                   return RetNumb;
                }
                catch
                {
                    return null;
                }               
                
            }
            else
            {
                return null;
            }
         
        }
        /// <summary>
        /// 获取BYTE数据
        /// </summary>
        /// <param name="TorrentBytes"></param>
        /// <param name="StarIndex"></param>
        /// <returns></returns>
        private byte[] GetKeyByte(byte[] TorrentBytes, ref int StarIndex)
        {
            int Numb = 0;
            for (int i = StarIndex; i != TorrentBytes.Length; i++)
            {
                if ((char)TorrentBytes[i] == ':') break;
                Numb++;
            }
            string TextNumb = Encoding.UTF8.GetString(TorrentBytes, StarIndex, Numb);

            try
            {
                int ReadNumb = Int32.Parse(TextNumb);                
                StarIndex = StarIndex + Numb + 1;
                System.IO.MemoryStream KeyMemory = new System.IO.MemoryStream(TorrentBytes, StarIndex, ReadNumb);
                byte[] KeyBytes = new byte[ReadNumb];
                KeyMemory.Read(KeyBytes, 0, ReadNumb);
                KeyMemory.Close();              
                StarIndex += ReadNumb;
                return KeyBytes;
            }
            catch
            {
                return null;
            }

            

            
        }
        /// <summary>
        /// 对付INFO的结构
        /// </summary>
        /// <param name="TorrentBytes"></param>
        /// <param name="StarIndex"></param>
        /// <param name="ListCount"></param>
        private void GetFileInfo(byte[] TorrentBytes,ref int StarIndex,ref int ListCount)
        {
            if ((char)TorrentBytes[StarIndex] != 'd') return;
            StarIndex++;

            String getkey=GetKeyText(TorrentBytes, ref StarIndex).ToString().ToUpper();
            while (getkey != "FILES" && getkey != "LENGTH")
            {

                object PieceLengthNumb = GetKeyNumb(TorrentBytes, ref StarIndex);
                char temp1 = (char)TorrentBytes[StarIndex];
                while (temp1 == 'i')
                { 
                    object temp2 = GetKeyNumb(TorrentBytes, ref StarIndex);
                    temp1 = (char)TorrentBytes[StarIndex];
                }

                getkey = GetKeyText(TorrentBytes, ref StarIndex).ToString().ToUpper();
            }
            if (getkey == "FILES")
            {
                TorrentFileInfoClass Info = new TorrentFileInfoClass();             
                while (true)
                {
                    string TextStar = Encoding.UTF8.GetString(TorrentBytes, StarIndex, 1);
                    switch (TextStar)
                    {
                        case "l":
                            StarIndex++;
                            ListCount++;                      
                            break;
                        case "e":
                            ListCount--;
                            StarIndex++;
                            if (ListCount == 1) TorrentFileInfo.Add(Info);
                            if (ListCount == 0) return;
                            break;
                        case "d":
                            Info = new TorrentFileInfoClass();
                            ListCount++;
                            StarIndex++;                            
                            break;

                        default:
                            object ListText = GetKeyText(TorrentBytes, ref StarIndex);
                            if (ListText == null) return;
                            switch (ListText.ToString().ToUpper())   //转换为大写
                            {
                                case "ED2K":
                                    Info.De2K = GetKeyText(TorrentBytes, ref StarIndex).ToString();                                   
                                    break;
                                case "FILEHASH":
                                    Info.FileHash = GetKeyText(TorrentBytes, ref StarIndex).ToString();
                                    break;
                                    
                                case "LENGTH":
                                    Info.Length = Convert.ToInt64(GetKeyNumb(TorrentBytes, ref StarIndex));                                   
                                    break;
                                case "PATH":
                                    int PathCount=0;
                                    ArrayList PathList = GetKeyData(TorrentBytes, ref StarIndex, ref PathCount);
                                    string Temp = "";
                                    for (int i = 0; i != PathList.Count; i++)
                                    {
                                        Temp += PathList[i].ToString();
                                    }
                                    Info.Path=Temp;
                                    break;                              
                                case "PATH.UTF-8":
                                    int PathUtf8Count = 0;
                                    ArrayList Pathutf8List = GetKeyData(TorrentBytes, ref StarIndex, ref PathUtf8Count);
                                    string UtfTemp = "";
                                    for (int i = 0; i != Pathutf8List.Count; i++)
                                    {
                                        UtfTemp += Pathutf8List[i].ToString();
                                    }
                                    Info.PathUTF8=UtfTemp;
                                    break; 

                            }
                            break;
                    }
                }
            }
            else if (getkey == "LENGTH")
            {
                //单一文件结构时
                TorrentFileInfoClass Info = new TorrentFileInfoClass();
                Info.Length = Convert.ToInt64(GetKeyNumb(TorrentBytes, ref StarIndex));
                string test = Encoding.UTF8.GetString(TorrentBytes, StarIndex, TorrentBytes.Length - StarIndex > 500 ? 500 : TorrentBytes.Length - StarIndex);

                string TextStar = Encoding.UTF8.GetString(TorrentBytes, StarIndex, 1);

                object ListText = GetKeyText(TorrentBytes, ref StarIndex);
                if (ListText.ToString().ToUpper() == "NAME")
                {
                    Info.Path = GetKeyText(TorrentBytes, ref StarIndex).ToString();
                    TorrentFileInfo.Add(Info);
                    TorrentName = Info.Path;
                }
            }
        }
        #endregion


        /// <summary>
        /// 对应结构 INFO 多个文件时
        /// </summary>
        public class TorrentFileInfoClass
        {
            private string path = "";
            private string pathutf8 = "";
            private long length = 0;
            private string md5sum = "";
            private string de2k = "";
            private string filehash = "";

            
            /// <summary>
            /// 文件路径
            /// </summary>
            public string Path { get { return path; } set { path = value; } }
            /// <summary>
            /// UTF8的名称
            /// </summary>
            public string PathUTF8 { get { return pathutf8; } set { pathutf8 = value; } }
            /// <summary>
            /// 文件大小
            /// </summary>
            public long Length { get { return length; } set { length = value; } }
            /// <summary>
            /// MD5验效 （可选）
            /// </summary>
            public string MD5Sum { get { return md5sum; } set { md5sum = value; } }
            /// <summary>
            /// ED2K 未知
            /// </summary>
            public string De2K { get { return de2k; } set { de2k = value; } }
            /// <summary>
            /// FileHash 未知
            /// </summary>
            public string FileHash { get { return filehash; } set { filehash = value; } }   
 

        }
    
    }
}

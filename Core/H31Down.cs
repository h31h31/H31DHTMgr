using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Threading;


namespace H31DHTMgr
{
    public class H31Down
    {
        private string pathname=string.Empty;
        public bool webgood = true;
        private int downwebpos = 0;
        private string[] m_strURLList = new string[4];
        private int[] m_timeoutList=new int[4];

        #region 下载到内存中直接使用
        public byte[] DownLoadFileByHashToByte(string hashname)
        {
            byte[] res=null;
            try
            {
                //先检查本地有没有文件，如果有直接读取本地，没有再从网络上下载
                string filename = string.Format("{0}//{1}.torrent", pathname, hashname);
                if (File.Exists(filename))
                {
                    System.IO.FileStream TorrentFile = new System.IO.FileStream(filename, System.IO.FileMode.Open);
                    if (TorrentFile.Length > 0)
                    {
                        res = new byte[TorrentFile.Length];
                        TorrentFile.Read(res, 0, res.Length);
                        TorrentFile.Close();
                        return res;
                    }
                }
                m_strURLList[0] = string.Format("https://zoink.it/torrent/{0}.torrent", hashname);
                m_strURLList[1] = string.Format("http://bt.box.n0808.com/{0}/{1}/{2}.torrent", hashname.Substring(0, 2), hashname.Substring(hashname.Length - 2, 2), hashname);
                m_strURLList[2] = string.Format("http://torrage.com/torrent/{0}.torrent", hashname);
                m_strURLList[3] = string.Format("http://torcache.net/torrent/{0}.torrent", hashname);
            

                m_timeoutList[0] = 300;
                m_timeoutList[1] = 300;
                m_timeoutList[2] = 700;
                m_timeoutList[3] = 700;
                //随机从前面两个网站中的一个下载,因为前面两个网站速度快些
                downwebpos = (downwebpos + 1) % 2;
                //res = DownLoadFileToSaveByte(m_strURLList[downwebpos]);

                //随机打乱三个网址顺序下载,防止从一个网站下载过多被封
                res = DownLoadFileToSaveByte(m_strURLList[downwebpos], m_timeoutList[downwebpos]);
                if (res == null)
                {
                    //res = DownLoadFileToSaveByte(m_strURLList[(downwebpos + 1) % 2]);
                    //if (res == null)
                    {
                        res = DownLoadFileToSaveByte(m_strURLList[2], m_timeoutList[2]);
                    }
                }

                return res;
            }
            catch (Exception e)
            {
                H31Debug.PrintLn(e.Message);
                return null;
            }
        }
        private byte[] DownLoadFileToSaveByte(string strURL,int timeout1)
        {
            Int32 ticktime1 = System.Environment.TickCount;
            byte[] result = null;
            try
            {
                Int32 ticktime2 = 0;
                byte[] buffer = new byte[4096];

                WebRequest wr = WebRequest.Create(strURL);
                wr.ContentType = "application/x-bittorrent";
                wr.Timeout = timeout1;
                WebResponse response = wr.GetResponse();
                int readsize = 0;
                {
                    bool gzip = response.Headers["Content-Encoding"] == "gzip";
                    Stream responseStream = gzip ? new GZipStream(response.GetResponseStream(), CompressionMode.Decompress) : response.GetResponseStream();

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        responseStream.ReadTimeout = timeout1*2;
                        int count = 0;
                        do
                        {
                            count = responseStream.Read(buffer, 0, buffer.Length);
                            memoryStream.Write(buffer, 0, count);
                            readsize += count;
                            Thread.Sleep(1);
                        } while (count != 0);
                        ticktime2 = System.Environment.TickCount;

                        Thread.Sleep(10);
                        result = memoryStream.ToArray();
                    }
                    Int32 ticktime3 = System.Environment.TickCount;
                    //H31Debug.PrintLn("下载成功" + strURL + ":" + readsize.ToString() + ":" + (ticktime2 - ticktime1).ToString() + "-" + (ticktime3 - ticktime2).ToString());
                }
                wr.Abort();
                return result;
            }
            catch (Exception e)
            {
                Int32 ticktime3 = System.Environment.TickCount;
                //H31Debug.PrintLn("下载失败" + strURL + ":" +  (ticktime3 - ticktime1).ToString());
                return null;
            }
        }
        #endregion

        #region 下载到文件
        public int DownLoadFileByHashToFile(string hashname)
        {
            try
            {
                if (pathname == string.Empty)
                {
                    string localfile = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                    pathname = localfile.Replace("file:\\", "") + "\\Torrent";
                    if (!Directory.Exists(pathname))
                    {
                        Directory.CreateDirectory(pathname);
                    }
                }

                //检测子文件夹是否存在
                string pathname1 = pathname + "\\" + hashname.Substring(hashname.Length - 1, 1);
                if (!Directory.Exists(pathname1))
                {
                    Directory.CreateDirectory(pathname1);
                }
                string filename = string.Format("{0}//{1}//{2}.torrent", pathname, hashname.Substring(hashname.Length - 1, 1), hashname);
                if (File.Exists(filename))
                    return 1;
                m_strURLList[3] = string.Format("http://torcache.net/torrent/{0}.torrent", hashname);
                m_strURLList[2] = string.Format("https://zoink.it/torrent/{0}.torrent", hashname);
                m_strURLList[1] = string.Format("http://bt.box.n0808.com/{0}/{1}/{2}.torrent", hashname.Substring(0,2), hashname.Substring(hashname.Length-2,2), hashname);
                m_strURLList[0] = string.Format("http://torrage.com/torrent/{0}.torrent", hashname);
                m_timeoutList[0] = 500;
                m_timeoutList[1] = 500;
                m_timeoutList[2] = 1000;
 
                //随机从一个网址下载
                //downwebpos = (downwebpos + 1) % 2;
                //if (DownLoadFileToSaveFile(m_strURLList[downwebpos], filename) == 1)
                //    return 1;

                //随机打乱三个网址顺序下载,防止从一个网站下载过多被封
                downwebpos = (downwebpos + 1);
                //从三种网址一一测试下载
                if (DownLoadFileToSaveFile(m_strURLList[(downwebpos) % 3], filename, m_timeoutList[(downwebpos) % 3]) == 1)
                    return 1;
                if (DownLoadFileToSaveFile(m_strURLList[(downwebpos + 1) % 3], filename, m_timeoutList[(downwebpos + 1) % 3]) == 1)
                    return 1;

                if (DownLoadFileToSaveFile(m_strURLList[(downwebpos + 2) % 3], filename, m_timeoutList[(downwebpos + 2) % 3]) == 1)
                    return 1;

                return 0;
            }
            catch (Exception e)
            {
                H31Debug.PrintLn(e.Message);
                return -2;
            }
        }
        private int DownLoadFileToSaveFile(string strURL, string fileName,int timeout1)
        {
            Int32 ticktime1 = System.Environment.TickCount;
            try
            {
                Int32 ticktime2 = 0;
                byte[] buffer = new byte[4096];

                WebRequest wr = WebRequest.Create(strURL);
                wr.ContentType = "application/x-bittorrent";
                wr.Timeout = timeout1;
                WebResponse response = wr.GetResponse();
                int readsize = 0;
                {
                    bool gzip = response.Headers["Content-Encoding"] == "gzip";
                    Stream responseStream = gzip ? new GZipStream(response.GetResponseStream(), CompressionMode.Decompress) : response.GetResponseStream();

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        responseStream.ReadTimeout = timeout1*2;
                        int count = 0;
                        do
                        {
                            count = responseStream.Read(buffer, 0, buffer.Length);
                            memoryStream.Write(buffer, 0, count);
                            readsize += count;
                            Thread.Sleep(1);
                        } while (count != 0);
                        ticktime2 = System.Environment.TickCount;

                        byte[] result = memoryStream.ToArray();
                        Thread.Sleep(10);
                        using (BinaryWriter writer = new BinaryWriter(new FileStream(fileName, FileMode.Create)))
                        {
                            writer.Write(result);
                        }
                    }
                    Int32 ticktime3 = System.Environment.TickCount;
                    //H31Debug.PrintLn("下载成功" + strURL + ":" + readsize.ToString() + ":" + (ticktime2 - ticktime1).ToString() + "-" + (ticktime3 - ticktime2).ToString());
                }
                return 1;
            }
            catch (Exception e)
            {
                Int32 ticktime3 = System.Environment.TickCount;
                //H31Debug.PrintLn("下载失败" + strURL + ":" +  (ticktime3 - ticktime1).ToString());
                return -2;
            }
        }
        #endregion

    }
}

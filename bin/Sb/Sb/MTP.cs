using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sb
{
    class Info
    {
        public int Unk { get; set; }
        public int SpeakerIndex { get; set; }
        public int Offset { get; set; }

        public int Offset1 { get; set; }
        public List<int> OffsetList { get; set; }
        

        public bool DummySize { get; set; }

    }
    public class MTP
    {
        private Br reader;
        //hdr
        byte[] magic;
        int eofOffset;
        int pfOffset;
        int unk1;
        int f1Offset;
        int enrOffset;
        //f1 hdr
        int unk2;
        public int count1;
        int ptrSize;
        public int count2;
        byte[] unk3;
        List<int> ptrOffsets = new List<int>();
        List<Info> infos =new List<Info>();
        byte[] stringBuffer;
        byte[] enr;
        byte[] eof;
        bool isSpeaker = false;
        //
        byte[] newStringBuffer;
        int stringBufferSize;
        int newStringBufferSize;
        int diffSize;
        Dictionary<int, string> ds = new Dictionary<int, string>() 
        {
            {0x00000001, "フリード,char_001 Fried"},
{0x00000002, "パニス,char_002 Panis"},
{0x00000003, "ヴァイオラ,char_003 Viola"},
{0x00000004, "シャノン,char_004 Shannon"},
{0x00000005, "ジンガ,char_005 zynga"},
{0x00000006, "アダム,char_006 a-dam"},
{0x00000007, "キルマリア,char_007 Kilmaria"},
{0x00000008, "ベルベット,char_008 Velvet"},
{0x00000009, "レヴィン,char_009 Levin"},
{0x0000000A, "ローナ,char_010 rohna"},
{0x0000000B, "フリードパニス,char_011 FinalFried "},
{0x0000000C, "カノン,char_012 FinalPanis"},
{0x00000065, "異形の巨人,char_101 unknown1_1"},
{0x00000066, "異形の巨人,char_102 unknown1_2"},
{0x00000067, "異形の大魚,char_103 unknown2"},
{0x00000068, "異形の蝶,char_104 unknown3"},
{0x00000069, "異形の蜘蛛,char_105 unknown4"},
{0x0000006A, "異形の華,char_106 unknown5"},
{0x0000006B, "異形の天使,char_107 unknown6"},
{0x0000006C, "終末の巨人,char_108 lastboss1"},
{0x0000006D, "終末の巨人,char_109 lastboss2"},
{0x0000006E, "終末の巨人,char_110 lastboss3"},
{0x0000006F, "ＥＦ－０Ｄ　Dトール,char_111 exboss"},
{0x00000070, "バッカス,char_112 Bacchus"},
{0x00000071, "バッカス,char_113 Bacchus"},
{0x00000072, "バッカス,char_114 Bacchus"},
{0x00000073, "ベルベット,char_115 Velvet"},
{0x00000074, "レヴィン,char_116 Levin"},
{0x00000075, "レヴィン,char_117 Levin"},
{0x00000076, "ヴァイオラ,char_118 viola"},
{0x00000077, "ヴァイオラ,char_119 viola"},
{0x00000078, "ヴァイオラ,char_120 viola"},
{0x00000079, "ヴァイオラ,char_121 viola"},
{0x0000007A, "海賊船,char_122 ship1"},
{0x0000007B, "海賊船,char_123 ship2-1"},
{0x0000007C, "海賊船,char_124 ship2-2"},
{0x0000007D, "海賊船,char_125 ship3"},
{0x0000007E, "ケルビム,char_126 Cherubim"},
{0x0000007F, "ケルビム,char_127 Cherubim"},
{0x00000080, "バッカス,char_128 Bacchus"},
{0x00000081, "異形の子蜘蛛,char_129 unknown4-child"},
{0x00000082, "バッカス,char_130 Bacchus"},
{0x00000083, "ケルビム,char_131 Cherubim"},
{0x00000084, "ケルビム,char_132 Cherubim"},
{0x00000085, "海賊船,char_133 ship1"},
{0x00000086, "巨人の怒り,char_134"},
{0x00000087, "巨人の嘆き,char_135"},
{0x00000088, "ラスボス３イバラ,char_136"},
{0x00000089, "海賊船,char_137 ship1"},
{0x0000008A, "ＥＦ－０Ｄ　Dトール,char_138 exboss"},
{0x000000C9, "魔晶のルビー,char_201"},
{0x000000CA, "魔晶のサファイア,char_202"},
{0x000000CB, "魔晶のエメラルド,char_203"},
{0x000000CC, "魔晶のトパーズ,char_204"},
{0x000000CD, "ダーククリスタル,char_205"},
{0x000000CE, "ライトクリスタル,char_206"},
{0x000000CF, "ブルーペースト,char_207"},
{0x000000D0, "レッドペースト,char_208"},
{0x000000D1, "グリーンペースト,char_209"},
{0x000000D2, "イエローペースト,char_210"},
{0x000000D3, "シルバーペースト,char_211"},
{0x000000D4, "パープルペースト,char_212"},
{0x000000D5, "シュリーカー,char_213"},
{0x000000D6, "ダリア,char_214"},
{0x000000D7, "お化けパイン,char_215"},
{0x000000D8, "大砲キノコ,char_216"},
{0x000000D9, "スティンガー,char_217"},
{0x000000DA, "ブラストビー,char_218"},
{0x000000DB, "ドラゴンフライ,char_219"},
{0x000000DC, "ディープスティンガー,char_220"},
{0x000000DD, "スパイダー,char_221"},
{0x000000DE, "サンダースパイダー,char_222"},
{0x000000DF, "ミズグモ,char_223"},
{0x000000E0, "スコーピオン,char_224"},
{0x000000E1, "サンドスコーピオン,char_225"},
{0x000000E2, "スピアロブスター,char_226"},
{0x000000E3, "ダークスカル,char_227"},
{0x000000E4, "クリムゾンスカル,char_228"},
{0x000000E5, "アクアスカル,char_229"},
{0x000000E6, "トーチ,char_230"},
{0x000000E7, "チャコル,char_231"},
{0x000000E8, "パール,char_232"},
{0x000000E9, "ゴブリン,char_233"},
{0x000000EA, "ゴブリンソード,char_234"},
{0x000000EB, "ゴブリングレイブ,char_235"},
{0x000000EC, "ホブゴブリン,char_236"},
{0x000000ED, "ホブゴブリンメイス,char_237"},
{0x000000EE, "ボーンファイター,char_238"},
{0x000000EF, "ボーンアーチャー,char_239"},
{0x000000F0, "ボーンエリート,char_239"},
{0x000000F1, "ボーンメイジ,char_241"},
{0x000000F2, "イノブタ,char_242"},
{0x000000F3, "スノーボア,char_243"},
{0x000000F4, "フォレストボア,char_244"},
{0x000000F5, "ストーンイノブタ,char_245"},
{0x000000F6, "ウルフ,char_246"},
{0x000000F7, "ブリザードウルフ,char_247"},
{0x000000F8, "ファイアウルフ,char_248"},
{0x000000F9, "ロックハウンド,char_249"},
{0x000000FA, "リザード,char_250"},
{0x000000FB, "ハイリザード,char_251"},
{0x000000FC, "フレアリザード,char_252"},
{0x000000FD, "シェルタートル,char_253"},
{0x000000FE, "ヴォルカタートル,char_254"},
{0x000000FF, "メカタートル,char_255"},
{0x00000100, "ジュエルタートル,char_256"},
{0x00000101, "ランプスマッシャー,char_257"},
{0x00000102, "ビルドスマッシャー,char_258"},
{0x00000103, "ダークナイト,char_259"},
{0x00000104, "マシンナイト,char_260"},
{0x00000105, "ブリックゴーレム,char_261"},
{0x00000106, "マグマゴーレム,char_262"},
{0x00000107, "アイスゴーレム,char_263"},
{0x00000108, "魔竜サラマンドル,char_264"},
{0x00000109, "魔竜グレンデル,char_265"},
{0x0000010A, "魔竜アースグリム,char_266"},
{0x0000010B, "魔竜シュツルム,char_267"},
{0x0000010C, "魔竜ダーイン,char_268"},
{0x0000010D, "魔竜ドヴァリン,char_269"},
{0x0000010E, "魔竜ドゥネイル,char_270"},
{0x0000010F, "魔竜ドゥラスロール,char_271"},
{0x00000110, "魔竜ニーズヘッグ,char_272"},
{0x00000111, "魔竜ムスペル,char_273"},
{0x00000112, "ガンドロイド,char_274"},
{0x00000113, "インターセプター,char_275"},
{0x00000114, "デストロイヤー,char_276"},
{0x00000115, "パイルバンカー,char_277"},
{0x00000116, "オートマタ,char_278"},
{0x00000117, "オートビット,char_279"},
{0x00000118, "クロックワーク,char_280"},
{0x00000119, "コッペリア,char_281"},
{0x0000011A, "ナイトギア,char_282"},
{0x0000011B, "キングギア,char_283"},
{0x0000011C, "ルークギア,char_284"},
{0x0000011D, "スライサー,char_285"},
{0x0000011E, "スプラッシャー,char_286"},
{0x0000011F, "クリーバー,char_287"},
{0x00000120, "ガンパイレーツ,char_288"},
{0x00000121, "ガンパイレーツ,char_289"},
{0x00000122, "ソードパイレーツ,char_290"},
{0x00000123, "ソードパイレーツ,char_291"},
{0x00000124, "ボムパイレーツ,char_292"},
{0x00000125, "ボムパイレーツ,char_293"},
{0x00000126, "爆弾スパイダー,char_294"},
{0x00000127, "ボートパイレーツ,char_295"},
{0x00000128, "オオイノブタ,char_296"},
{0x00000191, "フリード,char_401"},
{0x00000192, "パニス,char_402"},
{0x00000193, "ヴァイオラ,char_403"},
{0x00000194, "シャノン,char_404"},
{0x00000195, "ジンガ,char_405"},
{0x00000196, "アダム,char_406"},
{0x00000197, "キルマリア,char_407"},
{0x00000198, "ベルベット,char_408"},
{0x00000199, "レヴィン,char_409"},
{0x0000019A, "ローナ,char_410"},
{0x000001F5, "ガルゼー,char_501"},
{0x000001F6, "ジム,char_502"},
{0x000001F7, "マイア,char_503"},
{0x000001F8, "アリーゼ,char_504"},
{0x000001F9, "ダイアナ,char_505"},
{0x000001FA, "ドルトン,char_506"},
{0x000001FB, "ロラン,char_507"},
{0x000001FC, "カール,char_508"},
{0x000001FD, "メリー,char_509"},
{0x000001FE, "ハンス,char_510"},
{0x000001FF, "ボリス,char_511"},
{0x00000200, "デボラ,char_512"},
{0x00000201, "ハイラム,char_513"},
{0x00000202, "リアン,char_514"},
{0x00000203, "ダリオ,char_515"},
{0x00000204, "ミルコ,char_516"},
{0x00000205, "ロドリゴ,char_517"},
{0x00000206, "ダニエル,char_518"},
{0x00000207, "ブレンダ,char_519"},
{0x00000208, "ピーター,char_520"},
{0x00000209, "ヒツジ,char_521"},
{0x0000020A, "大きなヒツジ,char_522"},
{0x0000020B, "小さなヒツジ,char_523"},
{0x0000020C, "オスのニワトリ,char_524"},
{0x0000020D, "メスのニワトリ,char_525"},
{0x0000020E, "クープ,char_526"},
{0x0000020F, "バッカス,char_527"},
{0x00000210, "ラナ,char_528"},
{0x00000211, "ケルビム,char_529"},
{0x00000212, "鋼鱗丸,char_530"},
{0x00000213, "鋼刃丸,char_531"},
{0x00000218, "ヴァイオラ,char_536"},
{0x00000219, "箱舟,char_537"},
{0x00000259, "？？？,char_601"},
{0x0000025A, "海賊,char_602"},

        };
 
        public List<string> speakers = new List<string>();
        public List<string> listText = new List<string>();

        public List<string> listNewText = new List<string>();
        public MTP(string mtpName)
        {
            reader = new Br(new FileStream(mtpName,FileMode.Open,FileAccess.Read));
            magic = reader.ReadBytes(4);
            eofOffset = reader.ReadInt32();  
            pfOffset = reader.ReadInt32();
            unk1 = reader.ReadInt32();
            f1Offset = reader.ReadInt32();
            enrOffset = reader.ReadInt32();
            reader.BaseStream.Seek(pfOffset, SeekOrigin.Begin);
            unk2 = reader.ReadInt32();
            count1 = reader.ReadInt32();
            ptrSize = reader.ReadInt32();
            count2 = reader.ReadInt32();
            unk3 = reader.ReadBytes(ptrSize*4);
            for (int i = 0; i < count1; i++)
            {
                ptrOffsets.Add(reader.ReadInt32());
            }
            for (int i = 0; i < count1; i++)
            {
                Info info = new Info();
                switch (ptrSize)
                {
                    case 4:
                        isSpeaker = true;   
                        info.Unk = reader.ReadInt32();
                        info.SpeakerIndex = reader.ReadInt32();
                        info.Offset = reader.ReadInt32();
                        info.Offset1 = reader.ReadInt32();
                        try
                        {
                            speakers.Add(ds[info.SpeakerIndex]);
                        }
                        catch (Exception)
                        {

                            speakers.Add("");
                        }
                       
                        break;
                    case 2:
                        info.Unk = reader.ReadInt32();
                        info.Offset = reader.ReadInt32();
                        break;
                    case 6:
                        info.OffsetList = new List<int>();
                        info.Unk = reader.ReadInt32();
                        info.Offset = reader.ReadInt32();
                        info.OffsetList.Add(reader.ReadInt32());
                        info.OffsetList.Add(reader.ReadInt32());
                        info.OffsetList.Add(reader.ReadInt32());
                        info.OffsetList.Add(reader.ReadInt32());
                        break;
                    default:
                        
                        break;
                }
                infos.Add(info);
            }
            //read to memory
            stringBufferSize = enrOffset - (int)reader.BaseStream.Position + pfOffset;
            stringBuffer = reader.ReadBytes(stringBufferSize);
            enr = reader.ReadBytes(eofOffset - (int)reader.BaseStream.Position);
            eof = reader.ReadBytes(64);
            reader.Close();
            for (int i = 0; i < stringBuffer.Length; i++)
            {
                stringBuffer[i]--;
            }
            reader = new Br(new MemoryStream(stringBuffer));
            for (int i = 0; i < count1; i++)
            {
                Info info = infos[i];
                byte[] binaryString;
                int len;
                string text="";

                if(ptrSize == 6)
                {
                    reader.BaseStream.Seek(info.Offset, SeekOrigin.Begin);
                    len = reader.ReadInt32() & 0xff;
                    if (len == 0)
                    {
                        infos[i].DummySize = true;
                        binaryString = reader.GetBinaryNullTerm();
                    }
                    else
                    {
                        infos[i].DummySize = false;
                        binaryString = reader.ReadBytes(len);


                    }
                    text = Encoding.GetEncoding(932).GetString(binaryString )+ "[s]";
                    for (int j = 0; j < 4; j++)
                    {
                        reader.BaseStream.Seek(info.OffsetList[j], SeekOrigin.Begin);
                        len = reader.ReadInt32() & 0xff;
                        binaryString = reader.ReadBytes(len);

                        text += Encoding.Unicode.GetString(binaryString)+"[s]";
                    }

                }
                else
                {
                    reader.BaseStream.Seek(info.Offset, SeekOrigin.Begin);
                    len = reader.ReadInt32() & 0xff;
                    if (len == 0)
                    {
                        infos[i].DummySize = true;
                        binaryString = reader.GetBinaryNullTerm();
                    }
                    else
                    {
                        infos[i].DummySize = false;
                        binaryString = reader.ReadBytes(len);


                    }
                     text = Encoding.GetEncoding(932).GetString(binaryString);
                }
                listText.Add(text);

            }
            
           
        }
        public void WriteMtp(string txtName,string newMtpName)
        {
            LoadTXt(txtName);
            GenStringBuffer();
            WriteFile(newMtpName);
          
           
           

        }
        private void GenStringBuffer()
        {
            MemoryStream memoryStream = new MemoryStream();
            Bw writer = new Bw(memoryStream);
            int reffPadding = 16-(((ptrSize*4* count2) +(count2*4)+(ptrSize * 4))%16);
            for (int i = 0; i < count1; i++)
            {

                byte[] binaryText;
                int len =0;
                //Console.WriteLine(listNewText[i]);
                if (ptrSize == 6)
                {
					infos[i].Offset = (int)writer.BaseStream.Position;
                    string[] texts = listNewText[i].Replace("\\n","\n").Split(new string[] { "[s]"}, StringSplitOptions.None);
                    binaryText = Encoding.GetEncoding(932).GetBytes(texts[0]);
                    len = binaryText.Length;
                    writer.Write(len);
                    writer.Write(binaryText, 0, len);
                    writer.Write((byte)0);
                    writer.WritePadding(4, 0);
                    for (int j = 0; j < 4; j++)
                    {
                        infos[i].OffsetList[j]=(int)writer.BaseStream.Position;
                        binaryText = Encoding.Unicode.GetBytes(texts[j+1]);
                        len = binaryText.Length;
                        writer.Write(len);
                        writer.Write(binaryText, 0, len);
                        writer.Write((byte)0);
                        writer.WritePadding(4, 0);
                    }
                }
                else
                {
                    infos[i].Offset = (int)writer.BaseStream.Position;
                    binaryText = Encoding.GetEncoding(932).GetBytes(listNewText[i]);
                    len = binaryText.Length - 1;
                    if (infos[i].DummySize)
                    {
                        writer.Write((int)0);
                    }
                    else
                    {
                        writer.Write(len);
                    }

                    writer.Write(binaryText, 0, len);
                    writer.Write((byte)0);
                    writer.WritePadding(4, 0);
                }
            }
            writer.WritePadding(16, reffPadding,(sbyte)-1);
            writer.Flush();
            newStringBuffer = memoryStream.ToArray();
            writer.Close();
            newStringBufferSize = newStringBuffer.Length;
            diffSize = newStringBufferSize - stringBufferSize;
        }
        private void LoadTXt(string name)
        {

            StreamReader reader = new StreamReader(new FileStream(name, FileMode.Open, FileAccess.Read));
            string line = "";
            reader.ReadLine();
            reader.ReadLine();
            for (int i = 0; i < count1; i++)
            {
                string text = "";
                string indexS = reader.ReadLine();
                while (true)
                {
                    line = reader.ReadLine();
                    if (line.StartsWith("----------"))
                    {
                        break;
                    }
                    text += line+"\n";
                }
                //if ((!text.Contains("@"))&(!indexS.Contains("!")))
                //{
                //    
                //    text = Text.Wrap(text);
                //}
                //else
                //{
                //   // Console.WriteLine(  text);
                //}
                listNewText.Add(text);
            }

        }
        private void WriteFile(string name)
        {
            Bw writer = new Bw(new FileStream(name, FileMode.Create, FileAccess.Write));
            //hdr
            writer.Write(magic);
            writer.Write(eofOffset+diffSize);
            writer.Write(pfOffset);
            writer.Write(unk1);
            writer.Write(f1Offset);
            writer.Write(enrOffset+diffSize);
            writer.BaseStream.Seek(pfOffset, SeekOrigin.Begin);
            //f1 hdr
            writer.Write(unk2);
            writer.Write(count1);
            writer.Write(ptrSize);
            writer.Write(count2);
            writer.Write(unk3);
            foreach (int j in ptrOffsets)
            {
                writer.Write(j);
            }
            foreach (Info info in infos)
            {
                switch (ptrSize)
                {
                    case 4:
                        writer.Write(info.Unk);
                        writer.Write(info.SpeakerIndex);
                        writer.Write(info.Offset);
                        writer.Write(info.Offset1);
                        break;
                    case 2:
                        writer.Write(info.Unk);
                        writer.Write(info.Offset);
                        break;
                    case 6:

                        writer.Write(info.Unk);
                        writer.Write(info.Offset);
                        writer.Write(info.OffsetList[0]);
                        writer.Write(info.OffsetList[1]);
                        writer.Write(info.OffsetList[2]);
                        writer.Write(info.OffsetList[3]);
                        
                        break;
                    default:
                        break;
                }
            }
            foreach (byte k in newStringBuffer)
            {
                writer.Write((byte)(k + 1));
            }
            writer.Write(enr);
            writer.Write(eof);
            writer.Flush();
            writer.Close();

        }
        public void WriteTxt(string txtName)
        {
           
            StreamWriter writer = new StreamWriter(new FileStream(txtName, FileMode.Create, FileAccess.Write));
            int i = 0;
            writer.WriteLine(string.Format("[{0,0:d4}]\n*****************",listText.Count));
            foreach (string text in listText)
            {
                if (isSpeaker)
                {
                     writer.Write(string.Format("[{0,0:d8}][{1}]\n", i, speakers[i]));
                    //writer.Write(string.Format("{0}: ",speakers[i]));
                }
                else
                {
                    writer.Write(string.Format("[{0,0:d8}]\n", i));
                }
                
                writer.WriteLine(text);
                writer.WriteLine("--------------------------------");
                i++;
            }
            writer.Close();
        }
    }
}

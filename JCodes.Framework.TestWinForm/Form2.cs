using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JCodes.Framework.TestWinForm
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 抓包 http://content.qlclubs.com/dy-content/api/content/v4/shareContent?dyContentID=3164561
            // 请求包
            /*
             GET http://content.qlclubs.com/dy-content/api/content/v4/shareContent?dyContentID=3164561 HTTP/1.1
            Host: content.qlclubs.com
            Connection: keep-alive
            Upgrade-Insecure-Requests: 1
            User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141 Safari/537.36 Edg/87.0.664.75
            Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,* / *;q=0.8,application/signed-exchange;v=b3;q=0.9
            Accept-Encoding: gzip, deflate
            Accept-Language: zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6
            Cookie: UM_distinctid=1772076a37e72f-04d338122cba92-7d677965-1fa400-1772076a37fa04; CNZZDATA1274569006=1791706746-1611156151-%7C1611156151
             */

            // 正确应答包
            /*
             HTTP/1.1 200 OK
            Server: openresty/1.13.6.2
            Date: Wed, 20 Jan 2021 15:49:44 GMT
            Content-Type: application/json
            Connection: keep-alive
            Access-Control-Allow-Credentials: true
            Access-Control-Allow-Methods: GET,POST,OPTIONS
            Content-Length: 13167

            {"respMessage":"OK",
             * "shareContent":[{"adopt":"1","algoVersion":"","bestComments":[],"chatGroupID":"","collectionTime":"2019-10-28 10:52:20","commentNum":515,
             * "contextText":"\u60F3\u95EE\u4E00\u53E5\u4E3A\u4EC0\u4E48\u4F60\u4EEC\u7537\u4EBA\u5B81\u613F\u82B1\u5341\u51E0\u4E07\u4E70\u4E00\u8F86\u8F66\u90A3\u4E48\u723D\u5FEB\u4E3A\u4EC0\u4E48\u4E0D\u82B1\u5DEE\u4E0D\u591A\u7684*\u5A36\u4E00\u4E2A\u5AB3\u5987\uFF0C\u8F66\u548C\u5AB3\u5987\u6709\u53EF\u6BD4\u6027\u5417[what][what]",
             * "contextType":"2","contextUrl":"","duration":"0","dyContextID":3164561,"fileSize":"0","headOrnamentDown":"","headOrnamentUp":"","headPortraitUrl":"http:\/\/resource.qlclubs.com\/headPortrait\/default\/default16.jpg","images":[],"isLike":"0","isSelected":"7","isV":false,"nickName":"x\u012Bn","p":"41ABEBF94B7ABCDC07BA1EADECA7848239F471740312CC6C19D36B7BAC28A501931D68D045E19F83054515162A389CDB","pixelHeight":0,"pixelWidth":0,"playAdvTime":5,"praiseNum":11849,"publisherDyID":"7820116","remark":"","sex":"","shareNum":0,"shareUrl":"http:\/\/share.qlclubs.com\/share\/index.html#\/dyContentID=aTJFN0xHY0hQSm1pQzlqN2xGUG9EZz09","source":"","tag":"","topicId":0,"topicTitle":"","trampleNum":1,"uploadDate":"2019-10-28 10:52:20","videoDisplay":"","videoPlayCount":0,"videos":[],"vipInfo":null}],"comments":[],"dyContexts":[{"adopt":"1","algoVersion":"","bestComments":[],"chatGroupID":"","collectionTime":"2020-07-05 00:05:44","commentNum":28,"contextText":"\u7537\u5B50\u7A7F\u8D8A\u5230\u53E4\u4EE3\uFF0C\u6210\u4E3A\u4E86\u7EDD\u4E16\u9AD8\u624B\uFF1F","contextType":"4","contextUrl":"http:\/\/resource.qlclubs.com\/videoSample\/e0c4f156063949a384f68473364abc0c.mp4","duration":"127","dyContextID":3213911,"fileSize":"0","headOrnamentDown":"","headOrnamentUp":"","headPortraitUrl":"http:\/\/resource.qlclubs.com\/headPortrait\/default\/default19.jpg","images":[{"createType":2,"displayUrl":"","downloadUrl":"","duration":0,"height":540,"mark":0,"mediaId":4395690,"mediaType":1,"tags":"","url":"http:\/\/resource.qlclubs.com\/jpg\/730f3f8de3f8473cab0717933fc341dd.jpg","width":880}],"isLike":"0","isSelected":"7","isV":false,"nickName":"\u8D6B\u9633","p":"2318FC94485524021F9E16466E0A0E89031A8C0F0CC1BF353EBAD854D8A0E1025D0B30C45560A967503F5B820410FD1C","pixelHeight":540,"pixelWidth":880,"playAdvTime":5,"praiseNum":652,"publisherDyID":"4297999","remark":"","sex":"1","shareNum":0,"shareUrl":"http:\/\/share.qlclubs.com\/share\/index.html#\/dyContentID=Z0lRWnlnWUZoU1ZxYlpCR1ZmQmtidz09","source":"","tag":"","topicId":0,"topicTitle":"","trampleNum":0,"uploadDate":"2020-07-05 00:05:44","videoDisplay":"http:\/\/resource.qlclubs.com\/jpg\/730f3f8de3f8473cab0717933fc341dd.jpg","videoPlayCount":9780,"videos":[{"createType":2,"displayUrl":"http:\/\/resource.qlclubs.com\/jpg\/730f3f8de3f8473cab0717933fc341dd.jpg","downloadUrl":"","duration":127,"height":540,"mark":0,"mediaId":4395691,"mediaType":2,"tags":"","url":"http:\/\/resource.qlclubs.com\/videoSample\/e0c4f156063949a384f68473364abc0c.mp4","width":880}],"vipInfo":null},{"adopt":"1","algoVersion":"","bestComments":[],"chatGroupID":"","collectionTime":"2020-08-05 14:08:09","commentNum":24,"contextText":"\u53D1\u4E2A\u6BB5\u5B50","contextType":"4","contextUrl":"http:\/\/resource.qlclubs.com\/videoSample\/fe4f917dd8634a09a02c72cd2c7ed703.mp4","duration":"10","dyContextID":3223799,"fileSize":"0","headOrnamentDown":"http:\/\/resource.qlclubs.com\/club\/bottom_ordinary.png","headOrnamentUp":"http:\/\/resource.qlclubs.com\/club\/qufu_ordinary.png","headPortraitUrl":"http:\/\/resource.qlclubs.com\/headPortrait\/default\/73.jpg","images":[{"createType":2,"displayUrl":"","downloadUrl":"","duration":0,"height":1024,"mark":0,"mediaId":4407956,"mediaType":1,"tags":"","url":"http:\/\/resource.qlclubs.com\/jpg\/9b12436c012646a68675fc8670661fdf.jpg","width":576}],"isLike":"0","isSelected":"7","isV":false,"nickName":"\u751F\u4EA7\u961F\u7684\u9A74","p":"6566CE087F15786378896F873B5DEA45240E1890CC9E69B449B49AAA1071FC8D19C5480EC280E58A22B6EFC38E26DF63","pixelHeight":1024,"pixelWidth":576,"playAdvTime":5,"praiseNum":614,"publisherDyID":"8660024","remark":"","sex":"1","shareNum":0,"shareUrl":"http:\/\/share.qlclubs.com\/share\/index.html#\/dyContentID=OUcvOWZFYmFkdlg0OFZQZmlSTjFwZz09","source":"","tag":"","topicId":0,"topicTitle":"","trampleNum":0,"uploadDate":"2020-08-05 14:08:09","videoDisplay":"http:\/\/resource.qlclubs.com\/jpg\/9b12436c012646a68675fc8670661fdf.jpg","videoPlayCount":9210,"videos":[{"createType":2,"displayUrl":"http:\/\/resource.qlclubs.com\/jpg\/9b12436c012646a68675fc8670661fdf.jpg","downloadUrl":"","duration":10,"height":1024,"mark":0,"mediaId":4407957,"mediaType":2,"tags":"","url":"http:\/\/resource.qlclubs.com\/videoSample\/fe4f917dd8634a09a02c72cd2c7ed703.mp4","width":576}],"vipInfo":null},{"adopt":"1","algoVersion":"","bestComments":[],"chatGroupID":"","collectionTime":"2019-10-31 16:26:19","commentNum":25,"contextText":"\u795E\u8BC4\u5C31\u662F\u6807\u9898","contextType":"4","contextUrl":"http:\/\/resource.qlclubs.com\/videoSample\/2b12f51eceb04d5aaf077da5c3c3c7cd.mp4","duration":"115","dyContextID":3171973,"fileSize":"0","headOrnamentDown":"","headOrnamentUp":"","headPortraitUrl":"http:\/\/resource.qlclubs.com\/headPortrait\/default\/default25.jpg","images":[{"createType":2,"displayUrl":"","downloadUrl":"","duration":0,"height":540,"mark":0,"mediaId":4333364,"mediaType":1,"tags":"","url":"http:\/\/resource.qlclubs.com\/jpg\/4eb1356e3aad4bb18e55af4ad153e09d.jpg","width":960}],"isLike":"0","isSelected":"7","isV":false,"nickName":"\u8336\u9702","p":"E5A71D9C877458A28A215A1504CAD59E2B17165E54A690BE7D3A8A38C94CB221632A61D3DD40ABDC1DA81E9445F02A3A","pixelHeight":540,"pixelWidth":960,"playAdvTime":5,"praiseNum":583,"publisherDyID":"4628545","remark":"","sex":"1","shareNum":0,"shareUrl":"http:\/\/share.qlclubs.com\/share\/index.html#\/dyContentID=d3prNzZxRjU3VzlFSUp1QXplazZwdz09","source":"","tag":"","topicId":0,"topicTitle":"","trampleNum":0,"uploadDate":"2019-10-31 16:26:19","videoDisplay":"http:\/\/resource.qlclubs.com\/jpg\/4eb1356e3aad4bb18e55af4ad153e09d.jpg","videoPlayCount":8746,"videos":[{"createType":2,"displayUrl":"http:\/\/resource.qlclubs.com\/jpg\/4eb1356e3aad4bb18e55af4ad153e09d.jpg","downloadUrl":"","duration":115,"height":540,"mark":0,"mediaId":4333365,"mediaType":2,"tags":"","url":"http:\/\/resource.qlclubs.com\/videoSample\/2b12f51eceb04d5aaf077da5c3c3c7cd.mp4","width":960}],"vipInfo":null},{"adopt":"1","algoVersion":"","bestComments":[],"chatGroupID":"","collectionTime":"2020-07-06 18:30:03","commentNum":28,"contextText":"\u54C8\u54C8\u54C8\u592A\u5012\u9709\u4E86[\u6342\u8138]","contextType":"4","contextUrl":"http:\/\/resource.qlclubs.com\/videoSample\/b3ee430155bb4342ad81e416f204ef2a.mp4","duration":"14","dyContextID":3214750,"fileSize":"0","headOrnamentDown":"","headOrnamentUp":"","headPortraitUrl":"http:\/\/resource.qlclubs.com\/headPortrait\/default\/default44.jpg","images":[{"createType":2,"displayUrl":"","downloadUrl":"","duration":0,"height":774,"mark":0,"mediaId":4397374,"mediaType":1,"tags":"","url":"http:\/\/resource.qlclubs.com\/jpg\/d39f195ef47344c196d173ce0838139d.jpg","width":540}],"isLike":"0","isSelected":"7","isV":false,"nickName":"\u68A6\u4E00\u573A","p":"106CAFC853E85C2B4C6DBDC0F496995C4E8D3FE6366B613C314811CABD21184475C969690C7C08A1481CB9842B3DB91A","pixelHeight":774,"pixelWidth":540,"playAdvTime":5,"praiseNum":653,"publisherDyID":"0841184","remark":"","sex":"1","shareNum":0,"shareUrl":"http:\/\/share.qlclubs.com\/share\/index.html#\/dyContentID=d3M3a1ZiT2llemRqV1A4bHZsRzBqQT09","source":"","tag":"","topicId":0,"topicTitle":"","trampleNum":1,"uploadDate":"2020-07-06 18:30:03","videoDisplay":"http:\/\/resource.qlclubs.com\/jpg\/d39f195ef47344c196d173ce0838139d.jpg","videoPlayCount":9795,"videos":[{"createType":2,"displayUrl":"http:\/\/resource.qlclubs.com\/jpg\/d39f195ef47344c196d173ce0838139d.jpg","downloadUrl":"","duration":14,"height":774,"mark":0,"mediaId":4397375,"mediaType":2,"tags":"","url":"http:\/\/resource.qlclubs.com\/videoSample\/b3ee430155bb4342ad81e416f204ef2a.mp4","width":540}],"vipInfo":null},{"adopt":"1","algoVersion":"","bestComments":[],"chatGroupID":"","collectionTime":"2020-07-31 21:57:08","commentNum":27,"contextText":"\u597D\u795E\u5947\u7684\u4E00\u7F15\u9752\u70DF","contextType":"4","contextUrl":"http:\/\/resource.qlclubs.com\/videoSample\/d05d05f7b7fa4120ab9eb4878b61ee9d.mp4","duration":"13","dyContextID":3222182,"fileSize":"0","headOrnamentDown":"","headOrnamentUp":"","headPortraitUrl":"http:\/\/resource.qlclubs.com\/headPortrait\/default\/default5.jpg","images":[{"createType":2,"displayUrl":"","downloadUrl":"","duration":0,"height":960,"mark":0,"mediaId":4405812,"mediaType":1,"tags":"","url":"http:\/\/resource.qlclubs.com\/jpg\/bcec4b4003e64030be45752481a32197.jpg","width":540}],"isLike":"0","isSelected":"7","isV":false,"nickName":"\u786C\u786C","p":"8762C80619528C4A9AB939C990F6D2D6EA6F7BB3EAA1131C443E1F60AB2FCE456F64B4E27C6FA0CDC319BACE23C65E9D","pixelHeight":960,"pixelWidth":540,"playAdvTime":5,"praiseNum":630,"publisherDyID":"2642825","remark":"","sex":"1","shareNum":0,"shareUrl":"http:\/\/share.qlclubs.com\/share\/index.html#\/dyContentID=eWFXVzBMNnk5MFQxQlFaYlpjNVMzZz09","source":"","tag":"","topicId":0,"topicTitle":"","trampleNum":1,"uploadDate":"2020-07-31 21:57:08","videoDisplay":"http:\/\/resource.qlclubs.com\/jpg\/bcec4b4003e64030be45752481a32197.jpg","videoPlayCount":9450,"videos":[{"createType":2,"displayUrl":"http:\/\/resource.qlclubs.com\/jpg\/bcec4b4003e64030be45752481a32197.jpg","downloadUrl":"","duration":13,"height":960,"mark":0,"mediaId":4405813,"mediaType":2,"tags":"","url":"http:\/\/resource.qlclubs.com\/videoSample\/d05d05f7b7fa4120ab9eb4878b61ee9d.mp4","width":540}],"vipInfo":null},{"adopt":"1","algoVersion":"","bestComments":[],"chatGroupID":"","collectionTime":"2019-10-14 17:06:26","commentNum":28,"contextText":"\u8FD9\u662F\u5728\u5E72\u561B\uFF1F\u4E0D\u61C2","contextType":"4","contextUrl":"http:\/\/resource.qlclubs.com\/nomark04\/videoSample\/ea5167f80c424d3fba00b741ece6fadf.mp4","duration":"14","dyContextID":3137139,"fileSize":"0","headOrnamentDown":"","headOrnamentUp":"","headPortraitUrl":"http:\/\/resource.qlclubs.com\/headPortrait\/default\/default5.jpg","images":[{"createType":2,"displayUrl":"","downloadUrl":"","duration":0,"height":960,"mark":0,"mediaId":4296540,"mediaType":1,"tags":"","url":"http:\/\/resource.qlclubs.com\/jpg\/76f3d2efaa3d40da95155b5827937bb9.mp4","width":540}],"isLike":"0","isSelected":"7","isV":false,"nickName":"\u97F3\u76F2","p":"106CAFC853E85C2B4C6DBDC0F496995C4E8D3FE6366B613C314811CABD211844EFD95C767FE526B4DF7EEE5D481B0154","pixelHeight":960,"pixelWidth":540,"playAdvTime":5,"praiseNum":653,"publisherDyID":"2130485","remark":"","sex":"","shareNum":0,"shareUrl":"http:\/\/share.qlclubs.com\/share\/index.html#\/dyContentID=Nk5JbHZkUVJsMjNyQ3E0OU1nSGNnQT09","source":"","tag":"","topicId":0,"topicTitle":"","trampleNum":1,"uploadDate":"2019-10-14 17:06:26","videoDisplay":"http:\/\/resource.qlclubs.com\/nomark04\/videoSample\/ea5167f80c424d3fba00b741ece6fadf.jpg","videoPlayCount":9796,"videos":[{"createType":2,"displayUrl":"http:\/\/resource.qlclubs.com\/nomark04\/videoSample\/ea5167f80c424d3fba00b741ece6fadf.jpg","downloadUrl":"","duration":14,"height":960,"mark":0,"mediaId":4296541,"mediaType":2,"tags":"","url":"http:\/\/resource.qlclubs.com\/nomark04\/videoSample\/ea5167f80c424d3fba00b741ece6fadf.mp4","width":540}],"vipInfo":null},{"adopt":"1","algoVersion":"","bestComments":[],"chatGroupID":"","collectionTime":"2020-07-02 23:50:21","commentNum":27,"contextText":"\u90FD\u77E5\u9053\u4F60\u4EEC\u4E3A\u4EC0\u4E48\u5750\u6700\u540E\u4E00\u6392\u4E86\u5427\u3002","contextType":"4","contextUrl":"http:\/\/resource.qlclubs.com\/videoSample\/a81e801a5fe7409eaa541b48510b7feb.mp4","duration":"124","dyContextID":3212823,"fileSize":"0","headOrnamentDown":"","headOrnamentUp":"","headPortraitUrl":"http:\/\/resource.qlclubs.com\/headPortrait\/default\/default46.jpg","images":[{"createType":2,"displayUrl":"","downloadUrl":"","duration":0,"height":960,"mark":0,"mediaId":4393514,"mediaType":1,"tags":"","url":"http:\/\/resource.qlclubs.com\/jpg\/c30ff49384234567b8fbc790c35e487b.jpg","width":540}],"isLike":"0","isSelected":"7","isV":false,"nickName":"GanYuan.","p":"DA5A00BB3DFFB80E8DC02D691B503B1024C81E4D3F0EC9A9F38614610139716285495E267485A94530ED0DA41197E7E8","pixelHeight":960,"pixelWidth":540,"playAdvTime":5,"praiseNum":631,"publisherDyID":"7643146","remark":"","sex":"","shareNum":0,"shareUrl":"http:\/\/share.qlclubs.com\/share\/index.html#\/dyContentID=MmZJSEF0bWZ4NEwvVnBvWHdBVEcwZz09","source":"","tag":"","topicId":0,"topicTitle":"","trampleNum":2,"uploadDate":"2020-07-02 23:50:21","videoDisplay":"http:\/\/resource.qlclubs.com\/jpg\/c30ff49384234567b8fbc790c35e487b.jpg","videoPlayCount":9465,"videos":[{"createType":2,"displayUrl":"http:\/\/resource.qlclubs.com\/jpg\/c30ff49384234567b8fbc790c35e487b.jpg","downloadUrl":"","duration":124,"height":960,"mark":0,"mediaId":4393515,"mediaType":2,"tags":"","url":"http:\/\/resource.qlclubs.com\/videoSample\/a81e801a5fe7409eaa541b48510b7feb.mp4","width":540}],"vipInfo":null}],"respCode":"0"}
             */

            // 没有数据应答包
            /*HTTP/1.1 200 OK
            Server: openresty/1.13.6.2
            Date: Wed, 20 Jan 2021 15:56:04 GMT
            Content-Type: application/json
            Connection: keep-alive
            Access-Control-Allow-Credentials: true
            Access-Control-Allow-Methods: GET,POST,OPTIONS
            Content-Length: 12503

            {"respMessage":"OK","shareContent":[null],
             * "comments":[],"dyContexts":[{"adopt":"1","algoVersion":"","bestComments":[],"chatGroupID":"","collectionTime":"2019-10-14 09:45:53","commentNum":283,"contextText":"\u8FD9\u7F51\u6492\u5F97\u771F\u6F02\u4EAE\u2026\u2026","contextType":"4","contextUrl":"http:\/\/resource.qlclubs.com\/nomark04\/videoSample\/fac9de344af348aebd529bb5be5abc03.mp4","duration":"16","dyContextID":3133542,"fileSize":"0","headOrnamentDown":"","headOrnamentUp":"","headPortraitUrl":"http:\/\/resource.qlclubs.com\/headPortrait\/default\/default14.jpg","images":[{"createType":2,"displayUrl":"","downloadUrl":"","duration":0,"height":600,"mark":0,"mediaId":4289168,"mediaType":1,"tags":"","url":"http:\/\/resource.qlclubs.com\/jpg\/ec15fdc268bd4ab49f2ffc1a0c5f061a.mp4","width":480}],"isLike":"0","isSelected":"7","isV":false,"nickName":"\u4E1C","p":"85A26331E07DE9741DAFFC58F770FF8D27D0D25C3AC59CD7726B47333C41620604C05C3400377AF6F44A572A1818E3BF","pixelHeight":600,"pixelWidth":480,"playAdvTime":5,"praiseNum":6516,"publisherDyID":"7771994","remark":"","sex":"","shareNum":0,"shareUrl":"http:\/\/share.qlclubs.com\/share\/index.html#\/dyContentID=anc2S1B4RzNtTE82bDAwVXBZYzJXUT09","source":"","tag":"","topicId":0,"topicTitle":"","trampleNum":0,"uploadDate":"2019-10-14 09:45:53","videoDisplay":"http:\/\/resource.qlclubs.com\/nomark04\/videoSample\/fac9de344af348aebd529bb5be5abc03.jpg","videoPlayCount":97740,"videos":[{"createType":2,"displayUrl":"http:\/\/resource.qlclubs.com\/nomark04\/videoSample\/fac9de344af348aebd529bb5be5abc03.jpg","downloadUrl":"","duration":16,"height":600,"mark":0,"mediaId":4289169,"mediaType":2,"tags":"","url":"http:\/\/resource.qlclubs.com\/nomark04\/videoSample\/fac9de344af348aebd529bb5be5abc03.mp4","width":480}],"vipInfo":null},{"adopt":"1","algoVersion":"","bestComments":[],"chatGroupID":"","collectionTime":"2020-07-04 19:29:34","commentNum":245,"contextText":"\u8FD9\u4E2A\u548B\u5F04\u54A7[\u6342\u8138]","contextType":"4","contextUrl":"http:\/\/resource.qlclubs.com\/videoSample\/6309806a292040cc8c83ba43380e1242.mp4","duration":"25","dyContextID":3213625,"fileSize":"0","headOrnamentDown":"","headOrnamentUp":"","headPortraitUrl":"http:\/\/resource.qlclubs.com\/headPortrait\/default\/default1.jpg","images":[{"createType":2,"displayUrl":"","downloadUrl":"","duration":0,"height":518,"mark":0,"mediaId":4395118,"mediaType":1,"tags":"","url":"http:\/\/resource.qlclubs.com\/jpg\/2de6177db3184aeeadaaa7d39541582c.jpg","width":574}],"isLike":"0","isSelected":"7","isV":false,"nickName":"\u504F\u6267","p":"6432A7480A7DD508D1B2897CA9F239DE684B28C3FEF2B1F2DF498CF80C6316DA54E9C757D12776788284BB26FD11662F","pixelHeight":518,"pixelWidth":574,"playAdvTime":5,"praiseNum":5665,"publisherDyID":"4018621","remark":"","sex":"","shareNum":0,"shareUrl":"http:\/\/share.qlclubs.com\/share\/index.html#\/dyContentID=UDRCZHFUTWtWSDJlaVJWUXc3WmZBQT09","source":"","tag":"","topicId":0,"topicTitle":"","trampleNum":2,"uploadDate":"2020-07-04 19:29:34","videoDisplay":"http:\/\/resource.qlclubs.com\/jpg\/2de6177db3184aeeadaaa7d39541582c.jpg","videoPlayCount":84975,"videos":[{"createType":2,"displayUrl":"http:\/\/resource.qlclubs.com\/jpg\/2de6177db3184aeeadaaa7d39541582c.jpg","downloadUrl":"","duration":25,"height":518,"mark":0,"mediaId":4395119,"mediaType":2,"tags":"","url":"http:\/\/resource.qlclubs.com\/videoSample\/6309806a292040cc8c83ba43380e1242.mp4","width":574}],"vipInfo":null},{"adopt":"1","algoVersion":"","bestComments":[],"chatGroupID":"","collectionTime":"2019-01-10 22:43:18","commentNum":71,"contextText":"","contextType":"4","contextUrl":"http:\/\/resource.qlclubs.com\/videoSample\/bacafb9973d647c6ad8d4bd6e3b8d905.mp4","duration":"19","dyContextID":2393986,"fileSize":"0","headOrnamentDown":"","headOrnamentUp":"","headPortraitUrl":"http:\/\/resource.qlclubs.com\/headPortrait\/default\/default10.jpg","images":[{"createType":2,"displayUrl":"","downloadUrl":"","duration":0,"height":960,"mark":0,"mediaId":2672351,"mediaType":1,"tags":"","url":"http:\/\/resource.qlclubs.com\/jpg\/6d58e643f7cf4045b1b07ea6bae661d9.jpeg","width":540}],"isLike":"0","isSelected":"7","isV":false,"nickName":"\u6BB5\u53CB8716270","p":"87F4E0FB6F3A28D56A4ED3A86AF09081D0847799A7E8B9F0F4E5D89D286641E28F07DB705D492CF31B4B085D02F43795","pixelHeight":960,"pixelWidth":540,"playAdvTime":5,"praiseNum":4106,"publisherDyID":"8716270","remark":"","sex":"1","shareNum":0,"shareUrl":"http:\/\/share.qlclubs.com\/share\/index.html#\/dyContentID=bzdMVGdLdEJOejBVU2xubTBoR3Vpdz09","source":"","tag":"","topicId":91,"topicTitle":"\u5468\u661F\u9A70","trampleNum":0,"uploadDate":"2019-01-10 22:43:18","videoDisplay":"http:\/\/resource.qlclubs.com\/nomark04\/videoSample\/bacafb9973d647c6ad8d4bd6e3b8d905.jpg","videoPlayCount":78240,"videos":[{"createType":2,"displayUrl":"http:\/\/resource.qlclubs.com\/nomark04\/videoSample\/bacafb9973d647c6ad8d4bd6e3b8d905.jpg","downloadUrl":"http:\/\/resource.qlclubs.com\/watermark02\/videoSample\/bacafb9973d647c6ad8d4bd6e3b8d905.mp4","duration":19,"height":960,"mark":0,"mediaId":2672352,"mediaType":2,"tags":"","url":"http:\/\/resource.qlclubs.com\/videoSample\/bacafb9973d647c6ad8d4bd6e3b8d905.mp4","width":540}],"vipInfo":null},{"adopt":"1","algoVersion":"","bestComments":[],"chatGroupID":"","collectionTime":"2019-11-06 10:57:58","commentNum":274,"contextText":"\u6BB5\u53CB\u4EEC\u548C\u5973\u670B\u53CB\u7B2C\u4E00\u6B21\u51FA\u53BB\uFF0C\u4E0A\u6765\u5C31\u5531\u4E86\u4E00\u9996\u4F53\u9762\uFF0C\u6211\u5E94\u8BE5\u5531\u4EC0\u4E48\u6B4C\u56DE\u5979\uFF1F\u5728\u7EBF\u7B49\uFF0C\u6025\u6025\u6025\u6025","contextType":"4","contextUrl":"http:\/\/resource.qlclubs.com\/videoSample\/eaa4c025f6ea4a02b2be62908852000a.mp4","duration":"7","dyContextID":3181609,"fileSize":"0","headOrnamentDown":"","headOrnamentUp":"","headPortraitUrl":"http:\/\/resource.qlclubs.com\/headPortrait\/default\/default55.jpg","images":[{"createType":2,"displayUrl":"","downloadUrl":"","duration":0,"height":960,"mark":0,"mediaId":4345066,"mediaType":1,"tags":"","url":"http:\/\/resource.qlclubs.com\/jpg\/2271eb3b9b254aea91f02ca4699d09d1.jpg","width":540}],"isLike":"0","isSelected":"7","isV":false,"nickName":"\u66F9\u4F0A\u4FCA","p":"40F2C9B649F836923B3CCB3F81CD0045C3E4AF1AB151BAF21D89F7ED3BDBBB6F233E9E064A8ACB30BE52868DA4AE7CFB","pixelHeight":960,"pixelWidth":540,"playAdvTime":5,"praiseNum":6302,"publisherDyID":"4074055","remark":"","sex":"0","shareNum":0,"shareUrl":"http:\/\/share.qlclubs.com\/share\/index.html#\/dyContentID=RHJsc0ZrUDMwZ3BtOGtWaEJFM0V5Zz09","source":"","tag":"","topicId":0,"topicTitle":"","trampleNum":1,"uploadDate":"2019-11-06 10:57:58","videoDisplay":"http:\/\/resource.qlclubs.com\/jpg\/2271eb3b9b254aea91f02ca4699d09d1.jpg","videoPlayCount":94530,"videos":[{"createType":2,"displayUrl":"http:\/\/resource.qlclubs.com\/jpg\/2271eb3b9b254aea91f02ca4699d09d1.jpg","downloadUrl":"","duration":7,"height":960,"mark":0,"mediaId":4345067,"mediaType":2,"tags":"","url":"http:\/\/resource.qlclubs.com\/videoSample\/eaa4c025f6ea4a02b2be62908852000a.mp4","width":540}],"vipInfo":null},{"adopt":"1","algoVersion":"","bestComments":[],"chatGroupID":"","collectionTime":"2019-10-27 15:03:27","commentNum":278,"contextText":"\u7EC8\u4E8E\u53D1\u73B0\u6D77\u5E95\u635E\u7684\u7F3A\u70B9\u4E86\uFF01\uFF01\uFF01\u611F\u89C9\u5F88\u6709\u6210\u5C31\u611F","contextType":"4","contextUrl":"http:\/\/resource.qlclubs.com\/videoSample\/cb93983907f7433bbe0f5fa72d61ae25.mp4","duration":"13","dyContextID":3163421,"fileSize":"0","headOrnamentDown":"","headOrnamentUp":"","headPortraitUrl":"http:\/\/resource.qlclubs.com\/headPortrait\/default\/default21.jpg","images":[{"createType":2,"displayUrl":"","downloadUrl":"","duration":0,"height":960,"mark":0,"mediaId":4323065,"mediaType":1,"tags":"","url":"http:\/\/resource.qlclubs.com\/jpg\/97f51c1a12ce4a349dde2b4a5c739a6d.jpg","width":540}],"isLike":"0","isSelected":"7","isV":false,"nickName":"(o\uFFE3\u0414\uFFE3)","p":"8762C80619528C4A9AB939C990F6D2D6EA6F7BB3EAA1131C443E1F60AB2FCE458E1AFB3E851C4207CA4A89F9B83B2BF1","pixelHeight":960,"pixelWidth":540,"playAdvTime":5,"praiseNum":6395,"publisherDyID":"1696161","remark":"","sex":"1","shareNum":0,"shareUrl":"http:\/\/share.qlclubs.com\/share\/index.html#\/dyContentID=dzBSMGdFRGpETWFpSFRjVWVQT3pGdz09","source":"","tag":"","topicId":0,"topicTitle":"","trampleNum":0,"uploadDate":"2019-10-27 15:03:27","videoDisplay":"http:\/\/resource.qlclubs.com\/jpg\/97f51c1a12ce4a349dde2b4a5c739a6d.jpg","videoPlayCount":95925,"videos":[{"createType":2,"displayUrl":"http:\/\/resource.qlclubs.com\/jpg\/97f51c1a12ce4a349dde2b4a5c739a6d.jpg","downloadUrl":"","duration":13,"height":960,"mark":0,"mediaId":4323066,"mediaType":2,"tags":"","url":"http:\/\/resource.qlclubs.com\/videoSample\/cb93983907f7433bbe0f5fa72d61ae25.mp4","width":540}],"vipInfo":null},{"adopt":"1","algoVersion":"","bestComments":[],"chatGroupID":"","collectionTime":"2019-11-06 09:35:50","commentNum":274,"contextText":"\u6700\u70E6\u6709\u5973\u751F\u6A21\u4EFF\u8FD9\u79CD\u821E\u4E86\uFF0C\u771F\u96BE\u770B[\u7075\u5149\u4E00\u95EA][\u7075\u5149\u4E00\u95EA][\u7075\u5149\u4E00\u95EA]","contextType":"4","contextUrl":"http:\/\/resource.qlclubs.com\/videoSample\/bd40490d3fbe4fd1b8cd75cc4b742590.mp4","duration":"13","dyContextID":3181442,"fileSize":"0","headOrnamentDown":"","headOrnamentUp":"","headPortraitUrl":"http:\/\/resource.qlclubs.com\/headPortrait\/default\/default1.jpg","images":[{"createType":2,"displayUrl":"","downloadUrl":"","duration":0,"height":960,"mark":0,"mediaId":4344816,"mediaType":1,"tags":"","url":"http:\/\/resource.qlclubs.com\/jpg\/4b61aaf1b20e4208a9a361fc063f8a05.jpg","width":540}],"isLike":"0","isSelected":"7","isV":false,"nickName":"\u957F\u4E45","p":"8762C80619528C4A9AB939C990F6D2D6EA6F7BB3EAA1131C443E1F60AB2FCE45E06D0020171E400A8C4F22222E7B3C99","pixelHeight":960,"pixelWidth":540,"playAdvTime":5,"praiseNum":6303,"publisherDyID":"8498821","remark":"","sex":"","shareNum":0,"shareUrl":"http:\/\/share.qlclubs.com\/share\/index.html#\/dyContentID=V1pITXQxakowU0I5elhhL2Z0UVFEZz09","source":"","tag":"","topicId":0,"topicTitle":"","trampleNum":0,"uploadDate":"2019-11-06 09:35:50","videoDisplay":"http:\/\/resource.qlclubs.com\/jpg\/4b61aaf1b20e4208a9a361fc063f8a05.jpg","videoPlayCount":94545,"videos":[{"createType":2,"displayUrl":"http:\/\/resource.qlclubs.com\/jpg\/4b61aaf1b20e4208a9a361fc063f8a05.jpg","downloadUrl":"","duration":13,"height":960,"mark":0,"mediaId":4344817,"mediaType":2,"tags":"","url":"http:\/\/resource.qlclubs.com\/videoSample\/bd40490d3fbe4fd1b8cd75cc4b742590.mp4","width":540}],"vipInfo":null},{"adopt":"1","algoVersion":"","bestComments":[],"chatGroupID":"","collectionTime":"2019-11-13 15:31:57","commentNum":274,"contextText":"\u65B0\u5A18\u5E26\u7740\u5B69\u5B50\u51FA\u5AC1\uFF0C\u5728\u5A5A\u793C\u4E0A\u524D\u592B\u5F53\u4F34\u90CE\u5E2E\u5979\u62B1\u5B69\u5B50\uFF0C\u524D\u592B\u7684\u73B0\u4EFB\u8001\u5A46\u7ED9\u5979\u5F53\u4F34\u5A18\uFF0C\u4F60\u60F3\u77E5\u9053\u8FD9\u662F\u4E3A\u4EC0\u4E48\u5417\uFF1F","contextType":"4","contextUrl":"http:\/\/resource.qlclubs.com\/videoSample\/478e2b9a5b9444ef89f0916f9d5c3828.mp4","duration":"10","dyContextID":3192611,"fileSize":"0","headOrnamentDown":"","headOrnamentUp":"","headPortraitUrl":"http:\/\/resource.qlclubs.com\/headPortrait\/default\/default21.jpg","images":[{"createType":2,"displayUrl":"","downloadUrl":"","duration":0,"height":972,"mark":0,"mediaId":4363251,"mediaType":1,"tags":"","url":"http:\/\/resource.qlclubs.com\/jpg\/dbfb3aafe1df4e7ebf461a19acbd71f7.jpg","width":540}],"isLike":"0","isSelected":"7","isV":false,"nickName":"\u77F3","p":"6566CE087F15786378896F873B5DEA45240E1890CC9E69B449B49AAA1071FC8DEF8DAF235DB8FCB9F26B14350678390C","pixelHeight":972,"pixelWidth":540,"playAdvTime":5,"praiseNum":6303,"publisherDyID":"9780321","remark":"","sex":"1","shareNum":0,"shareUrl":"http:\/\/share.qlclubs.com\/share\/index.html#\/dyContentID=ck9CZWxNNFdkVzlldFNHaVN3U2lzZz09","source":"","tag":"","topicId":0,"topicTitle":"","trampleNum":0,"uploadDate":"2019-11-13 15:31:57","videoDisplay":"http:\/\/resource.qlclubs.com\/jpg\/dbfb3aafe1df4e7ebf461a19acbd71f7.jpg","videoPlayCount":94545,"videos":[{"createType":2,"displayUrl":"http:\/\/resource.qlclubs.com\/jpg\/dbfb3aafe1df4e7ebf461a19acbd71f7.jpg","downloadUrl":"","duration":10,"height":972,"mark":0,"mediaId":4363252,"mediaType":2,"tags":"","url":"http:\/\/resource.qlclubs.com\/videoSample\/478e2b9a5b9444ef89f0916f9d5c3828.mp4","width":540}],"vipInfo":null}],"respCode":"0"}
             * 
             */
            for (Int32 i = 3164561; i >= 0; i--)
            {
                string url = "http://content.qlclubs.com/dy-content/api/content/v4/shareContent?dyContentID=" + i;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "GET";
                request.Host = "content.qlclubs.com";
                //request.Connection = "keep-alive";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141 Safari/537.36 Edg/87.0.664.75";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
                request.Headers.Add("Cache-Control", "max-age=0");
                request.Headers.Add("Upgrade-Insecure-Requests", "1");

                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        // 请求成功的状态码：200
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            using (Stream stream = response.GetResponseStream())
                            {
                                using (var zipStream =
                                    new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Decompress))
                                {
                                    Encoding enc = GetEncoding(url);
                                    using (StreamReader reader = new System.IO.StreamReader(zipStream, enc))
                                    {
                                        string html = reader.ReadToEnd();
                                        JObject jo = (JObject)JsonConvert.DeserializeObject(html);

                                        String shareContent = jo["shareContent"].ToString();

                                        if (!string.IsNullOrEmpty(shareContent) && shareContent != "[\r\n  null\r\n]")
                                        {
                                            //LogHelper.WriteLog(LogLevel.LOG_LEVEL_INFO, jo["shareContent"][0]["contextText"].ToString(), typeof(Form1));
                                            if (jo["shareContent"][0]["contextText"].ToString().Length > 10)
                                            {
                                                JokeInfo jokeInfo = new JokeInfo();
                                                jokeInfo.CreatorId = 1;
                                                jokeInfo.CreatorTime = DateTimeHelper.GetServerDateTime2();
                                                jokeInfo.Introduce = jo["shareContent"][0]["contextText"].ToString();
                                                jokeInfo.NumLen = i;
                                                BLLFactory<Joke>.Instance.Insert(jokeInfo);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, string.Format("第{1}条数据，服务器返回错误：{0}", response.StatusCode, i), typeof(Form2));
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, string.Format("第{1}条数据，异常出错：{0}", ex.Message, i), typeof(Form2));
                }

            }

            LogHelper.WriteLog(LogLevel.LOG_LEVEL_INFO, "执行完成", typeof(Form2));
        }

        public Encoding GetEncoding(string strurl)
        {
            string urlToCrawl = strurl;
            //generate http request
            if (urlToCrawl != null && urlToCrawl != "")
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(urlToCrawl);
                //use GET method to get url's html
                req.Method = "GET";
                req.Accept = "*/*";
                req.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
                req.ContentType = "text/xml";
                //use request to get response
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Encoding enc;
                try
                {
                    if (resp.CharacterSet != "ISO-8859-1")
                        enc = Encoding.GetEncoding(resp.CharacterSet);
                    else
                        enc = Encoding.UTF8;
                }
                catch
                {
                    // *** Invalid encoding passed
                    enc = Encoding.UTF8;
                }
                string sHTML = string.Empty;
                using (StreamReader read = new StreamReader(resp.GetResponseStream(), enc))
                {
                    sHTML = read.ReadToEnd();
                    Match charSetMatch = Regex.Match(sHTML, "charset=(?<code>[a-zA-Z0-9\\-]+)", RegexOptions.IgnoreCase);
                    string sChartSet = charSetMatch.Groups["code"].Value;
                    //if it's not utf-8,we should redecode the html.
                    if (!string.IsNullOrEmpty(sChartSet) && !sChartSet.Equals("utf-8", StringComparison.OrdinalIgnoreCase))
                    {
                        enc = Encoding.GetEncoding(sChartSet);
                    }
                }
                return enc;
            }
            return Encoding.Default;
        }
    }
}

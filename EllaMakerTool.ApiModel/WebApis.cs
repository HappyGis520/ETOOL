using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using EllaMakerTool.Message;
using EllaMakerTool.Message.Data;
using EllaMakerTool.Message.Param;

namespace EllaMakerTool.Api
{
    public class WebApis
    {
        #region 路由常量
        private const string Login_UserLogin = "UserLogin";
        #endregion
        public WebApis(string url)
        {
            WebApiUtil.Url = url;
        }

        public string Token { get; set; }

        public ApiResult<AuthToken> Login(string username, string password, string imei = "", string mac = "")
        {
            var res = WebApiUtil.PostAPI<ApiResult<AuthToken>>(
                $"api/login?username={username}&password={password}&imei={imei}&mac={mac}",null);
            if (res.successful)
            {
                Token = res.Data.Token;
            }
            return res;
        }
        internal ResponseModelBase<LoginUser> UserLogin(string UserName, String Password)
        {
           UserLoginParam param = new UserLoginParam(UserName,Password);
            var obj = WebApiUtil.PostAPI<ResponseModelBase<LoginUser>>(Login_UserLogin, param
                 );
            return obj;

        }

        /// <summary>
        /// 获取上传路径
        /// </summary>
        /// <param name="rootType">0：其他 1-公司；2-共享；3-个人</param>
        /// <returns></returns>
        public TheResult<string> GetUpaloadPath(int rootType)
        {
            return WebApiUtil.GetAPI<TheResult<string>>($"api/docV1/getuploadpath?Token={Token}&rootType={rootType}");
        }

        /// <summary>
        /// 获取容量使用情况
        /// </summary>
        /// <returns></returns>
        public TheResult<CompanyStoreStatusApiModel> getCompanyStoreStatus()
        {
            return WebApiUtil.GetAPI<TheResult<CompanyStoreStatusApiModel>>(
                $"api/docV1/getCompanyStoreStatus?Token={Token}");
        }
        /// <summary>
        /// 获取根目录文件列表
        /// </summary>
        /// <param name="rootType">1-公司；2-共享；3-个人</param>
        /// <param name="pageIndex">第几页，从１开始</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="ordertype">排序类型：1 时间，2文件名，3类型, 4大小</param>
        /// <param name="isasc">排序类型：1 升序，2 降序</param>
        /// <returns></returns>
        public TheResult<DocumentV1QueryModel> GetRoot(int rootType, int ordertype, int isasc,
            int pageIndex = 1, int pageSize = 10000)
        {
            var res = WebApiUtil.GetAPI<TheResult<DocumentV1QueryModel>>(
                $"api/docV1/getRoot?Token={Token}&rootType={rootType}&ordertype={ordertype}&isasc={isasc}&pageIndex={pageIndex}&pageSize={pageSize}");
            if (res.Data == null) res.Data = new DocumentV1QueryModel()
            {
                Records=new List<DocumentV1ApiModel>()
            };
            if (res.Data.Records == null)
            {
                res.Data.Records = new List<DocumentV1ApiModel>();
            }
            return res;
        }

        /// <summary>
        /// 增加下载次数
        /// </summary>
        /// <param name="times">要增加的次数（默认1）</param>
        /// <param name="url">下载时给的URL路径（不含服务器根例如Company/244/Documents/2018/01/15/37c9ab08-e575-49e1-aecc-4a16de3296fb.exe）</param>
        /// <returns></returns>
        public TheResult<bool> AddDownloadTime(string url,int times)
        {
            return WebApiUtil.GetAPI<TheResult<bool>>(
                $"api/docV1/AddDownloadTimes?Token={Token}&url={url}&times={times}");
        }

        /// <summary>
        /// 获取公司列表
        /// </summary>
        /// <returns></returns>
        public ApiResult<List<EmployerApiModel>> GetMyCompanyList()
        {
            return WebApiUtil.GetAPI<ApiResult<List<EmployerApiModel>>>($"api/company/myCompany?Token={Token}");
        }

        /// <summary>
        /// 切换公司
        /// </summary>
        /// <param name="TargetCompanyId"></param>
        /// <returns></returns>
        public ApiResult<List<FunctionItemApiModel>> SwitchCompany(string TargetCompanyId)
        {
            
            return WebApiUtil.PostAPI<ApiResult<List<FunctionItemApiModel>>>(
                $"api/switch?Token={Token}&TargetCompanyId={TargetCompanyId}",null);
        }

        /// <summary>
        /// 获取权限信息
        /// </summary>
        /// <returns></returns>
        public ApiResult<List<FunctionItemApiModel>> GetFunc()
        {

            return WebApiUtil.GetAPI<ApiResult<List<FunctionItemApiModel>>>(
                $"api/auth/myfunc?Token={Token}");
        }

        /// <summary>
        /// 获取职位列表
        /// </summary>
        /// <returns></returns>
        public ApiResult<List<EmployeeJobApiModel>> GetJoblist()
        {
            return WebApiUtil.GetAPI<ApiResult<List<EmployeeJobApiModel>>>($"api/Employee/myjobs?Token={Token}");
        }
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="rootType">1-公司；2-共享；3-个人</param>
        /// <param name="model"></param>
        /// <returns></returns>
        public TheResult<string> CreateDCatalog(int rootType, DocumentCatalogV1Request model)
        {

            return WebApiUtil.PostAPI<TheResult<string>>(
                $"api/docV1/addcatalog?Token={Token}&rootType={rootType}", model);
        }

        /// <summary>
        /// 设置协同范围
        /// </summary>
        /// <param name="docId">文件或文件夹ID</param>
        /// <param name="type">文件类型：1文件 2文件夹</param>
        /// <param name="departList">新的部门范围列表</param>
        /// <param name="profileList">新的个人范围列表</param>
        /// <returns></returns>
        public TheResult<string> setSynergyRange(string docId, EnumDocType type, StatusList Model)
        {

            return WebApiUtil.PostAPI<TheResult<string>>(
                $"api/docV1/setSynergyRange?Token={Token}&docId={docId}&type={type}", Model);
        }

        /// <summary>
        /// 获取文档目录树
        /// </summary>
        /// <param name="rootType">目标文档类型</param>
        /// <returns></returns>
        public TheResult<DocumentTreeNodelApiModel> GetRootDocTree(int rootType)
        {
            return WebApiUtil.GetAPI<TheResult<DocumentTreeNodelApiModel>>(
                $"api/docV1/GetRootDocTree?Token={Token}&rootType={rootType}");
        }
        /// <summary>
        /// 设置协同范围
        /// </summary>
        /// <param name="docId">文件或文件夹ID</param>
        /// <param name="type">文件类型：1文件 2文件夹</param>
        /// <param name="departList">新的部门范围列表</param>
        /// <param name="profileList">新的个人范围列表</param>
        /// <returns></returns>
        public TheResult<string> setShareRange(string docId, EnumDocType type, StatusList Model)
        {

            return WebApiUtil.PostAPI<TheResult<string>>(
                $"api/docV1/setShareRange?Token={Token}&docId={docId}&type={type}", Model);
        }
        /// <summary>
        /// 重命名文件夹
        /// </summary>
        /// <param name="name">新名称</param>
        /// <param name="folderId">文件夹id</param>
        /// <returns></returns>
        public TheResult<bool> RenameCatalog(string name, string folderId)
        {
            return WebApiUtil.PostAPI<TheResult<bool>>(
                $"api/docV1/renamefolder?Token={Token}&name={name}&folderId={folderId}", null);
        }

        /// <summary>
        /// 重命名文件
        /// </summary>
        /// <param name="name">新名称</param>
        /// <param name="fileId">文件夹id</param>
        /// <returns></returns>
        public TheResult<bool> RenameFile(string name, string fileId)
        {
            return WebApiUtil.PostAPI<TheResult<bool>>(
                $"api/docV1/renamefile?Token={Token}&name={name}&fileId={fileId}", null);
        }

        /// <summary>
        /// 移动文件或文件夹
        /// </summary>
        /// <param name="sourceId">被移动的文件或文件夹ID</param>
        /// <param name="sourceName">被移动的文件或文件夹名（带后缀）</param>
        /// <param name="targetCatalogId">目标文件夹ID</param>
        /// <param name="type">被移动的文件类型：1文件 2文件夹</param>
        /// <param name="isCover">是否覆盖（覆盖时，同命文件不会失败，直接覆盖）</param>
        /// <returns></returns>
        public TheResult<string> MoveDoc(string sourceId, string sourceName, string targetCatalogId, EnumDocType type,
            bool isCover,bool isClearRange)
        {
            return WebApiUtil.GetAPI<TheResult<string>>(
                $"api/docV1/moveDoc?Token={Token}&sourceId={sourceId}&sourceName={sourceName}&targetCatalogId={targetCatalogId}&type={type}&isCover={isCover}&isClearRange={isClearRange}");
        }

        /// <summary>
        /// 源文档共享协作范围是否超过目标文件夹（用于共享文件移动前检验）
        /// </summary>
        /// <param name="sourceId">源文档ID</param>
        /// <param name="targetCatalogId">目标文件夹ID</param>
        /// <param name="type">源文档类型</param>
        /// <returns></returns>
        public TheResult<bool> IsOverRange(string sourceId, string targetCatalogId, EnumDocType type)
        {
            return WebApiUtil.GetAPI<TheResult<bool>>($"api/docV1/IsOverRange?Token={Token}&sourceId={sourceId}&targetCatalogId={targetCatalogId}&type={type}");
        }

        /// <summary>
        /// 获取重名文件数
        /// </summary>
        /// <param name="targetCatalogId">目标文件夹</param>
        /// <param name="model">文件列表</param>
        /// <returns></returns>
        public TheResult<List<CoverDocResultApiModel>> GetCoverNum(string targetCatalogId, List<DocBaseInfoApiModel> model)
        {
            return WebApiUtil.PostAPI<TheResult<List<CoverDocResultApiModel>>>(
                $"api/docv1/getcovernum?token={Token}&targetcatalogid={targetCatalogId}", model);
        }
        /// <summary>
        /// 删除文件夹及文档
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public TheResult<string> Delete(DocumentDeleteRequest model)
        {
            return WebApiUtil.PostAPI<TheResult<string>>(
                $"api/docV1/delete?Token={Token}", model);
        }

        /// <summary>
        /// 检测重名
        /// </summary>
        /// <param name="catalogId">父级文件夹ID</param>
        /// <param name="name">文件或文件夹名（喊后缀）</param>
        /// <param name="type">文件类型：1文件 2文件夹</param>
        /// <returns></returns>
        public TheResult<string> HasSameName(string catalogId, string name, EnumDocType type, string FilePath = "")
        {
            return WebApiUtil.GetAPI<TheResult<string>>(
                $"api/docV1/hasSame?Token={Token}&catalogId={catalogId}&name={name}&type={type}&FilePath={FilePath}");
        }

        /// <summary>
        /// 获取公司员工树
        /// </summary>
        /// <param name="deptId">部门ID，公司顶级传空""</param>
        /// <param name="isGetPerson">是否获取个人信息</param>
        /// <returns></returns>
        public TheResult<EmployeeAndDeptNodelApiModel> GetCompanyTree(string deptId="",bool isGetPerson=false)
        {
            return WebApiUtil.GetAPI<TheResult<EmployeeAndDeptNodelApiModel>>($"api/Employee/GetCompanyAllTree?Token={Token}&deptId={deptId}&isGetPerson={isGetPerson}");
        }
        /// <summary>
        /// 批量上传文档(带文件夹)
        /// </summary>
        /// <param name="rootType">1-公司；2-共享；3-个人</param>
        /// <param name="models"></param>
        /// <returns>Url字段为空时接口执行文件是否存在校验，存在Code为0，不存在Code为2</returns>
        public TheResult<List<DocBaseInfoApiModel>> Upload(int rootType, List<DocumentV1Request> models)
        {
            return WebApiUtil.PostAPI<TheResult<List<DocBaseInfoApiModel>>>(
                $"api/docV1/upload?Token={Token}&rootType={rootType}", models);
        }

        /// <summary>
        /// 上传协同文件
        /// </summary>
        /// <param name="fileId">目标文件ID</param>
        /// <param name="url">上传得到的实际地址</param>
        /// <param name="ThumbUrl">缩略图地址</param>
        /// <param name="Size">大小(单位KB，不足取1)</param>
        /// <returns></returns>
        public TheResult<bool> AddSyncFile(string fileId,string url,string ThumbUrl,long Size)
        {
            return WebApiUtil.GetAPI<TheResult<bool>>(
                $"api/docV1/AddSynergyFile?Token={Token}&fileId={fileId}&url={url}&ThumbUrl={ThumbUrl}&Size={Size}");
        }
        /// <summary>
        /// 根据文件夹id查看该文件夹包含的文档
        /// </summary>
        /// <param name="catalogId">文件夹id</param>
        /// <param name="key">查询关键字</param>
        /// <param name="rootType">1-公司；2-共享；3-个人</param>
        /// <param name="pageIndex">第几页，从１开始</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="ordertype">排序类型：1 时间，2文件名，3类型, 4大小</param>
        /// <param name="isasc">排序类型：1 升序，2 降序</param>
        /// <returns></returns>
        public TheResult<DocumentV1QueryModel> GetListQuery(string catalogId, string key, int rootType, int ordertype,
            int isasc,
            int pageIndex = 1, int pageSize = 1000)
        {
            var res = WebApiUtil.GetAPI<TheResult<DocumentV1QueryModel>>(
                $"api/docV1/get?Token={Token}&catalogId={catalogId}&key={key}&rootType={rootType}&ordertype={ordertype}&isasc={isasc}&pageIndex={pageIndex}&pageSize={pageSize}");
            if (res.Data == null) res.Data = new DocumentV1QueryModel()
            {
                Records = new List<DocumentV1ApiModel>()
            };
            if (res.Data.Records == null)
            {
                res.Data.Records = new List<DocumentV1ApiModel>();
            }
            return res;
        }

        public TheResult<string> SetNewStatus(string docId, EnumDocType type, EnumDocStatusType newStatus)
        {
            return WebApiUtil.GetAPI<TheResult<string>>(
                $"api/docV1/setDocStatus?Token={Token}&docId={docId}&type={type}&newStatus={newStatus}");
        }


    }
}

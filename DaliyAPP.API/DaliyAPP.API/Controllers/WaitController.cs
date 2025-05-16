using DaliyAPP.API.ApiResponses;
using DaliyAPP.API.DataModel;
using DaliyAPP.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DaliyAPP.API.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WaitController : ControllerBase
    {

        private readonly DaliyDBContext db;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_db"></param>


        public WaitController(DaliyDBContext _db)
        {
            db = _db;
        }
        /// <summary>
        /// 统计待办事项
        /// </summary>
        /// <returns>1:获取数据成功，-99:服务器繁忙</returns>
        [HttpGet]
        public IActionResult StaWait()
        {
            ApiResponse res = new ApiResponse();
            try
            {
                var list= db.WaitInfo.ToList();//获取所有待办事项
                var fishing = list.Where(x => x.Status == 1).Count();//已完成
                StaWaitDTO staWaitDTO = new StaWaitDTO()
                {
                    TotalCount = list.Count(),
                    FinishCount = fishing
                };
                res.ResultData = staWaitDTO;
                res.ResultCode = 1;
                res.Msg = "获取成功";

            }
            catch (Exception)
            {

                res.ResultCode = -99;
                res.Msg = "服务器繁忙";
            }

            return Ok(res);
        }
        /// <summary>
        /// 添加待办事项
        /// </summary>
        /// <param name="AddWaitDTO">待办事项清单</param>
        [HttpPost]
        public IActionResult AddWait([FromBody] AddWaitDTO AddWaitDTO)
        {
            ApiResponse res = new ApiResponse();
            try
            {
                //获取所有待办事项
                WaitInfo waitInfo = new WaitInfo()
                {
                    Title = AddWaitDTO.Title,
                    Content = AddWaitDTO.Content,
                    Status = AddWaitDTO.Status
                };
                //添加待办事项
                db.WaitInfo.Add(waitInfo);
                //保存到数据库
                int result = db.SaveChanges();
                if (result == 1)
                {
                    res.ResultCode = 1;
                    res.Msg = "添加待办成功";
                }
                else
                {
                    res.ResultCode = -1;
                    res.Msg = "添加待办失败";
                }
            }
            catch (Exception)
            {

               res.ResultCode = -99;
                res.Msg = "服务器繁忙";
            }
            return Ok(res);
        }
        /// <summary>
        /// 获取待办事项列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetWaitingList()
        {
            ApiResponse res = new ApiResponse();
            try
            {
                var list = from A in db.WaitInfo
                           where A.Status == 0
                           select new AddWaitDTO
                           {
                               WaitId = A.WaitId,
                               Title = A.Title,
                               Content = A.Content,
                               Status = A.Status
                           };
                if (list.Count() > 0)
                {
                    res.ResultData = list.ToList();
                    res.ResultCode = 1;
                    res.Msg = "获取成功";
                }
                else
                {
                    res.ResultCode = -1;
                    res.Msg = "没有待办事项";
                }

            }
            catch (Exception)
            {
                res.ResultCode = -99;
                res.Msg = "服务器繁忙";
            }
            return Ok(res);
        }
        /// <summary>
        /// 修改待办事项状态
        /// </summary>
        /// <param name="newaddWaitDTO"></param>
        /// <returns></returns>
        [HttpPut]

        public IActionResult UpdateStatus(AddWaitDTO newaddWaitDTO)
        {
            ApiResponse res = new ApiResponse();
            try
            {
                // var dbInfo = db.WaitInfo.Where(t => t.WaitId == addWaitDTO.WaitId).FirstOrDefault();
                //根据ID查询待办事项状态
                var dbInfo = db.WaitInfo.Find(newaddWaitDTO.WaitId);
                if (dbInfo != null)
                {
                    dbInfo.Status = newaddWaitDTO.Status;
                    var result= db.SaveChanges();
                   if(result == 1)
                    {
                        res.ResultCode = 1;
                        res.Msg =(newaddWaitDTO.Status==0 ? "状态成功设置为待办" : "状态成功设置为完成");
                    }
                    else
                    {
                        res.ResultCode = -1;
                        res.Msg = "修改失败";
                    }
                }
                else
                {
                    res.ResultCode = -1;
                    res.Msg = "没有该待办事项";
                }
            }
            catch (Exception)
            {

                res.ResultCode = -99;
                res.Msg = "服务器繁忙";
            }
            return Ok(res);
        }


        /// <summary>
        /// 修改待办事项
        /// </summary>
        /// <param name="newEditWaitDTO"></param>
        /// <returns></returns>
        [HttpPut]

        public IActionResult EditWait(AddWaitDTO newEditWaitDTO)
        {
            ApiResponse res = new ApiResponse();
            try
            {
                // var dbInfo = db.WaitInfo.Where(t => t.WaitId == addWaitDTO.WaitId).FirstOrDefault();
                //根据ID查询待办事项状态
                var dbInfo = db.WaitInfo.Find(newEditWaitDTO.WaitId);
                if (dbInfo != null)
                {
                    dbInfo.Status = newEditWaitDTO.Status;
                    dbInfo.Title = newEditWaitDTO.Title;
                    dbInfo.Content = newEditWaitDTO.Content;
                    var result = db.SaveChanges();
                    if (result == 1)
                    {
                        res.ResultCode = 1;
                        res.Msg = "修改成功";
                    }
                    else
                    {
                        res.ResultCode = -1;
                        res.Msg = "修改失败";
                    }
                }
                else
                {
                    res.ResultCode = -1;
                    res.Msg = "没有该待办事项";
                }
            }
            catch (Exception)
            {

                res.ResultCode = -99;
                res.Msg = "服务器繁忙";
            }
            return Ok(res);
        }

        /// <summary>
        /// 查询待办事项
        /// </summary>
        /// <param name="Ttile"></param>
        /// <param name="Status"></param>
        /// <returns></returns>

        [HttpGet]
        public IActionResult Query(string?Title,int?Status)
        {
            ApiResponse res = new ApiResponse();
            try
            {
               var query = from A in db.WaitInfo
                           select new AddWaitDTO
                           {
                               WaitId = A.WaitId,
                               Title = A.Title,
                               Content = A.Content,
                               Status = A.Status
                           };
                if (!string.IsNullOrEmpty(Title))
                {
                    query = query.Where(x => x.Title.Contains(Title));
                }
                if (Status != null)
                {
                    query = query.Where(x => x.Status == Status);
                }
                res.ResultCode = 1;
                res.Msg = "查询成功";
                res.ResultData = query;


            }
            catch (Exception)
            {

                res.ResultCode = -99;
                res.Msg = "服务器繁忙";
            }

            return Ok(res);
        }
        /// <summary>
        /// 删除待办事项
        /// </summary>
        /// <param name="AddWaitDTO"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteWait(AddWaitDTO AddWaitDTO)
        {
            ApiResponse res = new ApiResponse();
            try
            {
                //取出ID判断删除
                var dbinfo = db.WaitInfo.Find(AddWaitDTO.WaitId);
                if (dbinfo != null)
                {
                    db.WaitInfo.Remove(dbinfo);
                    int result = db.SaveChanges();
                    if (result == 1)
                    {
                        res.ResultCode = 1;
                        res.Msg = "删除成功";
                    }
                    else
                    {
                        res.ResultCode = -1;
                        res.Msg = "删除失败";
                    }
                }
                else
                {
                    res.ResultCode = -1;
                    res.Msg = "无法删除，内容为空";
                }
            }
            catch (Exception)
            {
                res.ResultCode = -99;
                res.Msg = "服务器繁忙";
            }
            return Ok(res);
        }

    }

}

using DaliyAPP.API.ApiResponses;
using DaliyAPP.API.DataModel;
using DaliyAPP.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DaliyAPP.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemoController : ControllerBase
    {
        private readonly DaliyDBContext db;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_db"></param>
        public MemoController(DaliyDBContext _db)
        {
            db = _db;
        }
        /// <summary>
        /// 统计备忘事项
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult StaMemo()
        {
            ApiResponse res = new ApiResponse();
            try
            {
                int Count = db.MemoInfo.Count();//获取所有备忘事项
                res.ResultData = Count;
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
        /// 添加备忘事项
        /// </summary>
        /// <param name="AddMemoInfo"></param>
        /// <returns></returns>
        [HttpPost]

        public IActionResult AddMemo([FromBody] MemoInfo AddMemoInfo)
        {
            ApiResponse res = new ApiResponse();
            try
            {
                MemoInfo memoInfo = new MemoInfo()
                {
                    Title = AddMemoInfo.Title,
                    Content = AddMemoInfo.Content,

                };
                db.MemoInfo.Add(memoInfo);
                int result = db.SaveChanges();
                if (result == 1)
                {
                    res.ResultCode = 1;
                    res.Msg = "添加成功";
                }
                else
                {
                    res.ResultCode = -1;
                    res.Msg = "添加失败";
                }

            }
            catch (Exception ex)
            {
                res.ResultCode = -99;
                res.Msg = "服务器繁忙：" + ex.Message; // 加入异常信息返回
            }

            return Ok(res);
        }
        /// <summary>
        /// 查询备忘事项
        /// </summary>
        /// <param name="Title"></param>
        /// <returns></returns>
        [HttpGet]

        public IActionResult QueryMemo(string? Title)
        {
            ApiResponse res = new ApiResponse();
            try
            {
                var query = from A in db.MemoInfo
                            select new MemoDTO
                            {
                                MemoId = A.MemoId,
                                Title = A.Title,
                                Content = A.Content
                            };

                if (!string.IsNullOrEmpty(Title))
                {
                    query = query.Where(x => x.Title.Contains(Title));
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
        /// 编辑备忘事项
        /// </summary>
        /// <param name="NewmemoDTO"></param>
        /// <returns></returns>
        [HttpPut]

        public IActionResult EditMemo(MemoDTO NewmemoDTO)
        {
            ApiResponse res = new ApiResponse();
            try
            {
                var dbinfo = db.MemoInfo.Find(NewmemoDTO.MemoId);
                if (dbinfo != null)
                {
                    dbinfo.Title = NewmemoDTO.Title;
                    dbinfo.Content = NewmemoDTO.Content;
                    int result = db.SaveChanges();
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
                    res.Msg = "修改失败";
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
        /// 删除备忘事项
        /// </summary>
        /// <param name="memoDTO"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteMemo(MemoDTO memoDTO)
        {
            ApiResponse res = new ApiResponse();
            try
            {
                //取出ID判断删除
                var dbinfo = db.MemoInfo.Find(memoDTO.MemoId); 
                if (dbinfo != null)
                {
                    db.MemoInfo.Remove(dbinfo);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Aryaban.Engine.Core.WebService;
using DigitalServices.Model;
using System.Web;

namespace DigitalServices.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "permission" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select permission.svc or permission.svc.cs at the Solution Explorer and start debugging.
    public class permission : Ipermission
    {
        private string UrlDecode(string s)
        {
            return HttpUtility.UrlDecode(s, Encoding.GetEncoding("ISO-8859-1"));
        }
        public ResultMessage<UserInfoWTO> login(UserInfoWTO userInfo)
        {
            DigitalSignageEntities db = new DigitalSignageEntities();
            try
            {
                string pass = Aryaban.Engine.Core.Security.Encryption.HashString(userInfo.password);
                var user = (from u in db.DS_Users
                            where u.username == userInfo.username && u.password == pass
                            select u).FirstOrDefault();
                if (user != null)
                {
                    UserInfoWTO usr = new UserInfoWTO();
                    usr.id = (long)user.id;
                    usr.fName = user.fName;
                    usr.lName = user.lName;
                    usr.username = user.username;
                    usr.password = user.password;
                    usr.companyId = (int)user.companyId;
                    return new ResultMessage<UserInfoWTO>
                    {
                        resultSet = usr,
                        result = new Result()
                        {
                            status = Result.state.success,
                          
                        }
                    };
                }
                else
                {
                    return new ResultMessage<UserInfoWTO>
                    {
                        resultSet = null,
                        result = new Result()
                        {
                            status = Result.state.error,
                            message = "نام کاربری و رمز عبور اشتباه می باشد.",

                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultMessage<UserInfoWTO>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.error,
                        message = ex.Message
                    }
                };
            }
        }


        public ResultMessage<UserInfoWTO> signup(UserInfoWTO userInfo)
        {
            DigitalSignageEntities db = new DigitalSignageEntities();
            try
            {
                string pass = Aryaban.Engine.Core.Security.Encryption.HashString(userInfo.password);
                var user = (from u in db.DS_Users
                            where u.username == userInfo.username && u.password == pass
                            select u).FirstOrDefault();
                if (user == null)
                {
                    DS_Users usr = new DS_Users();
                   
                    usr.fName = userInfo.fName;
                    usr.lName = userInfo.lName;
                    usr.username = userInfo.username;
                    usr.password = Aryaban.Engine.Core.Security.Encryption.HashString(userInfo.password);
                    usr.companyId = (int)userInfo.companyId;
                    db.DS_Users.Add(usr);
                    db.SaveChanges();
                    return new ResultMessage<UserInfoWTO>
                    {
                        resultSet = userInfo,
                        result = new Result()
                        {
                            status = Result.state.success,
                            message = "کاربر با موفقیت ثبت شد",
                        }
                    };
                }
                else
                {
                    return new ResultMessage<UserInfoWTO>
                    {
                        resultSet = null,
                        result = new Result()
                        {
                            status = Result.state.error,
                            message = "نام کاربری و رمز عبور تکراری می باشد.",

                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultMessage<UserInfoWTO>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.error,
                        message = ex.Message
                    }
                };
            }
        }

        public ResultMessage<bool> ChangePassword(long id,string OldPass, string NewPass, string ReNewPass)
        {
            DigitalSignageEntities db = new DigitalSignageEntities();
            OldPass = UrlDecode(OldPass);
            NewPass = UrlDecode(NewPass);
            ReNewPass = UrlDecode(ReNewPass);

            try
            {
                var user = (from u in db.DS_Users
                            where u.id == id 
                            select u).FirstOrDefault();
                if (user != null)
                {
                    if (user.password == Aryaban.Engine.Core.Security.Encryption.HashString(OldPass))
                    {
                        user.password = Aryaban.Engine.Core.Security.Encryption.HashString(NewPass);
                        db.SaveChanges();
                        return new ResultMessage<bool>
                        {
                            resultSet = true,
                            result = new Result()
                            {
                                status = Result.state.success,
                                message = "کلمه عبور با موفقیت تغییر یافت."
                            }
                        };
                    }
                    else
                    {
                        return new ResultMessage<bool>
                        {
                            resultSet = true,
                            result = new Result()
                            {
                                status = Result.state.error,
                                message = "کلمه عبور قدیم صحیح نمی باشد.",
                            }
                        };

                    }
                }
                return new ResultMessage<bool>
                {
                    resultSet = true,
                    result = new Result()
                    {
                        status = Result.state.error,
                        message = "کاربر وجود ندارد.",
                    }
                };

            }
            catch (Exception ex)
            {
                return new ResultMessage<bool>
                {
                    resultSet = false,
                    result = new Result()
                    {
                        status = Result.state.error,
                        message = ex.Message
                    }
                };


            }
        }
    }
}

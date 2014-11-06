using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;

namespace Scholarship.Models
{
    public class UserDatabase
    {
        public UserModel ValidUser(UserModel user)
        {
            DBObject db = new DBObject();
            
            String sqlstr = "SELECT passwordhash,salt FROM users where username= :username";
            List<OracleParameter> parameters = new List<OracleParameter>();
            parameters.Add(new OracleParameter("username", user.userName));
            DataTable dt = db.query(sqlstr, parameters);
            if (dt.Rows.Count==0){
                user.accessToken="";
                user.userName="";
                return user;
            }
            String storedHash=dt.Rows[0]["passwordhash"].ToString();
            String storedSalt=dt.Rows[0]["salt"].ToString();
            String inputHash = CreatePasswordHash(user.userPassword, storedSalt);
            if (storedHash.Equals(inputHash))
            {
                user.accessToken = generateToken(user);
                return user;
            }
            else
            {
                user.accessToken = "";
                user.userName = "";
                return user;
            }
        }
        public bool UserExists(UserModel user)
        {
            DBObject db = new DBObject();

            String sqlstr = "SELECT * FROM users where username= :username";
            List<OracleParameter> parameters = new List<OracleParameter>();
            parameters.Add(new OracleParameter("username", user.userName));
            DataTable dt = db.query(sqlstr, parameters);
            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
        public string RegisterUser(UserModel user)
        {
            if (UserExists(user))
            {
                return "User Exists Already";
            }
            DBObject db = new DBObject();
            String sqlstr = "INSERT INTO users (username,passwordhash,salt) VALUES (:username, :passwordhash,:salt)";
            string salt = CreateSalt(4);
            string passwordhash = CreatePasswordHash(user.userPassword, salt);
            List<OracleParameter> parameters = new List<OracleParameter>();
            parameters.Add(new OracleParameter("username", user.userName));
            parameters.Add(new OracleParameter("passwordhash", passwordhash));
            parameters.Add(new OracleParameter("salt", salt));
            int count = db.queryExecute(sqlstr, parameters);
            if (count == 1)
            {
                return "";
            }
            else
            {
                return "Could not create user.";
            }
        }
        public string generateToken(UserModel user)
        {
            string token= Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            DBObject db = new DBObject();
            String sqlstr = "INSERT INTO tokens (username,accesstoken,granted) VALUES (:username, :accesstoken,:granted)";
            List<OracleParameter> parameters = new List<OracleParameter>();
            parameters.Add(new OracleParameter("username", user.userName));
            parameters.Add(new OracleParameter("accesstoken", token));
            parameters.Add(new OracleParameter("granted", DateTime.Now));
            int count = db.queryExecute(sqlstr, parameters);
            return token;
            
        }

        private string CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }

        private string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            var sha = new SHA1Managed();
            string hashedPwd = Convert.ToBase64String(sha.ComputeHash(Convert.FromBase64String(saltAndPwd))); //FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha1");
            return hashedPwd;
        }

    }
}
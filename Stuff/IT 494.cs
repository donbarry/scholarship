*if select a major, include blank
CHange select * to specifics
8002948804
select * from summit.schlrshp s join summit.fund f on s.FUND_ACCT = f.FUND_ACCT where s.SCHLR_USER_VARBL2 = 'Y' and f.FUND_OPEN_ATTRB='O'	
SCHLRSHP_TITLE
PURPOSE
FRML_SCHLRSHP_NAME
SCHLRSHP_PRPS purpose
FUND_DEPT_ATTRB then select fund_dept_descr from Uhelp.fund_dept_attrb where fund_dept_attrb=
FUND_COLL_ATTRB then select fund_coll_descr from Uhelp.fund_coll_attrb where fund_coll_attrb=
class desc select USER_CD from Summit.USER_CODE where USER_GRP='SCHYR' and parent_audit_tran_id= ([audit_tran_id from initial table query])
	select User_CD_Descr from UHELP.USER_CD where User_Grp='SCHYR' and User_cd='
		*All Undergrad(Freshman, Sophomore, Junior, Senior), *All Grad(Masters, Doctorate), *All Academic Levels, *No code entered
audit_tran_id is also used in getMajorInfoFromMajor which uses getmajor
	again, select USER_CD from Summit.USER_CODE where USER_GRP='SCHMJ' and parent_audit_tran_id=
		then select User_CD_Descr from UHELP.USER_CD where User_Grp='SCHMJ' and User_cd= 
SCHLR_USER_VARBL13 - minimum undergrad gpa
SCHLR_USER_VARBL14 - mini grad gpa
SCHLR_USER_VARBL15 - high school gpa
if SCHLR_USER_VARBL11 then essay required
SCHLR_USER_VARBL32 deadline
if SCHLR_USER_VARBL4 require u to have financial need
if SCHLR_USER_VARBL3 is "n" not open for intl students
county info - select USER_CD from Summit.USER_CODE where USER_GRP='SCHCO' and parent_audit_tran_id=
	select User_CD_Descr from UHELP.USER_CD where User_Grp='SCHCO' and User_cd=
if SCHLR_USER_VARBL8, require community service
if SCHLR_USER_VARBL9, require leadership experience
if SCHLR_USER_VARBL16 minimum ACT ->
IF SCHLR_USER_VARBL10 REQ. resume
SCHLR_USER_VARBL31 is number of leters of ref needed
SCHLR_USER_VARBL18 MIN num ISU hours
fund stuff - hidden search searchTags
FUND_GRP_ATTRB
Fund Group:FUND_GRP_ATTRB
Fund Type: FUND_TYPE_ATTRB"].ToString().Trim() + "</p>";


Fund Purpose: </b>" + myDataRow["FUND_PRPS_ATTRB"].ToString().Trim() + "</p>";


Fund primary Goal: </b>" + myDataRow["PRMRY_GOAL_ATTRB"].ToString().Trim() + "</p>";


Fund Status: </b>" + myDataRow["FUND_OPEN_ATTRB"].ToString().Trim() + "</p>";


Scholarship Type: </b>" + myDataRow["SCHLR_USER_VARBL1"].ToString().Trim() + "</p>";


Searchable: </b>" + myDataRow["SCHLR_USER_VARBL2"].ToString().Trim() + "</p>";


Survey Code: </b>" + myDataRow["FUND_USER_VARBL1"].ToString().Trim() + "</p>";



	        public string getMajorInfoFromMajor(string sqlstr)
	        {
	            int index = -1;
	            string substr = "Theatre";
	            string tempStr = "";
	            string placeHolder = "";
	
	            placeHolder = this.getmajor(sqlstr);
	            if (placeHolder.Length > 0)
	            {
	                //Major information
	                tempStr = tempStr + "<p><b>Major: </b>";
	                if (placeHolder == "*ALL MAJORS")
	                {
	                    tempStr = tempStr + "Students from all Majors can apply for this scholarship";
	                    tempStr = tempStr + "<span class='searchTags'>AllMajor: <!--googleoff: snippet-->";
	                    DataTable tempdata = DBData("select distinct u.user_cd_descr from UHELP.USER_CD u join Summit.USER_CODE s on u.user_cd = s.user_cd join summit.schlrshp ss on s.parent_audit_tran_id = ss.audit_tran_id join summit.fund f on ss.FUND_ACCT = f.FUND_ACCT where u.User_Grp='SCHMJ' and s.User_Grp='SCHMJ' and ss.SCHLR_USER_VARBL2 = 'Y' and f.FUND_OPEN_ATTRB='O' order by User_CD_Descr");
	                    if (tempdata.Rows.Count != 0)
	                    {
	                        int n = tempdata.Rows.Count;
	                        for (int i = 0; i < n; i++)
	                        {
	                            if (tempdata.Rows[i][0].ToString().Trim() == "*ALL MAJORS" || tempdata.Rows[i][0].ToString().Trim() == "*No code entered")
	                            {
	                                i++;
	                            }
	                            else
	                            {
	                                tempStr = tempStr + tempdata.Rows[i][0].ToString().Trim() + ", ";
	                            }
	                        }
	                        tempStr = tempStr.Remove(tempStr.Length - 2);
	                        tempStr = tempStr + "<!--googleon: snippet--></span>";
	                    }
	                }
	                else
	                {
	                    tempStr = tempStr + placeHolder;
	                    tempStr = tempStr + "<div style=\"clear:both\"></div>";
	                }
	                tempStr = tempStr + "</p>";
	            }
	            else
	            {
	                tempStr = "<!-- googleoff: snippet --><span class='searchTags'>NOMAJOR</span><!-- googleon: snippet -->";
	            }
	            //search Theatre
	            index = tempStr.IndexOf(substr);
	            if (index >= 0)
	            {
	                tempStr = tempStr + "<!-- googleoff: snippet --><span class='searchTags'>Theater</span><!-- googleon: snippet -->";
	            }
	
	            return tempStr;
	        }

	        public string getmajor(string sqlstr)
	        {
	            string tempString = "";
	            string temp = ".... <a href= \"#\" id=\"slick-toggle\" onclick=\"toggle_name()\">more</a><div class=\"sample\" id=\"slickbox\"><ul class=\"my-list\">";
	            int k = 0;
	            DataTable user_code = DBData("select USER_CD from Summit.USER_CODE where USER_GRP='SCHMJ' and parent_audit_tran_id=" + sqlstr + "");
	            // string usertemp = user_code.Rows[0][0].ToString();
	            if (user_code.Rows.Count != 0)
	            {
	                foreach (DataRow myRow in user_code.Rows)
	                {
	                    DataTable tempdata = DBData("select User_CD_Descr from UHELP.USER_CD where User_Grp='SCHMJ' and User_cd='" + myRow[0].ToString() + "'");
	                    if (tempdata.Rows.Count != 0)
	                    {
	                        if (k < 2)
	                        {
	                            tempString = tempString + tempdata.Rows[0][0].ToString().Trim() + ", ";
	                            k++;
	                        }
	                        else
	                        {
	                            if (k == 2)
	                            {
	                                tempString = tempString + temp;
	                            }
	                            tempString = tempString + "<li>";
	                            tempString = tempString + tempdata.Rows[0][0].ToString().Trim() + "</li>> ";
	                            tempString = tempString.Remove(tempString.Length - 2);
	                            k++;
	                        }
	                    }
	                }
	                if ((k == 1) || (k == 2))
	                    tempString = tempString.Remove(tempString.Length - 2);
	                else
	                    if (k > 2)
	                        tempString = tempString + "</ul></div>";
	            }
	            else
	            {
	                tempString = "";
	            }
	            return tempString;
	        }

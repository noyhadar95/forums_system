using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.ForumManagement.Data_Access_Layer
{
    class DAL_Posts : DAL_Connection
    {
        /// <summary>
        /// Gets the current maximum id of posts
        /// </summary>
        /// <returns></returns>
        public int getMaxId()
        {
            Connect_to_DB();
            string sql = "Select MAX(PostID) AS MaxId From Posts";

            DataTable tb = connect_me.DownloadData(sql, "Posts");
            if (tb.Rows.Count == 0)
                return 0;
            if (tb.Rows[0][0] == null || tb.Rows[0][0].ToString() == "")
                return 0;
            return (int)tb.Rows[0][0];
        }

        /// <summary>
        /// Creates a post in thread
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="publisherUserName"></param>
        /// <param name="forumName"></param>
        /// <param name="parentPostId">0 or below if there is none</param>
        /// <param name="threadId"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        public void CreatePost(int postId, string publisherUserName, string forumName, int parentPostId, int threadId, string title, string content)
        {

            Connect_to_DB();
            string sql = "Insert into [Posts] values(@p1,@p2,@p3,@p4,@p5,@p6,@p7)";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", postId);
            cmd.Parameters.AddWithValue("@p2", publisherUserName);
            cmd.Parameters.AddWithValue("@p3", forumName);

            if (parentPostId <= 0)
                cmd.Parameters.AddWithValue("@p4", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@p4", parentPostId);

            cmd.Parameters.AddWithValue("@p5", threadId);
            cmd.Parameters.AddWithValue("@p6", title);
            cmd.Parameters.AddWithValue("@p7", content);


            connect_me.TakeAction(cmd);

        }


        public DataTable GetPost(int postId)
        {
            Connect_to_DB();
            string sql = "Select * From Posts WHERE PostID=@p1";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", postId);

            return connect_me.DownloadData2(cmd, "Posts");
        }


        public DataTable GetAllPostsFromThread(int threadId)
        {
            Connect_to_DB();
            string sql = "Select * From Posts WHERE ThreadID=@p1";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("@p1", threadId);

            return connect_me.DownloadData2(cmd, "Posts");
        }

        public void DeletePost(int postId)
        {
            Connect_to_DB();
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "Delete From [Posts] Where [PostID]=@p1";

            cmd.Parameters.AddWithValue("@p1", postId);

            connect_me.TakeAction(cmd);
            cmd = null;
        }
    }

}

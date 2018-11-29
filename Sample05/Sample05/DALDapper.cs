using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Sample05.Models;

namespace Sample05
{
    public class DALDapper
    {

        /// <summary>
        /// 测试插入单条数据
        /// </summary>
        public static void test_insert()
        {
            var content = new Content
            {
                title = "标题1",
                content = "内容1",

            };
            using (var conn = new SqlConnection("Data Source=.;User ID=sa;Password=123456;Initial Catalog=Sample05;Pooling=true;Max Pool Size=100;"))
            {
                string sql = @"INSERT INTO [Content]
                                                    (title, [content], status, add_time, modify_time)
                                    VALUES   (@title,@content,@status,@add_time,@modify_time)";
                var result = conn.Execute(sql, content);
                Console.WriteLine($"test_insert：插入了{result}条数据！");
            }
        }

        public static void test_mulit_insert_comment()
        {
            List<Comment> list = new List<Comment>() {
                new Comment(){ content_id = 6, content = "内容6的评论1" },
                new Comment(){ content_id = 6, content = "内容6的评论2" }
            };
            using (var conn = new SqlConnection("Data Source=.;User ID=sa;Password=123456;Initial Catalog=Sample05;Pooling=true;Max Pool Size=100;"))
            {
                string sql = "insert into comment(content_id, content, add_time) values(@content_id, @content, @add_time)";
                var result = conn.Execute(sql, list);

                Console.WriteLine($"test_mulit_insert_comment：插入了{result}条数据！");
            }
        }

        /// <summary>
        /// 测试一次批量插入两条数据
        /// </summary>
        public static void test_mult_insert()
        {
            List<Content> contents = new List<Content>() {
               new Content
                {
                    title = "批量插入标题1",
                    content = "批量插入内容1",

                },
                   new Content
                {
                    title = "批量插入标题2",
                    content = "批量插入内容2",

                }
            };

            using (var conn = new SqlConnection("Data Source=.;User ID=sa;Password=123456;Initial Catalog=Sample05;Pooling=true;Max Pool Size=100;"))
            {
                string sql = @"INSERT INTO [Content] (title, [content], status, add_time, modify_time) VALUES   (@title,@content,@status,@add_time,@modify_time)";
                var result = conn.Execute(sql, contents);
                Console.WriteLine($"test_mult_insert：插入了{result}条数据！");
            }
        }
        /// <summary>
        /// 测试删除单条数据
        /// </summary>
        public static void test_del()
        {
            var content = new Content
            {
                id = 2,

            };
            using (var conn = new SqlConnection("Data Source=.;User ID=sa;Password=123456;Initial Catalog=Sample05;Pooling=true;Max Pool Size=100;"))
            {
                string sql = @"DELETE FROM [Content] WHERE   (id = @id)";
                var result = conn.Execute(sql, content);
                Console.WriteLine($"test_del：删除了{result}条数据！");
            }
        }

        /// <summary>
        /// 测试一次批量删除两条数据
        /// </summary>
        public static void test_mult_del()
        {
            List<Content> contents = new List<Content>() {
               new Content
                {
                    id=1,

                },
                   new Content
                {
                    id=5,

                }
            };

            using (var conn = new SqlConnection("Data Source=.;User ID=sa;Password=123456;Initial Catalog=Sample05;Pooling=true;Max Pool Size=100;"))
            {
                string sql = @"DELETE FROM [Content] WHERE   (id = @id)";
                var result = conn.Execute(sql, contents);
                Console.WriteLine($"test_mult_del：删除了{result}条数据！");
            }
        }

        /// <summary>
        /// 测试修改单条数据
        /// </summary>
        public static void test_update()
        {
            var content = new Content
            {
                id = 6,
                title = "标题6",
                content = "内容6",

            };
            using (var conn = new SqlConnection("Data Source=.;User ID=sa;Password=123456;Initial Catalog=Sample05;Pooling=true;Max Pool Size=100;"))
            {
                string sql = @"UPDATE  [Content]
                                    SET         title = @title, [content] = @content, modify_time = GETDATE()
                                    WHERE   (id = @id)";
                var result = conn.Execute(sql, content);
                Console.WriteLine($"test_update：修改了{result}条数据！");
            }
        }

        /// <summary>
        /// 测试一次批量修改多条数据
        /// </summary>
        public static void test_mult_update()
        {
            List<Content> contents = new List<Content>() {
               new Content
            {
                id=7,
                title = "批量修改标题7",
                content = "批量修改内容7",

            },
               new Content
            {
                id =8,
                title = "批量修改标题8",
                content = "批量修改内容8",

            },
        };

            using (var conn = new SqlConnection("Data Source=.;User ID=sa;Password=123456;Initial Catalog=Sample05;Pooling=true;Max Pool Size=100;"))
            {
                string sql = @"UPDATE  [Content]
                                    SET         title = @title, [content] = @content, modify_time = GETDATE()
                                    WHERE   (id = @id)";
                var result = conn.Execute(sql, contents);
                Console.WriteLine($"test_mult_update：修改了{result}条数据！");
            }
        }

        /// <summary>
        /// 查询单条指定的数据
        /// </summary>
        public static void test_select_one()
        {
            using (var conn = new SqlConnection("Data Source=.;User ID=sa;Password=123456;Initial Catalog=Sample05;Pooling=true;Max Pool Size=100;"))
            {
                string sql = @"select * from [dbo].[content] where id=@id";
                var result = conn.QueryFirstOrDefault<Content>(sql, new { id = 6 });
                Console.WriteLine($"test_select_one：查到的数据为：{result}");
            }
        }

        /// <summary>
        /// 查询多条指定的数据
        /// </summary>
        public static void test_select_list()
        {
            using (var conn = new SqlConnection("Data Source=.;User ID=sa;Password=123456;Initial Catalog=Sample05;Pooling=true;Max Pool Size=100;"))
            {
                string sql = @"select * from [dbo].[content] where id in @ids";
                var result = conn.Query<Content>(sql, new { ids = new int[] { 7, 8 } });
                Console.WriteLine($"test_select_one：查到的数据为：{result}");
            }
        }

        public static void test_select_content_with_comment()
        {
            using (var conn = new SqlConnection("Data Source=.;User ID=sa;Password=123456;Initial Catalog=Sample05;Pooling=true;Max Pool Size=100;"))
            {
                string sql = @"select * from content where id=@id; 
                                    select * from comment where content_id=@id;";
                using (var result = conn.QueryMultiple(sql, new { id = 6 }))
                {
                    var content = result.ReadFirstOrDefault<ContentWithComment>();
                    content.comments = result.Read<Comment>().AsList();
                    Console.WriteLine($"test_select_content_with_comment:内容6的评论数量{content.comments.Count}");
                }

            }
        }

        public static void test_select_content_with_comment2()
        {
            using (var conn = new SqlConnection("Data Source=.;User ID=sa;Password=123456;Initial Catalog=Sample05;Pooling=true;Max Pool Size=100;"))
            {
                string sql = @"select a.*, b.* from content a left join comment  b on a.id = b.content_id ";
                var lookup = new Dictionary<int, ContentWithComment>();
                var result = conn.Query<ContentWithComment, Comment, ContentWithComment>(sql,
                                   (content, comment) =>
                                   {
                                       ContentWithComment tmp;
                                       if (!lookup.TryGetValue(content.id, out tmp))
                                       {
                                           tmp = content;
                                           lookup.Add(content.id, tmp);
                                       }
                                       tmp.comments.Add(comment);
                                       //content.comments.Add(comment);
                                       return content;
                                   }, "content_id");
                var list = lookup.Values.AsList();
                Console.WriteLine($"test_select_content_with_comment:查到的数据为{list}");
            }
        }

        public static void test_select_comment_with_content()
        {
            using (var conn = new SqlConnection("Data Source=.;User ID=sa;Password=123456;Initial Catalog=Sample05;Pooling=true;Max Pool Size=100;"))
            {
                string sql = @"select a.*, b.* from comment a inner join content b on a.content_id = b.id  ";
                //var result = conn.QueryFirstOrDefault(sql, new { id = 1 });

                var result = conn.Query<CommentWithContent, Content, CommentWithContent>(sql,
                                    (comment, content1) =>
                                    {
                                        comment.contentObj = content1;
                                        return comment;
                                    }, "id").AsList<CommentWithContent>();

                Console.WriteLine($"test_select_content_with_comment:查到的数据为{result}");

            }
        }
    }
}

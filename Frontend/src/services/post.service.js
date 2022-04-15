  import axios from "axios";

  const apiUrl = process.env.REACT_APP_API_URL;
  const token = JSON.parse(localStorage.getItem('user'))?.access_token;
  const instance = axios.create({
    baseURL: apiUrl,
    timeout: 1000,
    headers: { Authorization: "Bearer " + token },
  });

class PostService {
  addPost(post) {
    return instance
      .post('/Tweet',{
        body: post.body,
      })
      .then((response) => {
        return response.data;
      });
  }
  getPostById(id) {
    return instance
      .get("/Tweet/"+id)
      .then((response) => {
        return response.data;
      });
  }
  getPostByUserId(name) {
    return instance.get("/Tweet?userId=" + name).then((response) => {
      return response.data;
    });
  }

  getPostsFromFollowing() {
    return instance
      .get("/Feed")
      .then((response) => {
        return response.data;
      });
  }

  reportPost(postid) {
    return instance
      .post("/Tweet/report", {
        postId: postid,
      })
      .then((response) => {
        return response.data;
      });
  }

  commentOnPost({postid, body}){
    return instance.post("/Tweet/comment",
    {
      postId: postid,
      body: body
    })
    .then((response) => {return response.data})
  }
}

export default new PostService();
  import axios from "axios";

  const apiUrl = process.env.REACT_APP_API_URL;
  const token = JSON.parse(localStorage.getItem('user'))?.accessToken;
  const instance = axios.create({
    baseURL: apiUrl,
    timeout: 1000,
    headers: { Authorization: "Bearer " + token },
  });

class PostService {
  addPost(post) {
    return instance
      .post("posts", {
        body: post.body,
        imgData: post.imgData,
        postedBy: post.postedBy,
      })
      .then((response) => {
        return response.data;
      });
  }
  getPostById(id) {
    return instance
      .get("posts/"+id)
      .then((response) => {
        return response.data;
      });
  }
  getPostByUserId(id) {
    return instance
      .get("posts", {
        userid: id,
      })
      .then((response) => {
        return response.data;
      });
  }
  getPostByUsername(name) {
    return instance.get("posts?username=" + name).then((response) => {
      return response.data;
    });
  }

  getPostsFromFollowing() {
    return instance
      .get("posts/following")
      .then((response) => {
        return response.data;
      });
  }

  reportPost(postid) {
    return instance
      .post("report", {
        postId: postid,
      })
      .then((response) => {
        return response.data;
      });
  }

  commentOnPost({postid, body}){
    return instance.post("comment",
    {
      postId: postid,
      body: body
    })
    .then((response) => {return response.data})
  }
}

export default new PostService();

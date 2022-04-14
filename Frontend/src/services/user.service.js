import axios from "axios";

const apiUrl = process.env.REACT_APP_API_URL;
const token = JSON.parse(localStorage.getItem('user'))?.accessToken;
const instance = axios.create({
    baseURL: apiUrl,
    timeout: 5000,
    headers: {'Authorization': 'Bearer ' + token}
  });

class UserService {
    getUserByName(username) {
        if (username){
            return instance
            .get("user?name="+username)
            .then(response => {  
              return response.data;
            })
            .catch(error => {
                console.log(error)
            });
        }
        
      }

    followUserByName(username){
        return instance.post("user/follow", {
            usernameToFollow: username
        })
        .then(response => {
            return response.data;
        })
    }
    unFollowUserByName(username){
        return instance.post("user/unfollow", {
            usernameToFollow: username
        })
        .then(response => {
            return response.data;
        })
    }

    isFollowing(username){
        return instance.get("user/isfollowing?username="+username)
        .then(response => {
            return response.data;
        })
    }

    searchUsers(query){
        return instance.get("user/search?query="+query)
        .then(response => {
            return response.data;
        })
    }
    updateUserDetails(userDetails){
        return instance.put("user", {
            displayName: userDetails.username,
            profilePictureData: userDetails.profilePicture,
            email: userDetails.email,
            password: userDetails.password,
            confirmPassword: userDetails.passwordConfirm,
            bio: userDetails.bio
        })
        .then((res) => {return res.data})
    }

}

export default new UserService();
import axios from "axios";
const API_URL = process.env.REACT_APP_API_URL+"/authenticate";
const instance = axios.create({
    baseURL: API_URL,
    timeout: 5000,
  });
class AuthService {

  login(username, password) {
    return instance
      .post(API_URL, {
        username,
        password
      })
      .then(response => {
        if (response.data.accessToken) {
          localStorage.setItem("user", JSON.stringify(response.data));
        }

        return response.data;
      });
  }

  logout() {
    localStorage.removeItem("user");
    
    window.location.reload();

  }

  register(username, email, password) {
    return instance.post(API_URL + "/signup", {
      username,
      email,
      password
    });    
  }
  async isLoggedin(){
    var token = JSON.parse(localStorage.getItem('user'))
    if (token) {
      return fetch(API_URL + "/getuser", {
        headers:{
         'Authorization': 'Bearer ' + token.accessToken
        }
      }).then(response => {
        
        if (response) {
          return true;
        }
        return false
      })
       }
  }
  async getCurrentUser() {
    var token = JSON.parse(localStorage.getItem('user'))
    if (token) {
      return fetch(API_URL + "/getuser", {
        headers:{
         'Authorization': 'Bearer ' + token.accessToken
        }
      }).then(response => {
        if (response) {
          const json = response.json();
          return json
        }
      })
      .catch(error => {
        this.logout();
      })
      
       }
    }
     
}

export default new AuthService();
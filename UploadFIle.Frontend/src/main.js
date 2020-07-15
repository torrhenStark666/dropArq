import Vue from 'vue'
import App from './App.vue'
import VueResource from 'vue-resource'
import VueMaterial from 'vue-material'
import 'vue-material/dist/vue-material.min.css'
import 'vue-material/dist/theme/default.css'
import Login from './components/Login'
import NovoArquivo from './components/NovoArquivo'
import NovoLogin from './components/NovoLogin'
import Arquivo from './components/Arquivo'
import axios from 'axios'
import VueRouter from 'vue-router'

Vue.use(axios)
Vue.use(VueRouter)
Vue.use(VueMaterial)
Vue.use(VueResource)
Vue.config.productionTip = false

axios.interceptors.request.use(
  (config) => {
    let token = localStorage.token;

    if (token) {
      config.headers['Authorization'] = `${ token }`;
    }

    return config;
  }, 

  (error) => {
    return Promise.reject(error);
  }
);

Vue.prototype.$axios = axios
Vue.prototype.$auth = '';
Vue.prototype.$nameUser = '';
Vue.prototype.$idUser = '';

const routes = [
  { path: '/', component: Login },
  { path: '/Dashboard', 
    component: Arquivo, 
    beforeEnter: (to, from, next) => {
      console.log(Vue);
      if ( Vue.$auth === '' ){
        
        next ( '/' )
      }else{
        next()
      }
         
    } 
  },
  { path: '/NewAccount', component: NovoLogin},
  { path: '/NewFile', 
    component: NovoArquivo,
    beforeEnter: (to, from, next) => {
      if ( Vue.$auth === '' ){
        next ( '/' )
      }else{
        next()
      }
         
    }
  }
]

const router = new VueRouter({
  routes
})

new Vue({
  render: h => h(App),
  router
}).$mount('#app')

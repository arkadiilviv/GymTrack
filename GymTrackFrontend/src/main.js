import Vue from 'vue'
import App from './App.vue'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import Vuex from 'vuex'
import CustomToastPlugin from './plugins/toast.plugin'
// Cookie plugin
var VueCookie = require('vue-cookie');

Vue.use(VueCookie);
// Install BootstrapVue
Vue.use(BootstrapVue)
// Optionally install the BootstrapVue icon components plugin
Vue.use(IconsPlugin)
Vue.use(Vuex)
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
Vue.config.productionTip = false

Vue.use(CustomToastPlugin)


new Vue({
  render: h => h(App),
  store
}).$mount('#app')

const store = new Vuex.Store({
  state: {
    token : " ",
    username: " "
  }
})
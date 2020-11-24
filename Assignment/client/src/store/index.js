import Vue from 'vue';
import Vuex from 'vuex';
import dishes from './modules/dishes';
import categories from './modules/categories';
import Buefy from 'buefy';
import 'buefy/dist/buefy.css';
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';

// Install BootstrapVue
Vue.use(BootstrapVue)
// Optionally install the BootstrapVue icon components plugin
Vue.use(IconsPlugin)

//Load Vuex
Vue.use(Vuex);

Vue.use(Buefy)

//Create store
export default new Vuex.Store({
    modules:{
        dishes,
        categories
    }
});
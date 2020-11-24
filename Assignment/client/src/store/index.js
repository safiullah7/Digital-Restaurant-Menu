import Vue from 'vue';
import Vuex from 'vuex';
import dishes from './modules/dishes';
import Buefy from 'buefy'
import 'buefy/dist/buefy.css'

//Load Vuex
Vue.use(Vuex);

Vue.use(Buefy)

//Create store
export default new Vuex.Store({
    modules:{
        dishes
    }
});
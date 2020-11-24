import axios from 'axios';

const state = {
    dishes: []
};
const getters = {
    allDishes: (state) => state.dishes
};
const actions = {
    async fetchDishes({commit}) {
        const response = await axios.get('http://localhost:5000/api/dishes');
        if (response.data.statusCode === 200) {
            console.log('here');
            commit('setDishes', response.data.contents);
        }
    }
};
const mutations = {
    setDishes: (state, dishes) => state.dishes = dishes
};

export default {
    state,
    getters,
    actions,
    mutations
};
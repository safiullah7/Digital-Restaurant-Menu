import axios from 'axios';

const state = {
    dishes: [],
    listMode: true
};
const getters = {
    allDishes: (state) => state.dishes,
    listMode: (state) => state.listMode
};
const actions = {
    async fetchDishes({commit}) {
        const response = await axios.get('http://localhost:5000/api/dishes');
        if (response.data.statusCode === 200) {
            commit('setDishes', response.data.contents);
        }
    },
    setListMode({commit}, listMode) {
        commit('setListMode', listMode);
    },
    async addDish({commit}, dish) {
        await axios.post('http://localhost:5000/api/dishes', dish);
        commit('addNewDish', dish);
    }
};
const mutations = {
    setDishes: (state, dishes) => state.dishes = dishes,
    setListMode: (state, listMode) => state.listMode = listMode,
    addNewDish: (state, dish) => state.dishes.unshift(dish)
};

export default {
    state,
    getters,
    actions,
    mutations
};
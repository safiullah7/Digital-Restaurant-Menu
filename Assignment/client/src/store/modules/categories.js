import axios from 'axios';

const state = {
    categories: []
};

const getters = {
    allCategories: (state) => state.categories
};

const actions = {
    async fetchCategories({commit}) {
        const response = await axios.get('http://localhost:5000/api/categories');
        if (response.data.statusCode === 200) {
            commit('setCategories', response.data.contents);
        }
    }
};

const mutations = {
    setCategories: (state, categories) => state.categories = categories,
};

export default {
    state,
    getters,
    actions,
    mutations
}
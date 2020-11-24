<template>
  <div class="container">
    <div>
      <b-button @click="setListMode(!listMode)" block variant="success">
        {{listMode ? "Add Dish" : "Back"}}
      </b-button>
    </div>
    <div v-if="listMode">
      <b-card-group deck>
        <div v-for="dish in allDishes" :key="dish.id">
            <Dish :dish="dish" />
        </div>
      </b-card-group>
    </div>
    <div v-if="!listMode">
      <AddDish />
    </div>
  </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex';
import Dish from './Dish';
import Vue from 'vue';
import AddDish from './AddDish';

Vue.component('Dish', Dish);
export default {
    name: "Dishes",
     data: function () {
        return {
          // listMode: true,
          dish: {
            id: "",
            name: "",
            price: 0,
            category: "",
            availability: "",
            active: false,
            preparationTime: 0,
            createdAt: ""
          },
          myStyle: {
              backgroundColor:"#16a085"
          }
        }
    },
    methods: {
      ...mapActions(['fetchDishes', 'setListMode'])
    },
    computed: mapGetters(['allDishes', 'listMode']),
    created() {
      this.fetchDishes();
    },
    components: {
      Dish,
      AddDish
    }
}
</script>

<style>

</style>